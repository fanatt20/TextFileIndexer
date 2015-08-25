using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace FileIndexer
{
    internal class DirectoryStack
    {
        public DirectoryStack()
        {
            Collection = new List<DirectoryElement>();
        }

        public List<DirectoryElement> Collection { get; private set; }
    }

    internal class FileStack
    {
        public FileStack()
        {
            Collection = new List<TextFileElement>();
        }

        public List<TextFileElement> Collection { get; private set; }
    }

    public class TextFileElement
    {
        public string Path;
    }

    public static class DirectoryAnalyzer
    {
        public static void Analyze(DirectoryElement dir)
        {
            var subDir = Directory.GetDirectories(dir.Path);
            StackAdapter.AddRangeDirectory(subDir.Select(str => new DirectoryElement(str)));
        }
    }

    public static class FileAnalyzer
    {
        public static void Analyze(DirectoryElement dir)
        {
            var files = Directory.GetFiles(dir.Path);
            StackAdapter.AddRangeTextFile(files.Select(str => new TextFileElement { Path = str }));
        }
    }

    public static class StackAdapter
    {
        private static readonly DirectoryStack _dirStack = new DirectoryStack();
        private static readonly FileStack _fileStack = new FileStack();
        private static readonly int _fileAnalyzerIndex = -1;
        private static readonly object _lockForFileAnalyzer = new object();
        private static int _dirAnalyzerIndex = -1;
        private static readonly object _lockForDirAnalyzer = new object();

        public static DirectoryElement GetDirForDirAnayzer()
        {
            lock(_lockForDirAnalyzer) {
                Interlocked.Increment(ref _dirAnalyzerIndex);

                if(_dirAnalyzerIndex > _dirStack.Collection.Count) {
                }
                return _dirStack.Collection[_dirAnalyzerIndex];
            }
        }

        public static void AddDirectory(DirectoryElement dir)
        {
            lock(_dirStack) {
                _dirStack.Collection.Add(dir);
            }
        }

        public static void AddRangeDirectory(IEnumerable<DirectoryElement> collection)
        {
            lock(_dirStack) {
                _dirStack.Collection.AddRange(collection);
            }
        }

        public static DirectoryElement GetDirForFileAnayzer()
        {
            lock(_lockForFileAnalyzer) {
                if(_fileAnalyzerIndex > _dirStack.Collection.Count) {
                }
                return _dirStack.Collection[_fileAnalyzerIndex];
            }
        }

        public static void AddTextFile(TextFileElement dir)
        {
            lock(_fileStack) {
                _fileStack.Collection.Add(dir);
            }
        }

        public static void AddRangeTextFile(IEnumerable<TextFileElement> collection)
        {
            lock(_fileStack) {
                _fileStack.Collection.AddRange(collection);
            }
        }
    }

    public static class MultiThreadManager
    {
        public static int CurrentAnlyzedDirectoriesByDirAnalyzer;
        public static int CurrentAnlyzedDirectoriesByFileAnalyzer;

        public static void AnalyzeDirectoryForSubDirectories()
        {
            var dirForAnalyze = StackAdapter.GetDirForDirAnayzer();
            Interlocked.Increment(ref CurrentAnlyzedDirectoriesByDirAnalyzer);
            DirectoryAnalyzer.Analyze(dirForAnalyze);
            Interlocked.Decrement(ref CurrentAnlyzedDirectoriesByDirAnalyzer);
        }

        public static void AnalyzeDirectoryForFiles()
        {
            var dirForAnalyze = StackAdapter.GetDirForFileAnayzer();
            Interlocked.Increment(ref CurrentAnlyzedDirectoriesByFileAnalyzer);
            FileAnalyzer.Analyze(dirForAnalyze);
            Interlocked.Decrement(ref CurrentAnlyzedDirectoriesByFileAnalyzer);
        }
    }

    public class DirectoryElement
    {
        public string Path;

        public DirectoryElement(string path)
        {
            Path = path;
        }
    }
}