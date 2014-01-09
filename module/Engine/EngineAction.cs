using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EngineAction : ModuleAction {

	public float duration;

	public void EngineAction( ModuleInterface moduleController, float duration );

	void Start () {
		
		heatGeneration = duration * HeatRate();
	}

}