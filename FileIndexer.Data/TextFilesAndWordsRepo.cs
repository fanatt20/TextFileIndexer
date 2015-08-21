using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileIndexer.Data.Models;

namespace FileIndexer.Data
{
    public class TextFilesAndWordsRepo
    {
        private DbModelContainer _container;

        public TextFilesAndWordsRepo(DbModelContainer container)
        {
            _container = container;
        }

        public IQueryable<TextFileDto> GetFiles(int skip = 0, int take = -1)
        {
            var 
            var query = from textFile in _container.TextFiles
                join path in _container.PathToTextFileCollections on textFile.PathToTextFileId equals path.Id
                join words in _container.Words on textFile.Id equals words.TextFileId
                join wordsDictionary in _container.WordsDictionaries on words.WordsDictionaryId equals
                    wordsDictionary.Id
                select new TextFileDto() {Name = textFile.Name, Path =};
            return query;

        }

        private string GetFullPath()
        {
            
        }


        public void AddFile(TextFileDto file)
        {

        }
    }
}
