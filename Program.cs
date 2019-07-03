using System;
using System.Windows.Forms;

namespace GotifyWinClient
{
    static class Program
    {

        private static Controller controller = new Controller();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var form = new MainForm()
            {
                Connect = async (string gotifyURL) =>
                {
                    await controller.ReloadConnection(gotifyURL);
                },
                Cleanup = () =>
                {
                    controller.Dispose();
                }
            };
            controller.MsgCB = form.ShowMessageNotification;
            controller.CloseCB = (string closeReason) =>
            {
                form.OnConnectedStateChange(ConnectState.Disconnected);
                form.ShowToastMessage(5000, "Connection to Gotify server closed", closeReason, ToolTipIcon.Warning);
            };
            controller.ConnectedCB = () =>
            {
                form.OnConnectedStateChange(ConnectState.Connected);
                form.ShowToastMessage(3000, "Gotify", "Connected", ToolTipIcon.Info);
                form.SuspendToBackground(form, new EventArgs());
            };
            Application.Run(form);
        }
    }
}
