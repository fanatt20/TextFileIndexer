using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileIndexer.Data.Models;

namespace FileIndexer
{
    class FileIndexer
    {
        public static IQueryable<TextFileDto> GetTextFiles(string rootDirectory)
        {
            StackAdapter.AddDirectory(new DirectoryElement(rootDirectory));
            try {
                while(true) {
                    try {
                        while(true) {
                            MultiThreadManager.AnalyzeDirectoryForSubDirectories();
                        }
                    }
                    catch(NonDirectories) { }
                    
                    try {
                        while(true) {
                            MultiThreadManager.AnalyzeDirectoryForFiles();
                        }
                    }
                    catch(NoneFiles) { }

                    MultiThreadManager.AnalyzeFile();
                }

            }
            catch(EndOfWork) {
            }
            return TextFileAnalyzer.GetTextFileDtos();

        }

    }
}
