using System;
using System.IO;
using System.Linq;

namespace CalculatorApp
{
    class FileHandling
    {
        private string _userDirectory;


        public string[] ReadContent(string[] args)
        {
            string path = String.Join(' ', args);

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException($"'{nameof(path)}' cannot be null or empty", nameof(path));
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("The given file is invalid or does not exist");
            }

            string[] content = File.ReadAllLines(path);

            if (content == null || content.Any(x => x == null))
            {
                throw new ArgumentNullException("The file content cannot be null or empty");
            }

            _userDirectory = Path.GetDirectoryName(path);

            return content;
        }

        public string WriteResult(string[] result)
        {
            if (result == null)
            {
                throw new ArgumentNullException("Attempt to write nothing into a file");
            }

            try
            {
                string path = _userDirectory + @"\result.txt";

                using (StreamWriter sw = File.CreateText(path))
                {
                    foreach (string line in result)
                    {
                        sw.WriteLine(line);
                    }
                }

                return $"\nThe file with the processing results has been succesefully saved\nPath: '{path}'\n";
            }
            catch (IOException e)
            {
                return $"The file could not be saved: '{e}'";
            }           
        }
    }
}
