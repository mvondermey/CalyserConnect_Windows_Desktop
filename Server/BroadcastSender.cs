using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calyserconnect
{
    class BroadcastSender
    {
        public BroadcastSender()
        {
            var Client = new System.Net.Sockets.UdpClient();
            var RequestData = Encoding.ASCII.GetBytes("SomeRequestData");
            var ServerEp = new System.Net.IPEndPoint(System.Net.IPAddress.Any, 0);

            Client.EnableBroadcast = true;
            Client.Send(RequestData, RequestData.Length, new System.Net.IPEndPoint(System.Net.IPAddress.Broadcast, 8888));

            var ServerResponseData = Client.Receive(ref ServerEp);
            var ServerResponse = Encoding.ASCII.GetString(ServerResponseData);
            Console.WriteLine("Received {0} from {1}", ServerResponse, ServerEp.Address.ToString());

            Client.Close();
        }
    }
}
