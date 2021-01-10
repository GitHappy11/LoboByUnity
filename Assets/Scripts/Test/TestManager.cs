/****************************************************
    文件：TestManager.cs
	作者：Happy-11
    日期：2021年1月10日21:33:33
	功能：测试管理者
*****************************************************/

using UnityEngine;

public class TestManager : SingletonTemplate<TestManager> 
{
    private void Start()
    {
        Debug.Log(Instance);
    }
}