using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CityScript : MonoBehaviour
{

    public string language;
    public string currency;

    public List<CityGood> cityGoods;

    private List<Good> baseGoods;

    void Awake()
    {
        baseGoods = GameObject.FindGameObjectWithTag("Goods List").GetComponent<GoodsListScript>().goods;
    }

	void Start()
    {
        

	}
	
	void Update()
    {
	
	}

    public void EconomyTick()
    {

    }

    public void SellGood(string goodName)
    {
        var cityGood = cityGoods.Find(cg => cg.name == goodName);
        cityGood.quantity += 1;
        cityGood.quantity = cityGood.quantity <= 99 ? cityGood.quantity : 99;
    }

    public void BuyGood(string goodName)
    {
        var cityGood = cityGoods.Find(cg => cg.name == goodName);
        cityGood.quantity -= 1;
        cityGood.quantity = cityGood.quantity >= 0 ? cityGood.quantity : 0;
    }

    public List<ShopGood> CalculateGoodsForSelling()
    {
        List<ShopGood> shopGoods = new List<ShopGood>();

        foreach (var baseGood in baseGoods)
        {
            var cityGood = cityGoods.Find(cg => cg.name == baseGood.name);
            shopGoods.Add(CalculateGoodForBuying(cityGood, baseGood));
        }

        return shopGoods;
    }

    public List<ShopGood> CalculateGoodsForBuying()
    {
        List<ShopGood> shopGoods = new List<ShopGood>();

        foreach (var baseGood in baseGoods)
        {
            var cityGood = cityGoods.Find(cg => cg.name == baseGood.name);
            if (cityGood.quantity > 0)
            {
                shopGoods.Add(CalculateGoodForSelling(cityGood, baseGood));
            }
        }

        return shopGoods;
    }

    public ShopGood CalculateGoodForSelling(string goodName)
    {
        var cityGood = cityGoods.Find(cg => cg.name == goodName);
        var baseGood = baseGoods.Find(bg => bg.name == goodName);
        return CalculateGoodForBuying(cityGood, baseGood);
    }

    public ShopGood CalculateGoodForBuying(string goodName)
    {
        var cityGood = cityGoods.Find(cg => cg.name == goodName);
        var baseGood = baseGoods.Find(bg => bg.name == goodName);
        return CalculateGoodForSelling(cityGood, baseGood);
    }

    public ShopGood CalculateGoodForSelling(CityGood cg, Good bg)
    {
        var shopGood = new ShopGood();
        shopGood.name = bg.name;
        shopGood.price = bg.value * cg.multiplier - 0.5f;
        shopGood.quantity = cg.quantity;
        return shopGood;
    }

    public ShopGood CalculateGoodForBuying(CityGood cg, Good bg)
    {
        var shopGood = new ShopGood();
        shopGood.name = bg.name;
        shopGood.price = bg.value * cg.multiplier + 0.5f;
        shopGood.quantity = cg.quantity;
        return shopGood;
    }
}
