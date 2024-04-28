using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Attribute : MonoBehaviour
{
    [Header("基础信息")]
    public int characterType;
    public int npcID; //角色id
    public int camp; //阵营
    public bool die; //噶了
    public float dieTime;

    [Header("基础属性")]
    public int genGu; //根骨
    public int jinLi; //劲力
    public int lingQiao; //灵巧
    public int yang; //阳
    public int yin; //阴
    public int neiLi; //内里
    public int xinZhi; //心智
    public int wuXing; //悟性
    public int fuYuan; //福源

    [Header("系统成长")]
    public int jingMai;
    public int duanTi;

    [Header("战斗属性")]
    public int maxHp;
    public int hp;
    public float moveSpeed;

    [Header("基础攻击")]
    public int gangAttack; //
    public int rouAttack;
    public int yangAttack;
    public int yinAttack;
    public int taiJiAttack;


    [Header("攻击百分比")]
    public int gangAttackPlusPer;
    public int rouAttackPlusPer;
    public int yangAttackPlusPer;
    public int yinAttackPlusPer;
    public int taijiAttackPlusPer;

    [Header("防御百分比")]
    public int gangReducePer;
    public int rouReducePer;
    public int yangReducePer;
    public int yinReducePer;
    public int taijiReducePer;

    private Image hpLine;
    private NpcTable npcTable;
    private NpcTable.NpcBase npcBase;
    private bool createNpcDown;
    // Start is called before the first frame update
    void Start()
    {
        die = false;
        npcTable = GameObject.Find("DataTable").GetComponent<NpcTable>();
        if (characterType == 2)
        {
            createNpcDown = false;
            npcBase = npcTable.GetDataByID(npcID);
            SetNPCAttribute(npcBase);
        }

        hpLine = transform.Find("CharacterCanvas").transform.Find("HpLine").gameObject.GetComponent<Image>();



        //临时
        moveSpeed = 1;
        if (characterType == 1)
        {
            maxHp = 10000;
            hp = maxHp;
            gangAttack = 100;
        }
    }

    void Update()
    {
        HpLine();
        OnDie();
        DestroyGameObject();
    }

    /// <summary>
    /// 设定NPC属性,以后得重写
    /// </summary>
    /// <param name="npcBase"></param>
    public void SetNPCAttribute(NpcTable.NpcBase npcBase)
    {
        genGu = npcBase.GenGu; //根骨
        jinLi = npcBase.JinLi; //劲力
        lingQiao = npcBase.LingQiao; //灵巧
        yang = npcBase.YangQi; //阳
        yin = npcBase.YinQi; //阴
        neiLi = npcBase.NeiLi; //内里
        xinZhi = npcBase.XinZhi; //心智
        wuXing = npcBase.WuXing; //悟性
        fuYuan = npcBase.FuYuan; //福源
        maxHp = genGu * 20;
        hp = maxHp;
        createNpcDown = true;

    }

    /// <summary>
    /// 血条
    /// </summary>
    private void HpLine()
    {
        if (createNpcDown)
        {
            float hpLinePer = (float)hp / (float)maxHp;
            hpLine.fillAmount = hpLinePer;
        }
    }


    public void OnDie()
    {
        if (hp <= 0 && !die)
        {
            die = true;
            dieTime = Time.time;
            BattleManager battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
            battleManager.RemoveCharacter(gameObject);
        }
    }

    public void DestroyGameObject()
    {
        if (die)
        {
            if (Time.time - dieTime >= 1)
            {
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// 伤害计算
    /// </summary>
    /// <param name="damage"></param>
    public void SetDamage(BuffManager.Damage damage)
    {
        int gangDamage = 0;
        int rouDamage = 0;
        int yangDamage = 0;
        int yinDamage = 0;
        int taijiDamage = 0;
        if (damage.GangPer == 0)
        {
            gangDamage = 0;
        }
        else
        {
            gangDamage = damage.GangBase * ((damage.GangPer + (damage.GangPerPlus - gangReducePer)) / 100);
            if (gangDamage < 0) gangDamage = 0;
        }

        if (damage.RouPer == 0)
        {
            rouDamage = 0;
        }
        else
        {
            rouDamage = damage.RouBase * ((damage.RouPer + (damage.RouPerPlus - rouReducePer)) / 100);
            if (rouDamage < 0) rouDamage = 0;
        }

        if (damage.YangPer == 0)
        {
            yangDamage = 0;
        }
        else
        {
            yangDamage = damage.YangBase * ((damage.YangPer + (damage.YangPerPlus - yangReducePer)) / 100);
            if (yangDamage < 0) yangDamage = 0;
        }

        if (damage.YinPer == 0)
        {
            yinDamage = 0;
        }
        else
        {
            yinDamage = damage.YinBase * ((damage.YinPer + (damage.YinPerPlus - yinReducePer)) / 100);
            if (yinDamage < 0) yinDamage = 0;
        }

        if (damage.TaijiPer == 0)
        {
            taijiDamage = 0;
        }
        else
        {
            taijiDamage = damage.TaijiBase * ((damage.TaijiPer + (damage.TaijiPerPlus - taijiReducePer)) / 100);
            if (taijiDamage < 0) taijiDamage = 0;
        }

        int hpDamage = gangDamage + rouDamage + yangDamage + yinDamage + taijiDamage;
        hp -= hpDamage;

        if (hp < 0)
        {
            hp = 0;
        }
        //Debug.Log("伤害 = " + hpDamage);
    }
}
