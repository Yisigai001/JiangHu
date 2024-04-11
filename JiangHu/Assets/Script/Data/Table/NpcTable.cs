using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSVReaderNamespace;
using UnityEditor;

public class NpcTable : MonoBehaviour
{
    public TextAsset data; // csv�ļ�
    private List<List<string>> dataList; // csv����
    private Dictionary<int, NpcBase> dataDict; // װ�������ֵ�

    // ����װ�����ݽṹ
    public class NpcBase
    {
        public int ID; // ID
        public string Name; // ��������
        public string Describe; // ��������
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
        dataList = CSVReader.SplitCsvGrid(data.text); // ��csv�ļ���ȡ��dataList��
        LoadItem(); // ����װ�����ݵ�dataDict��
    }

    // ����װ�����ݵ�skillDict��
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

    // ͨ��ID��ȡ��������
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
