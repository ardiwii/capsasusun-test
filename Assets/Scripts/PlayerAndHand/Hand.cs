using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour, IComparable<Hand>
{
    protected List<Card> cardsInHand;
    [SerializeField] protected CardSet topSet;
    [SerializeField] protected CardSet midSet;
    [SerializeField] protected CardSet bottomSet;

    public int CompareTo(Hand other)
    {
        int totalDiff = 0;

        if (!IsHandValid()) return -3;
        else
        {
            if (!other.IsHandValid())
            {
                totalDiff = 3;
            }
            else
            {
                totalDiff += topSet.CompareTo(other.topSet);
                totalDiff += midSet.CompareTo(other.midSet);
                totalDiff += bottomSet.CompareTo(other.bottomSet);
            }
        }

        return totalDiff;
    }

    public int CompareSet(SetPosition setPosition, Hand other)
    {
        if (!IsHandValid()) return -1;
        else
        {
            if (!other.IsHandValid())
            {
                return 1;
            }
            else
            {
                switch (setPosition)
                {
                    case SetPosition.top: return topSet.CompareTo(other.topSet);
                    case SetPosition.mid: return midSet.CompareTo(other.midSet);
                    case SetPosition.bottom: return bottomSet.CompareTo(other.bottomSet);
                    default: return 0;
                }
            }
        }
    }

    public CardSet GetCardSet(SetPosition setPosition)
    {
        switch (setPosition)
        {
            case SetPosition.top: return topSet;
            case SetPosition.mid: return midSet;
            case SetPosition.bottom: return bottomSet;
            default: return bottomSet;
        }
    }

    public bool IsHandValid()
    {
        return topSet.CompareTo(midSet) <= 0 && midSet.CompareTo(bottomSet) <= 0;
    }

    public void ReturnCards()
    {
        for (int i = 0; i < cardsInHand.Count; i++)
        {
            cardsInHand[i].Discard();
        }
    }

    public ExtraPointStruct GetSetExtraPoints()
    {
        if (!IsHandValid())
        {
            return new ExtraPointStruct() { details = "", extraPoint = 0 };
        }
        else
        {
            int extraPoints = 0;
            string details = "";
            if (topSet.SetType == SetType.ThreeOfAKind)
            {
                extraPoints += 5;
                details += "3 of A Kind Top (+5)\n";
            }
            if (midSet.SetType == SetType.FourOfAKind)
            {
                extraPoints += 14;
                details += "4 of A Kind Middle (+14)\n";
            }
            else if (midSet.SetType == SetType.StraightFlush)
            {
                if (midSet.SetValue.numberValue == 1) //royal straight flush
                {
                    extraPoints += 22;
                    details += "Royal Straight Flush Middle (+22)\n";
                }
                else
                {
                    extraPoints += 18;
                    details += "Straight Flush Middle (+18)\n";
                }
            }
            else if (midSet.SetType == SetType.FullHouse)
            {
                extraPoints += 3;
                details += "Full House Middle (+18)\n";
            }
            if (bottomSet.SetType == SetType.FourOfAKind)
            {
                extraPoints += 7;
                details += "4 of A Kind Bottom (+7)\n";
            }
            else if (midSet.SetType == SetType.StraightFlush)
            {
                if (midSet.SetValue.numberValue == 1) //royal straight flush
                {
                    extraPoints += 11;
                    details += "Royal Straight Flush Middle (+11)\n";
                }
                else
                {
                    extraPoints += 9;
                    details += "Straight Flush Middle (+9)\n";
                }
            }
            return new ExtraPointStruct() { details = details, extraPoint = extraPoints};
        }
    }

    public virtual void DrawFirstHand(Deck deck)
    {

    }
}

public struct ExtraPointStruct
{
    public int extraPoint;
    public string details;
}
