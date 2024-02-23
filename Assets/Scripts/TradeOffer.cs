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
    public TradeType tradeType;
    //what are the data values this trade uses as effects?
    //Could be positive or negative numbers. 
    //Include inverse setting - when we deny a trade, the inverse effect happens. 
    public ShipResources resourceExchange;
    public ShipResources DenyExchange;
    public ShipResources NeutralExchange;
    public TextAsset exchangeMessage;
}

