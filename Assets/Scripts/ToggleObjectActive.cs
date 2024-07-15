using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObjectActive : MonoBehaviour
{
    public GameObject TheGameObject;

    public void Toggle()
    {
        if (TheGameObject != null)
        {
            if(TheGameObject.activeInHierarchy)
            {
                TheGameObject.SetActive(false);
            }
            else
            {
                TheGameObject.SetActive(true);
            }
        }
    }
}
