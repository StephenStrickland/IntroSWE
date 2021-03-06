﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Order
    {
        public Order(List<ShoppingCartBook> books)
        {
            BooksInCart = books;
        }

        public Order(List<ShoppingCartBook> books, ShippingInfo shipping, PaymentInfo payment)
        {
            BooksInCart = books;
            ShippingInfo = shipping;
            PaymentInfo = payment;
        }

        public Order()
        {

        }

        public IList<ShoppingCartBook> BooksInCart { get; set; }
        public ShippingInfo ShippingInfo { get; set; }
        public PaymentInfo PaymentInfo { get; set; }

        public decimal SubTotalCost
        {
            get
            {
                if (BooksInCart != null)
                {
                    return BooksInCart.Select(x => x.QuantityInCart * x.Price).Sum();
                }
                return 0;
            }
        }

        public decimal Tax { get { return (SubTotalCost * (decimal)(.07)); } }//7%
        public decimal TotalCost { get { return Tax + SubTotalCost; } }
    }
}
