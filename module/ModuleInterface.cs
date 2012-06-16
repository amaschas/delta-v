using UnityEngine;
using System.Collections;

public interface ModuleInterface {
  string Name ();
  ModuleInterface ActivateView ();
  void DeactivateView ();
  bool HasAction ();
  ModuleAction GetAction ();
  void Run (ModuleAction action);
  bool IsRunning ();
}
