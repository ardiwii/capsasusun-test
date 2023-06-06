using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int score;
    [SerializeField] private Hand hand;
    [SerializeField] private FinalHand finalHand;
    [SerializeField] private PlayerView view;
    [SerializeField] private Avatar avatar;

    public Hand Hand { get => hand; set => hand = value; }

    public void FinalizeHand()
    {
        finalHand.SetFinalHand(hand);
    }

    public void CompareHandAndScoreRound(List<Player> others)
    {
        int roundScore = 0;
        roundScore += CompareSetAndDisplay(SetPosition.top, others);
        roundScore += CompareSetAndDisplay(SetPosition.mid, others);
        int bottomSetScore = CompareSetAndDisplay(SetPosition.bottom, others);
        roundScore += bottomSetScore;
        string extraDetail = "";

        //check for "tembus"
        int headToHead = HeadToHeadCheck(others);
        if(headToHead == 3) //tembus keliling
        {
            roundScore *= 4;
            extraDetail += "Tembus Keliling (x4)\n";
        }
        else if(headToHead > 0) //tembus
        {
            roundScore *= 2;
            extraDetail += "Tembus (x2)\n";
        }

        //check point for "barang"
        ExtraPointStruct extra = hand.GetSetExtraPoints();
        extraDetail += extra.details;
        roundScore += extra.extraPoint;
        view.SetScoreDetail(extraDetail);
        view.SetRoundScore(roundScore);
        score += roundScore;
        view.SetTotalScore(score);
        if(roundScore < 0)
        {
            avatar.SetLoseFace();
        }
        else
        {
            avatar.SetWinFace();
        }
    }

    private int HeadToHeadCheck(List<Player> others)
    {
        int tembusCount = 0;
        for (int i = 0; i < others.Count; i++)
        {
            if (hand.CompareTo(others[i].hand) == 3)
            {
                tembusCount++;
            }
        }
        return tembusCount;
    }

    private int CompareSetAndDisplay(SetPosition setPosition, List<Player> others)
    {
        int setScore = 0;
        for (int i = 0; i < others.Count; i++)
        {
            setScore += hand.CompareSet(setPosition, others[i].hand);
        }
        view.SetPlayerSetScore(setPosition, setScore);
        return setScore;
    }

    public void ChooseAvatar(AvatarSO avatarToUse)
    {
        avatar.InitAvatar(avatarToUse);
    }
}
