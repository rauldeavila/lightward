using UnityEngine;
using UnityEditor;

public class DebugToolsWindow : EditorWindow
{
    [MenuItem("Window/Debug Tools")]
    public static void ShowWindow()
    {
        GetWindow<DebugToolsWindow>("Debug Tools");
    }

    private void OnGUI()
    {
        GUILayout.Label("Debug Tools", EditorStyles.boldLabel);

        if (GUILayout.Button("Select Camera"))
        {
            DebugTools debug = FindObjectOfType<DebugTools>();
            if (debug != null)
            {
                debug.SelectCamera();
            }
            else
            {
                Debug.LogWarning("cameraSystem component not found in the scene.");
            }
        }
        if (GUILayout.Button("Select Hero"))
        {
            DebugTools debug = FindObjectOfType<DebugTools>();
            if (debug != null)
            {
                debug.SelectHero();
            }
            else
            {
                Debug.LogWarning("Hero not found in the scene.");
            }
        }

        if (GUILayout.Button("CAM->Player"))
        {
            CameraSystem cameraSystem = FindObjectOfType<CameraSystem>();
            if (cameraSystem != null)
            {
                cameraSystem.MoveCameraToPlayer();
            }
            else
            {
                Debug.LogWarning("cameraSystem component not found in the scene.");
            }
        }
    }
}
