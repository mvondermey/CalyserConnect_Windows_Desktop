using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calyserconnect
{
    class BroadcastSender
    {
        static System.Net.Sockets.UdpClient Client;

        public BroadcastSender(System.Net.Sockets.UdpClient Client)
        {
            MessageJSON JSONToSend = new MessageJSON { Message = "Beep", Command = "" };
            //
            String SendInfo = JSONToSend.GetJSON();
            //
            var RequestData = Encoding.ASCII.GetBytes(SendInfo);
            var ServerEp = new System.Net.IPEndPoint(System.Net.IPAddress.Any, 0);

            //Client = new System.Net.Sockets.UdpClient(8003);

            Client.EnableBroadcast = true;
            while (true)
            {
                Client.Send(RequestData, RequestData.Length, new System.Net.IPEndPoint(System.Net.IPAddress.Broadcast, 8003));
                Console.WriteLine("broadcastSender.Sent to "+System.Net.IPAddress.Broadcast);
                /*
                Client.BeginReceive(new AsyncCallback(AcceptCallback), null);
                //
                var ServerResponseData = Client.Receive(ref ServerEp);
                //Console.WriteLine("broadcastSender.Received");
                var ServerResponse = Encoding.ASCII.GetString(ServerResponseData);
                Console.WriteLine("Received {0} from {1}", ServerResponse, ServerEp.Address.ToString());
                //
                Client.Send(RequestData, RequestData.Length, ServerEp);
                */
                System.Threading.Thread.Sleep(3000);
                //
            }
            //
            Client.Close();
        }

        private void AcceptCallback(IAsyncResult res)
        {
            System.Net.IPEndPoint remote = new System.Net.IPEndPoint(System.Net.IPAddress.Any, 0);
            byte[] data = Client.EndReceive(res, ref remote);

            // do something with data received from remote
            Console.WriteLine("Remote Address: "+remote.Address.ToString() + ": " + Encoding.ASCII.GetString(data));

            // get next packet
            Client.BeginReceive(AcceptCallback, null);

        }
    }
//
}
