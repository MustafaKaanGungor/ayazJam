using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CrockEnemy : Enemy
{
    private NavMeshAgent agent;
    private Player player;
    private Vector3 desiredDirection;
    private bool isPlayerInSide;
    public Transform enemyForward;
    private float timer;
    public GameObject bullet;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.Find("Player").transform;
        player = playerTransform.GetComponent<Player>();
    }
    public override void Move()
    {
    agent.SetDestination(playerTransform.position);
    }
    public void CheckPlayer()
    {
    desiredDirection = playerTransform.position - transform.position;
    RaycastHit2D hit2D = Physics2D.Raycast(enemyForward.position,desiredDirection,50);
    if(hit2D.collider.tag == "Player")
    {
    isPlayerInSide = true;
    }
    else
    {
    isPlayerInSide = false;
    }
    Debug.Log(hit2D.collider.name);
    }
    public override void Attack(Vector3 direction)
    {
        if(isPlayerInSide)
        {
            Instantiate(bullet,transform.position,Quaternion.Euler(direction));
            timer = 0;
        }
    }
    void Update()
    {
        timer += Time.deltaTime;
        FacePlayer();
        Move();
        CheckPlayer();
        if(timer >= 60 / attackPerMinute)
        {
            Attack(Vector3.zero);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(enemyForward.position,desiredDirection);
    }
}
