using System;
using System.Collections.Generic;
using System.Linq;

namespace FileIndexerWithTtd
{
    public class DirectoryTree : IEquatable<DirectoryTree>
    {
        private readonly List<string> _files = new List<string>();

        private readonly List<DirectoryTree> _subDirectories = new List<DirectoryTree>();

        public DirectoryTree(string name)
        {
            Name = name;
        }

        public DirectoryTree(DirectoryTree tree)
        {
            Name = tree.Name;
            _files.AddRange(tree._files);
            foreach (var dir in tree._subDirectories)
                _subDirectories.Add(new DirectoryTree(dir));
        }

        public string Name { get; private set; }

        public List<DirectoryTree> SubDirectories
        {
            get { return _subDirectories; }
        }

        public List<string> Files
        {
            get { return _files; }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((DirectoryTree) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (_subDirectories != null ? _subDirectories.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (_files != null ? _files.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return Name;
        }

        #region IEquatable<DirectoryTree> Members

        public bool Equals(DirectoryTree other)
        {
            if (_files.Count != other._files.Count || _subDirectories.Count != other._subDirectories.Count ||
                Name != other.Name)
                return false;
            if (_files.Any(str => !other._files.Contains(str)))
                return false;
            return !_subDirectories.Where((t, i) => !t.Equals(other._subDirectories[i])).Any();
        }

        public static bool operator ==(DirectoryTree left, DirectoryTree right)
        {
            if (left == null)
                return false;
            return left.Equals(right);
        }

        public static bool operator !=(DirectoryTree left, DirectoryTree right)
        {
            if (left == null)
                return false;
            return !left.Equals(right);
        }

        #endregion
    }
}