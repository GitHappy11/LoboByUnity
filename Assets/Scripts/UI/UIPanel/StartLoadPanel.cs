/****************************************************
    文件：StartLoadPanel.cs
	作者：Happy-11
    日期：2021年1月29日20:28:58
	功能：加载面板
*****************************************************/

using UnityEngine;

public class StartLoadPanel : BasePanel 
{
    protected override void Awake()
    {
        base.Awake();
        Invoke("LoadNextScene",2);
    }

    private void LoadNextScene()
    {
        mUIFacade.ChangeSceneState(new MainSceneState(mUIFacade));
    }


}