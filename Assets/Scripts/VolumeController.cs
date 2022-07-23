using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] string audioParametr;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider slider;
    [SerializeField] float valueMultipler;

    void Awake()
    {
        slider.onValueChanged.AddListener(SliderController);
        slider.minValue = 0.0001f;
        slider.value = PlayerPrefs.GetFloat(audioParametr, slider.value);
    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat(audioParametr, slider.value);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SliderController(float value)
    {
        audioMixer.SetFloat(audioParametr, Mathf.Log10(value) * valueMultipler);
    }
}
