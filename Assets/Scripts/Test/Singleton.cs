/****************************************************
    文件：Singleton.cs
	作者：Happy-11
    日期：2021年1月10日21:19:11
	功能：单例模式探究
*****************************************************/

using UnityEngine;

public class Singleton : MonoBehaviour 
{

    //饿汉式单例模式（调用到这个类的时候马上就示例化，不管它存不存在）
    //没继承Mono的单例
    //private static Singleton _instance=new Singleton();

    //private static Singleton _instance;

    //public static Singleton Instance
    //{
    //    get
    //    {
    //        return _instance;
    //    }
    //}

    //private void Awake()
    //{
    //    _instance = this;
    //}

    //懒汉式单例 (不存在的时候再去实例化)
    private static Singleton _instance;
    public static Singleton Instance
    {
        get
        {
            if (_instance==null)
            {
                _instance = new Singleton();
            }
            return _instance;
        }
    }




}


//单例模板                        确认类型    继承Mono         必须继承Mono才能用这个模板
public abstract class SingletonTemplate<T>:MonoBehaviour where T:MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            return _instance;
        }

       
    }

    private void Awake()
    {
        _instance = this as T;
    }

}