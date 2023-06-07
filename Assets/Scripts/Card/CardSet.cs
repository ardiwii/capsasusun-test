using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSet : MonoBehaviour, IComparable<CardSet>
{
    public delegate void setModified(SetType newType, CardValue newValue);
    public event setModified OnSetModified;

    private List<Card> cards;
    private SetType setType;
    private CardValue setValue;

    public List<Card> Cards { get => cards; }
    public SetType SetType { get => setType;}
    public CardValue SetValue { get => setValue;}

    public void InitSet()
    {
        if (cards == null) cards = new List<Card>();
        else cards.Clear();
    }

    public void AddCardToSet(Card card)
    {
        card.gameObject.transform.SetParent(transform);
        card.gameObject.SetActive(true);
        card.AddCardToSet(this);
        cards.Add(card);
    }

    public void RemoveCardFromSet(Card card)
    {
        cards.Remove(card);
    }

    public void MoveCardToSet(Card card, int layoutIndex)
    {
        card.gameObject.transform.SetParent(transform);
        card.gameObject.transform.SetSiblingIndex(layoutIndex);
        card.AddCardToSet(this);
        cards.Add(card);
        EvaluateSet();
    }

    public void EvaluateSet()
    {
        cards.Sort();
        bool isFlush = cards.Count == 5;
        bool isStraight = cards.Count == 5;
        int pairCount = 0;
        int mostSameChain = 1;
        int currentSameChain = 1;
        CardValue prevCard = cards[0].CardValue;
        bool isLowAce = false;
        for (int i = 1; i < cards.Count; i++)
        {
            if (isStraight)
            {
                if (i == 4 && cards[i].IsOneBefore(cards[0])) //special case for low ace
                {
                    isLowAce = true;
                    isStraight = true;
                }
                else isStraight = prevCard.IsOneBefore(cards[i].CardValue);
            }
            isFlush = isFlush && prevCard.suite == cards[i].CardValue.suite;
            if (prevCard.numberValue == cards[i].CardValue.numberValue)
            {
                currentSameChain++;
                if (currentSameChain > mostSameChain)
                {
                    mostSameChain++;
                }
                if (currentSameChain == 2)
                {
                    pairCount++;
                    setValue = cards[i].CardValue;
                }
                else if (currentSameChain > 2)
                {

                    setValue = cards[i].CardValue;
                }
            }
            else
            {
                currentSameChain = 1;
            }
            prevCard = cards[i].CardValue;
        }
        //re-sort the cards for low ace case
        if (isStraight && isLowAce)
        {
            Card lowAce = cards[4];
            cards.RemoveAt(4);
            cards.Insert(0, lowAce);
        }

        if (isFlush && isStraight)
        {
            setType = SetType.StraightFlush;
            setValue = cards[cards.Count - 1].CardValue;
        }
        else if (mostSameChain == 4) setType = SetType.FourOfAKind;
        else if (mostSameChain == 3 && pairCount == 2) setType = SetType.FullHouse;
        else if (isFlush)
        {
            setType = SetType.Flush;
            setValue = cards[cards.Count - 1].CardValue;
        }
        else if (isStraight)
        {
            setType = SetType.Straight;
            setValue = cards[cards.Count - 1].CardValue;
        }
        else if (mostSameChain == 3 && pairCount == 1) setType = SetType.ThreeOfAKind;
        else if (mostSameChain == 2 && pairCount == 2) setType = SetType.TwoPair;
        else if (mostSameChain == 2 && pairCount == 1) setType = SetType.Pair;
        else
        {
            setType = SetType.HighCard;
            setValue = cards[cards.Count - 1].CardValue;
        }
        OnSetModified?.Invoke(setType, setValue);
    }

    public int CompareTo(CardSet other)
    {
        if(setType != other.setType)
        {
            return setType.CompareTo(other.setType);
        }
        else
        {
            return setValue.CompareTo(other.setValue);
        }
    }
}

public enum SetType
{
    HighCard = 0,
    Pair = 1,
    TwoPair = 2,
    ThreeOfAKind = 3,
    Straight = 4,
    Flush = 5,
    FullHouse = 6,
    FourOfAKind = 7,
    StraightFlush = 8
}
