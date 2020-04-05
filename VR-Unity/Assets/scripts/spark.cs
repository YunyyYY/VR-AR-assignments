using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spark : MonoBehaviour {

	public float x;
    public int count;
    public int period;

	// Use this for initialization
	void Start () {
		count = 0;
		// period = period;
	}
	
	// Update is called once per frame
	void Update () {
		// Widen the object by x, y, and z values
        count += 1;
        if (count == period) {
        	transform.localScale += new Vector3(x, x, x);
        	x = -x;
        	count = 0;
        }
	}
}
