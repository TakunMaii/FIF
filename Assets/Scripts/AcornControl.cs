using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornControl : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] float speed;
    private Vector3 originalPos;

    private void Start()
    {
        originalPos = transform.position;
    }

    private void Update()
    {
        float calcuY = Mathf.Sin(speed * Time.time) * range;
        transform.position = new Vector3 (originalPos.x, originalPos.y + calcuY, originalPos.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController pc;
        if (collision.TryGetComponent<PlayerController>(out pc))
        {
            if (!pc.GetIsTakingAcorn())
            {
                pc.GetAcorn();
                AudioManager.Ins.PlaySounds("get",GameManager.Instance.GetPlayer().transform.position);
                GameManager.Instance.GetPlayerAnimator().GetAcornTrigger();
                Destroy(gameObject);
            }
        }
    }
}
