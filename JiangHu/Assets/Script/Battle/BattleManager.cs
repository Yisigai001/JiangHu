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
    void Start()
    {
        playerObj = Resources.Load<GameObject>("Character/Prefab/Player");
        npcObj = Resources.Load<GameObject>("Character/Prefab/Npc");
        SetBattle();
    }


    public void SetBattle()
    {
        float zPos = -0.15f;
        GameObject player = Instantiate(playerObj);
        player.transform.position = new Vector3(playerPos.transform.position.x, playerPos.transform.position.y, zPos);
        GameObject npc = Instantiate(npcObj);
        npc.transform.position = new Vector3(npcPos.transform.position.x, npcPos.transform.position.y, zPos);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
