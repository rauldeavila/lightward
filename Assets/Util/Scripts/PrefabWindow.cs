using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

#if UNITY_EDITOR
public class PrefabWindow : EditorWindow
{   
    private const float gridSize = 1f;

    [MenuItem("Window/Prefab Window %1")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<PrefabWindow>("Prefab Window");
    }

    private Object[] prefabs;
    private Dictionary<string, string> prefabPaths;
    private string[] folderNames;
    private string[] folderPaths;
    private int selectedFolderIndex = 0;

    private void OnEnable()
    {
        prefabPaths = new Dictionary<string, string>();
        string rootPath = "Assets/Prefabs";
        GetSubFoldersRecursive(rootPath);
        folderNames = new List<string>(prefabPaths.Keys).ToArray();
        folderPaths = new List<string>(prefabPaths.Values).ToArray();

        LoadPrefabs(rootPath);
    }

    private void LoadPrefabs(string path)
    {
        string[] prefabGUIDs = AssetDatabase.FindAssets("t:Prefab", new string[] { path });
        prefabs = new Object[prefabGUIDs.Length];

        for (int i = 0; i < prefabGUIDs.Length; i++)
        {
            string prefabPath = AssetDatabase.GUIDToAssetPath(prefabGUIDs[i]);
            prefabs[i] = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject));
        }
    }

    private Vector2 scrollPosition;
    private const float buttonSpacing = 20f;
    private float buttonSize = 100f;

    private void OnGUI()
    {
        selectedFolderIndex = GUILayout.Toolbar(selectedFolderIndex, folderNames);
        if (GUI.changed)
        {
            LoadPrefabs(folderPaths[selectedFolderIndex]);
        }

        buttonSize = EditorGUILayout.Slider("Button Size", buttonSize, 80f, 200f);

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        GUILayout.BeginVertical();
        int buttonsPerRow = Mathf.Max(Mathf.FloorToInt((position.width - buttonSpacing) / (buttonSize + buttonSpacing)), 1);
        int buttonCount = 0;

        GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
        labelStyle.alignment = TextAnchor.UpperCenter;
        labelStyle.wordWrap = true;

        GUILayout.BeginHorizontal();
        foreach (Object prefab in prefabs)
        {
            if (buttonCount % buttonsPerRow == 0 && buttonCount != 0)
            {
                GUILayout.EndHorizontal();
                GUILayout.Space(buttonSpacing);
                GUILayout.BeginHorizontal();
            }

            GameObject prefabObject = prefab as GameObject;
            SpriteRenderer spriteRenderer = GetSpriteRenderer(prefabObject);

            GUIContent buttonContent = new GUIContent();

            if (spriteRenderer != null && spriteRenderer.sprite != null)
            {
                buttonContent.image = spriteRenderer.sprite.texture;
            }
            else
            {
                buttonContent.text = prefab.name;
            }

            if (GUILayout.Button(buttonContent, GUILayout.Width(buttonSize), GUILayout.Height(buttonSize)))
            {
                SceneView sceneView = SceneView.lastActiveSceneView;

                if (sceneView != null)
                {
                    Vector3 center = sceneView.camera.transform.position + sceneView.camera.transform.forward * 5f;

                    center = new Vector3(
                        Mathf.Round(center.x / gridSize) * gridSize,
                        Mathf.Round(center.y / gridSize) * gridSize,
                        Mathf.Round(center.z / gridSize) * gridSize
                    );

                    GameObject prefabInstance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                    prefabInstance.transform.position = center;
                    prefabInstance.transform.SetAsLastSibling();
                }
            }

            if (Event.current.type == EventType.Repaint && GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
            {
                GUIContent tooltipContent = new GUIContent(prefab.name);
                Vector2 tooltipSize = labelStyle.CalcSize(tooltipContent);

                Rect tooltipRect = GUILayoutUtility.GetLastRect();
                tooltipRect.y += tooltipRect.height;
                tooltipRect.height = tooltipSize.y;
                GUI.Box(tooltipRect, tooltipContent, labelStyle);
            }

            GUILayout.Space(buttonSpacing);
            buttonCount++;
        }

        if (buttonCount % buttonsPerRow != 0)
        {
            GUILayout.EndHorizontal();
        }

        GUILayout.EndVertical();
        GUILayout.EndScrollView();
    }

    private SpriteRenderer GetSpriteRenderer(GameObject gameObject)
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            return spriteRenderer;
        }

        foreach (Transform child in gameObject.transform)
        {
            spriteRenderer = GetSpriteRenderer(child.gameObject);
            if (spriteRenderer != null)
            {
                return spriteRenderer;
            }
        }

        return null;
    }

    private void GetSubFoldersRecursive(string path)
    {
        string simplePathName = Path.GetFileName(path);
        prefabPaths[simplePathName] = path;

        string[] subFolders = AssetDatabase.GetSubFolders(path);
        foreach (string subFolder in subFolders)
        {
            GetSubFoldersRecursive(subFolder);
        }
    }
}
#endif
