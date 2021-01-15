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
    public FactoryManager factoryManager;
    public AudioSourceManager audioSourceManager;
    public UIManager uiManager;


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
        factoryManager = new FactoryManager();
        audioSourceManager = new AudioSourceManager();
        uiManager = new UIManager();
        //进入第一个场景
        uiManager.mUIFacade.currentSceneState.EnterScene();
    }

    public GameObject CreatItem(GameObject itemGO)
    {
        GameObject go = Instantiate(itemGO);
        return go;
    }

    //获取Sprite资源
    public Sprite GetSprite(string path)
    {
        return factoryManager.spriteFactory.GetSingleResources(path);
    }

    //获取AudioCLip资源
    public AudioClip GetAudioClip(string path)
    {
        return factoryManager.audioClipFactory.GetSingleResources(path);
    }

    //获取动画控制器
    public RuntimeAnimatorController GetRuntimeAnimatorController(string path)
    {
        return factoryManager.runTimeAnimatorControllerFactory.GetSingleResources(path);
    }

    //获取游戏物体
    public GameObject GetGameObjectResource(FactoryType factoryType,string itemName)
    {
        return factoryManager.factoryDict[factoryType].GetItem(itemName); 
    }

    //将游戏物体放入对象池
    public void PushGameObjectResourceToFactory(FactoryType factoryType, string itemName,GameObject itemGO)
    {
         factoryManager.factoryDict[factoryType].PushItem(itemName,itemGO);
    }
}