using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadrantController : MonoBehaviour
{
    public Collider2D A;
    public Collider2D B;
    public Collider2D C;
    public Collider2D D;
    public Collider2D E;
    public Collider2D F;
    public Collider2D G;
    public Collider2D H;
    public Collider2D I;

    public static QuadrantController Instance;

    private string currentQuadrant;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public string GetCurrentQuadrant()
    {
        return currentQuadrant;
    }

    public void SetCurrentQuadrant(string newQuadrant)
    {
        // Debug.Log("New quadrant: " + newQuadrant);
        currentQuadrant = newQuadrant;
    }
}
