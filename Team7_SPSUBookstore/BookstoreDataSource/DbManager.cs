using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Reflection;

namespace BookstoreDataSource
{
    public class DbManager
    {
        public DbManager(string userUrl, string bookUrl)
        {
            Users = new List<User>();
            Books = new List<BookDatabaseItem>();
            ReadFiles(userUrl, bookUrl);
        }
        public IList<User> Users { get; set; }
        public IList<BookDatabaseItem> Books {get; set;}

        public void ReadFiles(string userUrl, string bookUrl)
        {
            String root = AppDomain.CurrentDomain.BaseDirectory;
           
            //generate users in text file
            using (StreamReader sr = File.OpenText(root + userUrl))
            {
                string line = String.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] input = line.Split(new char[] { ' ' }, 2);
                    Users.Add(new User() { Email = input[0], Password = input[1]});
                }
                // @"Y:\Code\IntroSWE\Team7_SPSUBookstore\users.txt"
            }
            //generate the Book list
            if(bookUrl.Contains(".csv"))
            {
                using (StreamReader sr = File.OpenText(root + userUrl))
                {
                    string line = String.Empty;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] input = line.Split(new char[] { ',' }, 2);
                        //copy assign attribute values and 
                        Book newBook = new Book()
                            {
                                ISBN = input[0]
                            };

                        Books.Add(newBook);
                    }
                    // @"Y:\Code\IntroSWE\Team7_SPSUBookstore\users.txt"
                }
            }
        }
    }
}
