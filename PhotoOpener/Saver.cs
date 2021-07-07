﻿using System.IO;

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
        public void DeleteImage()
        {
            using (var streamWriter = new StreamWriter(_filePath))
            {
                streamWriter.WriteLine(string.Empty);
                streamWriter.Close();
            }
        }
        public string ReadImagePath()
        {
            if (!File.Exists(_filePath))
                return string.Empty;
            using (var streamReader = new StreamReader(_filePath))
            {                
                return streamReader.ReadLine();
            }
        }
    }
}
