// using UnityEngine;
using System.Collections;

// Utility class for raising events
public class EventManager {

	// need an optional EventArgs derived param
	public static void RaiseEvent ( EventHandler eventHandler, EventArgs args = EventArgs.Empty ) {

		// Make a copy to be thread safe
		EventHandler handler = eventHandler;

		// Raise the event if not null
		if (handler != null) {
			handler(this,  args);
		}
	}

}