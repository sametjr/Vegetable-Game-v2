using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager
{
    // public enum Foods
    // {
    //     Soup = 0,
    //     Beef = 1
    // }
    public static Dictionary<string, int> Soup
    {
        get
        {
            Dictionary<string, int> ingredients = new Dictionary<string, int>();
            ingredients["beet"] = 2;
            ingredients["caper"] = 1;
            return ingredients;
        }
    }

    public static Dictionary<string, int> Beef
    {
        get
        {
            Dictionary<string, int> ingredients = new Dictionary<string, int>();
            ingredients["beet"] = 3;
            ingredients["carrot"] = 2;
            return ingredients;
        }
    }

    public static Dictionary<string, int> Loaf
    {
        get
        {
            Dictionary<string, int> ingredients = new Dictionary<string, int>();
            ingredients["brocoli"] = 1;
            ingredients["caper"] = 1;
            ingredients["mushroom"] = 3;
            return ingredients;
        }
    }

    public static Dictionary<string, int> Salad
    {
        get
        {
            Dictionary<string, int> ingredients = new Dictionary<string, int>();
            ingredients["brocoli"] = 3;
            ingredients["caper"] = 2;
            ingredients["mushroom"] = 2;
            return ingredients;
        }
    }

    public static Dictionary<string, int> Sphagetti
    {
        get
        {
            Dictionary<string, int> ingredients = new Dictionary<string, int>();
            ingredients["corn"] = 1;
            ingredients["mushroom"] = 1;
            ingredients["tomato"] = 2;
            return ingredients;
        }
    }

    public static Dictionary<string, int> Sandwich
    {
        get
        {
            Dictionary<string, int> ingredients = new Dictionary<string, int>();
            ingredients["brocoli"] = 2;
            ingredients["onion"] = 2;
            ingredients["tomato"] = 3;
            return ingredients;
        }
    }

    public static Dictionary<string, int> Cookie
    {
        get
        {
            Dictionary<string, int> ingredients = new Dictionary<string, int>();
            ingredients["caper"] = 3;
            ingredients["eggplant"] = 3;
            ingredients["onion"] = 1;
            return ingredients;
        }
    }

    public static Dictionary<string, int> Muffin
    {
        get
        {
            Dictionary<string, int> ingredients = new Dictionary<string, int>();
            ingredients["corn"] = 4;
            ingredients["pumpkin"] = 3;
            return ingredients;
        }
    }

    public static Dictionary<string, int> Chips
    {
        get
        {
            Dictionary<string, int> ingredients = new Dictionary<string, int>();
            ingredients["eggplant"] = 4;
            ingredients["onion"] = 3;
            ingredients["brocoli"] = 4;
            return ingredients;
        }
    }

    public static Dictionary<string, int> Hotdog
    {
        get
        {
            Dictionary<string, int> ingredients = new Dictionary<string, int>();
            ingredients["beet"] = 1;
            ingredients["mushroom"] = 4;
            ingredients["corn"] = 2;
            return ingredients;
        }
    }



}
