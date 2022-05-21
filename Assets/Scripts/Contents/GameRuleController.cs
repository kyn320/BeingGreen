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

    public UnityEvent startGameEvent;
    public UnityEvent<int> endGameEvent;

    private void Start()
    {
        StartGame();
    }

    [Button("게임 시작")]
    public void StartGame()
    {
        playTime = gameRule.playTime;
        isPlay = true;
        startGameEvent?.Invoke();
        updatePlayTimeEvent?.Invoke(playTime, gameRule.playTime);
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
        }
    }
}
