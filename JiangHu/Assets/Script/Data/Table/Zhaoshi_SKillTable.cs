using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSVReaderNamespace;

public class Zhaoshi_SKillTable : MonoBehaviour
{
    public TextAsset data; // csv�ļ�
    private List<List<string>> dataList; // csv����
    private Dictionary<int, SkillBase> dataDict; // װ�������ֵ�

    // ����װ�����ݽṹ
    public class SkillBase
    {
        public int ID; // ID
        public string Name; // ������
        public string Describe; // ��������
        public string Anim;
        public float ReleaseDisstance;
        public int Type;
        public int MP;
        public float Cold;
        public int BulletID;


        public SkillBase(int id, string name, string describe, string anim, float releaseDisstance, int type, int mp, float cold, int bulletID)
        {
            ID = id;
            Name = name;
            Describe = describe;
            Anim = anim;
            ReleaseDisstance = releaseDisstance;
            Type = type;
            MP = mp;
            Cold = cold;
            BulletID = bulletID;
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
            string anim = dataList[i][3];
            float releaseDisstance = float.Parse(dataList[i][4]);
            int type = int.Parse(dataList[i][5]);
            int mp = int.Parse(dataList[i][6]);
            float cold = float.Parse(dataList[i][7]);
            int bulletID = int.Parse(dataList[i][8]);

            SkillBase item = new SkillBase(id, name, describe,anim, releaseDisstance, type, mp, cold, bulletID);
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
