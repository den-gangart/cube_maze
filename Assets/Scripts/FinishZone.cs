using System;
using UnityEngine;

public class FinishZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(GameTags.PLAYER))
        {
            PlayerState playerState = other.GetComponent<PlayerState>();
            playerState.Win();
        }
    }
}
