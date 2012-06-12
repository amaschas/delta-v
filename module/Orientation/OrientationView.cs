using UnityEngine;
using System.Collections;

public class OrientationView : MonoBehaviour {

  public Material lineMaterial;

  private OrientationController orientationController;
  private float pitch = 0.0f;
  private float yaw = 0.0f;
  private VectorLine headingIndicator;
  private Vector3[] linePoints = new Vector3[500];

  private GameObject sphere;

	void Start () {
    orientationController = gameObject.GetComponent<OrientationController>();
    Color lineColor = Color.white;
    float lineWidth = 2.0f;
    float capLength = 0.0f;
    int lineDepth = 0;
    LineType lineType = LineType.Discrete;
    Joins joins = Joins.None;
    linePoints[0] = Vector3.zero;
    // headingIndicator = new VectorLine("headingLine", linePoints, Color.white, lineMaterial, lineWidth, lineType, joins);
    VectorLine.SetLineParameters(lineColor, lineMaterial, lineWidth, capLength, lineDepth, lineType, joins);
    headingIndicator = VectorLine.MakeLine ("headingLine", linePoints);

    // sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
	}

	void LateUpdate () {
    // Vector3 topLeft = new Vector3 (100, Camera.main.pixelHeight - 100, 10);
    // Vector3 origin = Camera.main.ScreenToWorldPoint(topLeft);
    // sphere.transform.position = origin;
    DrawHeadingIndicator();
	}

  void OnGUI () {
    yaw = GUI.HorizontalSlider(new Rect(5, Screen.height - 20, 100, 20), yaw, -180.0f, 180.0f);
    pitch = GUI.HorizontalSlider(new Rect(5, Screen.height - 40, 100, 20), pitch, -180.0f, 180.0f);
    orientationController.SetOrientation(yaw, pitch);
  }

  void DrawHeadingIndicator () {
    linePoints[0] = transform.position;
    linePoints[1] = orientationController.orientation.GetTrueForward() * 3;
    headingIndicator.Draw();
  }
}
