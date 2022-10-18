using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L10案例 : MonoBehaviour
{
    public Slider slider;
    public AudioSource audioSource;
    public AudioClip audioClip;
    void Start()
    {
        audioSource.clip = audioClip;
        slider.onValueChanged.AddListener(sliderValueChaged);
        audioSource.volume = 1;
        if(PlayerPrefs.HasKey("audioSource.volume"))
        {
            audioSource.volume = PlayerPrefs.GetFloat("audioSource.volume");
        }
        slider.value = audioSource.volume;
    }

    private void sliderValueChaged(float value)
    {
        audioSource.volume = value;
        PlayerPrefs.SetFloat("audioSource.volume", value);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(audioSource.isPlaying)
            {
                audioSource.Stop();
            } else
            {
                audioSource.Play();
            }
        }
    }

}
