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

    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        player = playerTransform.GetComponent<Player>();
    }
    public override void Move()
    {
        agent.SetDestination(playerTransform.position);
    }
    public override void Attack(Vector3 direction)
    {
        player.TakeDamage(damage);
        timer = 0;
    }
    

    void Update()
    {
        if(agent.velocity.magnitude < 1)
        {
        timer += Time.deltaTime;
        desiredDirection = playerTransform.position - transform.position;
        if(timer >= 60 / attackPerMinute )
        {
            
            Attack(Vector3.zero);
            
        }
        
        }
        
        FacePlayer();
        Move();
        Debug.Log(player.healt);
    }

    
}
