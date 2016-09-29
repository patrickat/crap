using System;
using System.Collections.Concurrent;
using System.IO;
using System.IO.Ports;
using CRAP.Helper;
using CRAP.Models;

namespace CRAP
{
    public class SerialListener
    {
        private readonly SerialPort serialPort;
        private readonly object serialLock;
        private readonly ConcurrentDictionary<string, Tag> tagMap;
        private readonly string path;

        public SerialListener(string portName)
        {
            this.serialLock = new object();
            this.tagMap = new ConcurrentDictionary<string, Tag>();
            this.path = @"C:\Users\patri\Desktop\RFID\tags.txt";

            this.serialPort = new SerialPort(portName)
            {
                BaudRate = 115200,
                Parity = Parity.None,
                StopBits = StopBits.One,
                DataBits = 8,
                Handshake = Handshake.None
            };

            this.serialPort.DataReceived += this.DataReceivedHandler;
        }

        public void BeginListening()
        {
            this.serialPort.Open();

            Console.WriteLine("Begin listening...");
            
        }

        public void StopListening()
        {
            Console.WriteLine("Stop listening...");
            this.serialPort.Close();
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            var port = (SerialPort) sender;

            lock (this.serialLock)
            {
                var bytes = new byte[port.BytesToRead];
                port.Read(bytes, 0, bytes.Length);

                var stream = new MemoryStream(bytes);

                var notification = (Notification) XmlHelper.DeserializeToObject(stream, typeof(Notification));

                if (notification != null && notification.TagList.Tags.Count > 0)
                {
                    foreach (var t in notification.TagList.Tags)
                    {
                        if (!this.tagMap.ContainsKey(t.ID))
                        {
                            this.tagMap.TryAdd(t.ID, t);
                            Console.WriteLine(t.ID);
                            FileHelper.AddTag(this.path, t);
                        }
                    }
                }
            }
        }
    }
}
