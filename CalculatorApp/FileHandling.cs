using System;
using System.IO;
using System.Linq;

namespace CalculatorApp
{
    class FileHandling
    {
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
                throw new ArgumentNullException($"The file content cannot be null or empty");
            }

            return content;
        }

        public bool IsResultSaved(string[] results)
        {
            if (results == null)
            {
                throw new ArgumentNullException($"Attempt to write nothing into a file");
            }

            try
            {
                string path = Path.GetFullPath(@"..\..\..\result.txt");

                using (StreamWriter sw = File.CreateText(path))
                {
                    foreach (var result in results)
                    {
                        sw.WriteLine(result);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }           
        }
    }
}
