using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CrockEnemy : Enemy
{
    private NavMeshAgent agent;
    private Player player;
    private Vector3 desiredDirection;
    private bool isPlayerInSide;
    private Transform enemyForward;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.Find("Player").transform;
        player = playerTransform.GetComponent<Player>();
        enemyForward = GetComponentInChildren<Transform>();
    }
    public override void Move()
    {
    agent.SetDestination(playerTransform.position);
    }
    public void CheckPlayer()
    {
    desiredDirection = playerTransform.position - transform.position;
    RaycastHit2D hit2D = Physics2D.Raycast(-enemyForward.position,desiredDirection,50);
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
    void Update()
    {
        FacePlayer();
        Move();
        CheckPlayer();
        Debug.Log(isPlayerInSide);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(enemyForward.position,desiredDirection);
    }
}
