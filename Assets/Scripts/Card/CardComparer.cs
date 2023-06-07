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
                    if (x.CardValue.suite == y.CardValue.suite)
                    {
                        return x.CardValue.GetHighValue().CompareTo(y.CardValue.GetHighValue());
                    }
                    else
                    {
                        return x.CardValue.suite.CompareTo(y.CardValue.suite);
                    }
                }
            default: return x.CompareTo(y);
        }
    }
}
