using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BagScript : MonoBehaviour
{
    public int money = 10;
    public List<BagGood> bagGoods;
    public int capacity = 4;

    private int nextId = 0;
    private List<Good> baseGoods;

    void Awake()
    {
        baseGoods = GameObject.FindGameObjectWithTag("Goods List").GetComponent<GoodsListScript>().goods;
    }

    public void IncreaseCapacity(int amount)
    {
        capacity += amount;
    }

    public bool AddGood(string goodName)
    {
        if (bagGoods.Count == capacity)
        {
            return false;
        }
        var newGood = new BagGood();
        newGood.name = goodName;
        newGood.id = nextId;
        nextId += 1;

        bagGoods.Add(newGood);
        return true;
    }

    public void RemoveGood(long id)
    {
        bagGoods.RemoveAll(g => g.id == id);
    }

    public List<BagGood> CalculateGoods()
    {
        return bagGoods;
    }

    public BagGood CalculateGood(string goodName)
    {


        var bagGood = bagGoods.Find(bg => bg.name == goodName);
        var baseGood = baseGoods.Find(bg => bg.name == goodName);
        return CalculateGood(bagGood, baseGood);
    }

    public BagGood CalculateGood(BagGood bagGood, Good baseGood)
    {
        return null;
    }
}
