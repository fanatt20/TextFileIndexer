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
            foreach(var textFileDto in FileIndexer.GetTextFiles(@"G:\003_Бизнес\"))
            {
                Console.WriteLine(textFileDto.Path+textFileDto.Name);
                foreach (var word in textFileDto.WordsInFile)
                {
                    Console.Write(word.Value);
                }
            }
            Console.ReadKey();
        }
    }
}
