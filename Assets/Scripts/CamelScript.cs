using UnityEngine;
using System.Collections;

public class CamelScript : MonoBehaviour
{
    public Transform destination;
    public bool atDestination = false;

    private LeanController controller;

    void Start()
    {
        controller = gameObject.GetComponent<LeanController>();
    }

    void Update()
    {
        if (!atDestination)
        {
            controller.Move(destination.transform.position.x - transform.position.x);
            if (destination.transform.position.x - transform.position.x < 0.5f &&
                destination.transform.position.x - transform.position.x > -0.5f)
            {
                controller.Move(0);
                atDestination = true;
            }
        }

    }

    public void SetDestination(Transform dest)
    {
        destination = dest;
        atDestination = false;
    }

}

    /*
    public GameObject player;
    public SphereCollider visionSphere;
    public int ticksToForgetEnemy = 100;

    private LeanController controller;
    private int enemiesNearby = 0;
    private bool enemyNearby = false;

	void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = gameObject.GetComponent<LeanController>();
	}
	
	void Update()
    {
        if (enemiesNearby == 0)
        {
            controller.Move(player.transform.position.x - transform.position.x);
        }
        else
        {
            controller.Move(0);
        }

        ForgetEnemy();
	}

    public void AcknowledgeEnemy()
    {
        enemiesNearby = ticksToForgetEnemy;
        enemyNearby = true;
    }

    public void ForgetEnemy()
    {
        enemiesNearby = enemiesNearby > 0 ? enemiesNearby - 1 : 0;
    }
}*/
