using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryHome.Source
{
    public class BookCount
    {
        public Books Book { get; set; }
        public int Count { get; set; }

        public BookCount(Books book, int count)
        {
            Book = book;
            Count = count;
        }
    }
}
