using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public ShipResources resourceExchange;
    public TextAsset exchangeMessage;
}
