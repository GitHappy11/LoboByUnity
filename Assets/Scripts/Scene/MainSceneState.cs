/****************************************************
    文件：MainSceneState.cs
	作者：Happy-11
    日期：2021年1月10日23:03:03
	功能：主场景状态
*****************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneState : BaseSceneState 
{
    public MainSceneState(UIFacade uiFacade) : base(uiFacade)
    {

    }

    public override void EnterScene()
    {
        mUIFacade.AddPanelToDict(StringManager.MainPanel);
        mUIFacade.AddPanelToDict(StringManager.SetPanel);
        mUIFacade.AddPanelToDict(StringManager.HelpPanel);
        mUIFacade.AddPanelToDict(StringManager.GameLoadPanel);

        base.EnterScene();
        
    }

    public override void ExitScene()
    {
        base.ExitScene();
        //获取当前对象的类类型是否等于后面对比的类 typeof(类)
        //根据当前状态进行切换界面
        if (mUIFacade.currentSceneState.GetType()==typeof(NormalGameOptionState))
        {
            SceneManager.LoadScene(2);
        }
        else if (mUIFacade.currentSceneState.GetType()==typeof(BossGameOptionSceneState))
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            SceneManager.LoadScene(6);
        }
    }
}