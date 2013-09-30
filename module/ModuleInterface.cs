using UnityEngine;
using System.Collections;

public interface ModuleInterface {
  // string name { get; }
  // ModuleInterface ActivateView ();
  // void DeactivateView ();
  string Name();
  bool HasAction ();
  ModuleAction GetAction ();
  void Run (ModuleAction action);
  bool IsRunning ();
}
