using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavesBornControl : MonoBehaviour
{
    [SerializeField] LeavesControl lc;
    [SerializeField] Transform player;
    [SerializeField] Vector2 bias;
    [SerializeField] float width;
    [SerializeField] float delTime;
    [SerializeField] Vector2 speedRange;
    [SerializeField] Vector2 angleRange;

    private float timer;
    private void Start()
    {
        timer = 0;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer<0)
        {
            timer = delTime;

            Vector3 calcpos;
            if(player==null)
            {
                calcpos = Vector3.zero + new Vector3(bias.x, bias.y, 0);
            }
            else
            {
                calcpos = player.position + new Vector3(bias.x, bias.y, 0);
            }
            float Xbias = Random.Range(-width / 2, width / 2);
            calcpos.x += Xbias;
            float calcAngle = Random.Range(Mathf.PI + angleRange.x, Mathf.PI + angleRange.y);
            Vector2 dir = new Vector2(Mathf.Cos(calcAngle), Mathf.Sin(calcAngle));
            float calcV = Random.Range(speedRange.x, speedRange.y);
            LeavesControl newLeave = Instantiate<LeavesControl>(lc, calcpos, Quaternion.identity);

            newLeave.SetMove(dir, calcV);
        }
    }
}
