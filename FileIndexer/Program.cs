﻿using System;
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
            IQueryable<TextFileDto> files = FileIndexer.GetTextFiles(Console.ReadLine());
        }
    }
}
