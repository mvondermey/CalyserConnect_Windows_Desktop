using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;

namespace calyserconnect
{
    class SocketTask
    {
        // Thread signal.
        public static ManualResetEvent readDone = new ManualResetEvent(false);
        public static ManualResetEvent writeDone = new ManualResetEvent(false);
        //
        public SocketTask(Socket handler)
        {

            Debug.WriteLine("Inside SocketTask");

            // Create the state object.
            StateObject state = new StateObject();
            state.workSocket = handler;
            //
            Debug.WriteLine("Send");
            //
            MessageJSON JSONToSend = new MessageJSON { Message = "Beep", UUID = "XLM", Command = "" };
            //
            String SendInfo = JSONToSend.GetJSON();
            //
            Debug.WriteLine(SendInfo);
            Send(handler, SendInfo+"\n");
            //
            Debug.WriteLine("Before BeginReceive");
            //
            int count = 0;
            //
            while (true)
            {
                count++;
                //                   
                //try
                //
                {
                    Read(handler,state);
                    //Send(handler, SendInfo);

                }
                //
                //catch (Exception e)
                //{
                //  Debug.WriteLine("Delete");
                //Debug.WriteLine(e.ToString());
                //return;
                //}
            }
            //
            Debug.WriteLine("After BeginReceive");
            //
        }
        //
        public static void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            Debug.WriteLine("Inside ReadCallback");
            //
            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;
            //
            // Read data from the client socket. 
            int bytesRead = 0;
            try
            {
                bytesRead = handler.EndReceive(ar);

            }catch (Exception e)
            {
                Debug.WriteLine("Calyser.SocketTask.Exception1");
                Debug.WriteLine(e.ToString());
                readDone.Set();
                return;
            }
            //
            Debug.WriteLine("After Endreceive\n");
            //
            if (bytesRead > 0)
            {
                // There might be more data, so store the data received so far.
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read 
                // more data.
                content = state.sb.ToString();
                //
                if (content.IndexOf("<EOF>") > -1)
                {
                    // All the data has been read from the 
                    // client. Display it on the console.
                    //                   
                    state.sb.Clear();
                    Debug.WriteLine("***********content \n" + content+ "\n ************* Done content");
                    //
                } else {
                    // Not all data received. Get more.
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                    //
                  
                    //
                }
            }
            //          
            readDone.Set();
            //
        }
        //
        private static void Read(Socket handler,StateObject state)
        {
            readDone.Reset();
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
            new AsyncCallback(ReadCallback), state);
            readDone.WaitOne();
        }
        //
        private static void Send(Socket handler, String data)
        {
            // Add begin and end of file
            //
            data = "<BOF>" + data + "<EOF>";
            //
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            //
            Debug.WriteLine("Sending data "+data);

            writeDone.Reset();
            // Begin sending the data to the remote device.
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
            writeDone.WaitOne();
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
            writeDone.Set();
        }
        //

    }
}
