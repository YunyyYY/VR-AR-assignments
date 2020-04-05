using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SelectObjectMenu : MonoBehaviour
{
    public GameObject placementIndicator;
    public GameObject instructions;
    
    public GameObject Left1;
    public GameObject Lmid;
    public GameObject Rmid;
    public GameObject Right1;
    
    public GameObject LeftButton;
    public GameObject LmidButton;
    public GameObject RmidButton;
    public GameObject RightButton;
    
    ARRaycastManager arRaycastManager;
    bool planeIsDetected;
    Pose placementPose;
    GameObject objectToPlace;

    void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        objectToPlace = Left1;

        LeftButton.GetComponent<Button>().onClick.AddListener(delegate { SwitchObjectToPlace(LeftButton.name); });
        LmidButton.GetComponent<Button>().onClick.AddListener(delegate { SwitchObjectToPlace(LmidButton.name); });
        RmidButton.GetComponent<Button>().onClick.AddListener(delegate { SwitchObjectToPlace(RmidButton.name); });
        RightButton.GetComponent<Button>().onClick.AddListener(delegate { SwitchObjectToPlace(RightButton.name); });
    }


    void Update()
    {
        UpdatePlacementPose();
        UpdateUI();
        UpdatePlacementIndicator();

        if (planeIsDetected && Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject(Input.GetTouch(0));
        }
    }

    private void UpdatePlacementPose()
    {
        Vector3 screenCenter = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        arRaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon);

        planeIsDetected = hits.Count > 0;
        if (planeIsDetected)
        {
            placementPose = hits[0].pose;
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    private void UpdatePlacementIndicator()
    {
        if (planeIsDetected)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }


    private void PlaceObject(Touch touch)
    {
        if (!IsPointerOverUIObject(touch))
        {
            Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
        }

    }

    private void SwitchObjectToPlace(string buttonName)
    {
        Debug.Log(buttonName);
        switch(buttonName)
        {
            case "Left":
                objectToPlace = Left1;
                break;
            case "Lmid":
                objectToPlace = Lmid;
                break;
            case "Rmid":
                objectToPlace = Rmid;
                break;
            case "Right":
                objectToPlace = Right1;
                break;
        }
    }

    // Helper function
    public static bool IsPointerOverUIObject(Touch touch)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(touch.position.x, touch.position.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    void UpdateUI()
    {
        if (planeIsDetected)
        {
            instructions.GetComponent<Text>().text = "Tap anywhere to place selected object\nUse one finger to drag and two fingers to rotate";
            LeftButton.SetActive(true);
            LmidButton.SetActive(true);
            RmidButton.SetActive(true);
            RightButton.SetActive(true);
        }
        else
        {
            instructions.GetComponent<Text>().text = "Move device around to find a plane";
            LeftButton.SetActive(false);
            LmidButton.SetActive(false);
            RmidButton.SetActive(false);
            RightButton.SetActive(false);
        }
    }

}
