using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;
using System.Collections.Generic;

public class ShowReturnPrompt : MonoBehaviour
{
    [Header("Animation Settings")]
    private int FramesPerSecond = 2; 
    private bool PingPong = false; 

    public Image myImage;
    public TextMeshProUGUI myTextPrompt; 
    private Sprite[] _frames;
    private int _currentFrame;
    private float _timer;
    private bool _isReversing;
    private Color _originalColor;
    private Color _originalTextColor;
    public static ShowReturnPrompt Instance;

    private string currentControlScheme;

    private bool _isShown = false;

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

    void Start()
    {
        _currentFrame = 0;
        _timer = 0f;
        _isReversing = false;

        _originalColor = myImage.color;
        _originalTextColor = myTextPrompt.color;

        // Set initial alpha to 0%
        SetAlpha(myImage, 0f);
        SetAlpha(myTextPrompt, 0f);
        myTextPrompt.gameObject.SetActive(false);

        // Subscribe to the input change event
        InputListener.Instance.onLastUsedInputChanged.AddListener(OnInputChanged);
    }

    void Update()
    {
        if (_frames == null || _frames.Length == 0) return;

        _timer += Time.unscaledDeltaTime; // Use unscaledDeltaTime instead of deltaTime

        if (_timer >= 1f / FramesPerSecond)
        {
            _timer -= 1f / FramesPerSecond;

            if (PingPong)
            {
                if (!_isReversing)
                {
                    _currentFrame++;
                    if (_currentFrame >= _frames.Length)
                    {
                        _currentFrame = _frames.Length - 2;
                        _isReversing = true;
                    }
                }
                else
                {
                    _currentFrame--;
                    if (_currentFrame < 0)
                    {
                        _currentFrame = 1;
                        _isReversing = false;
                    }
                }
            }
            else
            {
                _currentFrame++;
                if (_currentFrame >= _frames.Length)
                {
                    _currentFrame = 0;
                }
            }

            myImage.sprite = _frames[_currentFrame];
        }
    }

    private void OnInputChanged()
    {
        if(_isShown)
        {
            DoShowReturnPrompt();
        }
    }

    [Button()]
    public void DoShowReturnPrompt()
    {
        _isShown = true;
        ShowButtonPrompt("Backspace", "return");
    }

    [Button()]
    public void HideReturnPrompt()
    {
        _isShown = false;
        HideButtonPrompt();
    }

    public void ShowButtonPrompt(string actionName, string text = null)
    {
        string currentControlScheme = InputListener.Instance.LastUsedInput;
        string binding;

        if (currentControlScheme == "Keyboard")
        {
            binding = "<Keyboard>/backspace";
        }
        else
        {
            binding = "<Gamepad>/buttonNorth";
        }

        // Determine the correct resource name based on the control scheme for displaying icons
        string resourceName;
        if (currentControlScheme == "Keyboard")
        {
            resourceName = binding.Replace("<Keyboard>/", "keyboard_");
        }
        else
        {
            resourceName = binding.Replace("<Gamepad>/", GetDevicePrefix(currentControlScheme));
        }

        resourceName = resourceName.Replace("/", "_");

        _frames = LoadSpritesWithFallback(resourceName);

        if (_frames.Length > 0)
        {
            // Debug.Log($"Loaded {_frames.Length} frames for {resourceName}");
            _currentFrame = 0;
            _timer = 0f;
            _isReversing = false;

            myImage.sprite = _frames[_currentFrame];

            // Set image alpha to 100%
            SetAlpha(myImage, 1f);
        }
        else
        {
            Debug.LogError($"No sprites found for the given resource name: {resourceName}");
        }

        myTextPrompt.gameObject.SetActive(true);
        if (text != null && text != "")
        {
            myTextPrompt.text = text;
        }
        else
        {
            myTextPrompt.text = actionName;
        }
        SetAlpha(myTextPrompt, 1f);
        myTextPrompt.ForceMeshUpdate(); // Force update
    }

    public void HideButtonPrompt()
    {
        _frames = null;
        myImage.sprite = null;

        // Set the alpha of the current image color to 0%
        SetAlpha(myImage, 0f);

        myTextPrompt.text = ""; // Clear the text
        myTextPrompt.ForceMeshUpdate(); // Force update
        SetAlpha(myTextPrompt, 0f); // Set alpha to 0%
        myTextPrompt.gameObject.SetActive(false);
    }

    private void SetAlpha(Graphic graphic, float alpha)
    {
        Color color = graphic.color;
        color.a = alpha;
        graphic.color = color;
    }

    private Sprite[] LoadSpritesWithFallback(string baseName)
    {
        List<Sprite> loadedSprites = new List<Sprite>();
        int index = 0;
        string fallbackBaseName = baseName;

        if (baseName.StartsWith("xbox_") || baseName.StartsWith("dualshock_"))
        {
            fallbackBaseName = baseName.Replace(baseName.Split('_')[0], "gamepad");
        }

        while (true)
        {
            Sprite sprite = Resources.Load<Sprite>($"Inputs/{baseName}_{index}");
            if (sprite == null)
            {
                // Try to load the gamepad version if specific version not found
                sprite = Resources.Load<Sprite>($"Inputs/{fallbackBaseName}_{index}");
                if (sprite == null)
                {
                    if (index == 0)
                    {
                        Debug.LogError($"Sprite Inputs/{baseName}_{index} and Inputs/{fallbackBaseName}_{index} not found.");
                    }
                    break;
                }
            }
            loadedSprites.Add(sprite);
            index++;
        }

        return loadedSprites.ToArray();
    }

    private string GetDevicePrefix(string deviceName)
    {
        switch (deviceName)
        {
            case "DualShock":
                return "dualshock_";
            case "Xbox":
                return "xbox_";
            case "Gamepad":
                return "gamepad_";
            default:
                return "gamepad_"; // Default to generic gamepad if no match
        }
    }
}
