using UnityEngine;
using UnityEditor;
using System.Collections;

public static class DebugToolsEditor
{
    [MenuItem("DebugTools/Center to Object _F1")]
    public static void CenterToObjectShortcut()
    {
        DebugTools debugTools = Object.FindObjectOfType<DebugTools>();
        if (debugTools != null && Selection.activeGameObject != null)
        {
            debugTools.CenterToObject(Selection.activeGameObject);
            debugTools.SelectCamera();
            
            // Add a delay before moving the selected object to center
            EditorApplication.delayCall += () => MoveSelectedObjectToCenterWithDelay(debugTools);
        }
        else
        {
            Debug.LogWarning("No active DebugTools component or no GameObject selected in the Hierarchy.");
        }
    }

    private static void MoveSelectedObjectToCenterWithDelay(DebugTools debugTools)
    {
        debugTools.MoveSelectedObjectToCenter();
    }
}
