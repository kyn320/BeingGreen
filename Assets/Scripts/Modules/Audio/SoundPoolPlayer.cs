using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundPoolPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioPlayer;

    public float PlayTime { get { return audioPlayer.time; } }

    public void ChangeVolume(float volume) { 
        audioPlayer.volume = volume;
    }

    public void Play(AudioClip clip)
    {
        audioPlayer.Stop();
        audioPlayer.clip = clip;
        audioPlayer.Play();
    }

}
