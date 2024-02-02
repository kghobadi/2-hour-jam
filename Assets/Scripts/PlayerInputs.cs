using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This will take input from the player regarding their choice in relation to trade offers.
/// It can also allow us to quit the game. 
/// </summary>
public class PlayerInputs : MonoBehaviour
{
    public GameManager manager;


    // Start is called before the first frame update
    void Start()
    {
        if(manager == null)
        {
            manager = FindObjectOfType<GameManager>();
        }
    }

    void Update()
    {
        //take yes/no/neutral input (or other way of accepting exchange). 
        if (Input.GetKeyDown(KeyCode.A))
        {
            manager.AcceptOffer();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            manager.DenyOffer();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            manager.NeutralOffer();
        }

        //Take quit input 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    /// <summary>
    /// Quits the game.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
