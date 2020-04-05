using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlaceObject: MonoBehaviour
{
    public GameObject placementIndicator;
    public GameObject objectToPlace;
    
    private ARRaycastManager arRaycastManager;
    private Pose placementPose;  // describe position and rotation of a 3D point.
    private bool placementPoseIsValid = false;
    
    // Start is called before the first frame update
    void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        Debug.Log(arRaycastManager);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (placementPoseIsValid && Input.touchCount > 0 && 
            Input.GetTouch(0).phase == TouchPhase.Began)
            // Input.touchCount checks if user has any fingers on the screen
            // Check phase of touch, if it just began
        {
            PlaceObject();
        }
    }

    private void PlaceObject()
    {
        Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
    }

    private void UpdatePlacementPose()
    {
        Vector3 screenCenter = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        Debug.Log(screenCenter);
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        Debug.Log(hits);
        // arRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);
        arRaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon);

        placementPoseIsValid = hits.Count > 0;
        Debug.Log(placementPoseIsValid);

        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
            // adjust the direction (rotation) of placement indicator the same as camera
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            // when using vectors to represent a direction, should always use the normalized version
            // this direction means y is perfectly vertical.
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }
    
    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);  // control visibility of indicator
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

}
