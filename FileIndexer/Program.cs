using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileIndexer.Data.Models;

namespace FileIndexer
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach(var textFileDto in FileIndexer.GetTextFiles(@"C:\Users\Андрей\Desktop\Tree"))
            {
                Console.WriteLine(textFileDto.Path+" "+textFileDto.Name);
                foreach (var word in textFileDto.WordsInFile)
                {
                    Console.Write(word.Value);
                }
                Console.WriteLine(Environment.NewLine);
            }
            Console.ReadKey();
        }
    }
}
