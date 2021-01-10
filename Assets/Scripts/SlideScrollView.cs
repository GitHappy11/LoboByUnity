/****************************************************
    文件：SlideScrollView.cs
	作者：Happy-11
    日期：2021年1月8日21:23:53
	功能：单选翻书效果
*****************************************************/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class SlideScrollView : MonoBehaviour,IBeginDragHandler,IEndDragHandler
{
    private RectTransform contentTrans;
    private float beginMousePositionX;
    private float endMousePositionX;
    private ScrollRect scrollRect;

    public int cellLength;
    public int spacing;
    public int leftOffset;
    private float moveOneItemLength;

    private Vector3 currentContentLocalPos;//上一次的位置
    private Vector3 contentInitPos;//Content初始位置
    private Vector2 contentTransSize;//Content初始大小

    public int totalItemNum;
    private int currentIndex;

    public Text pageText;

    public bool needSendMessage;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        contentTrans = scrollRect.content;
        moveOneItemLength = cellLength + spacing;
        currentContentLocalPos = contentTrans.localPosition;
        contentTransSize = contentTrans.sizeDelta;
        contentInitPos = contentTrans.localPosition;
        currentIndex = 1;
        if (pageText != null)
        {
            pageText.text = currentIndex.ToString() + "/" + totalItemNum;
        }
    }

    public void Init()
    {
        currentIndex = 1;

        if (contentTrans != null)
        {
            contentTrans.localPosition = contentInitPos;
            currentContentLocalPos = contentInitPos;
            if (pageText != null)
            {
                pageText.text = currentIndex.ToString() + "/" + totalItemNum;
            }
        }
    }

    /// <summary>
    /// 通过拖拽与松开来达成翻页效果
    /// </summary>
    /// <param name="eventData"></param>

    public void OnEndDrag(PointerEventData eventData)
    {
        endMousePositionX = Input.mousePosition.x;
        float offSetX = 0;
        float moveDistance = 0;//当次需要滑动的距离
        offSetX = beginMousePositionX - endMousePositionX;

        if (offSetX > 0)//右滑
        {
            if (currentIndex >= totalItemNum)
            {
                return;
            }
     

            moveDistance = -moveOneItemLength;
            currentIndex++;
        }
        else//左滑
        {
            if (currentIndex <= 1)
            {
                return;
            }
      
            moveDistance = moveOneItemLength;
            currentIndex--;
        }
        if (pageText != null)
        {
            pageText.text = currentIndex.ToString() + "/" + totalItemNum;
        }
        DOTween.To(() => contentTrans.localPosition, lerpValue => contentTrans.localPosition = lerpValue, currentContentLocalPos + new Vector3(moveDistance, 0, 0), 0.5f).SetEase(Ease.OutQuint);
        currentContentLocalPos += new Vector3(moveDistance, 0, 0);
        //只能存在于此项目
     
    }





    public void OnBeginDrag(PointerEventData eventData)
    {
        beginMousePositionX = Input.mousePosition.x;
    }




 
}