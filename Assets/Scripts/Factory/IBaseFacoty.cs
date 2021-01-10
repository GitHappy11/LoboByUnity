/****************************************************
    文件：IBaseFacoty.cs
	作者：Happy-11
    日期：2021年1月10日23:58:32
	功能：游戏物体工厂接口
*****************************************************/

using UnityEngine;

public interface IBaseFacoty  
{
    GameObject GetItem(string itemName);

    void PushItem(string itemName, GameObject item);

}