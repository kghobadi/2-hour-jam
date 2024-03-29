using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdibleItem : Item
{
    private PlayerInputs playerInputs;
    public AudioClip eatingSound;

    protected override void Start()
    {
        base.Start();
        playerInputs = FindObjectOfType<PlayerInputs>();
    }

    protected override void OnMouseDown()
    {
        //base gets called first
        base.OnMouseDown();
    }
    protected override void PlayerInteract()
    {
        base.PlayerInteract();
        playerInputs.PlaySound(eatingSound);
        ItemMgr.RemoveGenItem(this);
    }

}
