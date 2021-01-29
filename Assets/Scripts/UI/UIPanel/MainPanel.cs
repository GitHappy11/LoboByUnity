/****************************************************
    文件：MainPanel.cs
	作者：Happy-11
    日期：2021年1月29日22:05:47
	功能：主界面
*****************************************************/

using UnityEngine;
using DG.Tweening;

public class MainPanel : BasePanel 
{
    private Animator carrotAnimator;
    private Transform monsterTrans;
    private Transform cloudTrans;
    private Tween[] mainPanelTweens;//0.右移 1.左移
    private Tween ExitTween;


    protected override void Awake()
    {
        base.Awake();
        //获取成员变量，设置层级
        transform.SetSiblingIndex(8);
        carrotAnimator = transform.Find("Emp_Carrot").GetComponent<Animator>();
        carrotAnimator.Play("CarrotGrow");
        monsterTrans = transform.Find("Img_Monster");
        cloudTrans = transform.Find("Img_Cloud");

        //设置动画
        mainPanelTweens[0] = transform.DOLocalMoveX(1920, 0.5f);
        //关闭自动回收 重复使用
        mainPanelTweens[0].SetAutoKill(false);
        mainPanelTweens[0].Pause();

        mainPanelTweens[1] = transform.DOLocalMoveX(-1920, 0.5f);
        mainPanelTweens[1].SetAutoKill(false);
        mainPanelTweens[1].Pause();

        PlayerUITweent();

    }

    public override void EnterPanel()
    {
        carrotAnimator.Play("CarrotGrow");
        if (ExitTween!=null)
        {
            ExitTween.PlayBackwards();
        }
        cloudTrans.gameObject.SetActive(true);
    }
    public override void ExitPanel()
    {
        ExitTween.PlayForward();
        cloudTrans.gameObject.SetActive(false);
    }

    //UI动画播放
    public void PlayerUITweent()
    {
        monsterTrans.DOLocalMoveX(550, 2f).SetLoops(-1, LoopType.Yoyo);
        cloudTrans.DOLocalMoveX(1300, 8f).SetLoops(-1, LoopType.Restart);
    }

    public void MoveToRigh()
    {
        ExitTween = mainPanelTweens[0];
        mUIFacade.currentScenePanelDict[StringManager.SetPanel].EnterPanel();
    }

    public void MoveToLeft()
    {
        ExitTween = mainPanelTweens[1];
        mUIFacade.currentScenePanelDict[StringManager.HelpPanel].EnterPanel();
    }

    //场景场景切换的方法
    public void ToNormalModeScene()
    {
        mUIFacade.currentScenePanelDict[StringManager.GameBossOptionPanel].EnterPanel();
        mUIFacade.ChangeSceneState(new NormalGameOptionState(mUIFacade));
    }

    public void ToBossModelScene()
    {
        mUIFacade.currentScenePanelDict[StringManager.GameBossOptionPanel].EnterPanel();
        mUIFacade.ChangeSceneState(new BossModelSceneState(mUIFacade));
    }

    public void ToMonsterNestScene()
    {
        mUIFacade.currentScenePanelDict[StringManager.GameBossOptionPanel].EnterPanel();
        mUIFacade.ChangeSceneState(new MonstarNastSceneState(mUIFacade));
    }
    
    public void ExitGame()
    {
        //退出程序
        Application.Quit();
    }
}