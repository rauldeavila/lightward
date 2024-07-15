using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRendererChildGenerator : MonoBehaviour
{
    public Color childColor = new Color(128f / 255f, 128f / 255f, 128f / 255f, 1f); // Default color: #808080
    public Color brightestChildColor = Color.white; // Default color for the brightest child
    public Sprite[] childSprites;
    public Sprite[] brightestChildSprites;

    private SpriteRenderer parentSpriteRenderer;

    void Start()
    {
        parentSpriteRenderer = GetComponent<SpriteRenderer>();
        GenerateChild("ChildSprite", childColor, childSprites, -1); // Setting sorting order to parent - 1
        GenerateChild("BrightestChild", brightestChildColor, brightestChildSprites, 2); // Setting sorting order to parent + 1
    }

    void GenerateChild(string childName, Color color, Sprite[] sprites, int sortingOrderOffset)
    {
        // Create a child object
        GameObject childObject = new GameObject(childName);
        childObject.transform.parent = transform;

        // Set the child object's position to match the parent's position
        childObject.transform.localPosition = Vector3.zero;

        // Add a sprite renderer component to the child object
        SpriteRenderer childSpriteRenderer = childObject.AddComponent<SpriteRenderer>();

        // Set the child object's color
        childSpriteRenderer.color = color;

        // Set child sprite using its own texture
        childSpriteRenderer.sprite = sprites[0]; // Assuming first frame is at index 0

        // Set the sorting layer and order in layer for the child
        childSpriteRenderer.sortingLayerID = parentSpriteRenderer.sortingLayerID;
        childSpriteRenderer.sortingOrder = parentSpriteRenderer.sortingOrder + sortingOrderOffset; // Offset applied here
        
        if (childName == "BrightestChild")
        {
            childSpriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }
    
    }



    void LateUpdate()
    {
        // Update both child sprites based on the parent's current frame
        int frameIndex = GetCurrentFrameIndex();
        Transform childTransform = transform.Find("ChildSprite");
        Transform brightestChildTransform = transform.Find("BrightestChild");

        childTransform.GetComponent<SpriteRenderer>().sprite = childSprites[frameIndex];
        brightestChildTransform.GetComponent<SpriteRenderer>().sprite = brightestChildSprites[frameIndex];
    }

    int GetCurrentFrameIndex()
    {
        int frameIndex = 0;
        for (int i = 0; i < childSprites.Length; i++)
        {
            if (childSprites[i] == parentSpriteRenderer.sprite)
            {
                frameIndex = i;
                break;
            }
        }
        return frameIndex;
    }
}
