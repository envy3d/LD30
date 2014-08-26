using UnityEngine;
using System.Collections;

public class PlayerInput : AbstractInput
{
    private PlayerScript player;
    private Combatant combat;
    private Animator animator;

    void Start()
    {
        base.Start();
        player = gameObject.GetComponent<PlayerScript>();
        combat = gameObject.GetComponent<Combatant>();
        animator = gameObject.GetComponentInChildren<Animator>();
	}

    void Update()
    {
        if (enabled)
        {
            controller.Move(Input.GetAxis("Horizontal"));

            if (controller.TouchingGround && Input.GetButtonDown("Jump"))
            {
                animator.SetTrigger("Jump");
                controller.Jump();
            }
            else if (Input.GetButtonDown("Action"))
            {
                if (player.Interact())
                {


                }
                else
                {
                    combat.Attack();
                    animator.SetTrigger("Attack");
                }
            }
        }
    }

}
