using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopButtonScript : MonoBehaviour {

	public ShopGood shopGood;
    public ShopScript shop;

    public void Initialize(ShopGood shopGood, ShopScript shop)
    {
        this.shopGood = shopGood;
        transform.Find("Name").GetComponent<Text>().text = shopGood.name;
        transform.Find("Price").GetComponent<Text>().text = "Cost: " + shopGood.price;
        transform.Find("Quantity").GetComponent<Text>().text = "x" + shopGood.quantity;
        this.shop = shop;
    }

    public void Click()
    {
        shop.TransactGood(this);
    }

}
