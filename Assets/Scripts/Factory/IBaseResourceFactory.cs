/****************************************************
    文件：IBaseResourceFactory.cs
	作者：Happy-11
    日期：2021年1月11日00:00:18 
	功能：游戏资源工厂接口，每种获取资源的工厂都不同，使用泛型
*****************************************************/

using UnityEngine;

public interface IBaseResourceFactory<T>  
{
    T GetSingleResources(string resourcesPath);
}