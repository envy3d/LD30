using UnityEngine;
using System.Collections;

public class PlayerInput : AbstractInput
{
    private PlayerScript player;
    private Combatant combat;

    void Start()
    {
        base.Start();
        player = gameObject.GetComponent<PlayerScript>();
        combat = gameObject.GetComponent<Combatant>();
	}

    void Update()
    {
        if (enabled)
        {
            controller.Move(Input.GetAxis("Horizontal"));

            if (controller.TouchingGround && Input.GetButtonDown("Jump"))
            {

            }
            else if (Input.GetButtonDown("Action"))
            {
                if (player.Interact())
                {


                }
                else
                {
                    combat.Attack();
                }
            }
        }
    }

}
