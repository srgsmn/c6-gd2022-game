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
}