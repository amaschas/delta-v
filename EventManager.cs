// using UnityEngine;
using System.Collections;

public class EventManager {

	// need an optional EventArgs derived param
	public static void RaiseEvent ( EventHandler eventHandler, EventArgs args = ModuleActionArgs.Empty ) {

		// Make a copy to be thread safe
		EventHandler handler = eventHandler;

		if (handler != null) {
			handler(this,  args);
		}
	}

}