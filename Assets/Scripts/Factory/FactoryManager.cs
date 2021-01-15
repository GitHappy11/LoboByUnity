/****************************************************
    文件：FactoryManager.cs
	作者：Happy-11
    日期：2021年1月14日22:54:27
	功能：工厂管理，负责管理各种类型的工厂以及对象池
*****************************************************/

using System.Collections.Generic;
using UnityEngine;

public class FactoryManager 
{
    public Dictionary<FactoryType, IBaseFacoty> factoryDict = new Dictionary<FactoryType, IBaseFacoty>();
    public AudioClipFactory audioClipFactory;
    public SpriteFactory spriteFactory;
    public RunTimeAnimatorControllerFactory runTimeAnimatorControllerFactory;

    public FactoryManager()
    {
        factoryDict.Add(FactoryType.UIPanelFactory, new UIPanlFactory());
        factoryDict.Add(FactoryType.UIFactory, new UIFactory());
        factoryDict.Add(FactoryType.GameFactory, new GameFactory());

        audioClipFactory = new AudioClipFactory();
        spriteFactory = new SpriteFactory();
        runTimeAnimatorControllerFactory = new RunTimeAnimatorControllerFactory();
        
    }
}