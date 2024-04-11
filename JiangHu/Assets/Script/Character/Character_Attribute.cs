using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Attribute : MonoBehaviour
{
    [Header("������Ϣ")]
    public int characterType;
    public int characterID; //��ɫid
    public int camp; //��Ӫ

    [Header("��������")]
    public int genGu; //����
    public int jinLi; //����
    public int lingQiao; //����
    public int yang; //��
    public int yin; //��
    public int neiLi;
    public int xinZhi;
    public int wuXing;
    public int fuYuan;

    [Header("ϵͳ�ɳ�")]
    public int jingMai;
    public int duanTi;

    [Header("ս������")]
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
