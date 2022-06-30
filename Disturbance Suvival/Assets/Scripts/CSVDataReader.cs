using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

/*-----------------------------------------
CSVデータを読み込むプログラム
-----------------------------------------*/

public class CSVDataReader : MonoBehaviour
{
    TextAsset csvFile; // CSVファイル
    public List<string[]> csvDatas = new List<string[]>();// CSVの中身を入れるリスト;

    public List<int[]> csvDatasInt = new List<int[]>();

    string fileName;

    public int[][] csvInt;

    string[][] csvDatasArray;

    [HideInInspector] public int mapNumber;

    //public SpriteRenderer SR;

    private void Awake()
    {
        mapNumber = UnityEngine.Random.Range(0, 4);
        switch(mapNumber)
        {
            case 0:
                fileName = "Map_00";
                break;

            case 1:
                fileName = "Map_01";
                break;

            case 2:
                fileName = "Map_02";
                break;

            case 3:
                fileName = "Map_03";
                break;
        }
        csvFile = Resources.Load(fileName) as TextAsset; // Resouces下のCSV読み込み
        StringReader reader = new StringReader(csvFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            csvDatas.Add(line.Split(',')); // , 区切りでリストに追加
        }

        csvDatasInt = csvDatas.Select(x => x.Select(y => int.Parse(y)).ToArray()).ToList();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}


