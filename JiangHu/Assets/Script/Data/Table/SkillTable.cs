using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSVReaderNamespace;

public class SkillTable : MonoBehaviour
{
    public TextAsset data; // csv�ļ�
    private List<List<string>> dataList; // csv����
    private Dictionary<int, SkillBase> dataDict; // װ�������ֵ�

    // ����װ�����ݽṹ
    public class SkillBase
    {
        public int ID; // ID
        public string Name; // ��������
        public string Describe; // ��������



        public SkillBase(int id, string name, string describe)
        {
            ID = id;
            Name = name;
            Describe = describe;
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
        dataDict = new Dictionary<int, SkillBase>();
        for (int i = 3; i < dataList.Count; i++)
        {
            int id = int.Parse(dataList[i][0]);
            string name = dataList[i][1];
            string describe = dataList[i][2];

            SkillBase item = new SkillBase(id, name, describe);
            dataDict[id] = item;
        }
    }

    // ͨ��ID��ȡ��������
    public SkillBase GetDataByID(int id)
    {
        SkillBase data = null;
        if (dataDict.TryGetValue(id, out data))
        {
            return data;
        }
        Debug.LogError("EquipAffix with ID " + id + " not found.");
        return null;
    }
}
