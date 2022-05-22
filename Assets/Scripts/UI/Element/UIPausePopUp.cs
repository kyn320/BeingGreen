using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPausePopUp : UIBasePopup
{
    [SerializeField]
    private Slider bgmSlider;

    [SerializeField]
    private Slider sfxSlider;

    public override void Init(UIData uiData)
    {
        base.Init(uiData);

        bgmSlider.value = PlayerPrefs.GetFloat("BGM", 0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFX", 0.5f);
    }
    public void OpenPlayHelp()
    {
        UIController.Instance.OpenPopup("PlayHelp");
    }
    public void EnterTitle()
    {
        SceneController.Instance.LoadScene("Title");
    }

    public void ChangeBGMVolume(float volume)
    {
        SoundManager.Instance.ChangeBGMVolume(volume);
    }

    public void ChangeSFXVolume(float volume)
    {
        SoundManager.Instance.ChangeSFXVolume(volume);
    }

    public override void Close()
    {
        GameRuleController.Instance.UnPauseGame();
        base.Close();
    }


}
