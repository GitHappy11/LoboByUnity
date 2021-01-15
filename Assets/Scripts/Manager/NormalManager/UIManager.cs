/****************************************************
    文件：UIManager.cs
	作者：Happy-11
    日期：2021年1月10日21:52:33
	功能：负责管理UI
*****************************************************/

using System.Collections.Generic;
using UnityEngine;

public class UIManager 
{
    public UIFacade mUIFacade;
    public Dictionary<string, GameObject> currentScenePanelDict = new Dictionary<string, GameObject>();
    private GameManager mGameManager;

    public UIManager()
    {
        mGameManager = GameManager.Instance;
        currentScenePanelDict = new Dictionary<string, GameObject>();
        mUIFacade = new UIFacade();
    }


}