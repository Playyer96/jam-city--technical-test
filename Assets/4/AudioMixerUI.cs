using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerUI : MonoBehaviour
{
    [System.Serializable]
    public class AudioMixerGroup
    {
        public string groupName;
        public Slider slider;
    }
    
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private AudioMixerGroup _masterVolumeSlider;
    [SerializeField] private AudioMixerGroup _backgroundMusicSlider;
    [SerializeField] private AudioMixerGroup _footstepsSlider;

    [SerializeField] private float _multiplier = 20f;

    private void Awake()
    {
        _masterVolumeSlider.slider.onValueChanged.AddListener(HandleMasterVolumeSliderValueChanged);
        _backgroundMusicSlider.slider.onValueChanged.AddListener(HandleBackgroundMusicSliderValueChanged);
        _footstepsSlider.slider.onValueChanged.AddListener(HandleFootstepsSliderValueChanged);
    }

    private void HandleMasterVolumeSliderValueChanged(float value)
    {
        SetVolume(_masterVolumeSlider.groupName, value);
    }

    private void HandleFootstepsSliderValueChanged(float value)
    {
        SetVolume(_footstepsSlider.groupName, value);
    }

    private void HandleBackgroundMusicSliderValueChanged(float value)
    {
        SetVolume(_backgroundMusicSlider.groupName, value);
    }

    public void SetVolume(string group, float value)
    {
        audioMixer.SetFloat(group, Mathf.Log10(value) * _multiplier);
    }
}