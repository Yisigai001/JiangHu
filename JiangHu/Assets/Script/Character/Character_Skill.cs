using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Skill : MonoBehaviour
{
    [Header("ս��״̬")]
    public bool useingSkill;
    public bool useingQingGong;
    public bool canUseQingGong;
    [Header("�����б�")]
    public List<int> skillList;
    public List<int> neiGongList;
    public List<int> qingGongList;
    [Header("ս�������б�")]
    public List<int> useSkillList;
    public List<int> useNeiGongList;
    public List<int> useQingGongList;
    public List<int> removeList;
    public Dictionary<int, float> skillCoolDownDick;

    private SkillManager skillManager;
    private Zhaoshi_SKillTable sKillTable;
    private Character_Controller character_Controller;
    private BattleManager battleManager;

    private float useSkillTime; //��ʱ,֮����Ҫɾ��

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
        useingQingGong = false;
        skillCoolDownDick = new Dictionary<int, float>();
        skillManager = GameObject.Find("SkillManager").GetComponent<SkillManager>();
        sKillTable = GameObject.Find("DataTable").GetComponent<Zhaoshi_SKillTable>();
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        character_Controller = GetComponent<Character_Controller>();

    }

    // Update is called once per frame
    void Update()
    {
        SkillColdDown(); //������ȴ����
        if (character_Controller.target != null)
        {
            CharacterUseSkill();// ���ʹ�ü���
        }
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
        if (useingSkill)
        {
            if (Time.time - useSkillTime >= 0.5)
            {
                useingSkill = false;
            }
        }

        if (battleManager.battleStart && character_Controller.target != null)
        {
            if (!useingSkill && !useingQingGong && canUseQingGong)
            {
                if (useQingGongList.Count > 0)
                {
                    for (int i = 0; i < useQingGongList.Count; i++)
                    {
                        skillManager.ReleaseSkill(gameObject, useQingGongList[i]);
                        useSkillTime = Time.time;
                        skillCoolDownDick.Add(useQingGongList[i], Time.time);
                        useQingGongList.Remove(useQingGongList[i]);
                        SkillColdDown();

                    }
                }
                else
                {
                    canUseQingGong = false;
                }
            }
            else if (!useingSkill && !canUseQingGong && !useingQingGong)
            {
                float distance = Vector2.Distance(transform.position, character_Controller.target.transform.position);
                for (int i = 0; i < useSkillList.Count; i++)
                {
                    if (distance <= sKillTable.GetDataByID(useSkillList[i]).ReleaseDisstance)
                    {
                        skillManager.ReleaseSkill(gameObject, useSkillList[i]);
                        skillCoolDownDick.Add(useSkillList[i], Time.time);
                        useSkillList.Remove(useSkillList[i]);
                        SkillColdDown();
                        useSkillTime = Time.time;
                        //useingSkill = true;
                        return;
                    }
                }
            }
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
                        //Debug.Log("ʹ�ü����б�δ������ȴ�ļ���,������Ӽ���.");
                        if (skillBase.Type == 1)
                        {
                            useSkillList.Add(key);
                            removeList.Add(key);
                            //useSkillList.Sort(); //���մ�С�����˳������
                        }
                        else if (skillBase.Type == 10)
                        {
                            useQingGongList.Add(key);
                            removeList.Add(key);
                        }
                    }
                }
            }
        }
        if (removeList.Count > 0)
        {
            //Debug.Log("��ȴ����ɾ������");
            for (int i = 0; i < removeList.Count; i++)
            {
                skillCoolDownDick.Remove(removeList[i]);
            }
        }
        removeList.Clear();
    }
}
