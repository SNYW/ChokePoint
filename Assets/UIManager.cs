using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject selectedUnitPanel;

    void Start()
    {
        ResetUI();
    }

    void Update()
    {
        ManagePanels();
    }

    public void ResetUI()
    {
        selectedUnitPanel.SetActive(false);
    }

    private void ManagePanels()
    {
        ManageSelectedUnitPanel();
    }

    private void ManageSelectedUnitPanel()
    {
        if(GameManager.gm.RTSMode && GameManager.gm.selectedUnit != null)
        {
            selectedUnitPanel.SetActive(true);
        }
        else
        {
            selectedUnitPanel.SetActive(false);
        }
    }
}
