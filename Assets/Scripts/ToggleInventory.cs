using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleInventory : MonoBehaviour
{
    public GameObject TheGameObject;

    public void Toggle()
    {
        if (TheGameObject != null)
        {
            if(TheGameObject.activeInHierarchy)
            {
                TheGameObject.SetActive(false);
                PlayerController.Instance.EnablePlayerControls();
            }
            else
            {
                if(PlayerState.Instance.Grounded)
                {
                    InventoryDataController.Instance.LoadInventory();
                    PlayerController.Instance.StopWiz();
                    TheGameObject.SetActive(true);
                }
            }
        }
    }
}
