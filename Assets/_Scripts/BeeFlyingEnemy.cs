using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.AI;

public class BeeFlyingEnemy : Enemy
{
    private NavMeshAgent agent;
    private Vector3 desiredDirection;
    private float timer;
    private Player player;
    private Rigidbody2D rb;
    [SerializeField] private float movementSpeed;

    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        player = playerTransform.GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }
    public override void Attack(Vector3 direction)
    {
        player.TakeDamage(damage);
        timer = 0;
    }
    

    void Update()
    {
        FacePlayer();
        float distance = Vector3.Distance(playerTransform.position,transform.position);
        desiredDirection = playerTransform.position - transform.position;
        if(distance <= 1.75f)
        {
        rb.linearVelocity = Vector3.zero;
        timer += Time.deltaTime;
        if(timer >= 60 / attackPerMinute )
        {
            
            Attack(Vector3.zero);
            
        }
        
        }
        if(distance > 1.75f)
        {
        rb.linearVelocity = desiredDirection * movementSpeed;
        }
        Debug.Log(healt);
        Die();
    }

    
}
