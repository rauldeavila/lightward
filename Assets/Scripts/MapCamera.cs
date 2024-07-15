using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;


public class MapCamera : MonoBehaviour
{
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public GameObject mapGameObject; // Drag your map GameObject here in the inspector
    private Transform _wiz;
    private Texture2D mapTexture;
    private Texture2D editableMapTexture;  // New editable texture
    private Vector2 lastPosition;  

    private bool _flag = false;

    private HeatMapData heatMapData;


    void Awake(){
        _wiz =  FindObjectOfType<PlayerController>().transform;
        SpriteRenderer mapSpriteRenderer = mapGameObject.GetComponent<SpriteRenderer>();

        heatMapData = Resources.Load<HeatMapData>("ScriptableObjects/Heatmap/HeatMapDataObject");
        if (heatMapData == null) {
            Debug.LogError("Failed to load HeatMapDataObject from Resources.");
        }
        
        // Create a duplicate texture and make it editable
        mapTexture = mapSpriteRenderer.sprite.texture;
        editableMapTexture = new Texture2D(mapTexture.width, mapTexture.height);
        editableMapTexture.SetPixels(mapTexture.GetPixels());
        editableMapTexture.Apply();
        
        // Get the desktop path
        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);

        // Construct the file path
        string filePath = Path.Combine(desktopPath, "wiz_heatmap.png");

        // Check if file exists and load the existing texture
        if (File.Exists(filePath)) {
            byte[] fileData = File.ReadAllBytes(filePath);
            editableMapTexture.LoadImage(fileData);
        }

        // Subscribe to quitting events
        Application.quitting += OnApplicationQuitting;
    }

    public void ResetEditableMapTexture() {
        editableMapTexture.SetPixels(mapTexture.GetPixels());
        editableMapTexture.Apply();
    }

    void SaveHeatmapToFile() {
        // Get the desktop path
        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);

        // Construct the file path
        string filePath = Path.Combine(desktopPath, "wiz_heatmap.png");

        // Save the texture to the file
        byte[] bytes = editableMapTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes(filePath, bytes);
    }


    private void OnApplicationQuitting() {
        SaveHeatmapToFile();
    }

    private void OnDestroy() {
        SaveHeatmapToFile();
        Application.quitting -= OnApplicationQuitting;
    }

    public void BrandNewMap() {
        // Reset the texture
        ResetEditableMapTexture();
        // Get the desktop path
        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);

        // Construct the file path
        string filePath = Path.Combine(desktopPath, "wiz_heatmap.png");

        // Check if the file exists
        if (File.Exists(filePath)) {
            // Refresh the file with the placeholder texture
            byte[] bytes = mapTexture.EncodeToPNG();
            File.WriteAllBytes(filePath, bytes);
        }
    }

    void Update(){
        if(!_flag && PlayerController.Instance.AnimatorIsPlaying("lay")){
            _flag = true;
            BrandNewMap();
        }
    }

    void LateUpdate() {
        // Camera logic
        Vector3 desiredPosition = _wiz.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        smoothedPosition.z = transform.position.z;
        transform.position = smoothedPosition;

        // Get the map's position to adjust Wiz's position relative to it
        Vector3 mapPosition = mapGameObject.transform.position;

        // Adjust the coordinates for Wiz based on the map's position
        int x = Mathf.FloorToInt(_wiz.position.x - mapPosition.x + editableMapTexture.width / 2);
        int y = Mathf.FloorToInt(_wiz.position.y - mapPosition.y + editableMapTexture.height / 2);

        // Create a string key for the coordinates
        string key = x + "," + y;

        // Check if Wiz has moved to a new position
        if (lastPosition.x != x || lastPosition.y != y) {
            
            // Update the frequency count
            if (!heatMapData.heatMapFrequency.ContainsKey(key)) {
                heatMapData.heatMapFrequency[key] = 0;
            }
            heatMapData.heatMapFrequency[key]++;
            
            // Store the new position
            lastPosition = new Vector2(x, y);
        }

        if (x >= 0 && x < editableMapTexture.width && y >= 0 && y < editableMapTexture.height) {
            Color targetColor = new Color(1.0f, 1.0f, 0.0f); // Default color, which can be anything

            int frequency = heatMapData.heatMapFrequency[key];
            
            if (frequency == 1) {
                targetColor = new Color(1.0f, 0.91f, 0.77f, 0.25f); // #ffe9c5
            } else if (frequency == 2) {
                targetColor = new Color(1.0f, 0.91f, 0.77f, 0.5f); // #ffe9c5
            } else if (frequency == 3) {
                targetColor = new Color(1.0f, 0.91f, 0.77f); // #ffe9c5
            } else if (frequency == 4) {
                targetColor = new Color(0.97f, 0.97f, 0.53f); // #f8f888
            } else if (frequency == 5) {
                targetColor = new Color(1.0f, 0.9f, 0.22f); // #ffe737
            } else if (frequency == 6) {
                targetColor = new Color(0.97f, 0.69f, 0.19f); // #f8b030
            } else if (frequency == 7) {
                targetColor = new Color(0.81f, 0.24f, 0.44f); // #cf3c71
            } else if (frequency == 8) {
                targetColor = new Color(0.93f, 0.09f, 0.23f); // #ed163b
            } else if (frequency >= 9) {
                targetColor = new Color(0.97f, 0.03f, 0.16f); // #f80828
            }

            editableMapTexture.SetPixel(x, y, targetColor);
            editableMapTexture.Apply();
        }
    }

}
