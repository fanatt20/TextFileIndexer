using System.Collections.Generic;

namespace FileIndexer.Data.Models
{
    public class TextFileDto
    {
        public TextFileDto()
        {
            WordsInFile = new List<WordDto>();
        }

        public string Path { get; set; }
        public string Name { get; set; }
        public List<WordDto> WordsInFile { get;  set; }
    }
}