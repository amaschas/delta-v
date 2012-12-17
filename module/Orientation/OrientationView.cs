using UnityEngine;
using System;
using System.Collections;

public class OrientationView : MonoBehaviour {

  public VectorLine headingIndicator;
  public VectorLine orientationGridLine;
  public Material lineMaterial;

  private OrientationController orientationController;
  private float pitch = 0.0f;
  private float yaw = 0.0f;
  private Vector3[] linePoints = new Vector3[500];
  private Vector3[] gridPoints = new Vector3[1000];
  // Camera.main should update this via event
  private float cameraDistance;

  // This responds to the event dispatcher in Orientation
  public void ModuleStateChangeListener(GameObject e) {
    DrawHeadingIndicator(e.transform.position, e.transform.forward);
    DrawHeadingGrid(e.transform);
    // This should also tell the controller to update the ship action queue
  }

	void Start () {
    transform.GetComponent<Orientation>().StateChange += ModuleStateChangeListener;
    orientationController = gameObject.GetComponent<OrientationController>();
    
    // Defaults for HeadingLine and OrientationGrid VectorLine objects
    // Should maybe go in their own init method
    Color lineColor = Color.white;
    float lineWidth = 1.0f;
    float capLength = 0.0f;
    int lineDepth = 0;
    LineType lineType = LineType.Discrete;
    Joins joins = Joins.None;
    linePoints[0] = Vector3.zero;
    gridPoints[0] = Vector3.zero;
    VectorLine.SetLineParameters(lineColor, lineMaterial, lineWidth, capLength, lineDepth, lineType, joins);
    headingIndicator = VectorLine.MakeLine (transform.parent.name + "HeadingLine", linePoints, Color.green);
    orientationGridLine = VectorLine.MakeLine (transform.parent.name + "OrientationGrid", gridPoints);
	}

	void LateUpdate () {
    if(Input.GetMouseButton(0)) {
      orientationController.SetOrientation(Input.GetAxis("Mouse X") * 45, Input.GetAxis("Mouse Y") * 45);
    }
	}

  // Camera zoom changes should message the Selected Ship and the GameController
  void DrawHeadingIndicator (Vector3 orientationPosition, Vector3 orientationForward) {
    linePoints[0] = orientationPosition;
    // This stuff is intended to handle special zoom cases for the grid. Currently disabled.
    if(GetDistanceToCamera() > 500) {
    //   headingIndicator.Draw();
      linePoints[1] = orientationPosition + orientationForward * 200;
    }
    else {
    //   headingIndicator.Draw3D();
      linePoints[1] = orientationPosition + orientationForward * 200;
    }
    headingIndicator.Draw3DAuto();
  }

  // This should be named DrawOrientationGrid for clarity
  void DrawHeadingGrid (Transform origin) {
    cameraDistance = GetDistanceToCamera();
    if(cameraDistance > 500.0f && !orientationGridLine.active) {
      Debug.Log("enabling grid");
      orientationGridLine.active = true;
      SetVectorGrid(origin);
      orientationGridLine.Draw3DAuto();
    }
    else if (cameraDistance < 500.0f) {
      orientationGridLine.active = false;
    }
    else {
      SetVectorGrid(origin);
      orientationGridLine.Draw3DAuto();
    }
  }

  // This ultimately needs to happen when the camera sends a message
  float GetDistanceToCamera () {
    return Camera.main.GetComponent<MouseScrollPanZoom>().GetZoomFactor();
  }

  void SetVectorGrid (Transform origin) {
    int width = 500;
    int height = 500;
    int interval = 25;
    int index = 0;
    for(int x = 0 - width / 2; x <= width / 2; x += interval) {
      gridPoints[index++] = origin.TransformPoint(new Vector3((float) x, 0f, (float) height / 2));
      gridPoints[index++] = origin.TransformPoint(new Vector3((float) x, 0f, (float) 0 - height / 2));
    }
    for(int z = 0 - height / 2; z <= height / 2; z += interval) {
      gridPoints[index++] = origin.TransformPoint(new Vector3((float) height / 2, 0f, (float) z));
      gridPoints[index++] = origin.TransformPoint(new Vector3((float) 0 - height / 2, 0f, (float) z));
    }
  }

}