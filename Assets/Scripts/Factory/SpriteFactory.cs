/****************************************************
    文件：SpriteFactory.cs
	作者：Happy-11
    日期：2021年1月14日21:27:25
	功能：精灵工厂
*****************************************************/

using System.Collections.Generic;
using UnityEngine;

public class SpriteFactory : IBaseResourceFactory<Sprite> 
{
    protected Dictionary<string, Sprite> factoryDict = new Dictionary<string, Sprite>();
    protected string loadPath;

    public SpriteFactory()
    {
        loadPath = "Pictures/";
    }

    public Sprite GetSingleResources(string resourcesPath)
    {
        Sprite itemGO = null;
        string itemLoadPath = loadPath + resourcesPath;
        if (factoryDict.ContainsKey(resourcesPath))
        {
            itemGO = factoryDict[resourcesPath];
        }
        else
        {
            itemGO = Resources.Load<Sprite>(itemLoadPath);
            factoryDict.Add(resourcesPath, itemGO);
        }

        if (itemGO == null)
        {
            Debug.LogWarning("资源获取失败！当前加载路径：" + itemLoadPath);
        }


        return itemGO;
    }
}