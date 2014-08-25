using UnityEngine;
using System.Collections;

public class ShopScript : MonoBehaviour
{
    public ShopState shopType;
    public ShopButtonScript buttonPrefab;
    public CityScript city;

    private ShopManagerScript shopManager;

	void Start()
    {
        shopManager = GameObject.FindGameObjectWithTag("Shop Manager").GetComponent<ShopManagerScript>();
        Open();
	}
	
    public void Open()
    {
        var goods = city.CalculateGoodsForBuying();
        foreach (var good in goods)
        {
            ShopButtonScript button = GameObject.Instantiate(buttonPrefab) as ShopButtonScript;
            button.transform.parent = transform;
            button.Initialize(good, this);
        }
    }

    public void Close()
    {
        foreach (var shopButton in gameObject.GetComponentsInChildren<ShopButtonScript>())
        {
            Destroy(shopButton.gameObject);
        }
    }

    public void TransactGood(ShopButtonScript goodButton)
    {
        if (shopType == ShopState.Buy)
        {
            if (shopManager.Buy(goodButton.name) == false)
            {
                return;
            }
            city.BuyGood(goodButton.shopGood.name);
            var newGood = city.CalculateGoodForBuying(goodButton.shopGood.name);
            if (newGood.quantity == 0)
            {
                Destroy(goodButton.gameObject);
                return;
            }

            UpdateGood(goodButton, newGood);
        }
        else if (shopType == ShopState.Sell)
        {
            city.SellGood(goodButton.shopGood.name);
            var newGood = city.CalculateGoodForSelling(goodButton.shopGood.name);

            UpdateGood(goodButton, newGood);
        }

    }

    public void SetCity(CityScript city)
    {
        this.city = city;
    }

    private void UpdateGood(ShopButtonScript goodButton, ShopGood shopGood)
    {
        goodButton.Initialize(shopGood, this);
    }
}
