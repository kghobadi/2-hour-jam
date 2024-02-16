using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

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
    //tracks the initial resources at the start of a mission
    private ShipResources iterationResourceStart;

    [SerializeField]
    private ShipResources initialResources;

    [Tooltip("Tracks my ship resources")]
    public ShipResources shipResources;

    [Tooltip("Range of mission trades")]
    public Vector2 missionTradeAmtRange = new Vector2(3, 6);

    public TradeOffer[] missionTrades;
    public int currentTradeIndex; //indexes current trade within a mission 
    public TradeOffer currentTrade; // the actual current trade offer
    public int currentMission; // tracks missions

    [Tooltip("Lets other scripts know when a mission begins.")]
    public UnityEvent onMissionBegin;
    //add events like the above for next trade and end mission

    [Header("UI References")]
    public TMP_Text tradeText;

    public TMP_Text hpText;
    public string hpDefault = "HullHP: ";
    public TMP_Text moraleText;
    public string moraleDefault = "Crew Morale: ";
    public TMP_Text creditsText;
    public string creditsDefault = "Credits: ";

    public GameObject MissionRecapScreen;
    public TMP_Text MissionRecapText;
    public TMP_Text SpaceToProgressText;

    public GameObject MissionFailedScreen;
    public TMP_Text MissionFailedText;
    public TMP_Text MissionFailedPleaseContinue;


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
        MissionRecapScreen.SetActive(false);
        MissionFailedScreen.SetActive(false);
        iterationResourceStart.credits = shipResources.credits;
        iterationResourceStart.crewMorale = shipResources.crewMorale;
        iterationResourceStart.hullHP = shipResources.hullHP;
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

        //Invoke mission begin
        onMissionBegin.Invoke();
    }

    /// <summary>
    /// Updates current trade according to index and activates the obj.
    /// </summary>
    void SetCurrentTrade()
    {
        //deactivate 'prior' trade
        if (currentTrade != null)
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
        //check for failure conditions. if hull hp = 0, credits = 0, morale = 0
        if (shipResources.hullHP <= 0)
        {
            string failedText = "OH NO! You ran out of hull. Have fun swimming home in space!";
            missionFailure(failedText);
        }
        else if (shipResources.credits <= 0)
        {
            string failedText = "OH NO! You ran out of money. I hear McSpaceRonalds is hiring for 2 credits an hour!";
            missionFailure(failedText);
        }
        else if (shipResources.crewMorale <= 0)
        {
            string failedText = "OH NO! Your crew has no morale and mutinied. Should've given them more time off or a second lunch break.";
            missionFailure(failedText);
        }
        else
        {
            //what should this do?
            //Make sure we can show the player they ended, what resources they got, and begin a new mission.
            StartCoroutine(MissonEnds());
        }

    }

    void missionFailure(string failedText)
    {
        MissionFailedPleaseContinue.gameObject.SetActive(false);
        MissionFailedText.text = failedText;
        MissionFailedScreen.SetActive(true);
        StartCoroutine(MissionFailure());
        /*create an ienumerator like the mission ends that resets the game
         * TODO: get/set the base values so when the game restarts the values reset.
         *TODO: create the coroutine method
         */
    }

    IEnumerator MissonEnds()
    {
        SpaceToProgressText.gameObject.SetActive(false);
        //end event?
        Debug.Log("You JUST ENDED THE MISSION!!!! nice job");

        //want to add a box that has the summary of details and continue button
        int missionHullChanges = shipResources.hullHP - iterationResourceStart.hullHP;
        int missionMoraleChanges = shipResources.crewMorale - iterationResourceStart.crewMorale;
        int missionCreditsChanges = shipResources.credits - iterationResourceStart.credits;

        //sets the Summary text and activates the recap screen
        string RecapMessage = "Hull: " + missionHullChanges + "\nMorale: " + missionMoraleChanges + "\nCredits: " + missionCreditsChanges;
        MissionRecapText.text = RecapMessage;

        MissionRecapScreen.SetActive(true);

        yield return new WaitForSeconds(1);
        SpaceToProgressText.gameObject.SetActive(true);

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        BeginMission();
    }

    IEnumerator MissionFailure()
    {
        yield return new WaitForSeconds(3);
        MissionFailedPleaseContinue.gameObject.SetActive(true);

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        resetGame();
    }

    public void resetGame()
    {
        currentMission = 0;
        //resets initial resources
        shipResources.hullHP = initialResources.hullHP;
        shipResources.crewMorale = initialResources.crewMorale;
        shipResources.credits = initialResources.credits;
        BeginMission();
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
