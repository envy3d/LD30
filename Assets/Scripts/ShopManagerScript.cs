using UnityEngine;
using System.Collections;

public class ShopManagerScript : MonoBehaviour
{
    public GameObject buyMenu;
    public GameObject sellMenu;
    public GameObject equipMenu;
    public ShopState currentState = ShopState.Buy;

    private BagScript playerBag;

	void Start()
    {
        playerBag = GameObject.FindGameObjectWithTag("Player").GetComponent<BagScript>();
	}

    public bool Buy(string goodName)
    {
        return playerBag.AddGood(goodName);
    }

    public void Sell(string goodName)
    {
        //sellMenu.GetComponentInChildren<ShopScript>().TransactGood(goodName);
    }

}

public enum ShopState
{
    Buy, Sell, Equip
}