using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterObject : MonoBehaviour
{
    public void ToggleActive()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }
}
