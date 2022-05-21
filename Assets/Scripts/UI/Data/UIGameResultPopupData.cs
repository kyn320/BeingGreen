using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameResultPopupData : UIPopupData
{
    public int[] scores;
    public int winner;

    public UIGameResultPopupData(int[] scores, int winner) { 
        viewName = "GameResult";
        this.scores = scores;
        this.winner = winner;
    }
}
