using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Buff : MonoBehaviour
{
    public List<BuffTable.Buff> buffList;
    private BuffManager buffManager;
    private Character_Skill character_Skill;

    //类型2的buff,移动用
    private bool randomDirectionDone;
    private Vector2 randomDirection; //随机方向

    // Start is called before the first frame update
    void Start()
    {
        buffList = new List<BuffTable.Buff>();
        buffManager = GameObject.Find("BuffManager").GetComponent<BuffManager>();
        character_Skill = GetComponent<Character_Skill>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeRemoveBuff();
        RunningBuff();
    }

    /// <summary>
    /// 添加buff
    /// </summary>
    /// <param name="buffID"></param>
    /// <param name="buffCreater"></param>
    public void AddBuff(int buffID, GameObject buffCreater)
    {
        BuffTable.Buff buff = buffManager.CreateBuff(buffID, Time.time, buffCreater);
        
        if (buff.Type == 1) //普通伤害
        {
            //Debug.Log("添加buff");
            Character_Attribute creater_Attribute = buffCreater.GetComponent<Character_Attribute>();
            Character_Attribute character_Attribute = GetComponent<Character_Attribute>();
            //buffList.Add(buff);  基础伤害buff不加列表
            BuffManager.Damage damage = buffManager.CreateDamage(buff, creater_Attribute);
            character_Attribute.SetDamage(damage);
        }
        else if (buff.Type == 2) //随机方向移动
        {
            randomDirectionDone = false;
            character_Skill.useingSkill = true;
            character_Skill.useingQingGong = true;
            buffList.Add(buff);
        }
    }

    /// <summary>
    /// 时间删除buff
    /// </summary>
    public void TimeRemoveBuff()
    {
        if (buffList.Count > 0)
        {
            for (int i = buffList.Count - 1; i >= 0; i--)
            {
                if (Time.time - buffList[i].BuffCreateTime >= buffList[i].Time)
                {
                    if (buffList[i].Type == 2) //类型为2的是给自己加buff,删buff时候把使用技能置位false;
                    {
                        character_Skill.useingQingGong = false;
                    }
                    BuffTable.Buff buff = buffList[i];
                    buffList.Remove(buffList[i]);
                    buff = null;
                }
            }
        }
    }

    /// <summary>
    /// buff运行中
    /// </summary>
    public void RunningBuff()
    {
        if (buffList.Count > 0 )
        {
            Debug.Log("进buff");
            for (int i = 0; i < buffList.Count; i++)
            {
                if (buffList[i].Type == 2)
                {
                    // 生成一个随机的方向向量
                    
                    if (!randomDirectionDone)
                    {
                        randomDirection = Random.insideUnitCircle.normalized;
                        randomDirectionDone = true;
                    }
                    else
                    {
                        // 计算对象的移动向量
                        Vector2 moveVector = randomDirection * (float)buffList[i].Param1 * Time.deltaTime;

                        // 将对象沿着移动向量移动
                        transform.Translate(moveVector);
                    }
                }
            }
        }
    }
}
