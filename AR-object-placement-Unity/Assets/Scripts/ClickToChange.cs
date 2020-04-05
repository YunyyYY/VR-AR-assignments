using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ClickToChange : MonoBehaviour
{
    public GameObject Object1;
    public GameObject Object2;

    int objectName = 0;
    private GameObject placedObject;
    
    // Start is called before the first frame update
    void Start()
    {
        placedObject = Instantiate(Object1, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            ChangeObject();
        }
    }

    private void ChangeObject()
    {
        Destroy(placedObject);
        if (objectName == 0)
        {
            objectName = 1;
            placedObject = Instantiate(Object2, transform.position, transform.rotation);
        }
        else
        {
            objectName = 0;
            placedObject = Instantiate(Object1, transform.position, transform.rotation);
        }
    }
}
