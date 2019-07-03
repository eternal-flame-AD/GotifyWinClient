using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace GotifyWinClient
{
    internal delegate void MessageCallback(Message msg);
    internal delegate void CloseCallback(string reason);
    internal delegate void ConnectedCallback();
    class Controller : IDisposable
    {
        private ClientWebSocket webSocket;
        private WebSocketListener listener;

        public ConnectedCallback ConnectedCB;
        public MessageCallback MsgCB;
        public CloseCallback CloseCB;

        private Mutex reloadMutex = new Mutex();
        public async Task<bool> ReloadConnection(string wsURL)
        {
            using (reloadMutex)
            {

                this.cleanupConnection();

                webSocket = new ClientWebSocket();
                try
                {
                    await webSocket.ConnectAsync(new Uri(wsURL), CancellationToken.None);
                }
                catch (Exception e)
                {
                    this.CloseCB(e.ToString());
                    return false;
                }

                listener = new WebSocketListener(
                    webSocket,
                    MsgCB,
                    CloseCB
                );

                ConnectedCB();
                return true;
            }
        }

        void cleanupConnection()
        {
            if (webSocket != null)
            {
                webSocket.Dispose();
                webSocket = null;
            }
            if (listener != null)
            {
                listener.Dispose();
                listener = null;
            }
        }

        public void Dispose()
        {
            this.cleanupConnection();
        }
    }

}
