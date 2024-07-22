using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

public class DebugTools : MonoBehaviour
{

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

    public void MoveToSceneViewCenter(GameObject target)
    {
        SceneView sceneView = SceneView.lastActiveSceneView;
        if (sceneView != null && target != null)
        {
            Vector3 sceneViewCenter = sceneView.camera.transform.position + sceneView.camera.transform.forward * 5f; // Adjust the 5f multiplier as needed for distance
            target.transform.position = new Vector3(sceneViewCenter.x, sceneViewCenter.y, target.transform.position.z);
            Debug.Log("Moved object to center of Scene View.");
        }
        else
        {
            Debug.LogWarning("SceneView or target object is null.");
        }
    }


    public void MoveSelectedObjectToCenter()
    {
        if (Selection.activeGameObject != null)
        {
            MoveToSceneViewCenter(Selection.activeGameObject);
        }
        else
        {
            Debug.LogWarning("No GameObject selected in the Hierarchy.");
        }
    }
    
    public void CenterToObject(GameObject target)
    {
        SceneView.lastActiveSceneView.pivot = target.transform.position;
        SelectCamera();
        MoveToSceneViewCenter(FindObjectOfType<CameraSystem>().gameObject);
        SceneView.lastActiveSceneView.Repaint();
        Debug.Log("Centered view to " + target.name);
    }

}
