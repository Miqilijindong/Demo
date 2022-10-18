using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L10_6声息系统 : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    public AudioClip attendAudioClip;
    // Start is called before the first frame update
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //audioSource.Play();
        if (Input.GetMouseButtonDown(0))
        {
            audioSource.Play();
        }
        if (Input.GetMouseButtonDown(1))
        {
            audioSource.Stop();
        }
        if (Input.GetMouseButtonDown(2))
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
            else
            {
                audioSource.UnPause();
            }
        }
        if(Input.GetMouseButton(3))
        {
            audioSource.PlayOneShot(attendAudioClip);
        }
    }
}
