using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InventoryController : MonoBehaviour
{
    public UnityEvent CursorJustStoppedMoving;
    private bool _wasMoving = false;
    public GameObject InvFirstButton;
    public GameObject Cursor;
    public float CursorSmoothness = 0.1f;
    public float MovingScaleFactor = 0.001f; 
    public bool IsMoving = false;
    private Vector3 _cursorTargetPosition;
    private Vector3 _previousCursorPosition;
    public static InventoryController Instance;
    void Awake()
    {
        if (Instance != null && Instance != this)
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    void OnEnable()
    {
        UpdateStatsOpenedState();
    }
    void OnDisable()
    {
        UpdateStatsOpenedState();
    }
    void Update()
    {
        GameObject selectedObj = EventSystem.current.currentSelectedGameObject;
        if (selectedObj != null)
        {
            _cursorTargetPosition = selectedObj.transform.position;
        }

        // Scaling Logic (Updated)
        Vector3 targetScale = Vector3.one; 
        if ((Cursor.transform.position - _cursorTargetPosition).magnitude > 15f) 
        {
            targetScale *= MovingScaleFactor;
        } 
        Cursor.transform.localScale = Vector3.Lerp(Cursor.transform.localScale, targetScale, 0.1f);

        // Movement
        Vector3 currentCursorPosition = Cursor.transform.position; // Get current position
        Cursor.transform.position = Vector3.Lerp(currentCursorPosition, _cursorTargetPosition, CursorSmoothness);

        // Determine IsMoving
        IsMoving = Vector3.Distance(currentCursorPosition, _previousCursorPosition) > 5f; 
        if(_wasMoving && !IsMoving)
        {
            CursorJustStoppedMoving?.Invoke();
        }

        _previousCursorPosition = currentCursorPosition; 
        _wasMoving = IsMoving;
    }
    void UpdateStatsOpenedState()
    {
        if(gameObject.activeInHierarchy)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(InvFirstButton);
        }
        GameState.Instance.InventoryOpened = gameObject.activeInHierarchy;
    }
}
