using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private PlayerScoreDisplay scoreDisplay;
    [SerializeField] private GameObject scoreDetailPanel;
    [SerializeField] private TextMeshProUGUI scoreDetail;

    public void SetPlayerSetScore(SetPosition setPosition, int score)
    {
        scoreDisplay.SetSetScoreDisplay(setPosition, score);
    }

    public void SetRoundScore(int score)
    {
        scoreDisplay.SetRoundTotal(score);
    }

    public void SetTotalScore(int score)
    {
        scoreDisplay.SetGameTotal(score);
    }

    public void SetScoreDetail(string detailText)
    {
        scoreDetail.text = detailText;
        scoreDetailPanel.SetActive(false);
    }

    public void ToggleScoreDetail()
    {
        scoreDetailPanel.SetActive(!scoreDetail.IsActive());
    }

}
