using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavesControl : MonoBehaviour
{
    private Vector2 dirc = new Vector2(-1,-1);
    private float v = 0.5f;
    public void SetMove(Vector2 dir,float speed)
    {
        dirc = dir;
        v = speed;
    }

    private void Update()
    {
        transform.position += new Vector3(dirc.x, dirc.y, 0) * Time.deltaTime * v;
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
