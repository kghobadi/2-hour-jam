//using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TradeType
{
    ATTACKER,
    REPAIR,
    SMUGGLER,
    MERCHANT,
    //add more as needed to classify your trade types easily 
}
/// <summary>
/// When we spawn a character on the space TV -- they all come with a trade offer. 
/// There are positive and negative affects of each trade offer. 
/// Depending what choice the player makes, we pass these effects to the Game Manager and apply them.
/// Most trade offers have 3 options - [positive], [negative], [neutral]. 
/// </summary>
public class TradeOffer : MonoBehaviour
{
    //what are the data values this trade uses as effects?
    //Could be positive or negative numbers. 
    //Include inverse setting - when we deny a trade, the inverse effect happens. 
    public string baseYarnNode;
    [SerializeField]
    public ItemMemory[] itemMemories;
    public string characterName;
    //public TextAsset itemMemoryJson;
    void Start()
    {
        //TODO these methods let us convert back and forth from JSON to C# - if we want to save data memories, this is how we will do it. 
        //JsonSerializer.Deserialize<ItemMemory>(jsonString);
        //string itemMemoryString = JsonSerializer.Serialize(itemMemory);
    }

    public int GetCreditValueByName(string itemname)
    {
        int val = 0;
        foreach (var item in itemMemories)
        {
            if (item.itemName == itemname)
            {
                val = item.Value;
                break;
            }
        }
        return val;
    }
}


