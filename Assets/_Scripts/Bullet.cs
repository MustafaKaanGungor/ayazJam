using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    
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
        if(collider.tag != "Enemy")
        {
            Destroy(gameObject);
        }
        if(collider.tag == "Player")
        {
            player = collider.GetComponent<Player>();
            player.TakeDamage(damage);
            Debug.Log(player.healt);

        }
        
    }
}
