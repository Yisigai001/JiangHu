using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSVReaderNamespace;

public class DailyEventTable : MonoBehaviour
{
    public TextAsset data; // csv文件
    private List<List<string>> dataList; // csv数据
    private Dictionary<int, DailyEvent> dataDict; // 装备数据字典

    // 定义装备数据结构
    public class DailyEvent
    {
        public int ID; // ID
        public string Name; // 属性类型
        public string Describe; // 道具名称



        public DailyEvent(int id, string name, string describe)
        {
            ID = id;
            Name = name;
            Describe = describe;
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
        dataDict = new Dictionary<int, DailyEvent>();
        for (int i = 3; i < dataList.Count; i++)
        {
            int id = int.Parse(dataList[i][0]);
            string name = dataList[i][1];
            string describe = dataList[i][2];

            DailyEvent item = new DailyEvent(id, name, describe);
            dataDict[id] = item;
        }
    }

    // 通过ID获取整行数据
    public DailyEvent GetDataByID(int id)
    {
        DailyEvent data = null;
        if (dataDict.TryGetValue(id, out data))
        {
            return data;
        }
        Debug.LogError("EquipAffix with ID " + id + " not found.");
        return null;
    }
}
