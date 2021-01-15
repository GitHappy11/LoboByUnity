/****************************************************
    文件：StartLoadSceneState.cs
	作者：Happy-11
    日期：2021年1月10日22:57:09
	功能：开始加载场景状态
*****************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLoadSceneState : BaseSceneState
{
    public StartLoadSceneState(UIFacade uiFacade) : base(uiFacade)
    {

    }

    public override void EnterScene()
    {
        mUIFacade.AddPanelToDict(StringManager.StartLoadPanel);
        base.EnterScene();
        
    }

    public override void ExitScene()
    {
        base.ExitScene();
        SceneManager.LoadScene(1);
    }
}