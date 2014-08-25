using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnerScript : MonoBehaviour
{

    public Vector3 spawnPoint1;
    public Vector3 spawnPoint2;
    public List<EnemyScript> enemyPrefabs;
    public float spawnTime = 10;

    public bool readyToSpawn = true;
    public bool active = false;
    public Transform leftCameraBound;
    public Transform rightCameraBound;

	void Start()
    {
        Transform[] camBounds = GameObject.FindGameObjectWithTag("MainCamera").GetComponentsInChildren<Transform>();
        if (camBounds[1].position.x < camBounds[2].position.x)
        {
            leftCameraBound = camBounds[1];
            rightCameraBound = camBounds[2];
        }
        else
        {
            leftCameraBound = camBounds[2];
            rightCameraBound = camBounds[1];
        }
        Transform[] bounds = gameObject.GetComponentsInChildren<Transform>();
        spawnPoint1 = bounds[1].position;
        spawnPoint2 = bounds[2].position;
        bounds[1].GetComponent<MeshRenderer>().enabled = false;
        bounds[2].GetComponent<MeshRenderer>().enabled = false;
	}
	
	
	void Update()
    {
        if (active && readyToSpawn)
        {
            readyToSpawn = false;
            Invoke("Spawn", spawnTime);
        }
	}

    public void Spawn()
    {
        readyToSpawn = true;

        float xLoc = Random.Range(spawnPoint1.x, spawnPoint2.x);
        if (xLoc > leftCameraBound.position.x && xLoc < rightCameraBound.position.x)
        {
            return;
        }
        var position = new Vector3(xLoc, transform.position.y, transform.position.z);
        var enemyIdx = Random.Range(0, enemyPrefabs.Count - 1);
        GameObject.Instantiate(enemyPrefabs[enemyIdx], position, Quaternion.identity);
    }

    public void Activate()
    {
        active = true;
    }

    public void Deavtivate()
    {
        active = false;
        CancelInvoke("Spawn");
    }
}
