let cameraRig = document.querySelector("#cameraRig");
let camera = document.querySelector("#head");

// get all the waypoints
[].forEach.call(document.querySelectorAll(".waypoint"), function(waypoint) {
  // handle gaze fusing
  waypoint.addEventListener("click", function(e) {
    // move the camera rig, not the camera, to the new location
    cameraRig.setAttribute("position", this.getAttribute("position"));
    camera.setAttribute("orientation", "0 0 0");
    console.log("[waypoint] I was clicked at: ", e.detail.intersection.point);
  });
});
