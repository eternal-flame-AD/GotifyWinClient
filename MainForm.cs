using System;
using System.Windows.Forms;

namespace GotifyWinClient
{

    enum ConnectState
    {
        Connected,
        Connecting,
        Disconnected,
    }
    public partial class MainForm : Form
    {
        public delegate void ConnectMethod(string gotifyURL);
        public delegate void CleanupMethod();

        public ConnectMethod Connect;
        public CleanupMethod Cleanup;
        public MainForm()
        {
            InitializeComponent();

            MenuItem exitMenuItem = new MenuItem { Text = "Exit" };
            exitMenuItem.Click += (object o, EventArgs e) =>
            {
                Application.Exit();
            };
            ContextMenu contextMenu = new ContextMenu();
            contextMenu.MenuItems.Add(exitMenuItem);
            NotificationIcon.ContextMenu = contextMenu;
            NotificationIcon.Click += this.WakeFromBackground;
            NotificationIcon.Icon = GotifyWinClient.Properties.Resources.gotify_logo_bw;

            UrlInput.Text = (string)GotifyWinClient.Properties.Settings.Default["GotifyUrl"];

            ConnectBtn.Click += (object o, EventArgs e) =>
            {
                string url = this.UrlInput.Text;
                this.OnConnectedStateChange(ConnectState.Connecting);
                this.Connect(url);
            };
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.SuspendToBackground(this, e);
            }
            else
            {
                e.Cancel = false;
                Cleanup();
            }
            base.OnFormClosing(e);
        }

        internal void SuspendToBackground(object o, EventArgs e)
        {
            this.Visible = false;
        }

        internal void WakeFromBackground(object o, EventArgs e)
        {
            this.Visible = true;
        }

        internal void OnConnectedStateChange(ConnectState newState)
        {
            switch (newState)
            {
                case ConnectState.Connected:
                    this.NotificationIcon.Icon = GotifyWinClient.Properties.Resources.gotify_logo;
                    GotifyWinClient.Properties.Settings.Default["GotifyUrl"] = UrlInput.Text;
                    GotifyWinClient.Properties.Settings.Default.Save();
                    break;
                case ConnectState.Disconnected:
                case ConnectState.Connecting:
                    this.NotificationIcon.Icon = GotifyWinClient.Properties.Resources.gotify_logo_bw;
                    break;
            }

        }

        internal void ShowToastMessage(int timeout, string title, string content, ToolTipIcon icon)
        {
            this.NotificationIcon.ShowBalloonTip(timeout, title, content, icon);
        }

        internal void ShowMessageNotification(Message msg)
        {
            ShowToastMessage(5000, msg.title == "" ? "Gotify Notification" : msg.title, msg.message, ToolTipIcon.Info);
        }


    }
}
