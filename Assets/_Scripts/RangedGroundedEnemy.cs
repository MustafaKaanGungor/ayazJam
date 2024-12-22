using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RangedGroundedEnemy : Enemy
{
    public GameObject bullet;
    private Vector3 desiredDirection;
    private float timer;

    [SerializeField] private Transform muzzle;

    private void Start() {
        animator = GetComponent<Animator>();
    }
    
    public override void Attack(Vector3 direction)
    {
        Instantiate(bullet,muzzle.position,Quaternion.Euler(direction));
        animator.SetTrigger("Attack");
        timer = 0;
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        desiredDirection = playerTransform.position - muzzle.position;
        if(timer >= 60 / attackPerMinute )
        {
            Attack(desiredDirection);
        }
        FacePlayer();
        Die();
    }



}

