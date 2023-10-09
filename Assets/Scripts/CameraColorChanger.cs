using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColorChanger : MonoBehaviour
{
    [SerializeField] Color sColor;
    [SerializeField] Color tColor;
    int currStage;
    Camera cam;
    void Start()
    {
        currStage = 0;
        
        cam = GetComponent<Camera>();
        RendColor();
        GameManager.OnGameStageForward ogsf = new GameManager.OnGameStageForward(OnStage);
        GameManager.Instance.GameStageForwardHandler += ogsf;
    }

    void Update()
    {
        
    }

    private void RendColor()
    {
        float k = 1.0f * currStage / GameManager.Instance.GetMaxStage();
        float r, g, b;
        r = sColor.r * (1 - k) + tColor.r * k;
        g = sColor.g * (1 - k) + tColor.g * k;
        b = sColor.b * (1 - k) + tColor.b * k;
        cam.backgroundColor = new Color(r, g, b);
    }

    private void OnStage(int currStage)
    {
        this.currStage = currStage;
        RendColor();
    }
}
