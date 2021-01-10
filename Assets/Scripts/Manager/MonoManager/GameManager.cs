/****************************************************
    文件：GameManager.cs
	作者：Happy-11
    日期：2021年1月10日21:47:33
	功能：游戏管理者
*****************************************************/

using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public PlayerManager playerManager;
    public FatoryManager fatoryManager;
    public AudioSourceManager audioSourceManager;
    public UIManager uIManager;


    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }


    private void Awake()
    {
        //切换场景不销毁该游戏对象
        DontDestroyOnLoad(gameObject);
        _instance = this;
        //实例化各种管理者 顺序实例化
        playerManager = new PlayerManager();
        fatoryManager = new FatoryManager();
        audioSourceManager = new AudioSourceManager();
        uIManager = new UIManager();
    }
}