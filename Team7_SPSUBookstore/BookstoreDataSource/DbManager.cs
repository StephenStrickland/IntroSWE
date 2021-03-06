﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Reflection;
using Excel;
using System.Data;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace BookstoreDataSource
{
    public class DbManager
    {
        public DbManager(string uUrl, string bUrl)
        {
            Users = new List<User>();
            Books = new List<BookDatabaseItem>();
            userUrl = uUrl;
            bookUrl = bUrl;
            ReadFiles(uUrl, bUrl);
        }

        public IList<User> Users { get; set; }
        public IList<BookDatabaseItem> Books {get; set;}
        private static Workbook excelBook = null;
        private static Application excelApp = null;
        private static Worksheet MySheet = null;
        private string userUrl { get; set; }
        private string bookUrl { get; set; }

        //Reads in user and book files into usable objects.
        public void ReadFiles(string userUrl, string bookUrl)
        {
            String root = AppDomain.CurrentDomain.BaseDirectory;
            FileStream stream = File.Open(root + bookUrl, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            try
            {


                //generate users in text file
                using (StreamReader sr = File.OpenText(root + userUrl))
                {
                    string line = String.Empty;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] input = line.Split(new char[] { ' ' }, 2);
                        Users.Add(new User() { Email = input[0], Password = input[1] });
                    }
                    sr.Close();
                }

                //generate the Book list
                if (bookUrl.Contains(".xlsx"))
                {
                    stream = File.Open(root + bookUrl, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    DataSet result = excelReader.AsDataSet();
                    excelReader.IsFirstRowAsColumnNames = true;

                    result = excelReader.AsDataSet();

                    result.AcceptChanges();

                    Books = result.Tables[0].AsEnumerable().Select(x =>
                          new BookDatabaseItem()
                          {
                              ISBN = x.Field<string>("ISBN"),
                              Title = x.Field<string>("title"),
                              Author = x.Field<string>("author"),
                              Semester = x.Field<string>("semester"),
                              Course = x.Field<string>("course"),
                              Section = Convert.ToInt32(x.Field<double>("section")),
                              Professor = x.Field<string>("professor"),
                              CRN = x.Field<double>("CRN").ToString(),
                              isRequired = (x.Field<string>("use") == "Required"),
                              Stock = ConvertToStock(Convert.ToInt32(x.Field<object>("quantityNew")), Convert.ToInt32(x.Field<object>("quantityUsed")),
                                  Convert.ToInt32(x.Field<double>("quantityRental")), Convert.ToInt32(x.Field<double>("quantityEBook")), Convert.ToDecimal(x.Field<double>("priceNew")),
                                  Convert.ToDecimal(x.Field<double>("priceUsed")), Convert.ToDecimal(x.Field<double>("priceRental")), Convert.ToDecimal(x.Field<double>("priceEBook"))),

                              Description = x.Field<string>("description")
                          }).ToList();

                    result.Dispose();
                    excelReader.Close();
                    stream.Close();
                }
            }
            catch (Exception e)
            {
                excelReader.Close();
                stream.Close();
            }

        }

        public List<BookStock> ConvertToStock(int qtyNew, int qtyUsed, int qtyRental, int qtyEBook, decimal pNew, decimal pUsed, decimal pRental, decimal pEbook)
        {
            List<BookStock> stock = new List<BookStock>();
            stock.Add(new BookStock() { Price = pNew, Quantity = qtyNew, Type = StockType.New});
            stock.Add(new BookStock() { Price = pUsed, Quantity = qtyUsed, Type = StockType.Used });
            stock.Add(new BookStock() { Price = pRental, Quantity = qtyRental, Type = StockType.Rental });
            stock.Add(new BookStock() { Price = pEbook, Quantity = qtyEBook, Type = StockType.eBook });
            return stock;
        }

        public bool UpdateStock(string isbn, int quantityToRemove, StockType type)
        {
            String root = AppDomain.CurrentDomain.BaseDirectory;
            excelApp = new Application();
            try
            {
                excelApp.Visible = false;
                excelBook = excelApp.Workbooks.Open(root + bookUrl);
                MySheet = (Worksheet)excelBook.Sheets[1]; // Explicit cast is not required here
                var lastRow = MySheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell).Row;
                Range isbnCol = MySheet.get_Range("A2", "A" + lastRow.ToString());
                var bookRow = isbnCol.EntireRow.Find(isbn,
                                Missing.Value, XlFindLookIn.xlValues, XlLookAt.xlPart,
                                XlSearchOrder.xlByColumns, XlSearchDirection.xlNext,
                                true, Missing.Value, Missing.Value);
                int col = bookRow.Row;
                int oldQty = 0;

                switch (type)
                {
                    case StockType.Rental:
                        oldQty = Convert.ToInt32(MySheet.Cells[col, 12].Value);
                        MySheet.Cells[col, 12].Value = calcNewQuantity(oldQty, quantityToRemove);
                        break;
                    case StockType.New:
                        oldQty = Convert.ToInt32(MySheet.Cells[col, 10].Value);
                        MySheet.Cells[col, 10].Value = calcNewQuantity(oldQty, quantityToRemove);
                        break;
                    case StockType.Used:
                        oldQty = Convert.ToInt32(MySheet.Cells[col, 11].Value);
                        MySheet.Cells[col, 11].Value = calcNewQuantity(oldQty, quantityToRemove);
                        break;
                    case StockType.eBook:
                        oldQty = Convert.ToInt32(MySheet.Cells[col, 13].Value);
                        MySheet.Cells[col, 13].Value = calcNewQuantity(oldQty, quantityToRemove);
                        break;
                    default:
                        break;
                }

                excelBook.Save();

                excelBook.Close(false);
                excelApp.DisplayAlerts = true;
                excelApp.Quit();
                Marshal.ReleaseComObject(MySheet);
                Marshal.ReleaseComObject(excelBook);
                Marshal.ReleaseComObject(excelApp);
                MySheet = null;
                excelBook = null;
                excelApp = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                return true;
            }
            catch(Exception e)
            {
                excelBook.Close(false);
                excelApp.DisplayAlerts = true;
                excelApp.Quit();
                Marshal.ReleaseComObject(MySheet);
                Marshal.ReleaseComObject(excelBook);
                Marshal.ReleaseComObject(excelApp);
                MySheet = null;
                excelBook = null;
                excelApp = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                return false;
            }
        }

        public int calcNewQuantity(int origQty, int newQty)
        {
            int newVal = origQty - newQty;
            return (newVal >= 0? newVal: 0);
        }
    }
}
