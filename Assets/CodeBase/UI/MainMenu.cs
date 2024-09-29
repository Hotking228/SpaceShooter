using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject levelSelectionPanel;
    [SerializeField] private GameObject shipSelectionPanel;
    [SerializeField] private GameObject mainSelectionPanel;
    [SerializeField] private GameObject m_MainMenu;

    public void ShowShipSelectionPanel()
    {
        shipSelectionPanel.SetActive(true);
        mainSelectionPanel.SetActive(false);
    }

    public void ShowLevelSelection()
    {
        levelSelectionPanel.SetActive(true);
        mainSelectionPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowMainPanel()
    {
        m_MainMenu.SetActive(true);
        shipSelectionPanel.SetActive(false);
        levelSelectionPanel.SetActive(false);
    }

    private void Start()
    {
        ShowMainPanel();
    }
}
