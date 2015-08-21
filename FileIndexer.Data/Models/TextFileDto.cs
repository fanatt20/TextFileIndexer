using System.Collections.Generic;

namespace FileIndexer.Data.Models
{
    internal class TextFileDto
    {
        public TextFileDto()
        {
            WordsInFile = new List<WordDto>();
        }

        public string Path { get; set; }
        public string Name { get; set; }
        public List<WordDto> WordsInFile { get; private set; }
    }
}