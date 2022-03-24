using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SomerenModel;
using System.Data;
using System.Configuration;

namespace SomerenDAL
{
    public class CashRegisterDao : BaseDao
    {
        public List<Sale> GetAllSales()
        {
            string query = "SELECT [salesId], [studentId], [drinkId], [salesAmount], [totalPayment], [date] FROM [CashRegister]";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Sale> ReadTables(DataTable dataTable)
        {
            List<Sale> sales = new List<Sale>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Sale sale = new Sale()
                {
                    SaleId = (int)dr["salesId"],
                    StudentId = (int)dr["studentId"],
                    DrinkId = (int)dr["drinkId"],
                    SaleCount = (int)dr["salesAmount"],
                    TotalPayment = (decimal)dr["totalPayment"],
                    SaleDate = (DateTime)dr["date"]
                };
                sales.Add(sale);
            }
            return sales;
        }

        public void AddSale(Sale sale)
        {
            string query = "INSERT INTO CashRegister " +
            "(salesId, studentId, drinkId, " +
            "salesAmount, totalPayment, [date]) " +
            "VALUES (@salesId, @studentId, @drinkId, " +
            "@salesAmount, @totalPayment, @date) " +
            "SELECT SCOPE_IDENTITY()";

            List<Sale> currentList = new List<Sale>();
            currentList = GetAllSales();

            sale.SaleId = currentList.Count + 1;

            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@salesId", sale.SaleId),
                new SqlParameter("@StudentId", sale.StudentId),
                new SqlParameter("@drinkId", sale.DrinkId),
                new SqlParameter("@salesAmount", sale.SaleCount),
                new SqlParameter("@totalPayment", sale.TotalPayment),
                new SqlParameter("@date", sale.SaleDate)
            };
            ExecuteEditQuery(query, sqlParameters);
        }


    }
}
