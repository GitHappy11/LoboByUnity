/****************************************************
    文件：BasePanel.cs
	作者：Happy-11
    日期：2021年1月15日18:04:53
	功能：基类面板处理
*****************************************************/



public class BasePanel : IBasePanel
{
    protected UIFacade mUIFacade;

    public virtual void InitPanel()
    {
        
    }

    public virtual void UpdatePanel()
    {
        
    }

    public virtual void EnterPanel()
    {

    }

    public virtual void ExitPanel()
    {

    }

    protected virtual void  Awake()
    {
        mUIFacade = GameManager.Instance.uiManager.mUIFacade;
    }
}