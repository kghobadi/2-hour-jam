using System.Security.Cryptography;
using UnityEngine;
using Yarn.Unity;

public class CustomCommands : MonoBehaviour
{
    private GameManager gameManager;    
    // Drag and drop your Dialogue Runner into this variable.
    private DialogueRunner dialogueRunner;
    private ItemManager itemManager;

    public void Awake()
    {
        dialogueRunner = GetComponent<DialogueRunner>();
        gameManager = FindObjectOfType<GameManager>();
        itemManager = FindObjectOfType<ItemManager>();
        // Create a new command called 'camera_look', which looks at a target. 
        // Note how we're listing 'GameObject' as the parameter type.
        dialogueRunner.AddCommandHandler<int>(
            "addHull",     // the name of the command
            AddHull // the method to run
        );
        dialogueRunner.AddCommandHandler<int>(
            "addMorale",     // the name of the command
            AddMorale // the method to run
        );
        dialogueRunner.AddCommandHandler<int>(
            "addCredits",     // the name of the command
            AddCredits // the method to run
        );
        dialogueRunner.AddCommandHandler(
            "nextTrade",     // the name of the command
            NextTrade // the method to run
        );
        dialogueRunner.AddCommandHandler<string,bool>(
           "setGameObjectActive",     // the name of the command
           SetGameObjectActive // the method to run
       );
        dialogueRunner.AddCommandHandler<string>(
           "generateItem",     // the name of the command
           GenerateItem // the method to run
       );
        dialogueRunner.AddCommandHandler<string>(
           "tradeItem",     // the name of the command
           TradeGeneratedItem // the method to run
       );
        dialogueRunner.AddCommandHandler<string>(
           "addTrade",     // the name of the command
           addTradeSequence // the method to run
       );
    }

    private void AddHull(int hull)
    {
        gameManager.addHull(hull);
    }

    private void AddMorale(int morale)
    {
        gameManager.addMorale(morale);
    }

    private void AddCredits(int Credits)
    {
        gameManager.addCredits(Credits);
    }
    private void NextTrade()
    {
        gameManager.NextTrade();
    }

    //TODO: addd a command that targets a particular game object by its name in the hierarchy and toggles its activity state to a boolean
    private void SetGameObjectActive(string name, bool state)
    {
        GameObject go = GameObject.Find(name);
        go.SetActive(state);
    }
    //TODO: add a command that generates an item of a given name from the item manager
    private void GenerateItem(string ItemName)
    {
        itemManager.GenerateItem(ItemName);
    }
    //TODO: ad a command that trades  a given item from the players ship and progresses dialogue
    private void TradeGeneratedItem(string itemName)
    {
        Item item = itemManager.tradeItem(itemName);
        if(item != null)
        {
            gameManager.TradeItem(item);
        }
        else
        {
            Debug.Log("No item of that name to trade!!");
        }

    }
    private void addTradeSequence(string tradeName)
    {
        //todo: add method that adds a trade to our game manager
        TradeOffer[] allChars = FindObjectsOfType<TradeOffer>(true); //true to include inactive game objects
        foreach(TradeOffer trade in allChars)
        {
            if(trade.characterName == tradeName)
            {
                gameManager.addTrade(trade);
                break;
            }
        }
    }
}