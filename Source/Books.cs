using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryHome.Source
{
    public class Books
    {
        public Books(short article, string? bookName, short num, short year) {
            Article = article;
            BookName = bookName;
            Num = num;
            Year = year;
        }

        public short Article { get; set; }
        public string? BookName { get; set; }
        public short Num { get; set; }
        public short Year { get; set; }
    }
}
