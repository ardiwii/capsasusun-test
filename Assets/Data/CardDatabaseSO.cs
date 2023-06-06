using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class CardDatabaseSO : ScriptableObject
{
    public List<CardData> cards;
}

[System.Serializable]
public struct CardData 
{
    public Sprite cardImage;
    public CardValue value;
}

[System.Serializable]
public enum SuiteEnum
{
    Diamond = 0,
    Club = 1,
    Heart = 2,
    Spade = 3
}