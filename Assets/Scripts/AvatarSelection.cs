using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSelection : MonoBehaviour
{
    [SerializeField] private List<AvatarSO> avatars;

    public void SetPlayerAvatar(int chosenAvatarIdx, Player player, List<Player> bots)
    {
        List<int> availableAvatarIdx = new List<int>();
        for (int i = 0; i < avatars.Count; i++)
        {
            availableAvatarIdx.Add(i);
        }
        player.ChooseAvatar(avatars[chosenAvatarIdx]);
        availableAvatarIdx.Remove(chosenAvatarIdx);
        for (int i = 0; i < bots.Count; i++)
        {
            if(i < availableAvatarIdx.Count)
            {
                bots[i].ChooseAvatar(avatars[availableAvatarIdx[i]]);
            }
        }
    }
}
