using SomerenDAL;
using SomerenModel;
using System.Collections.Generic;

namespace SomerenLogic
{
    public class DrinkService
    {
        DrinkDao drinkdb;

        public DrinkService()
        {
            drinkdb = new DrinkDao();
        }

        public List<Drink> GetDrinks()
        {
            List<Drink> drinks = drinkdb.GetAllDrinks();
            return drinks;
        }

        public void Add(Drink drink)
        {
            drinkdb.Add(drink);
        }

        public void Delete(Drink drink)
        {
            drinkdb.Delete(drink);
        }

        public void Update(Drink drink)
        {
            drinkdb.Update(drink);
        }
    }
}
