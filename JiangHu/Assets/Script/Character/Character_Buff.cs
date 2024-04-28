using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Buff : MonoBehaviour
{
    public List<BuffTable.Buff> buffList;
    private BuffManager buffManager;
    private Character_Skill character_Skill;

    //����2��buff,�ƶ���
    private bool randomDirectionDone;
    private Vector2 randomDirection; //�������

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
    /// ���buff
    /// </summary>
    /// <param name="buffID"></param>
    /// <param name="buffCreater"></param>
    public void AddBuff(int buffID, GameObject buffCreater)
    {
        BuffTable.Buff buff = buffManager.CreateBuff(buffID, Time.time, buffCreater);
        
        if (buff.Type == 1) //��ͨ�˺�
        {
            //Debug.Log("���buff");
            Character_Attribute creater_Attribute = buffCreater.GetComponent<Character_Attribute>();
            Character_Attribute character_Attribute = GetComponent<Character_Attribute>();
            //buffList.Add(buff);  �����˺�buff�����б�
            BuffManager.Damage damage = buffManager.CreateDamage(buff, creater_Attribute);
            character_Attribute.SetDamage(damage);
        }
        else if (buff.Type == 2) //��������ƶ�
        {
            randomDirectionDone = false;
            character_Skill.useingSkill = true;
            character_Skill.useingQingGong = true;
            buffList.Add(buff);
        }
    }

    /// <summary>
    /// ʱ��ɾ��buff
    /// </summary>
    public void TimeRemoveBuff()
    {
        if (buffList.Count > 0)
        {
            for (int i = buffList.Count - 1; i >= 0; i--)
            {
                if (Time.time - buffList[i].BuffCreateTime >= buffList[i].Time)
                {
                    if (buffList[i].Type == 2) //����Ϊ2���Ǹ��Լ���buff,ɾbuffʱ���ʹ�ü�����λfalse;
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
    /// buff������
    /// </summary>
    public void RunningBuff()
    {
        if (buffList.Count > 0 )
        {
            Debug.Log("��buff");
            for (int i = 0; i < buffList.Count; i++)
            {
                if (buffList[i].Type == 2)
                {
                    // ����һ������ķ�������
                    
                    if (!randomDirectionDone)
                    {
                        randomDirection = Random.insideUnitCircle.normalized;
                        randomDirectionDone = true;
                    }
                    else
                    {
                        // ���������ƶ�����
                        Vector2 moveVector = randomDirection * (float)buffList[i].Param1 * Time.deltaTime;

                        // �����������ƶ������ƶ�
                        transform.Translate(moveVector);
                    }
                }
            }
        }
    }
}
