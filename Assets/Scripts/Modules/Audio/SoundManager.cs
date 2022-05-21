using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private AudioSource bgmAudioPlayer;

    protected override void Awake()
    {
        base.Awake();
        bgmAudioPlayer = GetComponent<AudioSource>();        
    }

    public void PlayBGM(AudioClip bgm)
    {
        bgmAudioPlayer.Stop();

        bgmAudioPlayer.clip = bgm;
        bgmAudioPlayer.Play();
    }

}
