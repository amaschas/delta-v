using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EngineController : ModuleController {

	// public event EventHandler<ModuleActionArgs> StartEngine;

	// public void RegisterShip ( ShipControllerInterface ship ) {
	// 	StartEngine += ship.AddThrust();
	// }

	public void Run (ModuleAction action) {
		// This needs to happen in a fixed update, maybe kill this run structure entirely, and just run fixedupdate in each module?
		// or maybe it makes sense for each ship to have one fixed update call that triggers each discrete module action
		// For reading the delta time it is recommended to use Time.deltaTime instead because it automatically returns the right delta time if you are inside a FixedUpdate function or Update function.
		// http://docs.unity3d.com/Documentation/ScriptReference/Time-fixedDeltaTime.html

		// We need an event here, all modules need to be able to fire events back at the ship
		transform.parent.gameObject.GetComponent<ShipControllerInterface>().AddThrust(engine.thrust * time.deltaTime);
	}
	
}
