/****************************************************
    文件：BaseFactory.cs
	作者：Happy-11
    日期：2021年1月14日19:45:30
	功能：游戏物体工厂基类
*****************************************************/

using System.Collections.Generic;
using UnityEngine;

public class BaseFactory : IBaseFacoty
{

    //当前拥有的GameObject类型的资源（UI，UIPanel，Game）存放的是游戏物体资源（加载的是预制体）
    protected Dictionary<string, GameObject> factoryDict = new Dictionary<string, GameObject>();
    //对象池(具体存储的游戏物体（存在于游戏中））
    protected Stack<GameObject> objectPool = new Stack<GameObject>();

    //对象池字典
    protected Dictionary<string, Stack<GameObject>> objectPoolDict = new Dictionary<string, Stack<GameObject>>();

    //加载路径
    protected string loadPath;
    public BaseFactory()
    {
        loadPath = "Study/Prefabs/";
    }
    

    
    public GameObject GetItem(string itemName)
    {
        GameObject itemGO = null;
        //查找是否有这个对象池
        if (objectPoolDict.ContainsKey(itemName))
        {
            //查看对象池内是否已经有对象
            if (objectPoolDict[itemName].Count==0)
            {
                GameObject go = GetResource(itemName);
                itemGO = GameManager.Instance.CreatItem(go);
            }
            else
            {
                itemGO = objectPoolDict[itemName].Pop();
                itemGO.SetActive(true);
            }
        }
        else
        {
            objectPoolDict.Add(itemName, new Stack<GameObject>());
            GameObject go = GetResource(itemName);
            //在游戏场景中实例化这个游戏对象
            itemGO = GameManager.Instance.CreatItem(go);
        }

        if (itemGO==null)
        {
            Debug.LogWarning(itemGO + "获取失败！");
        }
        

        
        

        return itemGO;
    }


    //放入池子的方法
    public void PushItem(string itemName, GameObject item)
    {
        item.SetActive(false);
        //放入池子的物体都放在GameManager游戏对象下
        item.transform.SetParent(GameManager.Instance.transform);
        //先看看这个对象专用的对象池有没有
        if (objectPoolDict.ContainsKey(itemName))
        {
            objectPoolDict[itemName].Push(item);
        }
        //没有
        else
        {
            Debug.LogWarning("当期物体没有" + itemName + "的栈");
        }
    }

    //取预制体资源的方法
    public GameObject GetResource(string itemName)
    {
        GameObject itemGO = null;
        string itemLoadPath = loadPath + itemName;
        if (factoryDict.ContainsKey(itemName))
        {
            itemGO = factoryDict[itemName];
        }
        else
        {
            itemGO = Resources.Load<GameObject>(itemLoadPath);
            if (itemGO!=null)
            {
                factoryDict.Add(itemName, itemGO);
            }
            else
            {
                Debug.LogWarning(itemName + "加载失败！请检查游戏资源路径！当前路径为："+itemLoadPath);
            }
        }

        return itemGO;
    }
}