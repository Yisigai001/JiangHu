using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("基础信息")]
    public int battleType; //攻击类型: 1,主动攻击. 2,逃逸
    public GameObject target;
    private Character_Attribute attribute;

    [Header("寻敌")]
    public float searchDelay; //寻敌间隔
    private float searchTime;

    [Header("移动")]
    private float enemydistance;
    public float stopMoveDistance;
    Animator animator;

    [Header("战斗")]
    public bool inBattle;
    private BattleManager battleManager;
    public List<GameObject> enemyList;

    void Start()
    {
        searchDelay = 0.5f; //寻敌间隔
        stopMoveDistance = 0.5f; 
        enemyList = new List<GameObject>();
        attribute = GetComponent<Character_Attribute>();
        SetRigBody(); //设置rigbody属性
        searchTime = Time.time;
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        animator = GetComponent<Animator>();
        inBattle = false;
        if (attribute.camp == 1)
        {
            enemyList = battleManager.camp2;
        }
        else if (attribute.camp == 2)
        {
            enemyList = battleManager.camp1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (battleManager.battleSetDown)
        {
            SearchEnemy();
        }
        if (battleManager.battleStart)
        {
            CharacterMove();
        }
    }

    /// <summary>
    /// 寻敌
    /// </summary>
    private void SearchEnemy()
    {
        if (Time.time - searchTime >= searchDelay)
        {
            searchTime = Time.time;
            GameObject nearestTarget = null;
            float nearestDistancd = Mathf.Infinity;
            Vector2 characterPosition = transform.position;
            for (int i = 0; i < enemyList.Count; i++)
            {
                Vector2 enemyPosition = enemyList[i].transform.position;
                float distance = Vector2.Distance(characterPosition, enemyPosition);
                if (distance < nearestDistancd)
                {
                    nearestTarget = enemyList[i];
                    nearestDistancd = distance;
                }
            }
            target = nearestTarget;
            Character_SkillReleasePoint character_SkillReleasePoint = transform.Find("Chest").GetComponent<Character_SkillReleasePoint>();
            character_SkillReleasePoint.enemy = nearestTarget;
        }
    }

    /// <summary>
    /// 设置rigbody
    /// </summary>
    private void SetRigBody()
    {
        // 确保两个对象都有刚体和碰撞体组件
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        Collider2D collider = gameObject.GetComponent<Collider2D>();

        if (rb == null || collider == null)
        {
            Debug.LogError("Rigidbody2D or Collider2D component is missing.");
            return;
        }

        // 设置碰撞体属性
        collider.isTrigger = false;

        // 创建一个新的物理材质，并设置摩擦力为零
        PhysicsMaterial2D physicsMaterial = new PhysicsMaterial2D();
        physicsMaterial.friction = 0;
        collider.sharedMaterial = physicsMaterial;

        // 设置刚体为运动学，防止受到物理引擎的力影响
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    /// <summary>
    /// 角色移动
    /// </summary>
    public void CharacterMove()
    {
        if (battleManager.battleStart)
        {
            if (target != null)
            {
                CharacterTurn();
                enemydistance = Vector2.Distance(transform.position, target.transform.position);
                if (enemydistance <= stopMoveDistance)
                {
                    animator.SetBool("Move", false);
                    return;
                }
                else
                {
                    Vector2 direction = target.transform.position - transform.position;

                    float distanceToMove = attribute.moveSpeed * Time.deltaTime;
                    // 如果对象距离目标对象的距离大于每帧移动的距离，则继续移动
                    if (direction.magnitude > distanceToMove)
                    {
                        // 根据每帧需要移动的距离，逐渐调整对象的位置
                        transform.position += (Vector3)direction.normalized * distanceToMove;
                    }
                    else
                    {
                        // 如果对象已经接近目标对象，则直接移动到目标对象的位置
                        transform.position = target.transform.position;
                    }
                    animator.SetBool("Move", true);
                }
            }
        }
    }

    private void CharacterTurn()
    {
        if (target != null)
        {
            if (target.transform.position.x > transform.position.x)
            {
                transform.Find("Body").transform.rotation = Quaternion.Euler(0, 180, 0); 
            }
            else
            {
                transform.Find("Body").transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        
    }
}
