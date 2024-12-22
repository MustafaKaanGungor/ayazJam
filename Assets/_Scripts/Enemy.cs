using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int healt;
    
    public int damage;


    public float attackPerMinute;


    public Transform playerTransform;


    public Animator animator;
    

    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        
    }


    public virtual void Move()
    {
    }



    public virtual void Attack(Vector3 direction)
    {
        
    }

    public  void FacePlayer()
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

    public void TakeDamage(int damage)
    {
        healt -= damage;
    }
    public void Die()
    {
        if(healt <= 0)
        {
            Destroy(gameObject);
        }
    }



}
