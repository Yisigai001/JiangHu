using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Attribute : MonoBehaviour
{
    [Header("������Ϣ")]
    public int characterType;
    public int npcID; //��ɫid
    public int camp; //��Ӫ
    public bool die; //����
    public float dieTime;

    [Header("��������")]
    public int genGu; //����
    public int jinLi; //����
    public int lingQiao; //����
    public int yang; //��
    public int yin; //��
    public int neiLi; //����
    public int xinZhi; //����
    public int wuXing; //����
    public int fuYuan; //��Դ

    [Header("ϵͳ�ɳ�")]
    public int jingMai;
    public int duanTi;

    [Header("ս������")]
    public int maxHp;
    public int hp;
    public float moveSpeed;

    [Header("��������")]
    public int gangAttack; //
    public int rouAttack;
    public int yangAttack;
    public int yinAttack;
    public int taiJiAttack;


    [Header("�����ٷֱ�")]
    public int gangAttackPlusPer;
    public int rouAttackPlusPer;
    public int yangAttackPlusPer;
    public int yinAttackPlusPer;
    public int taijiAttackPlusPer;

    [Header("�����ٷֱ�")]
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



        //��ʱ
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
    /// �趨NPC����,�Ժ����д
    /// </summary>
    /// <param name="npcBase"></param>
    public void SetNPCAttribute(NpcTable.NpcBase npcBase)
    {
        genGu = npcBase.GenGu; //����
        jinLi = npcBase.JinLi; //����
        lingQiao = npcBase.LingQiao; //����
        yang = npcBase.YangQi; //��
        yin = npcBase.YinQi; //��
        neiLi = npcBase.NeiLi; //����
        xinZhi = npcBase.XinZhi; //����
        wuXing = npcBase.WuXing; //����
        fuYuan = npcBase.FuYuan; //��Դ
        maxHp = genGu * 20;
        hp = maxHp;
        createNpcDown = true;

    }

    /// <summary>
    /// Ѫ��
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
    /// �˺�����
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
        //Debug.Log("�˺� = " + hpDamage);
    }
}
