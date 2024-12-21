using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float movementSpeed;
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
        playerTrigger = GetComponent<CircleCollider2D>();
        playerTrigger.radius = colliderRange;
    }


    public virtual void Move()
    {

    }



    public virtual void Attack()
    {

    }

    public void FacePlayer()
    {
        playerTransform = GameObject.Find("Player").transform;
        if(playerTransform.transform.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0,-180,0));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position,colliderRange);
    }

}
