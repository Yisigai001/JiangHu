using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int SP;
    public int SPMax;
    public int SPMaxBas = 60;
    public List<int> skillList;
    public List<int> useSkillList;
    public List<int> playerAllSkill;

    void Start()
    {
        skillList = new List<int>();
        useSkillList = new List<int>();
        playerAllSkill = new List<int>();
    }


}
