using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRAP.Models;

namespace CRAP.Helper
{
    public class FileHelper
    {
        public static void AddTag(string path, Tag tag)
        {
            using (var stream = new FileStream(path, FileMode.Append))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(tag.ToString());
                }
            }
        }
    }
}
