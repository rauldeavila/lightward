using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public List<GameObject> L1Objects = new List<GameObject>();
    public List<GameObject> L2Objects = new List<GameObject>();
    public List<GameObject> L3Objects = new List<GameObject>();

    public GameObject ItemHolder;
    public GameObject MagicHolder;

    void Start()
    {
        float maxHealth = PlayerStats.Instance.GetMaxHealth();

        SetActiveGroup(L1Objects, maxHealth == 40f);
        SetActiveGroup(L2Objects, maxHealth == 50f);
        SetActiveGroup(L3Objects, maxHealth == 60f);
        
        if (maxHealth == 50f) // L2
        {
            OffsetRectTransform(ItemHolder, 50f);
            OffsetRectTransform(MagicHolder, 50f);
        } 
        else if (maxHealth == 60f) // L3
        {
            OffsetRectTransform(ItemHolder, 100f);
            OffsetRectTransform(MagicHolder, 100f);
        }
    }

    private void SetActiveGroup(List<GameObject> objectList, bool activate)
    {
        foreach (GameObject obj in objectList)
        {
            obj.SetActive(activate);
        }
    }

    private void OffsetRectTransform(GameObject gameObject, float offsetX)
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            Vector3 position = rectTransform.position;
            position.x += offsetX;
            rectTransform.position = position;
        }
    }
}