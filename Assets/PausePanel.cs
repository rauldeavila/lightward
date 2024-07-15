using UnityEngine;

public class PausePanel : MonoBehaviour
{
    public GameObject basePanel; // Assign the BasePanel in the inspector
    public GameObject[] subpanels; // Assign Subpanel, Subpanel2, etc. in the inspector

    void OnEnable()
    {
        ResetPanels(); // Reset the panel states when the pause menu is enabled
        if(ShowReturnPrompt.Instance != null)
        {
            ShowReturnPrompt.Instance.HideReturnPrompt();
        }
        if(ShowRebindPrompt.Instance != null)
        {
            ShowRebindPrompt.Instance.HideRebindPrompt();
        }
    }

    void ResetPanels()
    {
        // Activate the base panel
        basePanel.SetActive(true);

        // Deactivate all subpanels
        foreach (var panel in subpanels)
        {
            panel.SetActive(false);
        }
    }
}
