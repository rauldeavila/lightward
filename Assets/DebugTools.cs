using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

public class DebugTools : MonoBehaviour
{

    [Button]
    public void SelectCamera()
    {
        GameObject cameraSystem = FindObjectOfType<CameraSystem>().gameObject;
        if (cameraSystem != null)
        {
            // Select the player GameObject
            Selection.activeGameObject = cameraSystem;
            EditorGUIUtility.PingObject(cameraSystem);
            Debug.Log("Selected Camera.");
        }
        else
        {
            Debug.LogWarning("Camera not found in the scene.");
        }
    }
    public void SelectHero()
    {
        GameObject playerController = FindObjectOfType<PlayerController>().gameObject;
        if (playerController != null)
        {
            // Select the player GameObject
            Selection.activeGameObject = playerController;
            EditorGUIUtility.PingObject(playerController);
            Debug.Log("Selected Hero.");
        }
        else
        {
            Debug.LogWarning("Hero not found in the scene.");
        }
    }

}
