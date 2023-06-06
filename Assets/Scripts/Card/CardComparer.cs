using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardComparer : IComparer<Card>
{
    public enum SortBy
    {
        Value,
        Suite
    }

    public SortBy compareField = SortBy.Value;

    public int Compare(Card x, Card y)
    {
        switch (compareField)
        {
            case SortBy.Value: return x.CompareTo(y);
            case SortBy.Suite: {
                    if (x.cardValue.suite == y.cardValue.suite)
                    {
                        return x.cardValue.GetHighValue().CompareTo(y.cardValue.GetHighValue());
                    }
                    else
                    {
                        return x.cardValue.suite.CompareTo(y.cardValue.suite);
                    }
                }
            default: return x.CompareTo(y);
        }
    }
}
