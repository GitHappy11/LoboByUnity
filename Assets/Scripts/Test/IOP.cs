/****************************************************
    文件：IOP.cs
	作者：Happy-11
    日期：2021年1月10日22:09:39
	功能：面向接口编程
*****************************************************/

using UnityEngine;

public class IOP : MonoBehaviour 
{
    private void Start()
    {
        IHero heroFromLeblanc = new Leblanc();
        heroFromLeblanc.SkillQ();
        IHero heroFromZed = new Zed();
        heroFromZed.SkillQ();
    }
}
//以LOL来探究接口编程
public interface IHero
{
    void SkillQ();
    void SKillW();
    void SKillE();
    void SKillR();
}

public class Leblanc : IHero
{
    public void SKillE()
    {
        Debug.Log(ToString()+":释放E技能");
    }

    public void SkillQ()
    {
        Debug.Log(ToString()+":释放Q技能");
    }

    public void SKillR()
    {
        Debug.Log(ToString()+":释放R技能");
    }

    public void SKillW()
    {
        Debug.Log(ToString()+":释放W技能");
    }
}

public class Zed : IHero
{
    public void SKillE()
    {
        Debug.Log(ToString() + ":释放E技能");
    }

    public void SkillQ()
    {
        Debug.Log(ToString() + ":释放Q技能");
    }

    public void SKillR()
    {
        Debug.Log(ToString() + ":释放R技能");
    }

    public void SKillW()
    {
        Debug.Log(ToString() + ":释放W技能");
    }
}
