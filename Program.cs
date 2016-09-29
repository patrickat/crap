using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serialListener = new SerialListener("COM4");

            serialListener.BeginListening();
            Console.ReadKey();
            serialListener.StopListening();
        }
    }
}
