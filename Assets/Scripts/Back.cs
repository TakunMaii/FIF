using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float parallax;
    [SerializeField] private Transform Player;
    [SerializeField] Color sColor;
    [SerializeField] Color tColor;
    private int currStage;

    float startZ;
    private Vector2 startPosition;
    private Vector2 travel => (Vector2)cam.transform.position - startPosition;
    private SpriteRenderer sr;

    void Start()
    {
        startPosition = transform.position;
        startZ = transform.position.z;
        currStage = 0;
        sr = GetComponent<SpriteRenderer>();
        GameManager.Instance.GameStageForwardHandler += new GameManager.OnGameStageForward(OnStage);

        RendColor();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 bias = new Vector2(0, 2);
        Vector3 newpos = startPosition + travel * parallax + bias;
        transform.position = new Vector3(newpos.x, newpos.y, startZ);
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
    }
}
