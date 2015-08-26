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
            var enusm = FileIndexer.GetTextFiles(@"C:\Users\Андрей\Desktop\Tree");
            Console.WriteLine("Total files checked:{0}",enusm.Count());
            Console.ReadKey();
            //Console.ReadLine();
            //foreach(var textFileDto in FileIndexer.GetTextFiles(@"G:\003_Бизнес")) {
            //    Console.WriteLine("File\n Name: " + textFileDto.Name + "\nPath: " + textFileDto.Path);
            //    foreach(var word in textFileDto.WordsInFile) {
            //        Console.WriteLine("Word : " + word.Value);
            //        Console.WriteLine("RowPosition: " + word.RowPosition);
            //        Console.WriteLine("ColumnPosition: " + word.ColumnPosition);

            //    }
            //    Console.WriteLine(Environment.NewLine);
            //}
            //Console.ReadKey();
        }
    }
}
