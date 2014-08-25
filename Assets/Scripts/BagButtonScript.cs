using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BagButtonScript : MonoBehaviour {

	public BagGood bagGood;
    public BagShopScript bagShop;

    public void Initialize(BagGood bagGood, BagShopScript bagShop)
    {
        this.bagGood = bagGood;
        transform.Find("Name").GetComponent<Text>().text = bagGood.name;
        this.bagShop = bagShop;
    }

    public void Click()
    {
        bagShop.TransactGood(this);
    }

}
