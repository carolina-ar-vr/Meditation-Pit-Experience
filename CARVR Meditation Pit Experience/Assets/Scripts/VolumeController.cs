using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;
    public TextMeshProUGUI volumeLabel;
    public AudioSource backgroundMusic;

    void Start()
    {
        volumeSlider.value = 100;
        UpdateVolumeLabel(volumeSlider.value);
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    // updates volume of music + slider label
    void OnVolumeChanged(float value)
    {
        backgroundMusic.volume = value;
        UpdateVolumeLabel(value);
    }

    // updates volume label to correct percentage
    void UpdateVolumeLabel(float value)
    {
        int percentage = Mathf.RoundToInt(value * 100);
        volumeLabel.text = percentage + "%";
    }
}
