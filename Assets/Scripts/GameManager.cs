using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager Instance;
    [SerializeField]
    private GameObject player;
    public GameObject GetPlayer() => player;
    [SerializeField]
    private PlayerAnimator playerAnimator;
    public PlayerAnimator GetPlayerAnimator() => playerAnimator;
    private int gameStage;
    [SerializeField]
    private int maxStage;
    [SerializeField]
    private int summerStage;
    [SerializeField]
    private GameObject leavesControl;
    public int GetMaxStage() => maxStage;

    [SerializeField]
    Image gameLose;
    [SerializeField]
    Image gameWin;
    [SerializeField]
    bool isGameOver;
    [SerializeField]
    GameObject acornPrefab;
    [SerializeField]
    Vector3 acornPos;

    // 是不是在游戏中，只有在游戏中玩家才能操作
    private bool isGaming;
    public bool GetIsGaming() => isGaming;

    //每次stage前进时候的响应事件
    public delegate void OnGameStageForward(int gameStage);
    public event OnGameStageForward GameStageForwardHandler;

    public void StageForward()
    {
        isGaming = false;
        player.GetComponent<PlayerController>().Reset();
        if (gameStage >= maxStage)
        {
            OnStageEnd();
            return;
        }
        if(gameStage==summerStage)
            leavesControl.SetActive(true);
        gameStage++;
        PlayTurning();
        Instantiate(acornPrefab, acornPos, Quaternion.identity);
        GameStageForwardHandler?.Invoke(gameStage);
    }

    public void OnPlayerDied()
    {
        Debug.Log("player die");
        AudioManager.Ins.PlayBGM("lose", 0.5f, false);
        isGameOver = true;
        isGaming = false;
        player.SetActive(false);
        gameLose.gameObject.SetActive(true);
    }

    //当所有的stage都走完了
    private void OnStageEnd()
    {
        Debug.Log("stage end");
        AudioManager.Ins.PlayBGM("win", 0.5f, false);
        isGameOver = true;
        isGaming = false;
        player.SetActive(false);
        gameWin.gameObject.SetActive(true);
    }

    //播放转场动画
    private void PlayTurning()
    {
        Debug.Log("play turning");
        AudioManager.Ins.PlayBGM("GAP", 0.2f, false);
        CameraControl.Ins.PlayTurning();
        StartCoroutine(TurningAnimation());
    }

    private IEnumerator TurningAnimation()
    {
        yield return new WaitForSeconds(3.6f);
        CameraControl.Ins.ReturnTheView();
        isGaming = true;
        if (gameStage <= summerStage)
            AudioManager.Ins.PlayBGM("summer");
        else
            AudioManager.Ins.PlayBGM("fall");
        Destroy(GameObject.FindWithTag("SceneAnimation"));
    }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        isGameOver = false;
        gameStage = 0;
        isGaming = true;
        AudioManager.Ins.Init();
        AudioManager.Ins.PlayBGM("summer");
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.P))
        {
            StageForward();
        }
#endif
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScene");
        }
        if (isGameOver && Input.anyKeyDown)
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
