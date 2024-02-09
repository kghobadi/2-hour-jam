using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public struct ShipResources
{
    //resources
    public int hullHP;
    public int crewMorale;
    public int credits;
}

/// <summary>
/// A Game Manager 'manages' the overall game state. 
/// In this case, it will keep track of what kind of characters/trade deals spawn each day.
/// It will pass time, and plug into resource collection/tracking. 
/// </summary>
public class GameManager : MonoBehaviour
{
    //reference to all trade offers
    public TradeOffer[] allTradeOffers;
    //this stores potential trades for a mission
    private List<TradeOffer> potentialTrades = new List<TradeOffer>();

    [Tooltip("Tracks my ship resources")]
    public ShipResources shipResources;

    [Tooltip("Range of mission trades")]
    public Vector2 missionTradeAmtRange = new Vector2(3, 6);

    public TradeOffer[] missionTrades;
    public int currentTradeIndex; //indexes current trade within a mission 
    public TradeOffer currentTrade; // the actual current trade offer
    public int currentMission; // tracks missions

    [Header("UI References")]
    public TMP_Text tradeText;

    public TMP_Text hpText;
    public string hpDefault = "HullHP: ";
    public TMP_Text moraleText;
    public string moraleDefault = "Crew Morale: ";
    public TMP_Text creditsText;
    public string creditsDefault = "Credits: ";

    private void Awake()
    {
        //grab all trade offers in the scene 
        allTradeOffers = FindObjectsOfType<TradeOffer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentMission = 0;
        BeginMission();
    }

    /// <summary>
    /// called each time we begin a new mission.
    /// </summary>
    void BeginMission()
    {
        //reset list to ALL trade offers
        potentialTrades.Clear();
        foreach (var trade in allTradeOffers)
        {
            potentialTrades.Add(trade);
            trade.gameObject.SetActive(false);
        }

        //get our trade amt
        int randomTradeAmt = UnityEngine.Random.Range((int)missionTradeAmtRange.x, (int)missionTradeAmtRange.y);

        //reset mission trades to random amt
        missionTrades = new TradeOffer[randomTradeAmt];

        //looping through mission trades and filling with potential trades
        for (int i = 0; i < missionTrades.Length; i++)
        {
            //get random potential trade index
            int randomPotentialTrade = UnityEngine.Random.Range(0, potentialTrades.Count);
            //set this index of mission trade to a random potential trade
            missionTrades[i] = potentialTrades[randomPotentialTrade];
            //remove the trade from potentials. 
            potentialTrades.RemoveAt(randomPotentialTrade);
        }

        //get current trade setup 
        currentTradeIndex = 0;
        SetCurrentTrade();

        //index mission counter
        currentMission++;
    }

    /// <summary>
    /// Updates current trade according to index and activates the obj.
    /// </summary>
    void SetCurrentTrade()
    {
        //deactivate 'prior' trade
        if(currentTrade != null)
        {
            currentTrade.gameObject.SetActive(false);
        }
   
        //new ones 
        currentTrade = missionTrades[currentTradeIndex];
        currentTrade.gameObject.SetActive(true);
        tradeText.text = currentTrade.exchangeMessage.text;
        UpdateResourcesText();
    }

    /// <summary>
    /// Called after player inputs offer. 
    /// </summary>
    public void NextTrade()
    {
        if (currentTradeIndex < missionTrades.Length - 1)
        {
            currentTradeIndex++;
            SetCurrentTrade();
        }
        else
        {
            EndMission();
        }
    }

    void EndMission()
    {
        Debug.Log("You JUST ENDED THE MISSION!!!! nice job");
        //what should this do?
        //Make sure we can show the player they ended, what resources they got, and begin a new mission.
    }

    public void AcceptOffer()
    {
        //what do we do with the trade offer? 
        AddResources();
        NextTrade();
    }

    public void DenyOffer()
    {
        //what do we do with the trade offer? 
        RemoveResources();
        NextTrade();
    }

    public void NeutralOffer()
    {
        //what do we do with the trade offer? 
        //do nothing 
        NeutralResources();
        NextTrade();
    }

    //TODO for something like the warlord trade -- how do we do a NEGATIVE accept, and a NEGATIVE Deny? 
    //When we accept his deal, we pay him. When we deny, he hurts us. 

    void AddResources()
    {
        ResourceChanges(currentTrade.resourceExchange);
    }

    void RemoveResources()
    {
        ResourceChanges(currentTrade.DenyExchange);
    }

    void NeutralResources()
    {
        ResourceChanges(currentTrade.NeutralExchange);
    }

    void ResourceChanges(ShipResources x)
    {
        shipResources.hullHP += x.hullHP;
        shipResources.crewMorale += x.crewMorale;
        shipResources.credits += x.credits;
    }

    void UpdateResourcesText()
    {
        hpText.text = hpDefault + shipResources.hullHP;
        moraleText.text = moraleDefault + shipResources.crewMorale;
        creditsText.text = creditsDefault + shipResources.credits;
    }
}
