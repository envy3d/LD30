using UnityEngine;
using System.Collections;

public abstract class AbstractInput : MonoBehaviour
{
    public bool enabled = true;
    protected LeanController controller;

	public void Start()
    {
        controller = this.GetComponent<LeanController>();
	}

    public void Enable()
    {
        enabled = true;
    }

    public void Disable()
    {
        enabled = false;
    }
}
