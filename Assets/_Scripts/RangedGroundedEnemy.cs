using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RangedGroundedEnemy : Enemy
{
    public GameObject bullet;
    private Vector3 desiredDirection;
    private float timer;
    public override void FacePlayer()

    {
        if(playerTransform.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0,-180,0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
    }
    
    public override void Attack(Vector3 direction)
    {
        Instantiate(bullet,transform.position,Quaternion.Euler(direction));
        timer = 0;
    }
    IEnumerator AttackWait()
    {
        yield return new WaitForSeconds(60 / attackPerMinute);
    }
    void Update()
    {
        timer += Time.deltaTime;
        desiredDirection = playerTransform.position - transform.position;
        if(timer >= 60 / attackPerMinute )
        {
            Attack(desiredDirection);
        }
        FacePlayer();
    }



}

