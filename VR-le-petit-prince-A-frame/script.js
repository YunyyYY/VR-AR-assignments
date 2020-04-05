let cameraRig = document.querySelector("#cameraRig");
let camera = document.querySelector("#head");

// get all the waypoints
[].forEach.call(document.querySelectorAll(".waypoint"), function(waypoint) {
  // handle gaze fusing
  waypoint.addEventListener("click", function(e) {
    // move the camera rig, not the camera, to the new location
    cameraRig.setAttribute("position", e.detail.intersection.point);
    // camera.setAttribute("orientation", "0 0 0");
    
    var audio = new Audio('//cdn.rawgit.com/michaelnebeling/src/master/button-1.mp3');
    audio.play();
    console.log("[waypoint] I was clicked at: ", e.detail.intersection.point);
  });
});
