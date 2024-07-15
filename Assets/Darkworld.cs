using UnityEngine;
using UnityEngine.Events;

public class Darkworld : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.OnEnterDarkworld.AddListener(EnableChildren);
        GameManager.Instance.OnExitDarkworld.AddListener(DisableChildren);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnEnterDarkworld.RemoveListener(EnableChildren);
        GameManager.Instance.OnExitDarkworld.RemoveListener(DisableChildren);
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