using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smooth;
    [SerializeField] private Vector2 minPosition;
    [SerializeField] private Vector2 maxPosition;


    private void LateUpdate()
    {
        if (target != null)
        {
            if (target.position.x!= transform.position.x||target.position.y!= transform.position.y)
            {
                Vector3 targetpos = new Vector3(target.position.x,target.position.y,transform.position.z);
                targetpos.x=Mathf.Clamp(targetpos.x,minPosition.x,maxPosition.x);
                targetpos.y=Mathf.Clamp(targetpos.y,minPosition.y,maxPosition.y);
                transform.position = Vector3.Lerp(transform.position, targetpos, smooth);
            }
        }

    }
}
