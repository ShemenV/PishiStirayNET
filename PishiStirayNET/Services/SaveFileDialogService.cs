using Microsoft.Win32;
using System.IO;

namespace PishiStirayNET.Services
{
    public class SaveFileDialogService
    {
        public string FilePath { get; set; }

        public string SaveFileDialog()
        {
            OpenFileDialog saveFileDialog = new();
            saveFileDialog.Filter = "Файлы изображений (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";

            var result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                string filepath = saveFileDialog.FileName;

                string resourcesPath = System.IO.Path.GetFullPath("Resources");

                byte[] data = default(byte[]);

                try
                {

                    using (FileStream fileStream = File.Create($"{resourcesPath}/{System.IO.Path.GetFileName(filepath)}"))
                    {

                    }

                    using (var stream = File.Open(filepath, FileMode.Open))
                    {
                        var reader = new StreamReader(stream);
                        using (var memstream = new MemoryStream())
                        {
                            reader.BaseStream.CopyTo(memstream);
                            data = memstream.ToArray();
                        }
                    }



                    File.WriteAllBytes($"{resourcesPath}/{System.IO.Path.GetFileName(filepath)}", data);

                }
                catch
                {

                }


                FilePath = System.IO.Path.GetFileName(filepath);
                return FilePath;
            }
            return FilePath;
        }
    }
}
