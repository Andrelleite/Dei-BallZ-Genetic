using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // These variables hold references to other game objects in our scene
    public GameObject cam;
    public GameObject car;
    public GameObject cameraRig;
    public GameObject ballCameraRig;
    public GameObject target;
    public GameObject ball; 

	// Update is called once per frame
	void Update () {
        SwitchTarget(); 
	}

    // LateUpdate is called after Update()
    void LateUpdate()
    {
        // call BallCam(), seen on line 40
        BallCam();
        // call CarCam(), seen on line 29
        CarCam(); 
    }

    void CarCam()
    {
        // if the target is the car, keep going
        if (target == car)
        {
            // set the camera position equal to the camera rig position
            cam.transform.position = cameraRig.transform.position;
            // rotate the camera to look at the car's position
            cam.transform.LookAt(car.transform.position);
        }
    }

    void BallCam()
    {
        // if the target is the ball, keep going
        if (target == ball)
        {
            // set the camera position equal to the ball camera rig position 
            cam.transform.position = ballCameraRig.transform.position;
            // rotate the camera to look at the ball
            cam.transform.LookAt(ball.transform.position);
        }
    }

    void SwitchTarget()
    {
        // If we detect input in the form of a left-mouse click from the player, keep going
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // if the target is the car, keep going
            if(target == car)
            {
                // make our target the ball
                target = ball; 
            }
            // if the target is NOT the car, keep going
            else
            {
                // make our target the car
                target = car; 
            }
        }
    }

}
