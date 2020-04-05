using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class rotate : MonoBehaviour
{
    // Start is called before the first frame update
    public double _alpha;
    public float _radius;
    public float _x_center;
    public float _y_center;
    public float _z_center;
    public int speed;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
//        transform.RotateAround(transform.position, Vector3.up, 30 * Time.deltaTime);
        _alpha +=  speed * 0.1 * Time.deltaTime;
        double x = _radius * Math.Cos(_alpha);
        double y = _radius * Math.Sin(_alpha);
        this.transform.position = new Vector3(_x_center + (float)x, _y_center + (float)y, _z_center);
//        Debug.Log(Time.deltaTime);
    }
}
