using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicPointsUpdater : MonoBehaviour
{
    private GameObject _magicPoint;
    private List<GameObject> activeMagicPoints = new List<GameObject>();

    void Start()
    {
        _magicPoint = Resources.Load<GameObject>("UI_PREFABS/magic_point");
        Magic.Instance.OnUpdateMagic.AddListener(UpdateMagicBar);
        UpdateMagicBar();
    }

    void UpdateMagicBar()
    {
        int currentMagic = (int)PlayerStats.Instance.GetCurrentMagic();
        bool gainingMagic = false;
        if(currentMagic > activeMagicPoints.Count)
        {
            gainingMagic = true;
        }

        // Gaining magic
        while (activeMagicPoints.Count < currentMagic)
        {
            float xPos = 0f + activeMagicPoints.Count;
            Vector3 spawnPosition = new Vector3(xPos, 0, 0);

            GameObject newMagicPoint = Instantiate(_magicPoint, spawnPosition, Quaternion.identity, transform);

            // Modify Rect Transform
            RectTransform rectTransform = newMagicPoint.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = spawnPosition; 
            }

            activeMagicPoints.Add(newMagicPoint);
        }
        // Losing magic
        while (activeMagicPoints.Count > currentMagic)
        {
            GameObject lastMagicPoint = activeMagicPoints[activeMagicPoints.Count - 1];
            activeMagicPoints.RemoveAt(activeMagicPoints.Count - 1);
            StartCoroutine(FadeAndDestroy(lastMagicPoint));
        }

        if(gainingMagic)
        {
            if((PlayerStats.Instance.GetCurrentMagic() <= 0f) || (PlayerStats.Instance.GetMaxMagic() - PlayerStats.Instance.GetCurrentMagic()<= 1f))
            {
                MagicMarker.Instance.GetComponent<RectTransform>().anchoredPosition = new Vector3(-300f, 0f, 0f);
            }
            else
            {
                MagicMarker.Instance.GetComponent<RectTransform>().anchoredPosition = new Vector3(activeMagicPoints.Count - 1, 0f, 0f);
            }
        }
    

    }

    IEnumerator FadeAndDestroy(GameObject magicPoint)
    {
        Image magicImage = magicPoint.GetComponent<Image>();
        if (magicImage != null)
        {
            // Fade out the image (adjust fade duration as needed)
            float fadeDuration = 1.0f;
            float startTime = Time.time;
            Color targetColor = new Color(0.7372f, 0.7372f, 0.7372f, 1f); // Replace with RGB equivalent of your hex code

            while (Time.time - startTime < fadeDuration)
            {
                float t = (Time.time - startTime) / fadeDuration;
                magicImage.color = Color.Lerp(magicImage.color, targetColor, t);
                yield return null; // Wait for the next frame update
            }
        }

        if((PlayerStats.Instance.GetCurrentMagic() <= 0f) || (PlayerStats.Instance.GetMaxMagic() - PlayerStats.Instance.GetCurrentMagic()<= 1f))
        {
            MagicMarker.Instance.GetComponent<RectTransform>().anchoredPosition = new Vector3(-300f, 0f, 0f);
        }
        else
        {
            MagicMarker.Instance.GetComponent<RectTransform>().anchoredPosition = new Vector3(activeMagicPoints.Count - 1, 0f, 0f);
        }
        Destroy(magicPoint);
    }

}