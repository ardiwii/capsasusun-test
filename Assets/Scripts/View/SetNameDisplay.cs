using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetNameDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayText;

    public void SetSetName(SetType setType)
    {
        displayText.text = setType.ToString();
    }
}
