using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackingCube : MonoBehaviour
{
    [SerializeField] Color sColor;
    [SerializeField] Color tColor;
    [SerializeField] float maxTime;
    [SerializeField] int crackingStage;
    [SerializeField] float shack;
    [SerializeField] Transform pic;
    [SerializeField]private SpriteRenderer sr;
    private GameManager.OnGameStageForward ogsf;
    private new BoxCollider2D collider;
    private bool isLater;
    private bool isPlayerIn;
    private float timer;
    private int currStage;

    private void Start()
    {
        //sr = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        ogsf = new GameManager.OnGameStageForward(OnStage);
        GameManager.Instance.GameStageForwardHandler += ogsf;

        isLater = false;
        isPlayerIn = false;
        sr.color = new Color(sColor.r, sColor.g, sColor.b);
        timer = 0;
        currStage = 0;
    }

    private void RendColor()
    {
        //线性插值算颜色
        float k = 1.0f * currStage / GameManager.Instance.GetMaxStage();
        float r, g, b;
        r = sColor.r * (1 - k) + tColor.r * k;
        g = sColor.g * (1 - k) + tColor.g * k;
        b = sColor.b * (1 - k) + tColor.b * k;
        sr.color = new Color(r, g, b);
    }

    private void OnStage(int currStage)
    {
        this.currStage = currStage;
        RendColor();

        if(currStage == crackingStage)
        {
            isLater = true;
        }
    }

    private void Update()
    {
        if(isPlayerIn)
        {
            timer += Time.deltaTime;
            pic.position = transform.position + UnityEngine.Random.insideUnitSphere * shack;

            if (timer >= maxTime)
            {
                StartCoroutine(disappearFor(3));
                timer = 0;
            }
        }
        
    }

    private IEnumerator disappearFor(float seconds)
    {
        isPlayerIn = false;
        sr.color = new Color(0, 0, 0, 0);
        collider.isTrigger = true;
        yield return new WaitForSeconds(seconds);
        RendColor();
        collider.isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player")&&isLater)
        {
            isPlayerIn = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&isLater)
        {
            isPlayerIn = false;
            timer = 0;
        }
    }
}
