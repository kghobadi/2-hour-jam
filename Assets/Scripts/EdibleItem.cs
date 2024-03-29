using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdibleItem : Item
{
    protected override void OnMouseDown()
    {
        //base gets called first
        base.OnMouseDown();
    }
}
