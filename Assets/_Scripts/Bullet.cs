using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    
    [SerializeField] private float bulletForce;
    [SerializeField] private int damage;
    private Transform playerTransform;
    private Player player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.Find("Player").transform;
        Vector3 desiredRotation = playerTransform.position - transform.position;
        rb.linearVelocity = desiredRotation * bulletForce;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(!collider.CompareTag("Sword")) 
        {
            Destroy(gameObject);
        }
        if(collider.tag == "Player")
        {
            player = collider.GetComponent<Player>();
            player.TakeDamage(damage);

        }
        if(collider.tag == "Enemy")
        {
            Enemy enemy = collider.transform.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }
        
    }
}
