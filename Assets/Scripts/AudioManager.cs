using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] sfx;
    public AudioSource[] bgm;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(int soundToPlay)
    {
        sfx[soundToPlay].pitch = Random.Range(0.9f, 1.1f);

        if (soundToPlay < sfx.Length)
        {
            sfx[soundToPlay].Play();
        }
    }

    public void StopSFX(int soundToStop)
    {
        if (soundToStop < sfx.Length)
        {
            sfx[soundToStop].Stop();
        }
    }

    public void PlayBGM(int musicToPlay)
    {
        StopBGM();

        if (musicToPlay < bgm.Length)
        {
            bgm[musicToPlay].Play();
        }
    }

    public void StopBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }
}
