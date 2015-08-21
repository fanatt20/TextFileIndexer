using System.Collections.Generic;
using System.Linq;
using FileIndexer.Data.Models;

namespace FileIndexer.Data
{
    public interface ITextFilesAndWordsRepo
    {
        IQueryable<TextFileDto> GetFiles(int skip = 0, int take = -1);
        void AddFile(TextFileDto file);
    }

    public class TextFilesAndWordsRepo : ITextFilesAndWordsRepo
    {
        private readonly DbModelContainer _container;

        public TextFilesAndWordsRepo(DbModelContainer container)
        {
            _container = container;
        }

        public IQueryable<TextFileDto> GetFiles(int skip = 0, int take = -1)
        {
            var query = from textFile in _container.TextFiles
                join path in _container.PathToTextFileCollections on textFile.PathToTextFileId equals path.Id
                join words in _container.Words on textFile.Id equals words.TextFileId
                join wordsDictionary in _container.WordsDictionaries on words.WordsDictionaryId equals
                    wordsDictionary.Id
                select new TextFileDto {Name = textFile.Name, Path = path.Value};
            return (new List<TextFileDto>()).AsQueryable();
        }

        public void AddFile(TextFileDto file)
        {
        }
    }
}