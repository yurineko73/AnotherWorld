using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Piece : MonoBehaviour {

    public int type;  //1 = 黑 0 = 白
    public int index;

    //类型，索引
    public void Init(int type,int index)
    {
        this.type = type;
        GetComponent<SpriteRenderer>().color = (type == 1?Color.black:Color.white);
        transform.position = new Vector3(index % GlobalValue.MAP_WIDTH, index / GlobalValue.MAP_WIDTH, 0);
        this.index = index;
    }
    //翻面
    public void TurnSelf()
    {
        type = (type == 1 ? 0 : 1);
        GetComponent<SpriteRenderer>().color = (type == 1 ? Color.black : Color.white);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.DORotate(new Vector3(0,180,0), 0.5f);
    }
    void OnMouseDown()
    {
        MapController.Instance.LinkTurn(index);
        Debug.Log("被点击到了");
    }
	
}
