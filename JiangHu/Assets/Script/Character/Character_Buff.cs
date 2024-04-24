using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Buff : MonoBehaviour
{
    public List<BuffTable.Buff> buffList;
    private BuffManager buffManager;


    // Start is called before the first frame update
    void Start()
    {
        buffList = new List<BuffTable.Buff>();
        buffManager = GameObject.Find("BuffManager").GetComponent<BuffManager>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeRemoveBuff();
    }

    /// <summary>
    /// 添加buff
    /// </summary>
    /// <param name="buffID"></param>
    /// <param name="buffCreater"></param>
    public void AddBuff(int buffID, GameObject buffCreater)
    {
        BuffTable.Buff buff = buffManager.CreateBuff(buffID, Time.time, buffCreater);
        
        if (buff.Type == 1)
        {
            Debug.Log("添加buff");
            Character_Attribute creater_Attribute = buffCreater.GetComponent<Character_Attribute>();
            Character_Attribute character_Attribute = GetComponent<Character_Attribute>();
            //buffList.Add(buff);  基础伤害buff不加列表
            BuffManager.Damage damage = buffManager.CreateDamage(buff, creater_Attribute);
            character_Attribute.SetDamage(damage);
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
                    BuffTable.Buff buff = buffList[i];
                    buffList.Remove(buffList[i]);
                    buff = null;
                }
            }
        }
    }
}
