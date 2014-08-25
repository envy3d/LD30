using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    public bool inputEnabled = true;

    private CharacterController controller;
    private IInput input;

	void Start()
    {
        controller = this.GetComponent<CharacterController>();
        input = this.GetComponent<IInput>();
	}
	
	void Update()
    {
        
	}
}
