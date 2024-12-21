using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int healt;
    public float movementSpeed;
    public float rotationSpeed;
    public float colliderRange;
    public CircleCollider2D playerTrigger;

    public bool isPlayerİnRange;

    public float rangeFromPlayer;

    public float wantedRangeFromPlayer;

    public float attackPerMinute;

    public bool isPlayerBehind;

    public Transform playerTransform;

    public float rayDistance;

    

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

    public virtual void FacePlayer()
    {
        
        
    }

    public void TakeDamage(int damage)
    {
        healt -= damage;
    }

}
