using SomerenDAL;
using SomerenModel;

namespace SomerenLogic
{
    public class CashRegisterService
    {
        CashRegisterDao cashRegisterdb;

        public CashRegisterService()
        {
            cashRegisterdb = new CashRegisterDao();
        }

        public void AddSale(Sale sale)
        {
            cashRegisterdb.AddSale(sale);
        }
    }
}
