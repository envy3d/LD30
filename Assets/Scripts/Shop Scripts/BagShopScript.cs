using UnityEngine;
using System.Collections;

public class BagShopScript : MonoBehaviour
{
    public ShopState shopType;
    public BagButtonScript buttonPrefab;
    public BagScript bag;

    private ShopManagerScript shopManager;

	void Start()
    {
        shopManager = GameObject.FindGameObjectWithTag("Shop Manager").GetComponent<ShopManagerScript>();
        Open();
	}
	
    public void Open()
    {
        var goods = bag.CalculateGoods();
        foreach (var good in goods)
        {
            BagButtonScript button = GameObject.Instantiate(buttonPrefab) as BagButtonScript;
            button.transform.parent = transform;
            button.Initialize(good, this);
        }
    }

    public void Close()
    {
        foreach (var shopButton in gameObject.GetComponentsInChildren<BagButtonScript>())
        {
            Destroy(shopButton.gameObject);
        }
    }

    public void TransactGood(BagButtonScript goodButton)
    {
        if (shopType == ShopState.Buy)
        {
            //if (shopManager.Buy(goodButton.name) == false)
            //{
            //    return;
            //}
            bag.AddGood(goodButton.bagGood.name);
        }
        else if (shopType == ShopState.Sell)
        {
            bag.RemoveGood(goodButton.bagGood.id);
            Destroy(goodButton.gameObject);
        }

    }

    public void SetBag(BagScript bag)
    {
        this.bag = bag;
    }

    private void UpdateGood(BagButtonScript goodButton, BagGood bagGood)
    {
        goodButton.Initialize(bagGood, this);
    }
}
