using UnityEngine;

public class PanelToggle : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    public void TogglePanel()
    {
        if (panel == null)
        {
            Debug.LogWarning("Panel reference is missing.");
            return;
        }

        panel.SetActive(!panel.activeSelf);

    }
    
}