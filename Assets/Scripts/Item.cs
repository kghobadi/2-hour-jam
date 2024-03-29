using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/*
 * 
 */

/// <summary>
/// Item will:
  ///  Act as a Base class for all Items. Could be abstract, but not necessary.
 ///   Stores some data on what kind of Item it is (name, any variables for effect/usage).
 ///   May have a GiveTo command that plugs its data into Yarn?
 ///   Could allow you to click and drag to move it around the ship.
 ///  Could allow you to drag and drop onto Trade screen to give it to a character.
 ///  Could have hover/click interactions.
  ///  Could use 2d Physics for fun.
/// </summary>
public class Item : MonoBehaviour
{
    public string itemName;
    private SpriteRenderer spriteRenderer;
    protected bool IsDragging;
    protected Camera MainCamera;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        MainCamera = Camera.main;
    }

    protected virtual void Update()
    {
        if (IsDragging) { 
            Vector3 MouseWorldPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position =  new Vector3(MouseWorldPosition .x, MouseWorldPosition.y, transform.position.z);
        }
    }

    protected virtual void OnMouseDown()
    {
        //do something on mouse click
        IsDragging = true;
        Debug.Log("player dragging" + itemName);

    }
    protected virtual void OnMouseUp()
    {
        IsDragging=false;
    }
    protected virtual void OnMouseEnter()
    {
        
    }
    protected virtual void OnMouseExit()
    {
        
    }
    protected virtual void OnMouseOver()
    {
        
    }

}