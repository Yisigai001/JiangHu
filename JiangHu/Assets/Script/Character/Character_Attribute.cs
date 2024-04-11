using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Attribute : MonoBehaviour
{
    [Header("基础属性")]
    public int CharacterID; //角色id
    public int camp; //阵营
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetAttribute()
    {

    }
}
