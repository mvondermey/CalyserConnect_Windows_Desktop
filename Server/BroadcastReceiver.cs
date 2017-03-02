using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calyserconnect
{
    class BroadcastReceiver
    {
        //
        public BroadcastReceiver() { 
        //
        var Server = new System.Net.Sockets.UdpClient(8003);
        var ResponseData = Encoding.ASCII.GetBytes("SomeResponseData");
            //
            Console.WriteLine("Wait for connection on port "+8003);
            //
            while (true)
            {
                //
                var ClientEp = new System.Net.IPEndPoint(System.Net.IPAddress.Any, 0);
                var ClientRequestData = Server.Receive(ref ClientEp);
                var ClientRequest = Encoding.ASCII.GetString(ClientRequestData);
                //
                Console.WriteLine("Received {0} from {1}, sending response", ClientRequest, ClientEp.Address.ToString());
                Server.Send(ResponseData, ResponseData.Length, ClientEp);
                //
            }
      }
    //
    }
}
