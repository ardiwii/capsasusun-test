using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class SetupHand : Hand
{
    [SerializeField] private FinalHand finalHand;

    [SerializeField] private TextMeshProUGUI handValidNote;
    [SerializeField] private SetNameDisplay topSetDisplay;
    [SerializeField] private SetNameDisplay midSetDisplay;
    [SerializeField] private SetNameDisplay bottomSetDisplay;
    [SerializeField] private CardController setupController;

    private void OnEnable()
    {
        topSet.OnSetModified += TopSet_OnSetModified;
        midSet.OnSetModified += MidSet_OnSetModified;
        bottomSet.OnSetModified += BottomSet_OnSetModified;
    }

    private void OnDisable()
    {
        topSet.OnSetModified -= TopSet_OnSetModified;
        midSet.OnSetModified -= MidSet_OnSetModified;
        bottomSet.OnSetModified -= BottomSet_OnSetModified;
        ClearControl();
    }

    private void TopSet_OnSetModified(SetType newType, CardValue newValue)
    {
        topSetDisplay.SetSetName(newType);
        UpdateValidity();
    }

    private void MidSet_OnSetModified(SetType newType, CardValue newValue)
    {
        midSetDisplay.SetSetName(newType);
        UpdateValidity();
    }

    private void BottomSet_OnSetModified(SetType newType, CardValue newValue)
    {
        bottomSetDisplay.SetSetName(newType);
        UpdateValidity();
    }

    public override void DrawFirstHand(Deck deck)
    {
        cardsInHand = new List<Card>();
        topSet.InitSet();
        midSet.InitSet();
        bottomSet.InitSet();
        for (int i = 0; i < 13; i++)
        {
            Card drawnCard = deck.DrawCard();
            drawnCard.CardView.OnCardClicked += SetupHand_OnCardClicked;
            cardsInHand.Add(drawnCard);
            if (i < 3)
            {
                topSet.AddCardToSet(drawnCard);
            }
            else if (i >= 3 && i < 8)
            {
                midSet.AddCardToSet(drawnCard);
            }
            else if (i >= 8 && i < 13)
            {
                bottomSet.AddCardToSet(drawnCard);
            }
        }
        topSet.EvaluateSet();
        midSet.EvaluateSet();
        bottomSet.EvaluateSet();
    }

    public void ClearControl()
    {
        for (int i = 0; i < cardsInHand.Count; i++)
        {
            cardsInHand[i].CardView.OnCardClicked -= SetupHand_OnCardClicked;
        }
    }

    private void SetupHand_OnCardClicked(Card clickedCard)
    {
        setupController.PickCard(clickedCard);
    }

    public void SwapMidAndBottom()
    {
        midSet.OnSetModified -= MidSet_OnSetModified;
        bottomSet.OnSetModified -= BottomSet_OnSetModified;

        CardSet temp = midSet;
        midSet = bottomSet;
        bottomSet = temp;

        midSet.transform.SetSiblingIndex(1);
        bottomSet.transform.SetSiblingIndex(2);

        SetNameDisplay tempDisplay = midSetDisplay;
        midSetDisplay = bottomSetDisplay;
        bottomSetDisplay = tempDisplay;

        int tempSiblingIdx = midSetDisplay.transform.GetSiblingIndex();
        midSetDisplay.transform.SetSiblingIndex(bottomSetDisplay.transform.GetSiblingIndex());
        bottomSetDisplay.transform.SetSiblingIndex(tempSiblingIdx);

        midSet.OnSetModified += MidSet_OnSetModified;
        bottomSet.OnSetModified += BottomSet_OnSetModified;

        UpdateValidity();
    }

    private void UpdateValidity()
    {
        if (topSet.SetValue == null || midSet.SetValue == null || bottomSet.SetValue == null) return;
        if (IsHandValid())
        {
            handValidNote.text = "Valid";
            handValidNote.color = Color.green;
        }
        else
        {
            handValidNote.text = "Invalid";
            handValidNote.color = Color.red;
        }

    }
}

public enum SetPosition
{
    top,
    mid,
    bottom
}
