using UnityEngine;
using System.Collections;

public class renderToForeground : MonoBehaviour {
	
	void Start ()
	{
		// Set the sorting layer of the particle system.
		particleSystem.renderer.sortingLayerName = "foreground";
		particleSystem.renderer.sortingOrder = 2;
	}
}