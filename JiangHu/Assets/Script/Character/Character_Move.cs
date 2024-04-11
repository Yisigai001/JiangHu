using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Move : MonoBehaviour
{
    public int moveType;
    public GameObject target;
    public Vector2 targetPosition;
    public float targetDistance;
    private Animator animator;
    public bool moveIng;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
