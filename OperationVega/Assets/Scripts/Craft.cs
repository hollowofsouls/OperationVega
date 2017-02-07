using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;


namespace Assets.Scripts
{
    
    public class Craft 
    {
        Food Items;
        int Amount;

        public Craft(Food item, int amount)
        {
            this.Items = item;
            this.Amount = amount;
        }
    }

    public class CanCraft
    {
        public List<Craft> Ingridients;
        Craft output;
    }

    public class Fuel : CanCraft
    {
        //Ingridients for Fuel
        public Fuel()
        {
            // List that contains ingridients for Fuel
            Ingridients = new List<Craft>();
            Ingridients.Add(new Craft(new Food(), 1));
        }
    }

    class Steel : CanCraft
    {
        //Ingridients for Steel
        public Steel()
        {
            //List that contains ingridients for Steel
            Ingridients = new List<Craft>();
            
        }
    }

    class CookedFood : CanCraft
    {
        //Ingridients for CookedFood
        public CookedFood()
        {
            //Ingridients needed for CookedFood
            Ingridients = new List<Craft>();
        }
    }
}
