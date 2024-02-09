using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMgr : MonoBehaviour
{
    GameManager gameManager;
    public int currentBackground;

    private void Awake()
    {
        //get game mgr
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        //subscribe to event
        gameManager.onMissionBegin.AddListener(SetRandomSpaceBackground);
    }

    private void OnDisable()
    {
        //unsubscribe to event
        gameManager.onMissionBegin.RemoveListener(SetRandomSpaceBackground);
    }

    /// <summary>
    /// Sets random child of All Space backgrounds game object active.
    /// </summary>
    void SetRandomSpaceBackground()
    {
        //Disable current background if there is one. 
        if (currentBackground >= 0)
        {
            transform.GetChild(currentBackground).gameObject.SetActive(false);
        }

        //reset current background 
        currentBackground = UnityEngine.Random.Range(0, transform.childCount);

        //enable new one 
        transform.GetChild(currentBackground).gameObject.SetActive(true);
    }
}
