using Unity.VisualScripting;
using UnityEngine;

public class HerbEnemy : Enemy
{
    private float timer;
    private Player player;
    private bool isInRange = false;
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        player = playerTransform.GetComponent<Player>();
        
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        FacePlayer();
        if(isInRange)
        {
            timer += Time.deltaTime;
            if(timer >= 60 / attackPerMinute)
            {
                Attack(Vector3.zero);
            }

        }
        Debug.Log(player.healt);
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            isInRange = true;
        }

    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            isInRange = false;
        }
    }
    public override void Attack(Vector3 direction)
    {
        player.TakeDamage(damage);
        timer = 0;
        animator.SetTrigger("Attack");

    }

}
