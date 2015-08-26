using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FileIndexer.Data;
using FileIndexer.Data.Models;

namespace FileIndexer
{
    public class EndOfWork:Exception
    {
    }

    public class NoneFiles:Exception
    {

    }

    public class NonDirectories:Exception
    {
    }

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
            var files = Directory.GetFiles(dir.Path).Where(str => Path.GetExtension(str).Contains(".txt"));
            StackAdapter.AddRangeTextFile(files.Select(str => new TextFileElement { Path = str }));
        }
    }

    public class TextFileAnalyzer
    {
        private static readonly List<TextFileDto> _coll = new List<TextFileDto>();

        public static void Analyze(TextFileElement file)
        {
            var words = new List<WordDto>();
            var rowPosition = -1;
            var line = string.Empty;
            using(var sr = new StreamReader(file.Path)) {
                while((line=sr.ReadLine())!=null) {
                    var columnPosition = 0;
                    rowPosition += 1;
                    foreach(var str in line.Split(' ')) {
                        words.Add(new WordDto { ColumnPosition = columnPosition, RowPosition = rowPosition, Value = str });
                        columnPosition += str.Length + 1;
                    }
                }
            }
            var textFileDto = new TextFileDto
            {
                Name = Path.GetFileName(file.Path),
                Path = Path.GetDirectoryName(file.Path),
                WordsInFile = words
            };
            lock(_coll) {
                _coll.Add(textFileDto);
            }
        }


        public static IQueryable<TextFileDto> GetTextFileDtos()
        {
            return _coll.AsQueryable();
        }
    }

    public static class StackAdapter
    {
        private static readonly DirectoryStack _dirStack = new DirectoryStack();
        private static readonly FileStack _fileStack = new FileStack();
        private static int _fileAnalyzerIndex = -1;
        private static readonly object _lockForFileAnalyzer = new object();
        private static int _dirAnalyzerIndex = -1;
        private static readonly object _lockForDirAnalyzer = new object();
        private static int _textfileAnalyzerIndex = -1;
        private static readonly object _lockForTextFileAnalyzer = new object();

        public static TextFileElement GetTextFile()
        {
            lock(_lockForTextFileAnalyzer) {
                Interlocked.Increment(ref _textfileAnalyzerIndex);//maybe redundante
                if(_textfileAnalyzerIndex >= _fileStack.Collection.Count) {
                    if(MultiThreadManager.CurrentAnlyzedDirectoriesByFileAnalyzer < 1)
                        throw new EndOfWork();
                    while(true) {
                        Thread.Sleep(5);
                        if(MultiThreadManager.CurrentAnlyzedDirectoriesByFileAnalyzer < 1 ||
                            _textfileAnalyzerIndex < _fileStack.Collection.Count)
                            break;
                    }
                    if(_textfileAnalyzerIndex < _fileStack.Collection.Count)
                        return _fileStack.Collection[_textfileAnalyzerIndex];
                    throw new EndOfWork();
                }
                return _fileStack.Collection[_textfileAnalyzerIndex];
            }
        }

        public static DirectoryElement GetDirForDirAnayzer()
        {
            lock(_lockForDirAnalyzer) {
                Interlocked.Increment(ref _dirAnalyzerIndex);

                if(_dirAnalyzerIndex >= _dirStack.Collection.Count) {
                    if(MultiThreadManager.CurrentAnlyzedDirectoriesByDirAnalyzer < 1)
                        throw new NonDirectories();
                    while(true) {
                        Thread.Sleep(5);
                        if(MultiThreadManager.CurrentAnlyzedDirectoriesByDirAnalyzer < 1 ||
                            _dirAnalyzerIndex < _dirStack.Collection.Count)
                            break;
                    }
                    if(_dirAnalyzerIndex < _dirStack.Collection.Count)
                        return _dirStack.Collection[_dirAnalyzerIndex];
                    throw new NonDirectories();
                }
                return _dirStack.Collection[_dirAnalyzerIndex];
            }
        }

        public static DirectoryElement GetDirForFileAnayzer()
        {
            lock(_lockForFileAnalyzer) {
                Interlocked.Increment(ref _fileAnalyzerIndex);
                if(_fileAnalyzerIndex >= _dirStack.Collection.Count) {
                    if(MultiThreadManager.CurrentAnlyzedDirectoriesByDirAnalyzer < 1)
                        throw new NoneFiles();
                    while(true) {
                        Thread.Sleep(5);
                        if(MultiThreadManager.CurrentAnlyzedDirectoriesByDirAnalyzer < 1 ||
                            _fileAnalyzerIndex < _dirStack.Collection.Count)
                            break;
                    }
                    if(_fileAnalyzerIndex < _dirStack.Collection.Count)
                        return _dirStack.Collection[_fileAnalyzerIndex];
                    throw new NoneFiles();
                }
                return _dirStack.Collection[_fileAnalyzerIndex];
            }
        }

        public static IEnumerable<DirectoryElement> GetDirectoryStack()
        {
            return _dirStack.Collection.AsEnumerable();
        }

        public static IEnumerable<TextFileElement> GetFileStack()
        {
            return _fileStack.Collection.AsEnumerable();
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

        public static void AnalyzeFile()
        {
            var file = StackAdapter.GetTextFile();
            TextFileAnalyzer.Analyze(file);
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