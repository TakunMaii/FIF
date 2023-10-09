using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float parallax;
    [SerializeField] private Transform Player;

    float startZ;
    private Vector2 startPosition;
    private Vector2 travel => (Vector2)cam.transform.position - startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 bias = new Vector2(0, 2);
        Vector3 newpos = startPosition + travel * parallax + bias;
        transform.position = new Vector3(newpos.x, newpos.y, startZ);
    }

    }
}
