using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class CardValue : IComparable<CardValue>
{
    public SuiteEnum suite;
    public int numberValue;

    public int CompareTo(CardValue other)
    {
        if (GetHighValue() > other.GetHighValue()) return 1;
        else if (GetHighValue() < other.GetHighValue()) return -1;
        else
        {
            return suite.CompareTo(other.suite);
        }
    }

    public int GetHighValue()
    {
        //think ace as 14 for high value
        return numberValue == 1 ? 14 : numberValue;
    }

    public bool IsOneBefore(CardValue other)
    {
        //special case for king to ace
        if (numberValue == 13)
        {
            return other.GetHighValue() == 14;
        }
        else return other.numberValue - numberValue == 1;
    }
}
