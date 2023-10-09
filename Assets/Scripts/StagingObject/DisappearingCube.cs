using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingCube : MonoBehaviour
{
    [SerializeField] Color sColor;
    [SerializeField] Color tColor;
    [SerializeField] int disppearStage;
    private SpriteRenderer sr;
    private GameManager.OnGameStageForward ogsf;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ogsf = new GameManager.OnGameStageForward(OnStage);
        GameManager.Instance.GameStageForwardHandler += ogsf;

        sr.color = new Color(sColor.r,sColor.g,sColor.b);
    }

    private void OnStage(int currStage)
    {
        //线性插值算颜色
        float k = 1.0f * currStage/disppearStage;
        float r, g, b;
        r = sColor.r * (1 - k) + tColor.r * k;
        g = sColor.g * (1 - k) + tColor.g * k;
        b = sColor.b * (1 - k) + tColor.b * k;
        sr.color = new Color(r, g, b);

        if (currStage>disppearStage)
        {
            GameManager.Instance.GameStageForwardHandler -= ogsf;
            Destroy(this.gameObject);
        }
    }
}
