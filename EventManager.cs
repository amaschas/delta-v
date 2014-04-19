// using UnityEngine;
using System.Collections;

// Good use case for a global singleton here

// Utility class for raising events
public class EventManager {

	// will EventArgs work here polymorphically?
	public static void RaiseEvent ( EventHandler eventHandler, EventArgs args = EventArgs.Empty ) {

		// Make a copy to be thread safe
		EventHandler handler = eventHandler;

		// Raise the event if not null
		if (handler != null) {
			handler(this,  args);
		}
	}

}