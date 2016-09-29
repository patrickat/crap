using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using CRAP.Helper;
using CRAP.Models;

namespace CRAP.Models
{
    public class Client
    {
        private UdpClient client;
        private Dictionary<string, Tag> tagMap;
        private readonly object tagLock = new object();

        public Dictionary<string, Tag> TagMap
        {
            get
            {
                lock (tagLock)
                {
                    return tagMap;
                }
            }
        }

        public Client() : this(11000)
        {
        }

        public Client(int port)
        {
            this.client = new UdpClient(port);
            this.tagMap = new Dictionary<string, Tag>();

            try
            {
                this.client.BeginReceive(new AsyncCallback(ReceiveBroadcast), null);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void ReceiveBroadcast(IAsyncResult result)
        {
            var endPoint = new IPEndPoint(IPAddress.Any, 8000);
            var received = this.client.EndReceive(result, ref endPoint);

            var notification = (Notification)XmlHelper.DeserializeToObject(new MemoryStream(received), typeof(Notification));

            lock(tagLock)
            {
                foreach (var tag in notification.TagList.Tags)
                {
                    if (!this.TagMap.ContainsKey(tag.ID))
                    {
                        this.TagMap[tag.ID] = tag;
                    }
                }
            }

            Console.WriteLine(notification.IpAddress);
            this.client.BeginReceive(new AsyncCallback(this.ReceiveBroadcast), null);
        }
    }
}
