using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace calyserconnect
{
    class SocketTask
    {
        public SocketTask(IAsyncResult ar)
        {

            Debug.WriteLine("Inside SocketTask");

            // Get the socket that handles the client request.
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.
            StateObject state = new StateObject();
            state.workSocket = handler;

            Debug.WriteLine("Send");

            //for (int i = 10; i < 11; i++)
            //{
            int ii = 11;
                Debug.WriteLine("I-am-CSync-Windows " + ii + "\n");
                Send(handler, "I-am-CSync-Windows " + ii + "\n");
            //}

            Debug.WriteLine("Before BeginReceive");

            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);

            Debug.WriteLine("Send");

            //for (int i = 0; i < 1; i++)
            //{
            ii = 100;
                Debug.WriteLine("I-am-CSync-Windows " + ii + "\n");
                Send(handler, "I-am-CSync-Windows " + ii + "\n");
            //}
        //
        }
        //
        public static void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            Debug.WriteLine("Inside ReadCallback");

            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket. 
            int bytesRead = handler.EndReceive(ar);

            Debug.WriteLine("After Endreceive");

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read 
                // more data.
                content = state.sb.ToString();

                Debug.WriteLine("content "+content);

            }
        }
        //
        private static void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }
        //
        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Debug.WriteLine("SendCallback");
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                Debug.WriteLine("Sent {0} bytes to client.", bytesSent);
                

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
        //

    }
}
