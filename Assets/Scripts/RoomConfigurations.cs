using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class RoomConfigurations : MonoBehaviour 
{
    public Transform Hero;
    public bool isCurrentRoom = false;
    public RoomData Data;
    public List<GameObject> objectsToEnable;
    public static RoomConfigurations CurrentRoom;
    public static UnityEvent OnRoomChanged = new UnityEvent();
    private CinemachineConfiner2D confiner;
    private CameraSystem cameraSystem;

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
        if(objectsToEnable.Count < 2)
        {
            Debug.LogError("Watch out! The room " + gameObject.name + " has less than 2 objects enabled.");
        }
        confiner = FindObjectOfType<CinemachineConfiner2D>();
        cameraSystem = FindObjectOfType<CameraSystem>();
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
            var nextRoom = this; // The room the player is entering
            var allRooms = FindObjectsOfType<RoomConfigurations>();

            // Sets all other rooms' isCurrentRoom to false
            foreach (var room in allRooms)
            {
                room.isCurrentRoom = false;
                room.DisableRoomObjects(nextRoom.objectsToEnable);
            }

            isCurrentRoom = true;
            CurrentRoom = this; // Update the static reference to the current room
            EnableRoomObjects(); // Enable objects for the current room
            OnRoomChanged.Invoke();
        }
    }

    void SetColliderAsConfiner()
    {
        Collider2D newRoomCollider = GetComponent<Collider2D>();
        if (newRoomCollider != null && confiner != null)
        {
            if (newRoomCollider is PolygonCollider2D || newRoomCollider is CompositeCollider2D)
            {
                StartCoroutine(cameraSystem.SmoothTransitionToNewConfiner(newRoomCollider, confiner));
            }
            else
            {
                Debug.LogWarning("Collider is not a PolygonCollider2D or CompositeCollider2D: " + newRoomCollider.name);
            }
        }
    }

    void EnableRoomObjects()
    {
        foreach (var obj in objectsToEnable)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }

    void DisableRoomObjects(List<GameObject> nextRoomObjects)
    {
        foreach (var obj in objectsToEnable)
        {
            if (obj != null && !nextRoomObjects.Contains(obj))
            {
                obj.SetActive(false);
            }
        }
    }
}
