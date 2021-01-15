/****************************************************
    文件：GameFactory.cs
	作者：Happy-11
    日期：2021年1月14日21:09:47
	功能：游戏工厂
*****************************************************/

using UnityEngine;

public class GameFactory : BaseFactory 
{
    public GameFactory()
    {
        loadPath += "Game/";
    }
}