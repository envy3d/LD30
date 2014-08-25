using UnityEngine;
using System.Collections;

public abstract class IInput : MonoBehaviour
{
    protected LeanController controller;

	public void Start()
    {
        controller = this.GetComponent<LeanController>();
	}
	
	public void Update()
    {
	
	}
}
