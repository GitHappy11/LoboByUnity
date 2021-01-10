/****************************************************
    文件：SlideCanCoverScrollView.cs
	作者：Happy-11
    日期：2020年12月25日22:56:08
	功能：ScrollView功能扩展
*****************************************************/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class SlideCanCoverScrollView : MonoBehaviour,IBeginDragHandler,IEndDragHandler
{
    private float contentLength;//容器长度

    //需要的翻书时鼠标的位置
    private float beginMousePostionX;
    private float endMousePostionX;

    private ScrollRect scrollRect;
    private float lastProportion=0;//上一个位置比例

    public int cellLength; //每个单元格的长度
    public int spacing;//间隙
    public int leftOffset;//左偏移量
    private float upperLimit;//上限值
    private float lowerLimit;//下限值
    private float firstItemLength;//移动第一个单元格的距离
    private float oneItemLength;//滑动一个单元格需要的距离
    private float oneItemProportion;//滑动一个单元格所占的比例

    public int totalItemNum;//一共有几个单元格
    private int currentIndex;//当前单元格索引

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        //contentLength=Content的容器总长度-左边的边-在减去每一个单元格长度
        contentLength = scrollRect.content.rect.xMax  ;
        Debug.Log(scrollRect.content.rect.xMax );
        firstItemLength = cellLength / 2 + leftOffset;
        oneItemLength = cellLength + spacing;
        oneItemProportion = oneItemLength / contentLength;
        upperLimit = 1 - firstItemLength / contentLength;
        lowerLimit = firstItemLength / contentLength;
        currentIndex = 1;
        //每次进入的时候，菜单显示第一个
        scrollRect.horizontalNormalizedPosition = 0;
    }

    //当鼠标开始拖拽的时候
    public void OnBeginDrag(PointerEventData eventData)
    {
        beginMousePostionX = Input.mousePosition.x;

    }
    //当鼠标结束拖拽的时候
    public void OnEndDrag(PointerEventData eventData)
    {

        //鼠标起始位置和终止位置的坐标差值
        float offSetX = 0;
        //这边拿到的坐标是世界坐标
        endMousePostionX = Input.mousePosition.x;
        offSetX = (beginMousePostionX - endMousePostionX) * 2;

        //Debug.Log("offSetX:"+offSetX);
        //Debug.Log("firstItemLength:" + firstItemLength);
        //滑动先决条件
        //取绝对值   执行滑动动作的前提是要大于第一个需要滑动的距离
        if (Mathf.Abs(offSetX) > firstItemLength)
        {
            if (offSetX > 0)//右滑
            {
                //如果右滑选中的目标超出了总共的数量，直接return  防止超出
                if (currentIndex >= totalItemNum)
                {
                    return;
                }
                //当次可以移动的单元格数目
                int moveCount = (int)((offSetX - firstItemLength) / oneItemLength) + 1;
                //更新当前单元格索引
                Debug.Log(moveCount);
                currentIndex += moveCount;
                //如果大于（超出了）
                if (currentIndex >= totalItemNum)
                {
                    //回弹到最大值
                    currentIndex = totalItemNum;
                }
                //当次需要移动的比例:上一次已经存在的单元格位置的比例加上这一次需要去移动的比例
                lastProportion += oneItemProportion * moveCount;
                //如果大于上限值，直接回弹，让图标处于正中心。
                if (lastProportion >= upperLimit)
                {
                    lastProportion = 1;
                }

            }
            else//左滑
            {
                //如果左滑的目标超出了第一个，那就回弹回第一个
                if (currentIndex <= 1)
                {
                    return;
                }
                //当次可以移动的单元格数目 左滑  获得的值是负值 所以后面的数值可以都是+号
                int moveCount = (int)((offSetX + firstItemLength) / oneItemLength) - 1;
                //更新当前单元格索引 
                currentIndex += moveCount;
                //如果大于（超出了）
                if (currentIndex <= 1)
                {
                    //回弹到第一个
                    currentIndex = 1;
                }
                //当次需要移动的比例:上一次已经存在的单元格位置的比例加上这一次需要去移动的比例
                lastProportion += oneItemProportion * moveCount;
                //如果小于于下限值，直接回弹，让图标处于正中心。
                if (lastProportion <= lowerLimit)
                {
                    lastProportion = 0;
                }
            }
        }

        DOTween.To(() => scrollRect.horizontalNormalizedPosition, lerpValue => scrollRect.horizontalNormalizedPosition = lerpValue, lastProportion, 0.5f).SetEase(Ease.OutQuint);

        Debug.Log(oneItemProportion);
        



    }

}