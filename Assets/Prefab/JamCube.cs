using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamCube : MonoBehaviour
{
    [SerializeField] string sRes;
    [SerializeField] string tRes;
    [SerializeField] int jammyStage;
    private SpriteRenderer sr;
    private GameManager.OnGameStageForward ogsf;
    private bool isJam = false;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ogsf = new GameManager.OnGameStageForward(OnStage);
        GameManager.Instance.GameStageForwardHandler += ogsf;

        sr.sprite = (Sprite)Resources.Load(sRes);
    }

    private void OnStage(int currStage)
    {
        if (currStage == jammyStage)
        {
            sr.sprite = (Sprite)Resources.Load(tRes);
            isJam = true;
        }
    }
}
