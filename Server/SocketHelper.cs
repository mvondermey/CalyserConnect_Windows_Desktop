using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace calyserconnect
{
    class SocketHelper
    {

            TcpClient mscClient;
            string mstrMessage;
            string mstrResponse;
            byte[] bytesSent;
            public void processMsg(TcpClient client, NetworkStream stream, byte[] bytesReceived)
            {
                // Handle the message received and  
                // send a response back to the client.
                mstrMessage = Encoding.ASCII.GetString(bytesReceived, 0, bytesReceived.Length);
                mscClient = client;
                Debug.WriteLine("Message received: " + mstrMessage);
                mstrMessage = mstrMessage.Substring(0, 5);
                //
                if (mstrMessage.Equals("Hello"))
                {
                    mstrResponse = "Goodbye";
                }
                else
                {
                    mstrResponse = "What?";
                }
                bytesSent = Encoding.ASCII.GetBytes(mstrResponse);
                stream.Write(bytesSent, 0, bytesSent.Length);
            }
        }
    }

