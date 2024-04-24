using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSVReaderNamespace;
using System;

public class ZhaoShi_BulletTable : MonoBehaviour
{
    public TextAsset data; // csv文件
    private List<List<string>> dataList; // csv数据
    private Dictionary<int, Bullet> dataDict; // 装备数据字典

    // 定义装备数据结构
    public class Bullet
    {
        public int ID; // ID
        public string Name; // 属性类型
        public string Describe; // 道具名称
        public int Type;
        public float Speed;
        public float Time;
        public float ReleaseDelay;
        public bool Disapper;
        public string VFX;
        public int Buff1;
        public int Buff2;
        public int Buff3;
        public int Buff4;
        public int Buff5;



        public Bullet(int id, string name, string describe, int type, float speed, float time, float releaseDelay, bool disapper, string vfx, int buff1, int buff2, int buff3, int buff4, int buff5)
        {
            ID = id;
            Name = name;
            Describe = describe;
            Type = type;
            Speed = speed;
            Time = time;
            ReleaseDelay = releaseDelay;
            Disapper = disapper;
            VFX = vfx;
            Buff1 = buff1;
            Buff2 = buff2;
            Buff3 = buff3;
            Buff4 = buff4;
            Buff5 = buff5;
        }
    }

    void Awake()
    {
        dataList = CSVReader.SplitCsvGrid(data.text); // 将csv文件读取到dataList中
        LoadItem(); // 加载装备数据到dataDict中
    }

    // 加载装备数据到skillDict中
    private void LoadItem()
    {
        dataDict = new Dictionary<int, Bullet>();
        for (int i = 3; i < dataList.Count; i++)
        {
            int id = int.Parse(dataList[i][0]);
            string name = dataList[i][1];
            string describe = dataList[i][2];
            int type = int.Parse(dataList[i][3]);
            float speed = float.Parse(dataList[i][4]);
            float time = float.Parse(dataList[i][5]);
            float releaseDelay = float.Parse(dataList[i][6]);
            bool disapper = bool.Parse(dataList[i][7]);
            string vfx = dataList[i][8];
            int buff1 = int.Parse(dataList[i][9]);
            int buff2 = int.Parse(dataList[i][10]);
            int buff3 = int.Parse(dataList[i][11]);
            int buff4 = int.Parse(dataList[i][12]);
            int buff5 = int.Parse(dataList[i][13]);

            Bullet item = new Bullet( id,  name,  describe,  type,  speed, time, releaseDelay,  disapper, vfx, buff1,  buff2,  buff3,  buff4,  buff5);
            dataDict[id] = item;
        }
    }

    // 通过ID获取整行数据
    public Bullet GetDataByID(int id)
    {
        Bullet data = null;
        if (dataDict.TryGetValue(id, out data))
        {
            return data;
        }
        Debug.LogError("EquipAffix with ID " + id + " not found.");
        return null;
    }
}
