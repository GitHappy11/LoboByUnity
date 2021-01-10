/****************************************************
    文件：ObjectPool.cs
	作者：Happy-11
    日期：2021年1月10日23:14:13
	功能：对象池技术探究
*****************************************************/


using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour 
{
    public GameObject monster;

    //一个栈  只能从末尾取，开头放的列表
    //对象池本身
    private Stack<GameObject> monsterPool;
    //当前游戏世界里存在（激活）对象  测试使用
    private Stack<GameObject> activeMonsterList;

    private void Start()
    {
        monsterPool = new Stack<GameObject>();
        activeMonsterList = new Stack<GameObject>();
    }

    //从对象池中获取
    private GameObject GetMonster()
    {
        GameObject monsterGo = null;
        //当池子里什么都没有的时候
        if (monsterPool.Count<=0)
        {
            //生成一个（不用加入对象池，等它失活的时候自然会调回对象池）
            monsterGo = Instantiate(monster);
        }
        else
        {
            //获取一个
            monsterGo = monsterPool.Pop();
        }
        monsterGo.SetActive(true);
        return monsterGo;
    }

    //不需要的话就放回对象池
    private void PushMonster(GameObject monsterGo)
    {
        //方便观察对象，项目可不用设置
        monsterGo.transform.SetParent(transform);
        //失活
        monsterGo.SetActive(false);
        //放回对象池
        monsterPool.Push(monsterGo);
    }


    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //去对象池内取对象
            GameObject itemGo = GetMonster();
            //设置对象属性
            itemGo.transform.position = Vector3.zero;
            //将对象加入测试数组
            activeMonsterList.Push(itemGo);
        }
        else if (Input.GetMouseButton(1))
        {
            if (activeMonsterList.Count>0)
            {
                //我们不使用或者销毁对象，直接把对象扔进池子
                PushMonster(activeMonsterList.Pop());
            }
        }
    }

}