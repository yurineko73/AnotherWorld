using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Piece : MonoBehaviour {

    public int type;  //1 = 黑 0 = 白
    public int index;

    //类型，索引
    public void Init(int type,int index)
    {
        this.type = type;
        GetComponent<SpriteRenderer>().color = (type == 1 ? GlobalValue.COLOR_ARRAY[0] : GlobalValue.COLOR_ARRAY[1]);
        transform.position = new Vector3(-GlobalValue.OFFSET + index % GlobalValue.MAP_WIDTH,
                                         GlobalValue.OFFSET - index / GlobalValue.MAP_WIDTH, 0);
        this.index = index;
    }
    //翻面
    public void TurnSelf()
    {
        type = (type == 1 ? 0 : 1);
        GetComponent<SpriteRenderer>().color = (type == 1 ? GlobalValue.COLOR_ARRAY[0] : GlobalValue.COLOR_ARRAY[1]);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.DORotate(new Vector3(0,180,0), 0.5f);
    }
    void OnMouseDown()
    {
        if (IsTouchedUI()) return;
        MapController.Instance.LinkTurn(index);
        Debug.Log("被点击到了");
    }

    //判断是否点击到ui上
    private bool IsTouchedUI()
    {
        bool touchedUI = false;
        //TODO 移动端
        if (Application.isMobilePlatform)
        {
            if (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                touchedUI = true;
            }
        }
        //TODO PC端
        else if (EventSystem.current.IsPointerOverGameObject())
        {
            touchedUI = true;
        }
        return touchedUI;
    }
}
