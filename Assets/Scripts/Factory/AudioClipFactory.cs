/****************************************************
    文件：AudioClipFactory.cs
	作者：Happy-11
    日期：2021年1月14日21:12:54
	功能：音频资源工厂
*****************************************************/

using UnityEngine;
using System.Collections.Generic;

public class AudioClipFactory : IBaseResourceFactory<AudioClip>
{
    protected Dictionary<string, AudioClip> factoryDict = new Dictionary<string, AudioClip>();
    protected string loadPath;
    public AudioClipFactory()
    {
        loadPath = "AudioClips/";
    }

    public AudioClip GetSingleResources(string resourcesPath)
    {
        AudioClip itemGO = null;
        string itemLoadPath = loadPath + resourcesPath;
        if (factoryDict.ContainsKey(resourcesPath))
        {
            itemGO = factoryDict[resourcesPath];
        }
        else
        {
            itemGO = Resources.Load<AudioClip>(itemLoadPath);
            factoryDict.Add(resourcesPath, itemGO);
        }

        if (itemGO==null)
        {
            Debug.LogWarning("资源获取失败！当前加载路径：" + itemLoadPath);
        }
        

        return itemGO;
    }
}