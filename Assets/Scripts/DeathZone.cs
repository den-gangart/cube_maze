using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(GameTags.PLAYER))
        {
            PlayerState playerState = other.GetComponent<PlayerState>();
            playerState.Kill();
        }
    }
}
