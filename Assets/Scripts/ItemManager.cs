using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Yarn.Unity;

[Serializable]
public struct ItemType
{
    public string Name;
    public GameObject ItemPrefab;
}
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
    public ItemType[] allItemTypes;
    public List<Item> GeneratedItems = new List<Item>();
    public Transform GenPoint;
    //this is the list of generated items in the game
    public void AddGenItem(Item y)
    {
        GeneratedItems.Add(y);
    }

    public void RemoveGenItem(Item y)
    {
        GeneratedItems.Remove(y);
        Destroy(y.gameObject);
    }

    public ItemType getItemByName(string x)
    {
        ItemType item = new ItemType();
        foreach (ItemType y in allItemTypes)
        {
            if (x == y.Name)
            {
                item = y;
                break;
            }
        }
        return item;
    }

    //TODO: find some way to randomize genpoint position within a range
    public Item GenerateItem(string key)
    {
        ItemType item = getItemByName(key);
        GameObject itemClone = Instantiate(item.ItemPrefab, GenPoint.position, Quaternion.identity, transform);
        Item itemComponent = itemClone.GetComponent<Item>();
        itemComponent.ItemMgr = this;
        AddGenItem(itemComponent);
        return itemComponent;
    }

    public void GenerateRandom()
    {
        int random = UnityEngine.Random.Range(0,allItemTypes.Length);
        GenerateItem(allItemTypes[random].Name);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            GenerateRandom();
        }
    }

    [YarnCommand]
    public void tradeItem(Item y)
    {

    }
}
