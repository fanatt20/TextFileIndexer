using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FileIndexer.Data.Models;

namespace FileIndexer
{
    class FileIndexer
    {
        private static void RunDirAnalyzer()
        {
            try {
                while(true) {
                    MultiThreadManager.AnalyzeDirectoryForSubDirectories();
                }
            }
            catch(NonDirectories) { }

        }
        private static void RunFileAnalyzer()
        {
            try {
                while(true) {
                    MultiThreadManager.AnalyzeDirectoryForFiles();
                }
            }
            catch(NoneFiles) { }
        }

        private static void RunTextFileAnalyzer()
        {
            try {
                while(true) {
                    MultiThreadManager.AnalyzeFile();
                }
            }
            catch(EndOfWork) { }
            
        }
        public static IQueryable<TextFileDto> GetTextFiles(string rootDirectory)
        {
            StackAdapter.AddDirectory(new DirectoryElement(rootDirectory));
#region MonoThread Variant
            /* try {
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
            }*/
#endregion
#region MultiThread Variant
            Thread dirThread= new Thread(RunDirAnalyzer);
            dirThread.Start();
            var filesThread=new Thread(RunFileAnalyzer);
            filesThread.Start();
            var textfilesThread1=new Thread(RunTextFileAnalyzer);
            var textfilesThread2 = new Thread(RunTextFileAnalyzer);
            textfilesThread1.Start();
            textfilesThread2.Start();

            textfilesThread1.Join();
            textfilesThread2.Join();

#endregion
            return TextFileAnalyzer.GetTextFileDtos();

        }

    }
}
