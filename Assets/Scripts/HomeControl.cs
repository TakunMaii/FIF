using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeControl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController pc;
        if(collision.TryGetComponent<PlayerController>(out pc))
        {
            if(pc.GetIsTakingAcorn())
            {
                GameManager.Instance.GetPlayerAnimator().LoseAcornTrigger();
                GameManager.Instance.StageForward();
            }
        }
    }
}
