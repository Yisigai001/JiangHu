using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    Zhaoshi_SKillTable sKillTable;
    ZhaoShi_BulletTable bulletTable;
    BuffTable buffTable;
    private string vfxPath = "VFX/Prefab/";

    void Start()
    {
        sKillTable = GameObject.Find("DataTable").GetComponent<Zhaoshi_SKillTable>();
        bulletTable = GameObject.Find("DataTable").GetComponent<ZhaoShi_BulletTable>();
    }

    public void ReleaseSkill(GameObject skillOnwer, int skillID)
    {
        //Debug.Log("SkillManager�ͷż���");
        Zhaoshi_SKillTable.SkillBase skill = sKillTable.GetDataByID(skillID);

        //����ʹ�ü�����״̬
        Character_Skill character_Skill = skillOnwer.GetComponent<Character_Skill>();
        

        if (skill.Type == 1) //�������ͼ���
        {
            character_Skill.useingSkill = true;
            ZhaoShi_BulletTable.Bullet bullet = bulletTable.GetDataByID(skill.BulletID);
            Transform skillPoint = skillOnwer.transform.Find("Chest").transform.Find("SKillReleasePoint");
            GameObject vfxResource = Resources.Load<GameObject>(vfxPath + bullet.VFX);
            GameObject vfx = Instantiate(vfxResource);
            vfx.transform.position = new Vector3(skillPoint.position.x, skillPoint.position.y, -0.15f);
            Quaternion quaternionRotation = skillPoint.rotation;
            vfx.transform.rotation = quaternionRotation;
            Bullet bulletComponent = vfx.AddComponent<Bullet>();

            //�ӵ��ĸ�������
            bulletComponent.owner = skillOnwer;
            bulletComponent.type = bullet.Type;
            bulletComponent.speed = bullet.Speed;
            bulletComponent.time = bullet.Time;
            bulletComponent.releaseDelay = bullet.ReleaseDelay;
            bulletComponent.disapper = bullet.Disapper;
            bulletComponent.buff1 = bullet.Buff1;
            bulletComponent.buff2 = bullet.Buff2;
            bulletComponent.buff3 = bullet.Buff3;
            bulletComponent.buff4 = bullet.Buff4;
            bulletComponent.buff5 = bullet.Buff5;
        }
        else if (skill.Type == 10) //���Լ����һ���Ṧbuff
        {
            Debug.Log("�ͷ����Ṧ");
            Character_Buff character_Buff = skillOnwer.GetComponent<Character_Buff>();
            character_Buff.AddBuff(skill.BuffID, skillOnwer);
        }
    }


}
