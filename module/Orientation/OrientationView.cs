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

  // *******************
  // private class ModuleStateChangeListener {
  //Might not need a class here, because the event delegate holds a function
    public void ModuleStateChangeListener(GameObject e) {
      // Debug.Log("Orientation Changed");
      // Debug.Log(e.name);
      DrawHeadingIndicator(e.transform.position, e.transform.forward);
      // DrawHeadingGrid(e.transform.parent);
      DrawHeadingGrid(e.transform);
    }
  // }
  // *******************

	void Start () {
    transform.GetComponent<Orientation>().StateChange += ModuleStateChangeListener;
    orientationController = gameObject.GetComponent<OrientationController>();
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

  void OnGUI () {
    // yaw = GUI.HorizontalSlider(new Rect(5, Screen.height - 20, 100, 20), yaw, -180.0f, 180.0f);
    // pitch = GUI.HorizontalSlider(new Rect(5, Screen.height - 40, 100, 20), pitch, -180.0f, 180.0f);
    // orientationController.SetOrientation(yaw, pitch);
  }

  // DrawHeadingIndicator should be a response to an OrientationChange Event in Orientation
  // Camera zoom changes should also message the Selected Ship and the GameController
  void DrawHeadingIndicator (Vector3 orientationPosition, Vector3 orientationForward) {
    linePoints[0] = orientationPosition;
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

  void DrawHeadingGrid (Transform origin) {
    cameraDistance = GetDistanceToCamera();
    // double scaling = Math.Round(cameraDistance, 0, MidpointRounding.ToEven) * 0.1;
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