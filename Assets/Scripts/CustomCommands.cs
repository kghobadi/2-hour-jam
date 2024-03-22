using UnityEngine;
using Yarn.Unity;

public class CustomCommands : MonoBehaviour
{
    private GameManager gameManager;    
    // Drag and drop your Dialogue Runner into this variable.
    private DialogueRunner dialogueRunner;

    public void Awake()
    {
        dialogueRunner = GetComponent<DialogueRunner>();
        gameManager = FindObjectOfType<GameManager>();  
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
}