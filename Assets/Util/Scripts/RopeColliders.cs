using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeColliders : MonoBehaviour
{
    private LineRenderer rope;
    private EdgeCollider2D edgeCollider;

    Vector3 points;
    Vector2[] points2 = new Vector2[35];

    private void Start()
    {
        edgeCollider = this.gameObject.GetComponent<EdgeCollider2D>();
        rope = this.gameObject.GetComponent<LineRenderer>();

        getNewPositions();

        edgeCollider.points = points2;
    }

    private void Update()
    {
        getNewPositions();

        edgeCollider.points = points2;
    }

    void getNewPositions()
    {
        for (int i = 0; i < rope.positionCount; i++)
        {
            points = rope.GetPosition(i);
            points2[i] = new Vector2(points.x, points.y);
        }
    }
}