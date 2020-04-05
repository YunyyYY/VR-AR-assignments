using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSound : MonoBehaviour
{
    private float _y;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        _y = 0;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y>0 && transform.position.y > 0 && _y < 0)
        {
            source.Play();
            Debug.Log("yes");
        }
        _y = transform.position.y;
    }
}
