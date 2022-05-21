using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class GameRuleController : Singleton<GameRuleController>
{
    [SerializeField]
    private GameRule gameRule;

    [SerializeField]
    protected WorldController worldController;

    [ShowInInspector]
    [ReadOnly]
    private float playTime;
    public float CurrentPlayTime { get { return playTime; } }

    public UnityEvent<float, float> updatePlayTimeEvent;
    public UnityEvent<float> updatePlayDelaTimeEvent;

    public bool isPlay = false;

    public UnityEvent initalizeGameEvent;
    public UnityEvent startGameEvent;
    public UnityEvent<int> endGameEvent;

    private void Start()
    {
        InitializeGame();
    }


    [Button("게임 준비")]
    public void InitializeGame()
    {
        playTime = gameRule.playTime;
        updatePlayTimeEvent?.Invoke(playTime, gameRule.playTime);
        initalizeGameEvent?.Invoke();
        StartCoroutine("CoWaitStartCountUpdate");
    }

    public void StartGame()
    {

        isPlay = true;
        startGameEvent?.Invoke();
    }

    public void EndGame()
    {
        if(!isPlay)
            return;

        isPlay = false;

        var ownerTileCounts = worldController.GetOwnerTileCount();
        Debug.Log($"0 : {ownerTileCounts[0]} / 1 : {ownerTileCounts[1]}");

        var winner = 0;

        if (ownerTileCounts[0] > ownerTileCounts[1])
        {
            winner = 0;
        }
        else if (ownerTileCounts[0] < ownerTileCounts[1])
        {
            winner = 1;
        }
        else
        {
            winner = 2;
        }
        endGameEvent?.Invoke(winner);
        var resultData = new UIGameResultPopupData(ownerTileCounts, winner);
        UIController.Instance.OpenPopup(resultData);
    }

    private void Update()
    {
        if (!isPlay)
            return;

        playTime -= Time.deltaTime;
        updatePlayTimeEvent?.Invoke(playTime, gameRule.playTime);
        updatePlayDelaTimeEvent?.Invoke(Time.deltaTime);

        if (playTime <= 0)
        {
            EndGame();
        }
    }

    public void UpdateBingoTileOwners(int bingoCount1P, int bingoCount2P, int maxCount)
    {
        if (bingoCount1P == maxCount || bingoCount2P == maxCount)
        {
            EndGame();
        }
    }

    IEnumerator CoWaitStartCountUpdate()
    {
        var currentTime = gameRule.startCountTime;
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            yield return null;
        }

        StartGame();
    }
}
