using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSVReaderNamespace;
using UnityEditor;

public class NpcTable : MonoBehaviour
{
    public TextAsset data; // csv文件
    private List<List<string>> dataList; // csv数据
    private Dictionary<int, NpcBase> dataDict; // 装备数据字典

    // 定义装备数据结构
    public class NpcBase
    {
        public int ID; // ID
        public string Name; // 属性类型
        public string Describe; // 道具名称
        public int GenGu; // ID
        public int JinLi; // ID
        public int LingQiao; // ID
        public int YangQi; // ID
        public int YinQi; // ID
        public int NeiLi; // ID
        public int XinZhi; // ID
        public int WuXing; // ID
        public int FuYuan; // ID
        public int JingMai; // ID
        public int DuanTi; // ID



        public NpcBase(int id, string name, string describe, int genGu, int jinLi, int lingQiao, int yangQi, int yinQi, int neiLi, int xinZhi, int wuXing, int fuYuan, int jingMai, int duanTi)
        {
            ID = id;
            Name = name;
            Describe = describe;
            GenGu = genGu;
            JinLi = jinLi;
            LingQiao = lingQiao;
            YangQi = yangQi;
            YinQi = yinQi;
            NeiLi = neiLi;
            XinZhi = xinZhi;
            WuXing = wuXing;
            FuYuan = fuYuan;
            JingMai = jingMai;
            DuanTi = duanTi;

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
        dataDict = new Dictionary<int, NpcBase>();
        for (int i = 3; i < dataList.Count; i++)
        {
            int id = int.Parse(dataList[i][0]);
            string name = dataList[i][1];
            string describe = dataList[i][2];
            int genGu = int.Parse(dataList[i][3]);
            int jinLi = int.Parse(dataList[i][4]);
            int lingQiao = int.Parse(dataList[i][5]);
            int yangQi = int.Parse(dataList[i][6]);
            int yinQi = int.Parse(dataList[i][7]);
            int neiLi = int.Parse(dataList[i][8]);
            int xinZhi = int.Parse(dataList[i][9]);
            int wuXing = int.Parse(dataList[i][10]);
            int fuYuan = int.Parse(dataList[i][11]);
            int jingMai = int.Parse(dataList[i][12]);
            int duanTi = int.Parse(dataList[i][13]);

            NpcBase item = new NpcBase(id, name, describe, genGu, jinLi, lingQiao, yangQi, yinQi, neiLi, xinZhi, wuXing, fuYuan, jingMai, duanTi);
            dataDict[id] = item;
        }
    }

    // 通过ID获取整行数据
    public NpcBase GetDataByID(int id)
    {
        NpcBase data = null;
        if (dataDict.TryGetValue(id, out data))
        {
            return data;
        }
        Debug.LogError("EquipAffix with ID " + id + " not found.");
        return null;
    }
}
