using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioClip;

    public bool autoPlay = true;

    private void Start()
    {
        if (autoPlay)
            SoundManager.Instance.PlayBGM(audioClip);
    }

    public void Play()
    {
        SoundManager.Instance.PlayBGM(audioClip);
    }

    public void Pause()
    {
        SoundManager.Instance.PauseBGM();
    }

    public void UnPause()
    {
        SoundManager.Instance.UnPauseBGM();
    }

    public void Stop()
    {
        SoundManager.Instance.StopBGM();
    }

}
