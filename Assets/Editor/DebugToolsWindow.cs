using UnityEngine;
using UnityEditor;
using System.Linq;

public class DebugToolsWindow : EditorWindow
{
    private Texture2D heroIcon;
    private Texture2D camToHero;
    private Texture2D cam;
    private Vector2 scrollPosition;
    private DebugTools debugTools;

    [MenuItem("Window/Debug Tools")]
    public static void ShowWindow()
    {
        GetWindow<DebugToolsWindow>("Debug Tools");
    }

    private void OnEnable()
    {
        // Load custom icon
        heroIcon = Resources.Load<Texture2D>("Debug/hero_icon");
        camToHero = Resources.Load<Texture2D>("Debug/cam_to_hero");
        cam = Resources.Load<Texture2D>("Debug/cam");

        // Find the DebugTools component in the scene
        debugTools = FindObjectOfType<DebugTools>();
        if (debugTools == null)
        {
            Debug.LogWarning("DebugTools component not found in the scene.");
        }
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Toggle New Game", CreateButtonStyle("111111")))
        {
            SwitchRealms();
        }
        // GUILayout.Label("Debug Tools", EditorStyles.boldLabel);
        if (GUILayout.Button(new GUIContent("Select Hero", heroIcon), CreateButtonStyle("00FF00")))
        {
            SelectHero();
        }
        if (GUILayout.Button(new GUIContent("Select Camera", cam), CreateButtonStyle("FF0000")))
        {
            SelectCamera();
        }
        if (GUILayout.Button(new GUIContent("CAM->Hero", camToHero), CreateButtonStyle("08ACB5")))
        {
            MoveCameraToPlayer();
        }
        // GUILayout.Space(10); // Add some space between the buttons
        if (GUILayout.Button("Center Selected", CreateButtonStyle("E7510A")))
        {
            MoveSelectedObjectToCenter();
        }
        // GUILayout.Space(10); // Add some space between the buttons
        if (GUILayout.Button("Switch Realms ↑↓↑↓SELECT", CreateButtonStyle("0E9C9A")))
        {
            SwitchRealms();
        }
        if (GUILayout.Button("No Clip ←→↑↓SELECT", CreateButtonStyle("FFD700")))
        {
            ToggleNoClip();
        }

        GUILayout.Space(20); // Add some space before the asset list
        GUILayout.Label("Prefabs", EditorStyles.boldLabel);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(200));
        DisplayAssetsFromFolder("Assets/Resources/_prefabs");
        GUILayout.EndScrollView();
    }

    private GUIStyle CreateButtonStyle(string hexColor)
    {
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
        {
            normal = { textColor = HexToColor(hexColor), background = MakeTex(2, 2, HexToColor("000000")) },
            imagePosition = ImagePosition.ImageAbove,
            fontStyle = FontStyle.Bold
            // fixedWidth = 150,  // Adjust the size as needed
            // fixedHeight = 100  // Adjust the size as needed
        };
        return buttonStyle;
    }

    private Color HexToColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString($"#{hex}", out Color color))
        {
            return color;
        }
        else
        {
            Debug.LogError("Invalid hex color string");
            return Color.white;
        }
    }

    private void SelectCamera()
    {
        DebugTools debug = FindObjectOfType<DebugTools>();
        if (debug != null)
        {
            debug.SelectCamera();
        }
        else
        {
            Debug.LogWarning("DebugTools component not found in the scene.");
        }
    }

    private void SelectHero()
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

    private void MoveCameraToPlayer()
    {
        CameraSystem cameraSystem = FindObjectOfType<CameraSystem>();
        cameraSystem.MoveCameraToPlayer();
    }

    private void MoveSelectedObjectToCenter()
    {
        DebugTools debug = FindObjectOfType<DebugTools>();
        debug.MoveSelectedObjectToCenter();

    }

    private void SwitchRealms()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.InstantShadowWalk();
    }
    private void ToggleNoClip()
    {
        Move moveScript = FindObjectOfType<Move>();
        moveScript.NoClip();
    }

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; i++)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }

    private void DisplayAssetsFromFolder(string folderPath)
    {
        string[] assetGUIDs = AssetDatabase.FindAssets("", new[] { folderPath });
        foreach (string guid in assetGUIDs)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Object asset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);

            if (asset != null)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.ObjectField(asset, typeof(Object), false);
                if (GUILayout.Button("Add to Scene"))
                {
                    AddAssetToScene(assetPath);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }

    private void AddAssetToScene(string assetPath)
    {
        Object asset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
        if (asset is GameObject)
        {
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(asset);
            Undo.RegisterCreatedObjectUndo(instance, "Add " + instance.name);
            
            // Move the newly instantiated object to the center of the Scene View
            if (debugTools != null)
            {
                debugTools.MoveToSceneViewCenter(instance);
            }
        }
    }
}
