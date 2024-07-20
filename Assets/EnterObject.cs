using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterObject : MonoBehaviour
{
    public void ToggleActive()
    {
        GameState.Instance.InsideBuilding = !GameState.Instance.InsideBuilding;
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }
}
