using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents.DocumentStructures;

namespace LibraryHome.Source
{
    public class People
    {
        public People(int id, string name, string email, List<int> bookArticles){ 
            Id = id;
            Name = name;    
            Email = email;
            Arts = new Dictionary<int, int>();

            foreach (int article in bookArticles)
            {
                if (Arts.ContainsKey(article))
                    Arts[article]++;
                else
                    Arts[article] = 1;
            }
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public Dictionary<int,int> Arts { get; set;}
    }
}
