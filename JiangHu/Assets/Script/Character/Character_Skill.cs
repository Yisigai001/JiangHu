using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Skill : MonoBehaviour
{
    [Header("战斗状态")]
    public bool useingSkill;
    public bool useingQingGOng;
    [Header("技能列表")]
    public List<int> skillList;
    public List<int> neiGongList;
    public List<int> qingGongList;
    [Header("战斗技能列表")]
    public List<int> useSkillList;
    public List<int> useNeiGongList;
    public List<int> useQingGongList;
    public List<int> removeList;
    public Dictionary<int, float> skillCoolDownDick;

    private SkillManager skillManager;
    private Zhaoshi_SKillTable sKillTable;
    private Character_Controller character_Controller;
    private BattleManager battleManager;

    private float useSkillTime; //临时,之后需要删除

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
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        character_Controller = GetComponent<Character_Controller>();

    }

    // Update is called once per frame
    void Update()
    {
        SkillColdDown(); //技能冷却处理
        if (character_Controller.target != null)
        {
            CharacterUseSkill();// 随机使用技能
        }
    }

    /// <summary>
    /// 添加技能到可使用列表
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
    /// 添加技能
    /// </summary>
    /// <param name="SkillID"></param>
    public void AddSkill(int SkillID)
    {
        if (skillList.Count <= 0)
        {
            skillList.Add(SkillID);
            Debug.Log("技能列表=0,直接添加技能; " + SkillID);
        }
        else
        {
            for (int i = 0; i < skillList.Count; i++)
            {
                if (skillList[i] == SkillID)
                {
                    Debug.Log("已存在该技能");
                    return;
                }
                else
                {
                    skillList.Add(SkillID);
                    Debug.Log("添加了技能: " + SkillID);
                }
            }
        }
    }
    
    /// <summary>
    /// 随机释放技能
    /// </summary>
    public void CharacterUseSkill()
    {
        if (useingSkill) //临时,之后要改.
        {
            if (Time.time - useSkillTime >= 1) useingSkill = false;
        }


        if (useSkillList.Count > 0 && battleManager.battleStart &&character_Controller.target != null)
        {
            if (!useingSkill)
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
                        useingSkill = true;
                        return;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 设置技能冷却
    /// </summary>
    private void SkillColdDown()
    {
        if (skillCoolDownDick.Count > 0)
        {
            //Debug.Log("设定冷却");
            foreach (int key in skillCoolDownDick.Keys)
            {
                //Debug.Log("这个key是: " + key);
                Zhaoshi_SKillTable.SkillBase skillBase = sKillTable.GetDataByID(key);
                //Debug.Log("当前时间: " + Time.time + "  技能释放时的时间: " + skillCoolDownDick[key] + "  技能冷却时间" + skillBase.Cold);
                if (Time.time - skillCoolDownDick[key] >= skillBase.Cold / 1000)
                {
                    if (!useSkillList.Contains(key))
                    {
                        Debug.Log("使用技能列表未包含冷却的技能,所以添加技能.");
                        useSkillList.Add(key);
                        removeList.Add(key);
                        useSkillList.Sort(); //按照从小到大的顺序排列
                    }
                }
            }
        }
        if (removeList.Count > 0)
        {
            Debug.Log("冷却组里删除技能");
            for (int i = 0; i < removeList.Count; i++)
            {
                skillCoolDownDick.Remove(removeList[i]);
            }
        }
        removeList.Clear();
    }
}
