using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StoreData
{
    struct Price
    {
        public int sl, cc;

        public override string ToString()
        {
            return "Price: [SL: "+sl+", CC: "+cc+"]";
        }
    }

    struct LifeStatus
    {
        public float health, armor;
    }

    struct Effect
    {
        public LifeStatus damage, strengthening;

        public override string ToString()
        {
            return "Strengthening : [H: "+strengthening.health+", A: "+strengthening.armor+"], Damage : [H: "+damage.health+", A: "+damage.armor+"]";
        }
    }


    public class Item
    {
        private int id;
        private string name;
        private string description;
        private Price price;
        private Effect effect;
        private int availability;

        public Item(int id,
                    string name,
                    string description,
                    int sl,
                    int cc,
                    float strengtheningH,
                    float strenghteningA,
                    float damageH,
                    float damageA,
                    int availability)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            price.sl = sl;
            price.cc = cc;
            effect.strengthening.health = strengtheningH;
            effect.strengthening.armor = strenghteningA;
            effect.damage.health = damageH;
            effect.damage.armor = damageA;
            this.availability = availability;
        }

        public int GetID() { return id; }
        public string GetName() { return name; }
        public string GetDescription() { return description; }
        public int GetPriceSL() { return price.sl; }
        public int GetPriceCC() { return price.cc; }
        public float GetDamageFactor() { return effect.damage.health; }
        public float GetStrengtheningHealth() { return effect.strengthening.health; }
        public float GetStrengtheningArmor() { return effect.strengthening.armor; }
        public int GetAvailability() { return availability; }

        public override string ToString()
        {
            return "[" + id + " | " + name + ", " + description + ", " + price.ToString() + ", " + effect.ToString() + "," + availability + ",  ]";
        }
    }

    public static Item[] items = {
            new Item(0,
                "Glucose Syrup",
                "Helps you restore your health level (+20 pts)",
                10, 0, 20, 0, 0, 0, 0),
            new Item(1,
                "Hot Choco Mug",
                "A nice mug of hot chocolate to restore your armor (+25 pts)",
                0,10,20,0,0,0,2),
            new Item(2,
                "Choco Armor",
                "A chocolate armor that reduces enemy damages of 10%",
                5,20,20,0,0,0,1),
            new Item(3,
                "Dark Choco Armor",
                "A darker chocolate armor that reduces enemy damages of 25%",
                10,50,20,0,0,0,3),
            new Item(4,
                "Sprinkled Armor",
                "description here",
                0,0,0,0,0,0,4),
            new Item(5,
                "Rainbow Armor",
                "description here",
                0,0,0,0,0,0,10)
        };

    public static string ItemsToString()
    {
        string result = "Listed items are:";

        foreach (Item item in items)
        {
            result += "\n" + item.ToString();
        }

        return result;
    }

}
