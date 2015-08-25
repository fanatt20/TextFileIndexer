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

    public class TextFilesAndWordsRepo:ITextFilesAndWordsRepo
    {
        private readonly DbModelContainer _container;

        public TextFilesAndWordsRepo(DbModelContainer container)
        {
            _container = container;
        }

        public IQueryable<TextFileDto> GetFiles(int skip = 0, int take = -1)
        {
            var query =_container.TextFiles.Select(file => file)
                    .GroupJoin(_container.Words, file => file.Id, word => word.TextFileId,
                        (file, word) => new {File = file, Words = word,Path=""});

            
                   
            return (new List<TextFileDto>()).AsQueryable();
        }

        public void AddFile(TextFileDto file)
        {
        }
    }
}