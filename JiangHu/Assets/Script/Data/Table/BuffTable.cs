using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSVReaderNamespace;

public class BuffTable : MonoBehaviour
{
    public TextAsset data; // csv文件
    private List<List<string>> dataList; // csv数据
    private Dictionary<int, Buff> dataDict; // 装备数据字典

    // 定义装备数据结构
    public class Buff
    {
        public int ID; // ID
        public string Name; // 属性类型
        public string Describe; // 道具名称
        public float Time;
        public float DelayTime;
        public int Type;
        public int Param1;
        public int Param2;
        public int Param3;
        public int Param4;
        public int Param5;
        public int Param6;
        public int Param7;
        public int Param8;
        public int Param9;
        public int Param10;
        public float BuffCreateTime;
        public GameObject BuffCreater;


        public Buff(int id, string name, string describe, float time, float delayTime, int type, int param1, int param2, int param3, int param4, int param5, int param6, int param7, int param8, int param9, int param10, float buffCreateTime, GameObject buffCreater)
        {
            ID = id;
            Name = name;
            Describe = describe;
            Time = time;
            DelayTime = delayTime;
            Type = type;
            Param1 = param1;
            Param2 = param2;
            Param3 = param3;
            Param4 = param4;
            Param5 = param5;
            Param6 = param6;
            Param7 = param7;
            Param8 = param8;
            Param9 = param9;
            Param10 = param10;
            BuffCreateTime = buffCreateTime;
            BuffCreater = buffCreater;
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
        dataDict = new Dictionary<int, Buff>();
        for (int i = 3; i < dataList.Count; i++)
        {
            int id = int.Parse(dataList[i][0]);
            string name = dataList[i][1];
            string describe = dataList[i][2];
            float time = float.Parse(dataList[i][3]);
            float delayTime = float.Parse(dataList[i][4]);
            int type = int.Parse(dataList[i][5]);
            int param1 = int.Parse(dataList[i][6]);
            int param2 = int.Parse(dataList[i][7]);
            int param3 = int.Parse(dataList[i][8]);
            int param4 = int.Parse(dataList[i][9]);
            int param5 = int.Parse(dataList[i][10]);
            int param6 = int.Parse(dataList[i][11]);
            int param7 = int.Parse(dataList[i][12]);
            int param8 = int.Parse(dataList[i][13]);
            int param9 = int.Parse(dataList[i][14]);
            int param10 = int.Parse(dataList[i][15]);
            float buffCreateTime = 1f;
            GameObject buffCreater = null;

            Buff item = new Buff(id, name, describe, time, delayTime, type, param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, buffCreateTime, buffCreater);
            dataDict[id] = item;
        }
    }

    // 通过ID获取整行数据
    public Buff GetDataByID(int id)
    {
        Buff data = null;
        if (dataDict.TryGetValue(id, out data))
        {
            return data;
        }
        Debug.LogError("EquipAffix with ID " + id + " not found.");
        return null;
    }
}
