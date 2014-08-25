using UnityEngine;
using System.Collections;

public class PlayerInput : IInput
{

	void Start()
    {
        base.Start();
	}
	
	void Update()
    {
        base.Update();

        controller.Move(Input.GetAxis("Horizontal"));

        if (controller.TouchingGround && Input.GetButtonDown("Jump"))
        {

        }
        else if (Input.GetButtonDown("Action"))
        {

        }

	}
}
