using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAnimation1 : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator=gameObject.GetComponent<Animator>();
        // new WaitForSeconds(0.5f);
        animator.SetTrigger("Play");
    }
}
