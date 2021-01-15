/****************************************************
    文件：RunTimeAnimatorControllerFactory.cs
	作者：Happy-11
    日期：2021年1月14日21:24:58
	功能：动画控制器资源工厂
*****************************************************/

using System.Collections.Generic;
using UnityEngine;

public class RunTimeAnimatorControllerFactory : IBaseResourceFactory<RuntimeAnimatorController>
{
    protected Dictionary<string, RuntimeAnimatorController> factoryDict = new Dictionary<string, RuntimeAnimatorController>();
    protected string loadPath;
    
    public RunTimeAnimatorControllerFactory()
    {
        loadPath = "Study/Animator/AnimatorController/";
    }

    public RuntimeAnimatorController GetSingleResources(string resourcesPath)
    {
        RuntimeAnimatorController itemGO = null;
        string itemLoadPath = loadPath + resourcesPath;
        if (factoryDict.ContainsKey(resourcesPath))
        {
            itemGO = factoryDict[resourcesPath];
        }
        else
        {
            itemGO = Resources.Load<RuntimeAnimatorController>(itemLoadPath);
            factoryDict.Add(resourcesPath, itemGO);
        }

        if (itemGO == null)
        {
            Debug.LogWarning("资源获取失败！当前加载路径：" + itemLoadPath);
        }


        return itemGO;
    }
}