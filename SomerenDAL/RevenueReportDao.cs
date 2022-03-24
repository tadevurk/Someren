using SomerenModel;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SomerenDAL
{
    public class RevenueReportDao : BaseDao
    {
        public RevenueReport GetSalesAmount(DateTime dateFrom, DateTime dateTo)
        {

            string query = "SELECT COUNT (DISTINCT [studentId]) AS [TotalCustomer], SUM(CashRegister.salesAmount) AS SalesAmount, SUM(CashRegister.salesAmount*Drinks.drinkPrice) AS TotalTurnOver" +
                " FROM CashRegister JOIN Drinks ON CashRegister.drinkId = Drinks.drinkId " +
                "WHERE date BETWEEN @dateFrom AND @dateTo";

            SqlParameter[] dateParameters =
            {
                new SqlParameter("@dateFrom", dateFrom),
                new SqlParameter("@dateTo", dateTo),
            };

            return ReadRevenue(ExecuteSelectQuery(query, dateParameters));
        }

        public RevenueReport ReadRevenue(DataTable dataTable)
        {
            // Returning only a object because my query returns only a row.
            RevenueReport revenueReport = new RevenueReport();

            foreach (DataRow dr in dataTable.Rows)
            {
                // Checking if one of the column is empty...
                object value = dr["SalesAmount"];

                if (value == DBNull.Value)
                {
                    throw new Exception("There is no data between the selected date!");
                }
                revenueReport.NumberOfSales = (int)dr["SalesAmount"];
                revenueReport.TurnOver = (decimal)dr["TotalTurnOver"];
                revenueReport.NumberOfCustomers = (int)dr["TotalCustomer"];
            }
            return revenueReport;
        }
    }
}
