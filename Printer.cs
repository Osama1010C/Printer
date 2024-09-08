using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printing_Machine
{
    public class Printer
    {
        private string text;
        private int copies;
        private string fileName;

        public Printer(string text, int copies, string fileName)
        {
            this.text = text;
            this.copies = copies;
            this.fileName = fileName;
        }

        public string Text { get { return text; } }
        public int Copies { get { return copies; } }
        public string FileName { get { return fileName; } }

        private bool IsValidPath(string path)
        {
            return Uri.TryCreate(path, UriKind.Absolute, out Uri result) && result != null && Directory.Exists(path);
        }
        private bool IsFolderExist(string path)
        {
            return Directory.Exists(path);
        }

        public void CreateInPath(string path)
        {
            if (!IsValidPath(path))
            {
                throw new Exception("Invalid Path");
            }
            
            for (int i = 0; i < Copies; i++)
            {
                string filePath = $@"{path}\{FileName}{i + 1}.txt";
                File.WriteAllText(filePath, Text);
            }
        }

        public void CreateInOneFolder(string path)
        {
            if (!IsValidPath(path))
            {
                throw new Exception("Invalid Path");
            }
            string folderPath = path + $@"\{FileName.ToUpper()[0]}{FileName[1..]}";
            if (!IsFolderExist(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }


            for (int i = 0; i < Copies; i++)
            {
                string filePath = $@"{folderPath}\{FileName}{i + 1}.txt";
                File.WriteAllText(filePath, Text);
            }
        }

        public void CreateEachFileInFolder(string path)
        {
            if (!IsValidPath(path))
            {
                throw new Exception("Invalid Path");
            }
            string folderPath = path + $@"\{FileName.ToUpper()[0]}{FileName[1..]}";
            if (!IsFolderExist(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }


            for (int i = 0; i < Copies; i++)
            {
                string newFolder = $@"{folderPath}\{FileName.ToUpper()[0]}{FileName[1..]}{i + 1}";
                Directory.CreateDirectory(newFolder);
                string filePath = $@"{newFolder}\{FileName}{i + 1}.txt";
                File.WriteAllText(filePath, Text);
            }
        }


    }
}
