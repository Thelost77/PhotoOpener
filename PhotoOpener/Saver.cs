using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoOpener
{
    class Saver
    {
        private string _filePath;

        public Saver()
        {
            _filePath = Program.FilePath;
        }
        public void SaveImage()
        {
            if (Program.PhotoPath != null)
            {
                using (var streamWriter = new StreamWriter(_filePath))
                {
                    streamWriter.WriteLine(Program.PhotoPath);
                    streamWriter.Close();
                }
            }
            else
                return;

        }

        public string ReadImagePath()
        {
            using (var streamReader = new StreamReader(_filePath))
            {
                return streamReader.ReadLine();
            }
        }
    }
}
