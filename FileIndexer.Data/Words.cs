//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FileIndexer.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Words
    {
        public int Id { get; set; }
        public int WordsDictionaryId { get; set; }
        public int TextFileId { get; set; }
        public int RowPosition { get; set; }
        public int ColumnPosition { get; set; }
    
        public virtual WordsDictionary WordsDictionary { get; set; }
        public virtual TextFile TextFile { get; set; }
    }
}
