using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalHand : MonoBehaviour
{
    [SerializeField] private List<CommittedCard> topCards;
    [SerializeField] private List<CommittedCard> midCards;
    [SerializeField] private List<CommittedCard> bottomCards;


    public void SetFinalHand(Hand hand)
    {
        List<Card> topSetCards = hand.GetCardSet(SetPosition.top).Cards;
        for (int i = 0; i < topSetCards.Count; i++)
        {
            topCards[i].SetCard(topSetCards[i]);
        }
        List<Card> midSetCards = hand.GetCardSet(SetPosition.mid).Cards;
        for (int i = 0; i < midSetCards.Count; i++)
        {
            midCards[i].SetCard(midSetCards[i]);
        }
        List<Card> bottomSetCards = hand.GetCardSet(SetPosition.bottom).Cards;
        for (int i = 0; i < bottomSetCards.Count; i++)
        {
            bottomCards[i].SetCard(bottomSetCards[i]);
        }
    }
}
