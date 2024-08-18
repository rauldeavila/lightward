using UnityEngine;
using UnityEngine.Events;

public class Overworld : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.OnEnterDarkworld.AddListener(DisableChildren);
        GameManager.Instance.OnExitDarkworld.AddListener(EnableChildren);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnEnterDarkworld.RemoveListener(DisableChildren);
        GameManager.Instance.OnExitDarkworld.RemoveListener(EnableChildren);
    }

    private void EnableChildren()
    {
        Invoke("EnableNow", 2.5f);
    }

    private void DisableChildren()
    {
        Invoke("DisableNow", 2.5f);
    }

    private void EnableNow()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    private void DisableNow()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}