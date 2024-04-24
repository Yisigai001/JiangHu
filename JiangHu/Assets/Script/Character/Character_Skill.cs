using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Skill : MonoBehaviour
{
    [Header("ս��״̬")]
    public bool useingSkill;
    public bool useingQingGOng;
    [Header("�����б�")]
    public List<int> skillList;
    public List<int> neiGongList;
    public List<int> qingGongList;
    [Header("���������б�")]
    public List<int> useSkillList;
    public List<int> useNeiGongList;
    public List<int> useQingGongList;
    public List<int> removeList;
    public Dictionary<int, float> skillCoolDownDick;

    private SkillManager skillManager;
    private Zhaoshi_SKillTable sKillTable;
    private Character_Controller character_Controller;

    void Start()
    {
        skillList = new List<int>();
        neiGongList = new List<int>();
        qingGongList = new List<int>();
        useSkillList = new List<int>();
        useNeiGongList = new List<int>();
        useQingGongList = new List<int>();
        removeList = new List<int>();
        useingSkill = false;
        useingQingGOng = false;
        skillCoolDownDick = new Dictionary<int, float>();
        skillManager = GameObject.Find("SkillManager").GetComponent<SkillManager>();
        sKillTable = GameObject.Find("DataTable").GetComponent<Zhaoshi_SKillTable>();
        character_Controller = GetComponent<Character_Controller>();
        //����
        int linshiSkill = 1;
        AddSkill(linshiSkill);
        AddSkillToUseList(); //��Ӽ��ܵ���ʹ���б�

        
    }

    // Update is called once per frame
    void Update()
    {
        SkillColdDown(); //������ȴ����
        CharacterUseSkill();// ���ʹ�ü���
    }

    /// <summary>
    /// ��Ӽ��ܵ���ʹ���б�
    /// </summary>
    private void AddSkillToUseList()
    {
        if (skillList.Count > 0)
        {
            for (int i = 0; i < skillList.Count; i++)
            {
                useSkillList.Add(skillList[i]);
            }
        }

        if (qingGongList.Count > 0)
        {
            for (int i = 0; i < qingGongList.Count; i++)
            {
                useQingGongList.Add(qingGongList[i]);
            }
        }
    }

    /// <summary>
    /// ��Ӽ���
    /// </summary>
    /// <param name="SkillID"></param>
    public void AddSkill(int SkillID)
    {
        if (skillList.Count <= 0)
        {
            skillList.Add(SkillID);
            Debug.Log("�����б�=0,ֱ����Ӽ���; " + SkillID);
        }
        else
        {
            for (int i = 0; i < skillList.Count; i++)
            {
                if (skillList[i] == SkillID)
                {
                    Debug.Log("�Ѵ��ڸü���");
                    return;
                }
                else
                {
                    skillList.Add(SkillID);
                    Debug.Log("����˼���: " + SkillID);
                }
            }
        }
    }
    
    /// <summary>
    /// ����ͷż���
    /// </summary>
    public void CharacterUseSkill()
    {
        if (useSkillList.Count > 0 && character_Controller.inBattle)
        {
            //Debug.Log("��ɫ�ͷż���");
            int randomSkill = Random.Range(0, useSkillList.Count);
            //Debug.Log("����ID: " + useSkillList[randomSkill]);
            skillManager.ReleaseSkill(gameObject, useSkillList[randomSkill]);
            skillCoolDownDick.Add(useSkillList[randomSkill], Time.time);
            useSkillList.Remove(useSkillList[randomSkill]);
            SkillColdDown();
        }
    }

    /// <summary>
    /// ���ü�����ȴ
    /// </summary>
    private void SkillColdDown()
    {
        if (skillCoolDownDick.Count > 0)
        {
            //Debug.Log("�趨��ȴ");
            foreach (int key in skillCoolDownDick.Keys)
            {
                //Debug.Log("���key��: " + key);
                Zhaoshi_SKillTable.SkillBase skillBase = sKillTable.GetDataByID(key);
                //Debug.Log("��ǰʱ��: " + Time.time + "  �����ͷ�ʱ��ʱ��: " + skillCoolDownDick[key] + "  ������ȴʱ��" + skillBase.Cold);
                if (Time.time - skillCoolDownDick[key] >= skillBase.Cold / 1000)
                {
                    if (!useSkillList.Contains(key))
                    {
                        Debug.Log("ʹ�ü����б�δ������ȴ�ļ���,������Ӽ���.");
                        useSkillList.Add(key);
                        removeList.Add(key);
                    }
                }
            }
        }
        if (removeList.Count > 0)
        {
            Debug.Log("��ȴ����ɾ������");
            for (int i = 0; i < removeList.Count; i++)
            {
                skillCoolDownDick.Remove(removeList[i]);
            }
        }
        removeList.Clear();
    }
}
