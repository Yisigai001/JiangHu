using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPanel : MonoBehaviour
{
    public bool uiOpen;
    public GameObject testPanel;
    public GameObject player;
    public Character_Skill character_Skill;

    void Start()
    {
        testPanel = transform.Find("TestPanel").gameObject;
        uiOpen = false;
        testPanel.SetActive(false);
        
    }

    public void OnOpenUI()
    {
        if (!uiOpen)
        {
            testPanel.SetActive(true);
            uiOpen = true;
        }
        else
        {
            testPanel.SetActive(false);
            uiOpen = false;
        }
    }

    /// <summary>
    /// 添加所有武学
    /// </summary>
    public void OnAddAllZhaoshi()
    {
        player = GameObject.FindWithTag("Player");
        character_Skill = player.GetComponent<Character_Skill>();
        if (player != null)
        {
            character_Skill.useSkillList.Add(1);
            character_Skill.useSkillList.Add(2);
            character_Skill.useSkillList.Add(3);
            character_Skill.useSkillList.Add(4);
            character_Skill.useSkillList.Add(5);

            character_Skill.useQingGongList.Add(7);
            character_Skill.useQingGongList.Add(8);
            character_Skill.useQingGongList.Add(9);
            character_Skill.useQingGongList.Add(10);
            character_Skill.useQingGongList.Add(11);
        }

        GameObject npc = GameObject.FindWithTag("NPC");
        Character_Skill npcSkill = npc.GetComponent<Character_Skill>();
        npcSkill.useSkillList.Add(1);
        npcSkill.useSkillList.Add(2);
        npcSkill.useSkillList.Add(3);

        npcSkill.useQingGongList.Add(7);
        npcSkill.useQingGongList.Add(8);
        npcSkill.useQingGongList.Add(9);
    }

    public void OnStartBattle()
    {
        BattleManager battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        battleManager.battleStart = true;
    }
}
