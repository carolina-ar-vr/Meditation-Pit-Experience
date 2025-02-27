using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkModeController : MonoBehaviour
{
    public Material lightModeSkybox;
    public Material darkModeSkybox;
    public Toggle toggleButton;

    void Start()
    {
        if (toggleButton.isOn)
        {
            RenderSettings.skybox = darkModeSkybox;
        }
        else
        {
            RenderSettings.skybox = lightModeSkybox;
        }

        toggleButton.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool darkModeOn)
    {
        if (darkModeOn)
        {
            RenderSettings.skybox = darkModeSkybox;
        }
        else
        {
            RenderSettings.skybox = lightModeSkybox;
        }

        DynamicGI.UpdateEnvironment(); // updates environment lighting
    }
}