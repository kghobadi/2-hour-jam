using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Yarn.Unity;

public class ItemManager : MonoBehaviour

/*
 * ItemManager will:

Store a list of all potential Items & their Prefabs. This may need to be a dictionary to have a 'string' name as a key that pairs with a GameObject reference.
Method for Generating Item in your Ship from the above list.
A Yarn Command which plugs into the above method.
Store a location for the Item Pile on the ship keep track of all Generated Items.
A Yarn Command from grabbing an instance of an Item from the Item Pile to give for Trades during dialogue.
 */
{
    
    public Dictionary<string, Item> ItemDictionary = new Dictionary<string, Item>();
    //This is the dictionary of all items in the game
    public List<Item> GeneratedItems = new List<Item>();
    //this is the list of generated items in the game
    public void AddGenItem(Item y)
    {

    }

    public void RemoveGenItem(Item y)
    {

    }

    [YarnCommand]
    public Item GenerateItem(string key)
    {
        return ItemDictionary[key];
    }

    [YarnCommand]
    public void tradeItem(Item y)
    {

    }
}
