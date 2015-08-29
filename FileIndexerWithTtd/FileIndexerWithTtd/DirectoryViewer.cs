using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIndexerWithTtd
{
    public class DirectoryViewer
    {
        private IFileSystem _fileSystem;
        public DirectoryViewer(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public string[] GetDirectories(string path)
        {
            return _fileSystem.Directory.GetDirectories(path).Select(str => _fileSystem.Path.GetDirectoryName(str)).ToArray();
        }

        public DirectoryTree GetAllDirectories(string path)
        {
            return BuildTree(path);
        }

        private DirectoryTree BuildTree(string path)
        {
            var tree = new DirectoryTree(path);
            BuildTreeRecursion(tree);
            return tree;
        }
        private void BuildTreeRecursion(DirectoryTree tree)
        {

            foreach(var str in _fileSystem.Directory.GetDirectories(tree.Name)
                .Select(str => new DirectoryTree(_fileSystem.Path.GetDirectoryName(str)))) {
                if(str.Name == tree.Name)
                    continue;
                tree.SubDirectories.Add(str);
                BuildTreeRecursion(str);
            }

        }
    }
}
