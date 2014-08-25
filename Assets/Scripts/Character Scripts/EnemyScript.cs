using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    public float playerAggroDist = 8;
    public float camelAggroDist = 5;
    public float attackDist = 1.2f;
    public float stealDist = 2;

    private Transform player;
    private Transform camel;
    private Vector3 patrolDest;
    private Vector3 targetPosition;
    private AIState state;

    private LeanController controller;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        camel = GameObject.FindGameObjectWithTag("Camel").transform;
        controller = gameObject.GetComponent<LeanController>();
        state = AIState.Patrol;
        targetPosition = transform.position;
    }

    void Update()
    {

        switch (state)
        {
            case AIState.Patrol:
                CheckPatrol();

                break;
            case AIState.Assault:
                CheckAssault();

                break;
            case AIState.Theft:
                CheckTheft();

                break;
        }

    }

    public void KillCharacter()
    {
        Destroy(gameObject);
    }

    private void CheckPatrol()
    {
        if (Mathf.Abs(transform.position.x - player.position.x) < playerAggroDist)
        {
            state = AIState.Assault;
            targetPosition = player.position;
            return;
        }
        if (Mathf.Abs(transform.position.x - camel.position.x) < camelAggroDist)
        {
            state = AIState.Theft;
            targetPosition = camel.position;
            return;
        }
    }

    private void CheckAssault()
    {
        if (Mathf.Abs(transform.position.x - player.position.x) < attackDist)
        {
            targetPosition = player.position;
            //Attack!
            return;
        }
        if (Mathf.Abs(transform.position.x - player.position.x) < playerAggroDist)
        {
            targetPosition = player.position;
            return;
        }
        state = AIState.Patrol;
        // Set up patrol route
    }

    private void CheckTheft()
    {
        if (Mathf.Abs(transform.position.x - player.position.x) < attackDist)
        {
            state = AIState.Assault;
            targetPosition = player.position;
            return;
        }
        if (Mathf.Abs(transform.position.x - camel.position.x) < stealDist)
        {
            targetPosition = camel.position;
            // Steal!!
            return;
        }
        if (Mathf.Abs(transform.position.x - camel.position.x) < camelAggroDist)
        {
            targetPosition = camel.position;
            return;
        }
        if (Mathf.Abs(transform.position.x - player.position.x) < playerAggroDist)
        {
            state = AIState.Assault;
            targetPosition = player.position;
            return;
        }
        state = AIState.Patrol;
    }

    private enum AIState
    {
        Patrol,
        Assault,
        Theft
    }

}
