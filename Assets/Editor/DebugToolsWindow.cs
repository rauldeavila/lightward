using UnityEngine;
using UnityEditor;

public class DebugToolsWindow : EditorWindow
{
    private Texture2D heroIcon;
    private Texture2D camToHero;
    private Texture2D cam;

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
    }

    private void OnGUI()
    {
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
        GUILayout.Space(10); // Add some space between the buttons
        if (GUILayout.Button("Center Selected", CreateButtonStyle("E7510A")))
        {
            MoveSelectedObjectToCenter();
        }
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
        if (cameraSystem != null)
        {
            cameraSystem.MoveCameraToPlayer();
        }
        else
        {
            Debug.LogWarning("CameraSystem component not found in the scene.");
        }
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

    private void MoveSelectedObjectToCenter()
    {
        DebugTools debug = FindObjectOfType<DebugTools>();
        if (debug != null)
        {
            debug.MoveSelectedObjectToCenter();
        }
        else
        {
            Debug.LogWarning("DebugTools component not found in the scene.");
        }
    }
}
