using System.Configuration;
using System.IO.Abstractions;
using System.Linq;

namespace FileIndexerWithTtd
{
    public class FileViewer
    {
        private readonly IFileSystem _fileSystem;

        public FileViewer(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public string[] GetFiles(string path)
        {
            return _fileSystem.Directory.GetFiles(path);
        }

        public DirectoryTree GetAllFiles(DirectoryTree tree)
        {
            return AppendTree(tree, false);
        }

        private DirectoryTree AppendTree(DirectoryTree tree, bool isTextSearch)
        {
            var result = new DirectoryTree(tree);
            AppendTreeRecursion(result, isTextSearch);
            return result;
        }

        private void AppendTreeRecursion(DirectoryTree tree, bool isTextSearch)
        {
            if (isTextSearch)
                foreach (var file in ConfigurationManager.AppSettings["TextFileFormats"].Split('|')
                    .SelectMany(str => _fileSystem.Directory.GetFiles(tree.Name, str)))
                    tree.Files.Add(file);

            else
                foreach (var file in _fileSystem.Directory.GetFiles(tree.Name))
                    tree.Files.Add(file);

            foreach (var dir in tree.SubDirectories)
                AppendTreeRecursion(dir, isTextSearch);
        }

        public DirectoryTree GetAllTextFiles(DirectoryTree tree)
        {
            return AppendTree(tree, true);
        }
    }
}