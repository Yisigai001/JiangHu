using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("������Ϣ")]
    public int battleType; //��������: 1,��������. 2,����
    public GameObject target;
    private Character_Attribute attribute;

    [Header("Ѱ��")]
    public float searchDelay; //Ѱ�м��
    private float searchTime;

    [Header("�ƶ�")]
    private float enemydistance;
    public float stopMoveDistance;
    Animator animator;

    [Header("ս��")]
    public bool inBattle;
    private BattleManager battleManager;
    public List<GameObject> enemyList;

    void Start()
    {
        searchDelay = 0.5f; //Ѱ�м��
        stopMoveDistance = 0.5f; 
        enemyList = new List<GameObject>();
        attribute = GetComponent<Character_Attribute>();
        SetRigBody(); //����rigbody����
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
    /// Ѱ��
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
    /// ����rigbody
    /// </summary>
    private void SetRigBody()
    {
        // ȷ�����������и������ײ�����
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        Collider2D collider = gameObject.GetComponent<Collider2D>();

        if (rb == null || collider == null)
        {
            Debug.LogError("Rigidbody2D or Collider2D component is missing.");
            return;
        }

        // ������ײ������
        collider.isTrigger = false;

        // ����һ���µ�������ʣ�������Ħ����Ϊ��
        PhysicsMaterial2D physicsMaterial = new PhysicsMaterial2D();
        physicsMaterial.friction = 0;
        collider.sharedMaterial = physicsMaterial;

        // ���ø���Ϊ�˶�ѧ����ֹ�ܵ������������Ӱ��
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    /// <summary>
    /// ��ɫ�ƶ�
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
                    // ����������Ŀ�����ľ������ÿ֡�ƶ��ľ��룬������ƶ�
                    if (direction.magnitude > distanceToMove)
                    {
                        // ����ÿ֡��Ҫ�ƶ��ľ��룬�𽥵��������λ��
                        transform.position += (Vector3)direction.normalized * distanceToMove;
                    }
                    else
                    {
                        // ��������Ѿ��ӽ�Ŀ�������ֱ���ƶ���Ŀ������λ��
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
