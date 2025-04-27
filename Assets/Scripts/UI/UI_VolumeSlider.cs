using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_VolumeSlider : MonoBehaviour
{
    public Slider slider;
    public string parametr;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float multiplier;



    public void SliderValue(float _value) => audioMixer.SetFloat(parametr, Mathf.Log10(_value) * multiplier);


    //public void SliderValue(float _value)
    //{
    //    Debug.Log($"SliderValue called, param: {parametr}, raw value: {_value}");

    //    float volume = Mathf.Log10(Mathf.Clamp(_value, 0.001f, 1f)) * multiplier;
    //    audioMixer.SetFloat(parametr, volume);

    //    Debug.Log($"{parametr} volume set to {volume} dB");
    //}






    public void LoadSlider(float _value)
    {
        if (_value >= 0.001f)
            slider.value = _value;
    }

}
