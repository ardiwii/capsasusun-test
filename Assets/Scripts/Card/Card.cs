using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Card : MonoBehaviour, IComparable<Card>
{
    public CardView cardView;
    public CardValue cardValue;
    private CardPool cardPool;
    private CardSet currentSet;

    public void SetCard(CardData data, CardPool pool)
    {
        cardView.SetSprite(data.cardImage);
        cardValue = data.value;
        cardPool = pool;
    }

    public void Discard()
    {
        cardPool.ReturnCardToPool(this);
    }

    public int CompareTo(Card other)
    {
        return cardValue.CompareTo(other.cardValue);
    }

    public bool IsOneBefore(Card other)
    {
        return cardValue.IsOneBefore(other.cardValue);
    }

    public void AddCardToSet(CardSet set)
    {
        currentSet = set;
    }

    public void SwapCard(Card cardToSwap)
    {
        CardSet temp = currentSet;
        int layoutIdx = transform.GetSiblingIndex();
        currentSet.RemoveCardFromSet(this);
        cardToSwap.currentSet.RemoveCardFromSet(cardToSwap);
        cardToSwap.currentSet.MoveCardToSet(this, cardToSwap.transform.GetSiblingIndex());
        temp.MoveCardToSet(cardToSwap, layoutIdx);
    }
}
