using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    // Start is called before the first frame update
    private BuffTable buffTable;


    public class Damage
    {
        public int GangBase;
        public int RouBase;
        public int YangBase;
        public int YinBase;
        public int TaijiBase;
        public int GangPer;
        public int RouPer;
        public int YangPer;
        public int YinPer;
        public int TaijiPer;
        public int GangPerPlus;
        public int RouPerPlus;
        public int YangPerPlus;
        public int YinPerPlus;
        public int TaijiPerPlus;

        public Damage (int gangBase, int rouBase, int yangBase, int yinBase, int taijiBase, int gangPer, int rouPer, int yangPer, int yinPer, int taijiPer, int gangPerPlus, int rouPerPlus, int yangPerPlus, int yinPerPlus, int taijiPerPlus)
            {
               GangBase = gangBase;
               RouBase = rouBase;
               YangBase = yangBase;
               YinBase = yinBase;
               TaijiBase = taijiBase;
               GangPer = gangPer;
               RouPer = rouPer;
               YangPer = yangPer;
               YinPer = yinPer;
               TaijiPer = taijiPer;
               GangPerPlus = gangPerPlus;
               RouPerPlus = rouPerPlus;
               YangPerPlus = yangPerPlus;
               YinPerPlus = yinPerPlus;
               TaijiPerPlus = taijiPerPlus;
        }
    }

    void Start()
    {
        buffTable = GameObject.Find("DataTable").GetComponent<BuffTable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 创建buff
    /// </summary>
    /// <param name="buffID"></param>
    /// <param name="buffCreateTime"></param>
    /// <param name="buffCreater"></param>
    /// <returns></returns>
    public BuffTable.Buff CreateBuff(int buffID, float buffCreateTime, GameObject buffCreater)
    {
        Debug.Log("创建Buff");
        BuffTable.Buff buff = buffTable.GetDataByID(buffID);
        buff.BuffCreateTime = buffCreateTime;
        buff.BuffCreater = buffCreater;

        return buff;
    }
    
    /// <summary>
    /// 创建伤害类
    /// </summary>
    /// <param name="buff"></param>
    /// <param name="attribute"></param>
    /// <returns></returns>
    public Damage CreateDamage(BuffTable.Buff buff, Character_Attribute attribute)
    {
        Debug.Log("创建伤害");
        int gangBase = attribute.gangAttack;
        int rouBase = attribute.rouAttack;
        int yangBase = attribute.yangAttack;
        int yinBase = attribute.yinAttack;
        int taijiBase = attribute.taiJiAttack;

        int gangPer = buff.Param1;
        int rouPer = buff.Param2;
        int yangPer = buff.Param3;
        int yinPer = buff.Param4;
        int taijiPer = buff.Param5;

        int gangPerPlus = attribute.gangAttackPlusPer;
        int rouPerPlus = attribute.rouAttackPlusPer;
        int yangPerPlus = attribute.yangAttackPlusPer;
        int yinPerPlus = attribute.yinAttackPlusPer;
        int taijiPerPlus = attribute.taijiAttackPlusPer;

        Damage damage = new Damage(gangBase, rouBase, yangBase, yinBase, taijiBase, gangPer, rouPer, yangPer, yinPer, taijiPer, gangPerPlus, rouPerPlus, yangPerPlus, yinPerPlus, taijiPerPlus);
        return damage;
    }

}
