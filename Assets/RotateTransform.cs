using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTransform : MonoBehaviour
{
    public Vector3 axis;
    public Transform _transform;
    public float speed = 12;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _transform.RotateAround(transform.position, axis, speed * Time.deltaTime);
    }
}
