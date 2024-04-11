using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Attribute : MonoBehaviour
{
    [Header("基础信息")]
    public int characterType;
    public int characterID; //角色id
    public int camp; //阵营

    [Header("基础属性")]
    public int genGu; //根骨
    public int jinLi; //劲力
    public int lingQiao; //灵巧
    public int yang; //阳
    public int yin; //阴
    public int neiLi;
    public int xinZhi;
    public int wuXing;
    public int fuYuan;

    [Header("系统成长")]
    public int jingMai;
    public int duanTi;

    [Header("战斗属性")]
    public int maxHp;
    public int hp;
    public int gangGong; //
    public int rouGong;
    public int yangGong;
    public int yinGong;
    public int taiJi;
    public int gangMian;
    public int rouMian;
    public int yangMian;
    public int yinMian;

    public int gangGongBai;
    public int rouGongBai;
    public int yangGongBai;
    public int yinGongBai;

    private Image hpLine;
    // Start is called before the first frame update
    void Start()
    {
        maxHp = 1;
        hpLine = transform.Find("CharacterCanvas").transform.Find("HpLine").gameObject.GetComponent<Image>();
    }

    private void Update()
    {
        HpLine();
    }

    public void SetAttribute()
    {

    }

    private void HpLine()
    {
        hpLine.fillAmount = hp / maxHp;
    }
}
