/****************************************************
    文件：IBasePanel.cs
	作者：Happy-11
    日期：2021年1月15日18:01:46
	功能：面板接口
*****************************************************/


public interface IBasePanel  
{
    //初始化面板
    void InitPanel();
    //进入面板
    void EnterPanel();
    //退出面板
    void ExitPanel();
    //更新面板
    void UpdatePanel();
}