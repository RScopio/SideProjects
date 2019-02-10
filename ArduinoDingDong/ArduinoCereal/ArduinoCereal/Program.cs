using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Diagnostics;

namespace ArduinoCereal
{
    class Program
    {
        static SerialPort port;
        static bool waitForOpen = true;
        static string data = "";
        static bool sendDong = true;
        static bool exit = false;

        private static void port_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                while (port.BytesToRead > 0)
                {
                    char c = (char)port.ReadChar();
                    if (waitForOpen)
                    {
                        data = "";
                        if (c == '{')
                        {
                            waitForOpen = false;
                        }
                    }
                    else
                    {
                        if (c == '}')
                        {
                            waitForOpen = true;
                            if (data == "ding")
                            {
                                Console.WriteLine("ding");
                                sendDong = true;
                                data = "";
                            }
                            if(data == "bye")
                            {
                                exit = true;
                                data = "";
                            }
                            return;
                        }
                        data += c;
                    }
                }
            }
            catch { }
        }

        static void Main(string[] args)
        {
            //if you receive ding, send dong
            Stopwatch st = new Stopwatch();
            port = new SerialPort("COM8", 115200);
            port.Open();
            port.DataReceived += port_DataReceived;
            st.Start();
            while (!exit)
            {
                //send data (delayed)
                if (sendDong && st.ElapsedMilliseconds > 50)
                {
                    Console.WriteLine(" dong");
                    sendDong = false;
                    st.Restart();
                    port.Write("{dong}");
                    data = "";
                }

                //read data (fast as possible / use the loop?)


            }
            port.Close();
        }
    }
}
