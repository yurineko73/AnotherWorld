using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapController : MonoBehaviour {
    public static MapController Instance = null;
    void Awake()
    {
        Instance = this;
    }

    public GameObject maptilePre;
    public List<Piece> mapList = new List<Piece>();
    public string[] mapData = new string[]{};
    public int cur_level = 1;
	
	void Start () {
        TextAsset mapText = Resources.Load("level") as TextAsset;
        //mapData = mapText.ToString().Split(new string[] { "\r\n" }, StringSplitOptions.None);
        mapData = mapText.ToString().Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        GlobalValue.LEVEL_COUNT = mapData.Length;
        InitMap();
	}

    public void InitMap(int level = 1)
    {
        print("----------------------initMap-----------------------------");
        int[] maps = new int[]
        {
            1,1,1,1,1,1,
            1,1,1,1,1,1,
            1,1,1,1,1,1,
            1,1,1,1,1,1,
            1,1,1,1,1,1,
            1,1,1,1,1,1,
        };
        DestroyMap();
        if (level < 1) level = 1;
        else if (level > GlobalValue.LEVEL_COUNT) level = GlobalValue.LEVEL_COUNT;
        cur_level = level;
        string map = mapData[level - 1].Trim();
        for (int k = 0; k < map.Length; k++)
        {
            if (int.Parse(map[k].ToString()) == -1) continue;
            var go = Instantiate(maptilePre).GetComponent<Piece>();
            go.Init(int.Parse(map[k].ToString()), k);
            mapList.Add(go);
        }
    }
    public void reloadMap()
    {
        DestroyMap();
        InitMap(cur_level);
    }
    private void DestroyMap()
    {
        for(int i=0;i<mapList.Count;i++)
        {
            Destroy(mapList[i].gameObject,0.05f);
        }
        mapList.Clear();
    }
    public void LinkTurn(int index)
    {
        print("---------------LinkTurn-----------------");
        var origin = FindPiece(index);
        if (null == origin) print("没找到对应索引的地图块！");
        origin.TurnSelf();
        Vector2Int tp = new Vector2Int(index % GlobalValue.MAP_WIDTH, index / GlobalValue.MAP_WIDTH);
        for (int i = 0; i < GlobalValue.MAP_WIDTH; i++)
        {
            var temp = FindPiece(i * GlobalValue.MAP_WIDTH + tp.x);
            if (null != temp) temp.TurnSelf();
            temp = FindPiece(i + tp.y * GlobalValue.MAP_WIDTH);
            if (null != temp) temp.TurnSelf();
        }
        GameManager.Instance.CheckWin();
    }
    public bool CheckWin()
    {
        for (int i = 1; i < mapList.Count; i++)
            if (mapList[i].type != mapList[0].type) return false;
        return true;
    }
    public void saveMapData()
    {
        string map = "";
        for (int i = 0; i < GlobalValue.MAP_WIDTH; i++)
        {
            for (int j = 0; j < GlobalValue.MAP_WIDTH; j++)
                map += "" + FindPiece(i * GlobalValue.MAP_WIDTH + j).type;
        }
        CreateOrOPenFile("C:/Users/Administrator/Desktop", "level.txt", map);
    }
    
    //根据索引从地图列表里找到对应块
    private Piece FindPiece(int index)
    {
        foreach (var p in mapList)
            if (p.index == index) return p;
        return null;
    }

    void CreateOrOPenFile(string path, string name, string info)
    {          //路径、文件名、写入内容
        StreamWriter sw;
        FileInfo fi = new FileInfo(path + "//" + name);
        sw = fi.AppendText();        //直接重新写入，如果要在原文件后面追加内容，应用fi.AppendText()
        info += "\n";
        sw.WriteLine(info);
        sw.Close();
        sw.Dispose();
    }
}
