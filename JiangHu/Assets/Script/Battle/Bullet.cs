using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public int type;
    public float speed;
    public float time;
    public float releaseDelay;
    public bool disapper;
    public int buff1;
    public int buff2;
    public int buff3;
    public int buff4;
    public int buff5;
    public GameObject owner;
    public GameObject target;
    private Character_Attribute owner_Attribute;
    Rigidbody2D rigidbody2D;
    private float bulletCreateTime;
    void Start()
    {
        rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 0;
        rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        owner_Attribute = owner.GetComponent<Character_Attribute>();
        bulletCreateTime = Time.time;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector2 movement = transform.up * speed;
        rigidbody2D.velocity = movement;
        RemoveBullet();
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.layer == 6)
            {
                Character_Attribute target_Attribute = collision.GetComponent<Character_Attribute>();
                if (collision.gameObject != owner && owner_Attribute.camp != target_Attribute.camp)
                {
                    target = collision.gameObject;
                    Character_Buff character_Buff = collision.GetComponent<Character_Buff>();
                    if (buff1 > 0) character_Buff.AddBuff(buff1, owner);
                    if (buff2 > 0) character_Buff.AddBuff(buff2, owner);
                    if (buff3 > 0) character_Buff.AddBuff(buff3, owner);
                    if (buff4 > 0) character_Buff.AddBuff(buff4, owner);
                    if (buff5 > 0) character_Buff.AddBuff(buff5, owner);
                }
            }
            else if (collision.gameObject.layer == 9)
            {
                if (collision.transform.parent.gameObject != owner)
                {
                    Character_Skill character_Skill = collision.transform.parent.GetComponent<Character_Skill>();
                    character_Skill.canUseQingGong = true;
                    Debug.Log("找到了父节点: " + character_Skill);
                }
            }
        }
    }

    private void RemoveBullet()
    {
        if (Time.time - bulletCreateTime >= time/1000)
        {
            Destroy(gameObject);
        }
    }
}
