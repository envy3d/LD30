using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoodsListScript : MonoBehaviour
{
    public List<Good> goods;

    private static GoodsListScript goodListReference;

    void Awake()
    {
        goodListReference = this;
    }

    void Start()
    {
        //foreach (var good in this.GetComponentsInChildren<GoodScript>())
        //{
        //    goods.Add(null);
        //}
    }

    public static Good getGoodInfo(string name)
    {
        return goodListReference.goods.Find(g => g.name == name);
    }
}