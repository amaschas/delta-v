using UnityEngine;
using System.Collections;

public interface ShipInterface {
  string Name ();
  void Select ();
  void Deselect ();
  void ActivateModule (ModuleInterface module);
  void AddCurrentActionToQueue ();
  void RunQueue ();
  void Reorient (Quaternion lookRotation, float rate);
  void AddThrust (int thrust);
  Vector3 GetVelocity ();
  Vector3 GetTransformPosition ();
}
