using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TopTilemapRenderControl : MonoBehaviour
{
    [SerializeField] private Color sColor;
    [SerializeField] private Color tColor;
    private Tilemap tilemap;
    private int currStage;

    private void Awake()
    {
        currStage = 0;
        tilemap = GetComponent<Tilemap>();
    }

    private void Start()
    {
        RendColor();
        GameManager.Instance.GameStageForwardHandler += new GameManager.OnGameStageForward(OnStage);
    }

    private void Update()
    {

    }

    private void RendColor()
    {
        //线性插值算颜色
        float k = 1.0f * currStage / GameManager.Instance.GetMaxStage();
        float r, g, b;
        r = sColor.r * (1 - k) + tColor.r * k;
        g = sColor.g * (1 - k) + tColor.g * k;
        b = sColor.b * (1 - k) + tColor.b * k;
        Debug.Log(r + " " + g + " " + b + " " + k);
        tilemap.color = new Color(r, g, b);
    }

    private void OnStage(int currStage)
    {
        this.currStage = currStage;
        RendColor();
    }

}
