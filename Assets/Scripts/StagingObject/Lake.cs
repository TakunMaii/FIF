using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private float alpha;
    [SerializeField] private float alphaMultiplier;
    [SerializeField] private int freezeStage;
    [SerializeField] private int halfFreezeStage;

    private int currStage;
    private SpriteRenderer sr;
    private GameManager.OnGameStageForward ogsf;
    private BoxCollider2D boxCollider2D;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ogsf = new GameManager.OnGameStageForward(OnStage);
        boxCollider2D = GetComponent<BoxCollider2D>();
        GameManager.Instance.GameStageForwardHandler += ogsf;
    }

    private void OnStage(int stage)
    {
        currStage=stage;
        if (stage < freezeStage)
        {
            alpha *= alphaMultiplier;
            Color color = new Color(1, 1, 1, 1);
            sr.color = color;
        }
        if (stage == halfFreezeStage)
        {
            sr.sprite = Resources.Load<Sprite>("artworks/SpecialObject/lake-half-frozen");
        }
        if (stage == freezeStage)
        {
            sr.sprite = Resources.Load<Sprite>("artworks/SpecialObject/lake-frozen");
            sr.color = new Color(1, 1, 1, 1);
            boxCollider2D.isTrigger = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")&&currStage<freezeStage)
        {
            GameManager.Instance.OnPlayerDied();
            Debug.Log("die");
        }
    }

}
