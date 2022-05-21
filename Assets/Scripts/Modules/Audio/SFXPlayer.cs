using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioClip;

    public bool autoPlay = true;

    private void Start()
    {
        if (autoPlay)
            SoundManager.Instance.PlaySFX(audioClip);
    }

    public void Play()
    {
        SoundManager.Instance.PlaySFX(audioClip);
    }

}
