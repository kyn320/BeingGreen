using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public const int MaxSFXPlayCount = 5;

    private float BGMVolume;
    private float SFXVolume;

    private AudioSource bgmAudioPlayer;

    private Dictionary<string, SoundPool> sfxPlayerDic = new Dictionary<string, SoundPool>();
    public GameObject sfxPlayerPrefab;

    protected override void Awake()
    {
        bgmAudioPlayer = GetComponent<AudioSource>();
        base.Awake();

        BGMVolume = PlayerPrefs.GetFloat("BGM", 0.5f);
        SFXVolume = PlayerPrefs.GetFloat("SFX", 0.5f);

        bgmAudioPlayer.volume = BGMVolume;

    }

    public void ChangeBGMVolume(float bgm)
    {
        BGMVolume = bgm;
        bgmAudioPlayer.volume = BGMVolume;
        PlayerPrefs.SetFloat("BGM", BGMVolume);
    }

    public void ChangeSFXVolume(float sfx)
    {
        SFXVolume = sfx;

        var keyList = sfxPlayerDic.Keys.ToList();

        for (var i = 0; i < keyList.Count; ++i)
        {
            sfxPlayerDic[keyList[i]].ChangeVolume(sfx);
        }

        PlayerPrefs.SetFloat("SFX", SFXVolume);
    }

    public void PlayBGM(AudioClip bgm)
    {
        bgmAudioPlayer.Stop();
        bgmAudioPlayer.clip = bgm;
        bgmAudioPlayer.Play();
    }

    public void PauseBGM()
    {
        bgmAudioPlayer.Pause();
    }

    public void UnPauseBGM()
    {
        bgmAudioPlayer.UnPause();
    }

    public void StopBGM()
    {
        bgmAudioPlayer.Stop();
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        SoundPool soundPool = null;

        if (!sfxPlayerDic.ContainsKey(sfxClip.name))
        {
            //Group 积己
            var groupGo = new GameObject(sfxClip.name);
            groupGo.transform.SetParent(transform);

            List<SoundPoolPlayer> sfxPlayers = new List<SoundPoolPlayer>();
            //SFX 积己 饶 殿废
            for (var i = 0; i < MaxSFXPlayCount; ++i)
            {
                var sfxGo = Instantiate(sfxPlayerPrefab, groupGo.transform);
                var sfxPlayer = sfxGo.GetComponent<SoundPoolPlayer>();
                sfxPlayer.ChangeVolume(SFXVolume);
                sfxPlayers.Add(sfxPlayer);
            }

            sfxPlayerDic.Add(sfxClip.name, new SoundPool(sfxClip, sfxPlayers));
        }

        soundPool = sfxPlayerDic[sfxClip.name];
        soundPool.PlaySFX();
    }

}
