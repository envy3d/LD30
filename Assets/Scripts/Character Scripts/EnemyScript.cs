using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    public float playerAggroDist = 8;
    public float camelAggroDist = 5;
    public float attackDist = 1.2f;
    public float stealDist = 2.2f;
    public EnemySpawnerScript ems;
    public float moveSpeedX;
    public float stealFromBagTime = 0.5f;
    public bool arrivedAtTarget = true;
    public bool pause = false;

    private Transform player;
    private Transform camel;
    //private Vector3 patrolDest;
    public Vector3 targetPosition;
    public AIState state;

    private Combatant combat;
    private Animator animator;
    private LeanController controller;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        camel = GameObject.FindGameObjectWithTag("Camel").transform;
        animator = gameObject.GetComponentInChildren<Animator>();
        controller = gameObject.GetComponent<LeanController>();
        combat = gameObject.GetComponent<Combatant>();
        //patrolDest = transform.position;
        targetPosition = transform.position;

        state = AIState.Patrol;
        EnterPatrol();
    }

    void Update()
    {
        if (!pause)
        {
            if (!arrivedAtTarget)
            {
                Move();
            }
            switch (state)
            {
                case AIState.Patrol:

                    CheckPatrol();
                    if (state != AIState.Patrol)
                        ExitPatrol();
                    break;
                case AIState.Assault:

                    CheckAssault();
                    break;
                case AIState.Theft:

                    CheckTheft();
                    break;
            }
        }

    }

    public void KillCharacter()
    {
        CancelInvoke();
        Destroy(gameObject);
    }



    private void Move()
    {
        if (Mathf.Abs(targetPosition.x - transform.position.x) < 0.6f)
        {
            arrivedAtTarget = true;
            moveSpeedX = 0;
            controller.Move(0);
            return;
        }
        moveSpeedX = Mathf.Clamp((targetPosition.x - transform.position.x) * 1.0f, -1.0f, 1.0f);
        moveSpeedX = CheckBounds(moveSpeedX);
        
        controller.Move(moveSpeedX);
        //Debug.Log(moveSpeedX);
    }

    private void CheckPatrol()
    {
        if (Mathf.Abs(transform.position.x - player.position.x) < playerAggroDist)
        {
            state = AIState.Assault;
            SetTarget(player.position);
            return;
        }
        if (Mathf.Abs(transform.position.x - camel.position.x) < camelAggroDist)
        {
            state = AIState.Theft;
            SetTarget(camel.position);
            return;
        }
    }

    private void CheckAssault()
    {
        if (Mathf.Abs(transform.position.x - player.position.x) < attackDist)
        {
            SetTarget(transform.position);
            animator.SetTrigger("Attack");
            combat.Attack();
            //Attack!
            return;
        }
        if (Mathf.Abs(transform.position.x - player.position.x) < playerAggroDist)
        {
            SetTarget(player.position);
            return;
        }
        state = AIState.Patrol;
        EnterPatrol();
    }

    private void CheckTheft()
    {
        if (Mathf.Abs(transform.position.x - player.position.x) < attackDist)
        {
            state = AIState.Assault;
            SetTarget(player.position);
            return;
        }
        if (Mathf.Abs(transform.position.x - camel.position.x) < stealDist)
        {
            SetTarget(transform.position);
            animator.SetTrigger("Steal");
            Invoke("Steal", stealFromBagTime);
            Debug.Log("Steal");
            Pause();
            // Steal!!
            return;
        }
        if (Mathf.Abs(transform.position.x - camel.position.x) < camelAggroDist)
        {
            SetTarget(camel.position);
            return;
        }
        if (Mathf.Abs(transform.position.x - player.position.x) < playerAggroDist)
        {
            state = AIState.Assault;
            SetTarget(player.position);
            return;
        }
        state = AIState.Patrol;
        EnterPatrol();
    }

    private void EnterPatrol()
    {
        CreatePatrolTarget();
        //Debug.Log("Enter Patrol");
    }

    private void ExitPatrol()
    {
        CancelInvoke("CreatePatrolTarget");
    }

    private void SetTarget(Vector3 target)
    {
        targetPosition = target;
        arrivedAtTarget = false;
    }

    private float CheckBounds(float displacementX)
    {
        if (ems.spawnPoint1.x > transform.position.x + displacementX)
        {
            transform.position = new Vector3(ems.spawnPoint1.x, transform.position.y, transform.position.z);
            if (state != AIState.Patrol)
            {
                state = AIState.Patrol;
                EnterPatrol();
            }
            return Mathf.Max(0, displacementX);
        }
        else if (ems.spawnPoint2.x < transform.position.x + displacementX)
        {
            transform.position = new Vector3(ems.spawnPoint2.x, transform.position.y, transform.position.z);
            if (state != AIState.Patrol)
            {
                state = AIState.Patrol;
                EnterPatrol();
            }
            return Mathf.Min(displacementX, 0);
        }
        return displacementX;
    }

    public void CreatePatrolTarget()
    {
        if (state == AIState.Patrol)
        {
            //Debug.Log("Create Patrol");
            Invoke("CreatePatrolTarget", Random.Range(3.0f, 10.0f));
            var targetX = Random.Range(ems.spawnPoint1.x, ems.spawnPoint2.x);
            SetTarget(new Vector3(targetX, transform.position.y, transform.position.z));
        }
    }

    public void Steal()
    {
        Unpause();
        if (state == AIState.Theft && Mathf.Abs(transform.position.x - camel.position.x) < stealDist)
        {
            Debug.Log("Stole");
            GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManagerScript>().Steal();
        }
    }

    private void Pause()
    {
        pause = true;
        controller.Move(0);
    }

    private void Unpause()
    {
        pause = false;
    }

    public enum AIState
    {
        Patrol,
        Assault,
        Theft
    }

}
