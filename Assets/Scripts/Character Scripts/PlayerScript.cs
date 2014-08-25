using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    //public bool inputEnabled = true;
    public bool atDestination = false;

    private CharacterController controller;
    private AbstractInput input;
    private BoxCollider destination;
    private GameManagerScript gms;

	void Start()
    {
        controller = this.GetComponent<CharacterController>();
        input = this.GetComponent<AbstractInput>();
        gms = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManagerScript>();
	}
	
	void Update()
    {
        
	}

    public bool Interact()
    {
        if (gms.TryToAccessShop())
        {
            Debug.Log("Access Shop true");
            return true;
        }
        return false;
    }

    public void SetDestination(BoxCollider destination)
    {
        atDestination = false;
        this.destination = destination;
    }

    public void KillCharacter()
    {
        Destroy(gameObject);
    }

    public void Activate()
    {
        input.Enable();
    }

    public void Deactivate()
    {
        input.Disable();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.Equals(destination.collider))
        {
            atDestination = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.Equals(destination.collider))
        {
            atDestination = false;
        }
    }

}
