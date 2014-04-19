using UnityEngine;
using System.Collections;

public interface ShipControllerInterface {

  // Ship name
  string Name ();

  // Activates ship UI on mouse click
  void Select ();

  // Deactivates ship UI on mouse click off
  void Deselect ();

  // re-orients the ship model
  void Reorient (Quaternion lookRotation, float rate);

  // adds thrust to the ship
  void AddThrust (object sender, ModuleActionArgs args);

  // Returns ship velocity
  Vector3 GetVelocity ();

  // Returns the ship position
  Vector3 GetTransformPosition ();
}
