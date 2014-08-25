using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour
{
    public bool TouchingGround { get { return touchingGround; } }

    private bool touchingGround = false;

	void Start()
    {
	
	}
	
	void Update()
    {
	    
	}

    public void Move(float amount)
    {
        transform.position = new Vector3(transform.position.x + amount,
                                         transform.position.y,
                                         transform.position.z);
    }

    public void Jump()
    {

    }

    public void Action()
    {

    }
}
