using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Card : MonoBehaviour, IComparable<Card>
{
    [SerializeField] private CardView cardView;
    private CardValue cardValue;
    private CardPool cardPool;
    private CardSet currentSet;

    public CardValue CardValue { get => cardValue; }
    public CardView CardView { get => cardView; }

    public void SetCard(CardData data, CardPool pool)
    {
        CardView.SetView(this, data.cardImage);
        cardValue = data.value;
        cardPool = pool;
    }

    public void Discard()
    {
        cardPool.ReturnCardToPool(this);
    }

    public int CompareTo(Card other)
    {
        return cardValue.CompareTo(other.CardValue);
    }

    public bool IsOneBefore(Card other)
    {
        return cardValue.IsOneBefore(other.CardValue);
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
