using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BattleManager : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject playerObj;
    GameObject npcObj;
    public GameObject playerPos;
    public GameObject npcPos;
    public List<GameObject> camp1;
    public List<GameObject> camp2;
    float zPos;
    NpcTable npcTable;
    public bool battleSetDown; //是否完成了战斗设置
    public bool battleStart; //开始战斗
    void Start()
    {
        battleSetDown = false;
        camp1 = new List<GameObject>();
        camp2 = new List<GameObject>();
        playerObj = Resources.Load<GameObject>("Character/Prefab/Player");
        npcObj = Resources.Load<GameObject>("Character/Prefab/Npc");
        zPos = -0.15f;
        npcTable = GameObject.Find("DataTable").GetComponent<NpcTable>();
        SetBattle();
    }


    public void SetBattle()
    {
        OnCreatePlayer();
        OnCreateNPC(1,2);
        battleSetDown = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 创建玩家
    /// </summary>
    public void OnCreatePlayer()
    {
        GameObject player = Instantiate(playerObj);
        Character_Attribute character_Attribute = player.GetComponent<Character_Attribute>();
        character_Attribute.characterType = 1;
        character_Attribute.npcID = 0;
        player.transform.position = new Vector3(playerPos.transform.position.x, playerPos.transform.position.y, zPos);
        character_Attribute.camp = 1;
        camp1.Add(player);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="npcID"></param>
    /// <param name="阵营"></param>
    public void OnCreateNPC(int objectID, int camp)
    {
        GameObject npc = Instantiate(npcObj);
        Character_Attribute character_Attribute = npc.GetComponent<Character_Attribute>();
        character_Attribute.characterType = 2;
        character_Attribute.npcID = objectID;
        npc.transform.position = new Vector3(npcPos.transform.position.x, npcPos.transform.position.y, zPos);
        character_Attribute.camp = camp;
        if (camp == 1)
        {
            camp1.Add(npc);
        }
        else if (camp == 2)
        {
            camp2.Add(npc);
        }
    }

    public void RemoveCharacter(GameObject gameObject)
    {
        for (int i = 0; i < camp1.Count; i++)
        {
            if (gameObject == camp1[i])
            {
                //Debug.Log("干掉npc");
                camp1.Remove(gameObject);
            }
        }

        for (int i = 0; i < camp2.Count; i++)
        {
            if (gameObject == camp2[i])
            {
                //Debug.Log("干掉npc");
                camp2.Remove(gameObject);
            }
        }
    }
    
}
