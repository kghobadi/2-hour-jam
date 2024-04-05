// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using System;
using System.Collections.Generic;

[Serializable]
public class ItemMemory
{
    public string itemName; //{ get; set; }
    public string Favor; //{ get; set; }
    public int Value; //{ get; set; }
}

public class Root
{
    public List<ItemMemory> ItemMemory { get; set; }
}

