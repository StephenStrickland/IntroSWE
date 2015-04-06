using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BookDatabaseItem
    {

        //add attributes here
        public virtual string ISBN { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Edition { get; set; }
        public string Course { get; set; }
        public int Section { get; set; }
        public string Semester { get; set; }
        public string Professor { get; set; }
        public string CRN { get; set; }
        public bool isRequired { get; set; }
        public IList<BookStock> Stock { get; set; }

        //        title : String
        //-author : String
        //-ISBN : String
        //-edition : String
        //-semester : String
        //-course : String
        //-section : Integer
        //-professor : String
        //-CRN : Integer
        //-required : BOOL
        //-recommended : BOOL
        //-price : Array<double> -quantityInStock : Array<Integer>

        public ShoppingCartBook convertBookStock(int quantity)
        {
            ShoppingCartBook b = new ShoppingCartBook();
            b.ISBN = this.ISBN;
            b.QuantityInCart = quantity;

            return b;
        }

    }

    public class BookStock
    {
        enum StockType { Rental, New, Used, E_Book}

        public StockType Type { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    enum StockType { Rental, New, Used, E_Book }
}
