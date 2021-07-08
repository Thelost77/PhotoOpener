using System.IO;

namespace PhotoOpener
{
    class Saver
    {
        private string _filePath;

        public Saver(string filePath)
        {
            _filePath = filePath;
        }
        public void SaveImage(string photoPath)
        {
            if (photoPath != null)
            {
                using (var streamWriter = new StreamWriter(_filePath))
                {
                    streamWriter.WriteLine(Program.PhotoPath);
                    streamWriter.Close();
                }
            }
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
