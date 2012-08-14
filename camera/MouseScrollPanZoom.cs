//
//Filename: MaxCamera.cs
//
// original: http://www.unifycommunity.com/wiki/index.php?title=MouseOrbitZoom
//
// --01-18-2010 - create temporary target, if none supplied at start

using UnityEngine;
using System.Collections;


public class MouseScrollPanZoom : MonoBehaviour
{
    public Transform target;
    public Vector3 targetOffset;
    public float distance = 10.0f;
    public float maxDistance = 20;
    public float minDistance = .6f;
    public float xSpeed = 200.0f;
    public float ySpeed = 200.0f;
    public int yMinLimit = -80;
    public int yMaxLimit = 80;
    public int zoomRate = 40;
    public float panSpeed = 0.3f;
    public float zoomDampening = 5.0f;

    private float x = 0.0f;
    private float y = 0.0f;
    private float xDegPrev = 0.0f;
    private float yDegPrev = 0.0f;
    // private float panSensitivity = 5.0f;

    private float currentDistance;
    private float desiredDistance;
    private Quaternion currentRotation;
    private Quaternion desiredRotation;
    private Quaternion rotation;
    private Vector3 position;

    private float scrollLeft = 120.0f;
    private float scrollRight = 120.0f;
    private float scrollUp = 120.0f;
    private float scrollDown = 120.0f;

    void Start() { Init(); }
    void OnEnable() { Init(); }

    public void Init()
    {
        //If there is no target, create a temporary target at 'distance' from the cameras current viewpoint
        if (!target)
        {
            GameObject go = new GameObject("Cam Target");
            go.transform.position = transform.position + (transform.forward * distance);
            target = go.transform;
        }

        distance = Vector3.Distance(transform.position, target.position);
        currentDistance = distance;
        desiredDistance = distance;
                
        //be sure to grab the current rotations as starting points.
        position = transform.position;
        rotation = transform.rotation;
        currentRotation = transform.rotation;
        desiredRotation = transform.rotation;
        
        // xDeg = Vector3.Angle(Vector3.right, transform.right );
        // yDeg = Vector3.Angle(Vector3.up, transform.up );
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        // Make the rigid body not change rotation
        if (rigidbody) 
            rigidbody.freezeRotation = true;
    }

    /*
     * Camera logic on LateUpdate to only update after all character movement logic has been handled. 
     */
    void LateUpdate()
    {
        // If Control and Alt and Middle button? ZOOM!
        if (Input.GetMouseButton(2) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.LeftControl))
        {
            desiredDistance -= Input.GetAxis("Mouse Y") * Time.deltaTime * zoomRate*0.125f * Mathf.Abs(desiredDistance);
        }
        // If middle mouse and left alt are selected? ORBIT
        else if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftAlt))
        {
            x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

            // y = ClampAngle(y, yMinLimit, yMaxLimit);

            rotation = Quaternion.Euler(y, x, 0);
            position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
        // otherwise if middle mouse is selected, we pan by way of transforming the target in screenspace
        else if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftControl))
        {
            float mousePosX = Input.mousePosition.x;
            float mousePosY = Input.mousePosition.y;
            if(mousePosX > scrollLeft && mousePosX < Screen.width - scrollRight && mousePosY > scrollUp && mousePosY < Screen.height - scrollDown) {
              //grab the rotation of the camera so we can move in a psuedo local XY space
              target.rotation = transform.rotation;
              target.Translate(Vector3.right * -Input.GetAxis("Mouse X") * panSpeed);
              target.Translate(transform.up * -Input.GetAxis("Mouse Y") * panSpeed, Space.World);
            }
        }

        ////////Orbit Position

        // affect the desired Zoom distance if we roll the scrollwheel
        desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
        //clamp the zoom min/max
        desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
        // For smoothing of the zoom, lerp distance
        currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * zoomDampening);

        // calculate position based on the new currentDistance 
        Vector3 newPosition = target.position - (rotation * Vector3.forward * currentDistance + targetOffset);
        transform.position = newPosition;

        Debug.DrawLine(transform.position, target.position, Color.red);
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    public float GetZoomFactor () {
        return currentDistance;
    }
}