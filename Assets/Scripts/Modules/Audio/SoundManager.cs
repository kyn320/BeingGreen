using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public const int MaxSFXPlayCount = 5;

    private AudioSource bgmAudioPlayer;

    private Dictionary<string, SoundPool> sfxPlayerDic = new Dictionary<string, SoundPool>();
    public GameObject sfxPlayerPrefab;

    protected override void Awake()
    {
        bgmAudioPlayer = GetComponent<AudioSource>();
        base.Awake();
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

    public void UnPauseBGM() { 
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

            List<SoundPoolPlayer> sFXPlayers = new List<SoundPoolPlayer>();
            //SFX 积己 饶 殿废
            for (var i = 0; i < MaxSFXPlayCount; ++i)
            {
                var sfxGo = Instantiate(sfxPlayerPrefab, groupGo.transform);
                sFXPlayers.Add(sfxGo.GetComponent<SoundPoolPlayer>());
            }

            sfxPlayerDic.Add(sfxClip.name, new SoundPool(sfxClip, sFXPlayers));
        }

        soundPool = sfxPlayerDic[sfxClip.name];
        soundPool.PlaySFX();
    }

}
