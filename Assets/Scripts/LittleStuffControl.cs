using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleStuffControl : MonoBehaviour
{
    [SerializeField] int start;
    [SerializeField] int end;
    private int currStage;
    private SpriteRenderer sr;
    void Start()
    {
        currStage = 0;
        sr = GetComponent<SpriteRenderer>();
        GameManager.Instance.GameStageForwardHandler += new GameManager.OnGameStageForward(OnStage);
    }

    private void OnStage(int currStage)
    {
        this.currStage = currStage;
        if(currStage==start)
        {
            sr.color = new Color(1, 1, 1, 1);
        }
        if(currStage==end)
        {
            sr.color = new Color(1, 1, 1, 0);
        }
    }
}
