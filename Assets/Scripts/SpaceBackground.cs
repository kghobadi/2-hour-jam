using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Placed on SpaceBackground groups.
/// Automatically updates name on Sprite placement.
/// </summary>
public class SpaceBackground : MonoBehaviour
{
    [SerializeField]private SpriteRenderer spriteRenderer;
    private Sprite lastSprite;

    [SerializeField]
    private string baseName = "SpaceBackground ";

#if UNITY_EDITOR
    void OnValidate()
    {
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if(spriteRenderer.sprite != lastSprite)
        {
            lastSprite = spriteRenderer.sprite; 
            UpdateGameObjName();
        }
    }
#endif

    void UpdateGameObjName()
    {
        gameObject.name = baseName + lastSprite.name;
    }
}
