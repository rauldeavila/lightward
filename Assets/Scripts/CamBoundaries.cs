using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamBoundaries : MonoBehaviour
{
    private CinemachineConfiner2D confiner;

    void Start()
    {
        // Find the Cinemachine Confiner 2D component in the scene
        confiner = FindObjectOfType<CinemachineConfiner2D>();
        if (confiner == null)
        {
            Debug.LogError("CinemachineConfiner2D not found in the scene.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WizHitBox"))
        {
            // When the player enters a new room, set this collider as the confiner
            Collider2D newRoomCollider = GetComponent<Collider2D>();
            if (newRoomCollider != null && confiner != null)
            {
                if (newRoomCollider is PolygonCollider2D || newRoomCollider is CompositeCollider2D)
                {
                    confiner.m_BoundingShape2D = newRoomCollider;
                    // Debug.Log("Switched to new room confiner: " + newRoomCollider.name);
                }
                else
                {
                    Debug.LogWarning("Collider is not a PolygonCollider2D or CompositeCollider2D: " + newRoomCollider.name);
                }
            }
        }
    }
}