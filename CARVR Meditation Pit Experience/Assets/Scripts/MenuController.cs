using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject posesMenu;
    public GameObject background;
    public TMPro.TextMeshProUGUI headerText;

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        posesMenu.SetActive(false);
        headerText.text = "Main Menu";
    }
    public void ShowSettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        posesMenu.SetActive(false);
        headerText.text = "Settings";
    }
    public void ShowPosesMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        posesMenu.SetActive(true);
        headerText.text = "Poses";
    }

    public void HideMenuAfterPoseSelection()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        posesMenu.SetActive(false);
        background.SetActive(false);
    }
    public void OnPoseModelClick()
    {
        ShowPosesMenu();
        background.SetActive(true);
    }
}