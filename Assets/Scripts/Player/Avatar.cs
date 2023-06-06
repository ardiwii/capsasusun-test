using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Avatar : MonoBehaviour
{
    [SerializeField] private Image avatarDisplay;
    private AvatarSO avatarData;

    public void InitAvatar(AvatarSO avatarToUse)
    {
        avatarData = avatarToUse;
    }

    public void SetDefaultFace()
    {
        avatarDisplay.sprite = avatarData.defaultFace;
    }
    public void SetWinFace()
    {
        avatarDisplay.sprite = avatarData.winFace;
    }
    public void SetLoseFace()
    {
        avatarDisplay.sprite = avatarData.loseFace;
    }
}
