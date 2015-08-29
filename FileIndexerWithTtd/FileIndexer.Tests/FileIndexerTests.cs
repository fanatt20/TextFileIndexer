using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using FileIndexerWithTtd;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileIndexer.Tests
{
    [TestClass]
    public class FileIndexerTests
    {
        //TODO: view subdirectories
        //TODO: view subdirectories in subdirectory
        //TODO: save direcory tree
        //TODO: view files in directory
        //TODO: find files in directory tree
        //TODO: recognize text file
        //TODO: recognize text files in directory tree
        //TODO: save text files location

        //TODO: read from text file
        //TODO: parse text file into words
        //TODO: create storage for words
        //TODO: save words into storage
        private IFileSystem _fileSystem;
        private DirectoryTree _tree;

        [TestInitialize]
        public void Init()
        {
            _fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                {@"c:\myfile.txt", new MockFileData("Testing is meh.")},
                {@"c:\picture.jpg", new MockFileData("")},
                {@"c:\anotherfile.txt", new MockFileData("Lorem Ipsum")},
                {@"c:\Dir\", new MockDirectoryData()},
                {@"c:\Dir\Subdir1\Subdir4\Subdir6\", new MockDirectoryData()},
                {@"c:\Dir\Subdir2\Subdir5\", new MockDirectoryData()},
                {@"c:\Dir\Subdir3\", new MockDirectoryData()},
                {@"c:\demo\jQuery.js", new MockFileData("some js")},
                {@"c:\demo\sample.txt", new MockFileData("text blub")}
            });
            _tree = new DirectoryTree(@"c:\");
            _tree.SubDirectories.Add(new DirectoryTree(@"c:\Dir"));
            _tree.SubDirectories[0].SubDirectories.Add(new DirectoryTree(@"c:\Dir\Subdir1"));
            _tree.SubDirectories[0].SubDirectories.Add(new DirectoryTree(@"c:\Dir\Subdir2"));
            _tree.SubDirectories[0].SubDirectories.Add(new DirectoryTree(@"c:\Dir\Subdir3"));
            _tree.SubDirectories[0].SubDirectories[0].SubDirectories.Add(new DirectoryTree(@"c:\Dir\Subdir1\Subdir4"));
            _tree.SubDirectories[0].SubDirectories[0].SubDirectories[0].SubDirectories.Add(
                new DirectoryTree(@"c:\Dir\Subdir1\Subdir4\Subdir6"));
            _tree.SubDirectories[0].SubDirectories[1].SubDirectories.Add(new DirectoryTree(@"c:\Dir\Subdir2\Subdir5"));
            _tree.SubDirectories.Add(new DirectoryTree(@"c:\demo"));
        }

        [TestMethod]
        public void ViewDirectories()
        {
            var directoryViewer = new DirectoryViewer(_fileSystem);
            var subdir = directoryViewer.GetDirectories(@"c:\");
            Assert.AreEqual(2, subdir.Length);
            Assert.IsTrue(subdir.Contains(@"c:\Dir"));
            Assert.IsTrue(subdir.Contains(@"c:\demo"));
        }

        [TestMethod]
        public void ViewSubDirectories()
        {
            var direcoryViewer = new DirectoryViewer(_fileSystem);
            var subdirs = direcoryViewer.GetAllDirectories(@"c:\");
            Assert.IsTrue(_tree.Equals(subdirs));
        }

        [TestMethod]
        public void GetFiles()
        {
            var fileViewer = new FileViewer(_fileSystem);
            var files = fileViewer.GetFiles(@"c:\");
            Assert.AreEqual(3, files.Length);
            Assert.IsTrue(files.Contains(@"c:\myfile.txt"));
            Assert.IsTrue(files.Contains(@"c:\picture.jpg"));
            Assert.IsTrue(files.Contains(@"c:\anotherfile.txt"));
        }

        [TestMethod]
        public void GetAllFiles()
        {
            var fileViewer = new FileViewer(_fileSystem);
            var files = fileViewer.GetAllFiles(_tree);
            var tree = new DirectoryTree(_tree);
            tree.Files.Add(@"c:\myfile.txt");
            tree.Files.Add(@"c:\picture.jpg");
            tree.Files.Add(@"c:\anotherfile.txt");
            tree.SubDirectories[1].Files.Add(@"c:\demo\jQuery.js");
            tree.SubDirectories[1].Files.Add(@"c:\demo\sample.txt");
            Assert.AreEqual(tree, files);
        }

        [TestMethod]
        public void GetAllTextFiles()
        {
            var fileViewer = new FileViewer(_fileSystem);
            var files = fileViewer.GetAllTextFiles(_tree);
            var tree = new DirectoryTree(_tree);
            tree.Files.Add(@"c:\myfile.txt");
            tree.Files.Add(@"c:\anotherfile.txt");
            tree.SubDirectories[1].Files.Add(@"c:\demo\sample.txt");
            Assert.AreEqual(tree, files);
        }
    }
}