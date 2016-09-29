using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CRAP.Models
{
    [XmlRoot("Alien-RFID-Reader-Auto-Notification")]
    public class Notification
    {
        [XmlElement("IPAddress")]
        public string IpAddress { get; set; }

        [XmlElement("CommandPort")]
        public int Port { get; set; }

        [XmlElement("Alien-RFID-Tag-List")]
        public TagList TagList { get; set; }
    }

    [XmlRoot("Alien-RFID-Tag-List")]
    public class TagList
    {
        [XmlElement("Alien-RFID-Tag")]
        public List<Tag> Tags { get; set; }
    }

    [XmlRoot("Alien-RFID-Tag")]
    public class Tag
    {
        [XmlElement("TagID")]
        public string ID { get; set; }

        public string DiscoveryTime { get; set; }

        public DateTime DateDiscovered => DateTime.Parse(this.DiscoveryTime);

        public DateTime DateDelivered { get; set; }

        public override string ToString()
        {
            return $"{this.ID},{this.DateDiscovered},{this.DateDelivered}";
        }
    }
}
