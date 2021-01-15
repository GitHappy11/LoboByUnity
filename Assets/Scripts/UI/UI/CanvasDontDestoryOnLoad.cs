/****************************************************
    文件：CanvasDontDestoryOnLoad.cs
	作者：Happy-11
    日期：2021年1月16日00:39:30
	功能：防止Canvas在切换场景的过程中被销毁
*****************************************************/

using UnityEngine;

public class CanvasDontDestoryOnLoad : MonoBehaviour 
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}