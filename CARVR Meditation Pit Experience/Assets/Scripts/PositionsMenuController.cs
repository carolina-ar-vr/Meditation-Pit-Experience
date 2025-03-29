using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject positionsMenu;
    public TMPro.TextMeshProUGUI headerText;

    public void ShowPositionsMenu()
    {
        settingsMenu.SetActive(false);
        positionsMenu.SetActive(true);
        headerText.text = "Positions";
    }

    public void ShowSettingsMenu()
    {
        positionsMenu.SetActive(false);
        settingsMenu.SetActive(true);
        headerText.text = "Settings";
    }
}