﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ShoppingCartBook
    {
        public string ISBN { get; set; }
        //public BookStock QuantityInCart { get; set; }
        public StockType TypeInCart { get; set; }
        public int QuantityInCart { get; set; }
        public decimal Price { get; set; }
    }
}
