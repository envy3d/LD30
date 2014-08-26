using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour
{
    public List<CityScript> cities;
    public EnemySpawnerScript[] enemySpawnAreas;

    public PlayerScript player;
    public CamelScript camel;
    private BagScript bagScript;
    private int currentCity = 0;
	
	void Start()
    {
        SetDestination(cities[1].name);
        bagScript = gameObject.GetComponent<BagScript>();
	}
	
	void Update()
    {
	    if (Input.GetButtonDown("Fire2"))
        {
            currentCity = currentCity == 0 ? 1 : 0;
            var t = cities[currentCity].camelDestination;
            camel.SetDestination(t);
        }
	}

    public void OpenShop()
    {
        player.Deactivate();
        ClearEnemiesFromMap();
    }

    public void CloseShop()
    {

        player.Activate();
    }

    public bool TryToAccessShop()
    {
        if (player.atDestination && camel.atDestination)
        {
            OpenShop();
            return true;
        }
        return false;
    }

    public void SetDestination(string cityName)
    {
        var city = cities.Find(c => c.name == cityName);
        camel.SetDestination(city.camelDestination);
        player.SetDestination(city.playerDestination);
    }

    public void Steal()
    {
        if (bagScript.bagGoods.Count > 0)
        {
            bagScript.RemoveGood(bagScript.bagGoods[Random.Range(0, bagScript.bagGoods.Count - 1)].id);
        }
    }

    private void ClearEnemiesFromMap()
    {
        foreach (var esa in enemySpawnAreas)
        {
            esa.Deavtivate();
        }
        foreach (var e in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(e);
        }
    }
}
