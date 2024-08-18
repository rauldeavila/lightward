using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Reflection;

public class DebugTextUpdater : MonoBehaviour
{
    public TextMeshProUGUI CamBoundaries;

    void Update()
    {
        CamBoundaries.text = "cam boundaries> " + CameraSystem.Instance.GetBoundaries();
    }
}
