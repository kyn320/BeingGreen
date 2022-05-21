using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundPool
{
    public string ClipName { get { return audioClip.name; } }
    public AudioClip audioClip;
    public List<SoundPoolPlayer> sfxPlayerList;

    public SoundPool(AudioClip audioClip, List<SoundPoolPlayer> sfxPlayerList)
    {
        this.audioClip = audioClip;
        this.sfxPlayerList = sfxPlayerList;
    }

    public void ChangeVolume(float volume)
    {
        for (var i = 0; i < sfxPlayerList.Count; ++i)
        {
            sfxPlayerList[i].ChangeVolume(volume);
        }
    }

    public void PlaySFX()
    {
        //5개 이상 재생인 경우 Stop후에 재생합니다..
        var sfxPlayer = sfxPlayerList[0];
        var minTime = sfxPlayer.PlayTime;

        for (var i = 1; i < sfxPlayerList.Count; ++i)
        {
            if (sfxPlayerList[i].PlayTime < minTime)
            {
                sfxPlayer = sfxPlayerList[i];
                minTime = sfxPlayer.PlayTime;
            }
        }

        sfxPlayer.Play(audioClip);
    }

}
