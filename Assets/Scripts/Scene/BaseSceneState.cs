/****************************************************
    文件：BaseSceneState.cs
	作者：Happy-11
    日期：2021年1月10日22:50:36
	功能：场景状态基类
*****************************************************/



public class BaseSceneState : IBaseSceneState
{
    protected UIFacade mUIFacade;

    public BaseSceneState(UIFacade uiFacade)
    {
        mUIFacade = uiFacade;
    }
    public virtual void EnterScene()
    {
        //获取当前场景所有的UIPanel，存入字典并实例化
        mUIFacade.InitDict();
    }

    public virtual void ExitScene()
    {
        //清理UI 并放入对象池
        mUIFacade.ClearDict();
    }
}