using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmixPOST.Components
{
    static class FileSaver
    {
        public static void SaveTextToFile(string fileName,string content)
        {
            using (StreamWriter file = new StreamWriter(File.Open(fileName, FileMode.Create), Encoding.UTF8))
            {
                file.Write(content);
            }
        }

        public static void OpenInBrowser(string fileName)
        {
            System.Diagnostics.Process.Start(fileName);
        }
    }
}
