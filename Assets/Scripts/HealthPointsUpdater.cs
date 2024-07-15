using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPointsUpdater : MonoBehaviour
{
    private GameObject _redHealthPoint;
    private GameObject _yellowHealthPoint;
    private List<GameObject> activeHealthPoints = new List<GameObject>();
    private List<GameObject> activeYellowHealthPoints = new List<GameObject>();

    void Start()
    {
        _redHealthPoint = Resources.Load<GameObject>("UI_PREFABS/red_health_point");
        _yellowHealthPoint = Resources.Load<GameObject>("UI_PREFABS/yellow_health_point");
        Health.Instance.OnUpdateHealth.AddListener(UpdateHealthBar);
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        int currentHealth = (int)PlayerStats.Instance.GetCurrentHealth();
        int currentYellowHealth = (int)PlayerStats.Instance.GetYellowHealth();
        bool gainingHealth = false;
        bool gainingYellowHealth = false;
        if(currentHealth > activeHealthPoints.Count)
        {
            gainingHealth = true;
        }
        if(currentYellowHealth > activeYellowHealthPoints.Count)
        {
            gainingYellowHealth = true;
        }

        // Gaining red health
        while (activeHealthPoints.Count < currentHealth)
        {
            float xPos = 0f + activeHealthPoints.Count;
            Vector3 spawnPosition = new Vector3(xPos, 0, 0);

            GameObject newHealthPoint = Instantiate(_redHealthPoint, spawnPosition, Quaternion.identity, transform);

            // Modify Rect Transform
            RectTransform rectTransform = newHealthPoint.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = spawnPosition; 
            }

            activeHealthPoints.Add(newHealthPoint);
        }
        // Gaining yellow health
        while (activeYellowHealthPoints.Count < currentYellowHealth)
        {
            float xPos = activeHealthPoints.Count + activeYellowHealthPoints.Count;
            Vector3 spawnPosition = new Vector3(xPos, 0, 0);

            GameObject newHealthPoint = Instantiate(_yellowHealthPoint, spawnPosition, Quaternion.identity, transform);

            // Modify Rect Transform
            RectTransform rectTransform = newHealthPoint.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = spawnPosition; 
            }

            activeYellowHealthPoints.Add(newHealthPoint);
        }

        // Losing red health
        while (activeHealthPoints.Count > currentHealth)
        {
            GameObject lastHealthPoint = activeHealthPoints[activeHealthPoints.Count - 1];
            activeHealthPoints.RemoveAt(activeHealthPoints.Count - 1);
            StartCoroutine(FadeAndDestroy(lastHealthPoint));
        }
        // Losing yellow health
        while (activeYellowHealthPoints.Count > currentYellowHealth)
        {
            GameObject lastHealthPoint = activeYellowHealthPoints[activeYellowHealthPoints.Count - 1];
            activeYellowHealthPoints.RemoveAt(activeYellowHealthPoints.Count - 1);
            StartCoroutine(FadeAndDestroy(lastHealthPoint));
        }


        if(gainingHealth)
        {
            if((PlayerStats.Instance.GetCurrentHealth() <= 0f) || (PlayerStats.Instance.GetMaxHealth() - PlayerStats.Instance.GetCurrentHealth()<= 1f))
            {
                HealthMarker.Instance.GetComponent<RectTransform>().anchoredPosition = new Vector3(-300f, 0f, 0f);
            }
            else
            {
                HealthMarker.Instance.GetComponent<RectTransform>().anchoredPosition = new Vector3(activeHealthPoints.Count - 1, 0f, 0f);
            }
        }
    

    }

    IEnumerator FadeAndDestroy(GameObject healthPoint)
    {
        Image healthImage = healthPoint.GetComponent<Image>();
        if (healthImage != null)
        {
            // Fade out the image (adjust fade duration as needed)
            float fadeDuration = 1.0f;
            float startTime = Time.time;
            Color targetColor = new Color(0.7372f, 0.7372f, 0.7372f, 1f); // Replace with RGB equivalent of your hex code

            while (Time.time - startTime < fadeDuration)
            {
                float t = (Time.time - startTime) / fadeDuration;
                healthImage.color = Color.Lerp(healthImage.color, targetColor, t);
                yield return null; // Wait for the next frame update
            }
        }

        if((PlayerStats.Instance.GetCurrentHealth() <= 0f) || (PlayerStats.Instance.GetMaxHealth() - PlayerStats.Instance.GetCurrentHealth()<= 1f))
        {
            HealthMarker.Instance.GetComponent<RectTransform>().anchoredPosition = new Vector3(-300f, 0f, 0f);
        }
        else
        {
            HealthMarker.Instance.GetComponent<RectTransform>().anchoredPosition = new Vector3(activeHealthPoints.Count - 1, 0f, 0f);
        }
        Destroy(healthPoint);
    }

}