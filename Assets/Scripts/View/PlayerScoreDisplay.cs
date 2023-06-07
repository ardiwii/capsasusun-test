using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI topSetScore;
    [SerializeField] private TextMeshProUGUI midSetScore;
    [SerializeField] private TextMeshProUGUI bottomSetScore;
    [SerializeField] private TextMeshProUGUI roundTotalScore;
    [SerializeField] private TextMeshProUGUI gameTotalScore;

    public void SetSetScoreDisplay(SetPosition setPos, int score)
    {
        switch (setPos)
        {
            case SetPosition.top: topSetScore.text = "Top: " + score; break;
            case SetPosition.mid: midSetScore.text = "Middle: " + score; break;
            case SetPosition.bottom: bottomSetScore.text = "Bottom: " + score; break;
        }
    }

    public void SetRoundTotal(int score)
    {
        roundTotalScore.text = "Round: " + score;
    }

    public void SetGameTotal(int score)
    {
        gameTotalScore.text = "Score: " + score;
    }
}
