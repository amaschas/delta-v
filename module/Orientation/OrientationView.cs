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
    public void ModuleStateChangeListener(object o, EventArgs e) {
      Debug.Log("Orientation Changed");
      DrawHeadingIndicator(e.orientationPosition, e.orientationForward);
    }
  // }
  // *******************

	void Start () {
    orientationController = gameObject.GetComponent<OrientationController>();
    Color lineColor = Color.white;
    float lineWidth = 2.0f;
    float capLength = 0.0f;
    int lineDepth = 0;
    LineType lineType = LineType.Discrete;
    Joins joins = Joins.None;
    linePoints[0] = Vector3.zero;
    gridPoints[0] = Vector3.zero;
    VectorLine.SetLineParameters(lineColor, lineMaterial, lineWidth, capLength, lineDepth, lineType, joins);
    headingIndicator = VectorLine.MakeLine (transform.parent.name + "HeadingLine", linePoints);
    orientationGridLine = VectorLine.MakeLine (transform.parent.name + "OrientationGrid", gridPoints);

    // So presumably, this subscribes OrientationView's listener method to Orientation's state change event
    // *******************
    // Not using a class to hold the listener
    // ModuleStateChangeListener mscl = new ModuleStateChangeListener();
    transform.GetComponent<Orientation>().StateChange += ModuleStateChangeListener;
    // *******************
	}

	void LateUpdate () {
    // Debug.Log(GetDistanceToCamera());
    DrawHeadingIndicator();
    DrawHeadingGrid();
	}

  void OnGUI () {
    yaw = GUI.HorizontalSlider(new Rect(5, Screen.height - 20, 100, 20), yaw, -180.0f, 180.0f);
    pitch = GUI.HorizontalSlider(new Rect(5, Screen.height - 40, 100, 20), pitch, -180.0f, 180.0f);
    orientationController.SetOrientation(yaw, pitch);
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
    headingIndicator.Draw();
  }

  void DrawHeadingGrid () {
    cameraDistance = GetDistanceToCamera();
    double scaling = Math.Round(cameraDistance, 0, MidpointRounding.ToEven) * 0.1;
    if(cameraDistance > 500.0f && !orientationGridLine.active) {
      Debug.Log("enabling grid");
      orientationGridLine.active = true;
      SetVectorGrid(scaling);
      orientationGridLine.Draw();
    }
    else if (cameraDistance < 500.0f) {
      orientationGridLine.active = false;
    }
    else {
      SetVectorGrid(scaling);
      orientationGridLine.Draw();
    }
  }

  float GetDistanceToCamera () {
    return Camera.main.GetComponent<MouseScrollPanZoom>().GetZoomFactor();
  }

  void SetVectorGrid (double scaling) {
    int width = (int) scaling * 10;
    int height = (int) scaling * 10;
    int interval = (int) scaling;
    Debug.Log(width);
    int index = 0;
    for(int x = 0 - width / 2; x <= width / 2; x += interval) {
      gridPoints[index++] = transform.parent.TransformPoint(new Vector3((float) x, 0f, (float) height / 2));
      gridPoints[index++] = transform.parent.TransformPoint(new Vector3((float) x, 0f, (float) 0 - height / 2));
    }
    for(int z = 0 - height / 2; z <= height / 2; z += interval) {
      gridPoints[index++] = transform.parent.TransformPoint(new Vector3((float) height / 2, 0f, (float) z));
      gridPoints[index++] = transform.parent.TransformPoint(new Vector3((float) 0 - height / 2, 0f, (float) z));
    }
  }

}