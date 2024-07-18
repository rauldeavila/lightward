using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;

public class RoomConfigurations : MonoBehaviour 
{
    public Transform Hero;
    public bool isCurrentRoom = false;
    public RoomData Data;
    public static RoomConfigurations CurrentRoom;
    private CinemachineConfiner2D confiner;

    void Awake()
    {
        if (GetComponent<Collider2D>().bounds.Contains(Hero.position))
        {
            isCurrentRoom = true;
            CurrentRoom = this;
            SetColliderAsConfiner();
        }
    }

    void Start()
    {
        if (Data == null)
        {
            Debug.LogError("The room " + this.gameObject.name + " does not have a data object assigned.");
        }
        else
        {
            SFXController.Instance.SetWindVolume(Data.SFXWindVolume);
            SFXController.Instance.SetGardensVolume(Data.SFXGardensVolume);
            SFXController.Instance.SetThunderstormVolume(Data.SFXThunderstormVolume);
        }

        confiner = FindObjectOfType<CinemachineConfiner2D>();
        if (confiner == null)
        {
            Debug.LogError("CinemachineConfiner2D not found in the scene.");
        }
    }

    public string GetProfileName() // Called from outside (?)
    {
        return Data.AreaName;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WizHitBox"))
        {
            // When the player enters a new room, set this collider as the confiner
            HandleCurrentRoom(); 
            SetColliderAsConfiner();
        }
    }

    void HandleCurrentRoom()
    {
        if (!isCurrentRoom)
        {
            // Sets all other rooms' isCurrentRoom to false
            var allRooms = FindObjectsOfType<RoomConfigurations>();
            foreach (var room in allRooms)
            {
                room.isCurrentRoom = false;
            }
            isCurrentRoom = true;
            CurrentRoom = this; // Update the static reference to the current room
        }
    }

    void SetColliderAsConfiner()
    {
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
