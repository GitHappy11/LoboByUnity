/****************************************************
    文件：Dotween.cs
	作者：Happy-11
    日期：2020年12月7日20:28:31
	功能：Dotween插件使用
    小结：Dotween插件里面能改变的值其实unity自己就是可以瞬间改变，只是Dotween使用了一种动画效果的改变（曲线变）
*****************************************************/

using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DotweenTest : MonoBehaviour 
{
    private Image img;
    private Tween tw;
    private Material material;
    //Unity自带的渐变色组件
    public Gradient gradient;


    private Text txt;
    private Camera cam;

    private void Awake()
    {
        if (GetComponent<Image>()!=null)
        {
            img = GetComponent<Image>();
        }
        if (GetComponent<MeshRenderer>() != null)
        {
            material = GetComponent<MeshRenderer>().material;
        }
        if (GetComponent<Camera>()!=null)
        {
            cam = GetComponent<Camera>();
        }
        if (GetComponent<Text>()!=null)
        {
            txt = GetComponent<Text>();
        }
        txt.DOText("好好学习好好学习好好学习好好学习好好学习好好学习好好学习好好学习", 10);



    }
    private void Start()
    {

        //赋值上去的Tween会直接播放
        //tw = TransFormMove();
        

    }

    //1.Dotween的静态方法 这里是要的是Lambda表达式  
    //Lambda表达式（这里写的是定义的一个变量（名））=>函数内的代码

    //例1：Image Material 材质改变
    public void DOTweenTO()
    {
        //一个img在两秒的时间内逐渐消失
        DOTween.To(() => img.color, toColor => img.color = toColor, new Color(0, 0, 0, 0), 2f);
        //详细分解
        DOTween.To(
            () =>
            img.color, //我们想要改变的对象值 使用一个Lambda表达式存入对象
            toColor //每次Dotween经过计算得到的结果（当前值到目标值的差值）【这个结果值的命名是自己命名的（Lambda表达式）】
            => img.color = toColor, //将这个值赋值给我们要改变的对象
            new Color(0, 0, 0, 0), 2f);//目标的颜色值，完成动画的时间


        //将材质的颜色在2秒内变成红色 (img也可以用,下面的方法大部分都一样)
        material.DOColor(Color.red, 2);
        //还有一个参数，可以指定材质的某一个模块颜色 具体可以查看Material信息
        material.DOColor(Color.red, "模块名", 2);
        //清理他的颜色（变透明）
        material.DOColor(Color.clear, 2);
        //改变材质的透明度 RGBA A确实变成0了 但是并没有变透明，应该是material的特性 待学习
        material.DOFade(0, 2);
        //改变材质颜色（渐变颜色（按照组件的颜色渐变轨迹来变颜色）（并不是渐变色！！！） 参数是unity的渐变色组件，在外面调好了，然后当做变量存入方法）后面的参数自然是时间
        material.DOGradientColor(gradient, 2);
        //改变材质贴图位置
        //material.DOOffset();
        //改变材质的另一种方法用向量来存储值，然后其他的一样，反正我不会用系列
        //material.DOVector()

        //改变材质的颜色  混合色 基于Blend
        material.DOBlendableColor(Color.red, 2);
        material.DOBlendableColor(Color.green, 2);
       




    }
    //例2：transform类方法综合
    public Tween TransFormMove()
    {
        //transform方法   可以至moveX，Y,Z轴  具体看方法transform的其他方法 
        //此方法并不是匀速，而是有一个曲线的速度
        transform.DOLocalMove(new Vector2(1000, 1000), 1f);
        //旋转
        transform.DORotate(new Vector3(90, 90, 90), 10);
        //四元素旋转
        transform.DOLocalRotateQuaternion(new Quaternion(0.1f, 0.1f, 0.1f, 0.1f),2f);
        //让物体看向目标点的缓动动画
        transform.DOLookAt(Vector3.one, 3);
        //缩放 可以有x，y z 这里是变成两倍大
        transform.DOScale(Vector3.one*2, 1);
        //类似于弹弹球   
        //1  Vec 力运动方向（数字越大力越大） 
        //2 持续时间 
        //3 震动频率
        //4 0~1 这个值越大 反弹后坐力就越大（超出原本的位置） 0就是不会超出原本的位置
        Tween tw = transform.DOPunchPosition(new Vector3(0, 1, 0), 2, 2, 1f);
        //transform.DOPunchRotation();
        //transform.DOPunchScale();

        //shake 乱震 乱跑(例子：相机震动)
        //1  持续时间
        //2 Vec 力运动方向（数字越大力越大)
        //3 震动频率（次数）
        //4 震动方向
        //5 是否淡入淡出
        transform.DOShakePosition(2, Vector3.one, 10, 90);
        //transform.DOShakeRotation();
        //transform.DOShakeScale();

        //blend 混合动画 动画行为和一般的Domove没有区别，但是如果是blend方法，他们会产生1+1的效果，而不是想普通的move动画，只会执行动画效果比较大（远）的那一个
        //它是一个增量
        transform.DOBlendableLocalMoveBy(Vector3.one, 2);
        transform.DOBlendableLocalMoveBy(-/*+*/Vector3.one*2, 3);
        //上面两个动画会被混合起来 变成走3次one 持续5秒钟，当然 如果是负数的话，也会做个减法。



        

        return tw;
    }
    //例3：相机方法综合
    public void TweenCam()
    {
        //改变摄像机宽和高的比值 宽/高
        cam.DOAspect(0.5f, 1);

        //改变相机的背景颜色
        cam.DOColor(Color.red, 1f);

        //改变摄像机的近切面和远切面
        //近
        cam.DONearClipPlane(0.3f, 1);
        //远
        cam.DOFarClipPlane(1000, 1);

        //改变摄像机的视野（放大镜，倍镜效果） 投影模式---透视
        cam.DOFieldOfView(1, 5);
        //改变摄像机的视野（放大镜，倍镜效果） 投影模式---正交
        cam.DOOrthoSize(1, 5);

        //改变摄相机的像素区域  就是相机不是变成覆盖一整块屏幕了，相机的视野也会受到影响（当然如果场景内只有这个相机的话还是覆盖一整块屏幕）
        //就是给其他相机留出了空间（自己占地面积被设定了，不是全覆盖了）再加入其他规定好面积的摄像机，实现分屏效果
        //这里的方法就是把相机的像素变成了500X500 原来应该是1920X1080 ---头两位0.0是轴点，代表左下角---- 摄像机里的参数--ViewPort矩形
        cam.DOPixelRect(new Rect(0, 0, 500, 500), 2);
        //改变摄像机的像素区域（百分比模式--更好用）
        //cam.DORect()

        //摄像机视角晃动
        //1.持续时间
        //2.晃动力度
        //3.晃动频率
        //4.晃动方向
        //5.是否淡出淡入效果（默认是true 就会有一种从慢慢抖动到剧烈抖动再到慢慢恢复的过程）
        cam.DOShakePosition(10, 5, 20, 90);
    }
    //例4：Text组件
    public void TweenText()
    {
        //常规方法 具体请查看前面几个例子
        //txt.DOColor();
        //txt.DOFade();
        //txt.DOBlendableColor();

        //实现对话一个个字弹出 默认为曲线效果，最好改为匀速效果 关于Ease请看相关缓动函数相关说明
        txt.DOText("好好学习好好学习好好学习好好学习好好学习好好学习好好学习好好学习",5).SetEase(Ease.Linear);



    }


    //2.关于Tween类型的方法使用（动画的存储）
    public void  TweenTest()
    {
        //大部分动画都可以使用Tween类型创建  然后方便再次使用 使用Tween直接创建后的，这个动画并不会播放，要使用该类型的方法进行播放
        //给已经定义出来的Tween类型赋值会执行动画，使用Tween直接创建的动画不会执行
        //使用类型存储该动画 这个并不是永久存储，播放完会直接“杀死”该动画
        Tween tween = transform.DOLocalMove(new Vector2(1000, 1000), 1f);
        //关闭自动杀死动画 这样动画播放完后就不会被杀死了。然后就可以正常的使用正播倒播方法了
        tween.SetAutoKill(false);
        //动画暂停
        tween.Pause();
        //动画播放（使用这个方法，只能播放一次（相对于倒播），要和PlayerForwad区分）
        tween.Play();



        //动画正播
        tween.PlayForward();
        //动画倒播
        tween.PlayBackwards();
        //不管是直接倒着播放还是先正播再倒播。都不存在直接倒播的情况，必须正播完成后，才能进行倒播
        //不管是正播还是倒播 都可以在播放的过程中切换播放模式
        
    }

    //3.动画的事件回调
    public Tween  TweenEvent()
    {
        Tween twEvent = transform.DOLocalMoveX(200,1f);
        twEvent.SetAutoKill(false);
        //注册一个播放完的事件 这里的注册事件不属于整个动画的一部分 倒播和正播没有他们的份！！！
        twEvent.OnComplete(()=>
        {
            DOTween.To(() => img.color, toColor => img.color = toColor, new Color(0, 0, 0, 0), 1f);
            
        }
        );
        //不要把倒播和正播【不止正播，这里指所有类型正播效果的方法】写在同一时间执行的代码里，否则正播刚刚开始就被倒播，效果就是原地不动
        //twEvent.PlayBackwards();

        return twEvent;



    }

    //4.动画的缓动函数以及循环，还有次数
    //缓动函数的参考网址 https://blog.csdn.net/yy763496668/article/details/78215014
    public Tween TweenEase()
    {
        Tween tw = transform.DOMoveX(200, 2f);
        tw.SetAutoKill(false);
        //设置动画的缓动函数
        tw.SetEase(Ease.InOutBounce);
        //设置循环的次数（-1的意思是无穷次） 循环的类型  
        //Increment播放完继续播（从播放完的地方继续播  叠加播放）  Restart正播完再正播再正播（动画直接回弹的初始点）     yoyo来回进行循环（正播倒播正播倒播~~~~）
        tw.SetLoops(-1, LoopType.Incremental);

        return tw;
    }

    //5.动画队列
    public void TweenSequence()
    {
        //建立动画队列的两种方式
        Sequence sequence = DOTween.Sequence();
        //DOTween.Sequence().Append();

        //将某项动画加入队列 使用队列播放会一个个动画顺序播放
        //sequence.Append(Tween t);
        ////可以在中途插入延时
        //sequence.AppendInterval(2);
        //可以中途混合动画-----把动画加入队列的某一个动画里（动画混合）什么时候加入就是和什么时刻的动画混合
        //sequence.Join()
        //sequence.Append(Tween w);

        //把动画插入队列，
        //1.插入的时间（如果该时间已经有了相同方法动画，则会把原本的动画效果覆盖，
        //【并不是全部覆盖。覆盖了多少秒就覆盖多少秒，如果还有剩依旧会执行剩余时间的动画】）当然也可以插入到空闲的时间线内
        //如果不是相同方法的动画，会同时执行，例如边放大边移动
        //2.插入的动画 
        //sequence.Insert(2, Tween t);

        //把动画加入Append之前  后添加的会先执行，不过始终会优先Append执行
        //sequence.Prepend(Tween t);

        //队列回调函数
        //1.回调函数的时间点 (可以不写时间点，然后将此行代码插入某个Append之类的中间，也算是队列中的一员，当他队列前面的动画都执行完毕后，就会执行这个回调函数)
        //2需要执行的回调函数
        //sequence.InsertCallback(5, callback);









    }




    private void Test()
    {
        
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tw.PlayForward();
        }
        if (Input.GetMouseButtonDown(1))
        {
            tw.PlayBackwards();
        }
    }


}