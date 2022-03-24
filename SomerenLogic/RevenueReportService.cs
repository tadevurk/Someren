using System;
using SomerenDAL;
using SomerenModel;

namespace SomerenLogic
{
    public class RevenueReportService
    {
        RevenueReportDao revenueRapordb;
        public RevenueReportService()
        {
            revenueRapordb = new RevenueReportDao();
        }

        public RevenueReport GetSales(DateTime dateFrom, DateTime dateTo)
        {
            RevenueReport revenueReports = revenueRapordb.GetSalesAmount(dateFrom, dateTo);
            return revenueReports;
        }
    }
}
