using System;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.Serialization.Json;
using System.Threading;

namespace GotifyWinClient
{
    class WebSocketListener : IDisposable
    {
        private Thread listenThread;
        private ClientWebSocket _ws;
        private MessageCallback _msgCb;
        private CloseCallback _closeCb;

        internal WebSocketListener(ClientWebSocket wsConn, MessageCallback msgCallback, CloseCallback closeCallback)
        {
            this._ws = wsConn;
            this._msgCb = msgCallback;
            this._closeCb = closeCallback;
            this.listenThread = new Thread(new ThreadStart(this.StartListeningForMessages));
            this.listenThread.Start();
        }

        void StartListeningForMessages()
        {
            var buf = WebSocket.CreateClientBuffer(65535, 4096);
            var msgDeserializer = new DataContractJsonSerializer(typeof(Message));
            while (this._ws.State == WebSocketState.Open)
            {
                try
                {
                    var nextMsgRecvResult = this._ws.ReceiveAsync(buf, CancellationToken.None).GetAwaiter().GetResult();

                    switch (nextMsgRecvResult.MessageType)
                    {
                        case WebSocketMessageType.Close:
                            _closeCb("Received WebSocket close message.");
                            break;
                        case WebSocketMessageType.Text:
                            var resultData = buf.Take(nextMsgRecvResult.Count);
                            var retMessage = (Message)msgDeserializer.ReadObject(new MemoryStream(resultData.ToArray()));
                            _msgCb(retMessage);
                            break;
                    }
                }
                catch (ThreadAbortException e) { }
                catch (Exception e)
                {
                    _closeCb(e.ToString());
                    break;
                }

            }
        }

        public void Dispose()
        {
            this.listenThread.Abort();
            try
            {
                if (_ws != null)
                {
                    _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None).GetAwaiter().GetResult();
                };
                _ws.Dispose();

            } catch (Exception e) { }
        }
    }
}
