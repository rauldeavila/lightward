using UnityEngine;
using UnityEngine.Events;

public class LanguageController : MonoBehaviour
{
    public static LanguageController Instance { get; private set; }

    public enum Language
    {
        English,
        Portuguese,
        // Add more languages here
    }

    private Language _currentLanguage;

    // Event that gets triggered when language changes
    public UnityEvent OnLanguageChanged;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        LoadLanguage();
    }

    void LoadLanguage()
    {
        int langCode = PlayerPrefs.GetInt("Language", 0); // Default to English
        _currentLanguage = (Language)langCode;
        OnLanguageChanged?.Invoke(); // Invoke the event
    }

    public void SetLanguage(Language newLanguage)
    {
        if (_currentLanguage != newLanguage)
        {
            _currentLanguage = newLanguage;
            PlayerPrefs.SetInt("Language", (int)_currentLanguage);
            PlayerPrefs.Save();
            OnLanguageChanged?.Invoke(); // Invoke the event
        }
    }

    public Language GetCurrentLanguage()
    {
        return _currentLanguage;
    }
}