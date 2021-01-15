/****************************************************
    文件：UIFacade.cs
	作者：Happy-11
    日期：2021年1月14日22:33:38
	功能：UI中介,上层与管理者做交互，下层与UI面板做交互
*****************************************************/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIFacade 
{
    //管理者
    private UIManager mUIManager;
    private GameManager mGameManager;
    private AudioSourceManager mAudioSourceManager;
    private PlayerManager mplayerManager;

    //UI面板
    public Dictionary<string, IBasePanel> currentScenePanelDict = new Dictionary<string, IBasePanel>();

    //其他成员变量
    private GameObject mask;
    private Image maskImage;
    public Transform canvasTransform;

    //场景状态
    public IBaseSceneState currentSceneState;
    public IBaseSceneState lastSceneState;

    public UIFacade(UIManager uIManager)
    {
        mGameManager = GameManager.Instance;
        mplayerManager = mGameManager.playerManager;
        mUIManager = uIManager;
        mAudioSourceManager = mGameManager.audioSourceManager;
    }

    //初始化遮罩（加载界面）
    public void InitMask()
    {
        canvasTransform = GameObject.Find("Canvas").transform;
        //mask = mGameManager.factoryManager.factoryDict[FactoryType.UIFactory].GetItem("imgMask");
        mask = GetGameObjectResource(FactoryType.UIFactory, "imgMask");
        maskImage = mask.GetComponent<Image>();
    }
    //显示遮罩
    public void ShowMask()
    {
        Tween t = DOTween.To(() => maskImage.color, toColor => maskImage.color = toColor, new Color(0, 0, 0, 1),2f);
        //动画结束后回调函数
        t.OnComplete(ExitSceneComplete);
    }
    //离开当前场景
    private void ExitSceneComplete()
    {
        lastSceneState.ExitScene();
        currentSceneState.EnterScene();      
    }
    //隐藏遮罩
    public  void HideMask()
    {
        //不需要回调事件 动画完毕即销毁
        DOTween.To(() => maskImage.color, toColor => maskImage.color = toColor, new Color(0, 0, 0, 0), 2f);
    }
    //更改当前场景的状态
    public void ChangeSceneState(IBaseSceneState baseSceneState)
    {
        //切换场景  现在变成上一个
        lastSceneState = currentSceneState;
        ShowMask();
        currentSceneState = baseSceneState;
    }

    //获取资源
    public Sprite GetSprite(string path)
    {
        return mGameManager.GetSprite(path);
    }

    public AudioClip GetAudioClip(string path)
    {
        return mGameManager.GetAudioClip(path);
    }

    public RuntimeAnimatorController GetRuntimeAnimatorController(string path)
    {
        return mGameManager.GetRuntimeAnimatorController(path);
    }
    //获取游戏物体
    public GameObject GetGameObjectResource(FactoryType factoryType, string itemName)
    {
        return mGameManager.GetGameObjectResource(factoryType, itemName);
    }

    //将游戏物体放入对象池
    public void PushGameObjectResource(FactoryType factoryType, string itemName, GameObject itemGO)
    {
        mGameManager.PushGameObjectResourceToFactory(factoryType, itemName, itemGO); 
    }

    //添加UIPanel到UIManager字典里
    public void AddPanelToDict(string uiPanelName)
    {
        mUIManager.currentScenePanelDict.Add(uiPanelName, GetGameObjectResource(FactoryType.UIPanelFactory, uiPanelName));
    }

    //实例化当前所有面板，并存入字典
    public void InitDict()
    {
        foreach (var item in mUIManager.currentScenePanelDict)
        {
            //遍历的是键值对，要的是值
            item.Value.transform.SetParent(canvasTransform);
            item.Value.transform.localPosition = Vector3.zero;
            item.Value.transform.localScale = Vector3.one;
            IBasePanel basePanel = item.Value.GetComponent<IBasePanel>();
            if (basePanel==null)
            {
                Debug.LogWarning("获取面板IBasePanel脚本失败！" + basePanel);              
            }
            basePanel.InitPanel();
            currentScenePanelDict.Add(item.Key, basePanel);
        }
    }

    //清空UIPanel字典
    public void ClearDict()
    {
        //清空当前界面的UIPanel字典
        currentScenePanelDict.Clear();
        //将字典内的游戏对象推入对象池后清空。
        mUIManager.ClearDict();
        
    }

    //实例化UI
    public GameObject CreateUIAndSetUIPosition(string uiName)
    {
        GameObject itemGO=GetGameObjectResource(FactoryType.UIFactory,uiName);
        itemGO.transform.SetParent(canvasTransform);
        itemGO.transform.localPosition = Vector3.zero;
        itemGO.transform.localScale = Vector3.one;
        return itemGO;
    }
    
    

}