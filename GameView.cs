using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameView : MonoBehaviour {

  public Material lineMaterial;

  private GameController gameController;
  private VectorLine velocityIndicator;
  private Vector3[] linePoints = new Vector3[500];

  // Use this for initialization
  void Start () {
    gameController = GameObject.Find("GameController").GetComponent<GameController>();
    Color lineColor = Color.green;
    float lineWidth = 2.0f;
    float capLength = 0.0f;
    int lineDepth = 0;
    LineType lineType = LineType.Discrete;
    Joins joins = Joins.None;
    linePoints[0] = Vector3.zero;
    // headingIndicator = new VectorLine("headingLine", linePoints, Color.white, lineMaterial, lineWidth, lineType, joins);
    VectorLine.SetLineParameters(lineColor, lineMaterial, lineWidth, capLength, lineDepth, lineType, joins);
    velocityIndicator = VectorLine.MakeLine ("velocityVector", linePoints);
  }
  
  void OnGUI () {
    if(GUI.Button(new Rect(Screen.width - 105, Screen.height - 25, 100, 20), "Play Actions")) {
      // Each queue has to run in parallel, maybe use sendmessage?
      // gameController.SendMessage("RunQueue"); <- doesn't work
      // Maybe trigger the queues by unfreezing the objects?
      Time.timeScale = 1;
      foreach(KeyValuePair<string, ShipControllerInterface> pair in gameController.ships) {
        pair.Value.RunQueue();
      }
      gameController.turnPlaying = true;
    }
  }

  // Update is called once per frame
  void Update () {
    if(Input.GetMouseButtonDown(0)) {
      RaycastHit hit = new RaycastHit();
      Ray ray =  new Ray();
      ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast (ray, out hit)) {
        gameController.SelectShip(hit.transform.gameObject);
      }
    }
    int i = 0;
    foreach(KeyValuePair<string, ShipControllerInterface> pair in gameController.ships) {
      linePoints[i] = pair.Value.GetTransformPosition();
      // velocityIndicator.minDrawIndex = i;
      i++;
      linePoints[i] = pair.Value.GetVelocity();
      // velocityIndicator.maxDrawIndex = i;
      i++;
    }
    velocityIndicator.Draw();
  }
}
