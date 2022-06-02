using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public enum CollectableType
    {
        SL, CC
    }

    public class CollectableItem
    {
        private CollectableType type;
        private int _quantity;
        private int quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public CollectableItem(CollectableType type, int quantity)
        {
            this.type = type;
            this.quantity = quantity;
        }
    }

    // OLD
    /*
    public struct Resources
    {
        public int sl { get; }
        public int cc { get; }

        public Resources(int sl, int cc)
        {
            this.sl = sl;
            this.cc = cc;
        }
    }

    //CLASSES
    public struct ShopItem
    {
        public string name;
        public Resources cost;
        public float value;

        public ShopItem(string name, Resources cost, float value)
        {
            this.name = name;
            this.cost = cost;
            this.value = value;
        }
    }

    //DATA
    static ShopItem[] shopItems =
    {
        new ShopItem("Sugar Syrup", new Resources(10,0), 20f),
        new ShopItem("Choco Layer", new Resources(0,3), 80f),
        new ShopItem("Sugar Icing", new Resources(500, 0), 100f)
    };

    public static string[] GetShopItemsName()
    {
        string[] result = new string[shopItems.Length];

        for (int i = 0; i < shopItems.Length; i++)
        {
            result[i] = shopItems[i].name;
        }

        return result;
    }

    public static int[] GetShopItemsSL()
    {
        int[] result = new int[shopItems.Length];

        for (int i = 0; i < shopItems.Length; i++)
        {
            result[i] = shopItems[i].cost.sl;
        }

        return result;
    }

    public static int[] GetShopItemsCC()
    {
        int[] result = new int[shopItems.Length];

        for (int i = 0; i < shopItems.Length; i++)
        {
            result[i] = shopItems[i].cost.cc;
        }

        return result;
    }

    public static float[] GetShopItemsValue()
    {
        float[] result = new float[shopItems.Length];

        for (int i=0; i<shopItems.Length; i++)
        {
            result[i] = shopItems[i].value;
        }

        return result;
    }

    public static int GetCostSL(string name)
    {
        foreach(ShopItem item in shopItems)
            if(item.name == name)
                return item.cost.sl;
        return 0;
    }

    public static int GetCostCC(string name)
    {
        foreach (ShopItem item in shopItems)
            if (item.name == name)
                return item.cost.cc;
        return 0;
    }

    public static float GetValue(string name)
    {
        foreach (ShopItem item in shopItems)
            if (item.name == name)
                return item.value;
        return 0f;
    }
    */
}
