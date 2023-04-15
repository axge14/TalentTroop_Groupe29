using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{

    [SerializeField]
    private AudioClip sound;

    [SerializeField]
    [Range(0.1f, 1f)] private float volume;

    [SerializeField]
    [Range(0.1f, 2.5f)] private float pitch;

    private AudioSource source;

    private void Awake()
    {
        gameObject.AddComponent<AudioSource>();
        source = GetComponent<AudioSource>();

        volume = 0.5f;
        pitch = 1; 
    }
    // Start is called before the first frame update
    void Start()
    {
        source.clip = sound;
        source.volume = volume;
        source.pitch = pitch;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.Input.GetAxisRaw("Vertical") > 0 && UnityEngine.Input.GetAxisRaw("Horizontal") < 0 && UnityEngine.Input.GetAxisRaw("Horizontal") > 0)
        {
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
        else
        {
            if (UnityEngine.Input.GetAxisRaw("Vertical") > 0 || UnityEngine.Input.GetAxisRaw("Vertical") < 0 || UnityEngine.Input.GetAxisRaw("Horizontal") < 0 || UnityEngine.Input.GetAxisRaw("Horizontal") > 0)
            {
                if (UnityEngine.Input.GetAxisRaw("Vertical") > 0 && UnityEngine.Input.GetAxisRaw("Vertical") < 0 || UnityEngine.Input.GetAxisRaw("Horizontal") < 0 && UnityEngine.Input.GetAxisRaw("Horizontal") > 0)
                {
                    source.Pause();
                }
                else
                {
                    if (!source.isPlaying)
                    {
                        source.Play();
                    }
                }
            }
            else
            {
                source.Pause();
            }
        }

        source.volume = volume;
        source.pitch = pitch;
    }

    private void PlayAndPause()
    {
        if (!source.isPlaying)
        {
            source.Play();
        }
    }
}
