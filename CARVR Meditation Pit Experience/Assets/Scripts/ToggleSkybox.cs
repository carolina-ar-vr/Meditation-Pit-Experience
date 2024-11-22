using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSkybox : MonoBehaviour
{
    public Material skyboxOptionDay; // Day
    public Material skyboxOptionNight; // Night

    // Call this method when the button is toggled
    public void ToggleSkyboxMaterial(bool isChecked)
    {
        if (isChecked)
        {
            RenderSettings.skybox = skyboxOptionNight;
        }
        else
        {
            RenderSettings.skybox = skyboxOptionDay;
        }

        DynamicGI.UpdateEnvironment(); // Optional if lighting depends on the skybox
    }
    
}
