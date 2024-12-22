using System;
using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Enemy enemy;
    [SerializeField] private int swordDamage;
    [SerializeField] private CinemachineImpulseSource impulse;
    [SerializeField] private Player player;
    private Rigidbody2D rb;
    [SerializeField] float swordWaitTime;
    private bool isSwinging = false;
    private float swordTimer;
    private bool canDestroy = false;
    [SerializeField] private float swordForce;
    [SerializeField] private float swordLerpTime;
    [SerializeField] private float pushForce;
    [SerializeField] private float reflectForce;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Player>();
        player.swordİnTheScenes++;
        transform.rotation = Quaternion.Euler(player.mousePos);
        float angle  = Mathf.Atan2(player.swordDirection.normalized.x,player.swordDirection.normalized.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,-angle);
    }
   void OnTriggerEnter2D(Collider2D collider)
   {
    if(collider.CompareTag("Enemy"))
    {
        Rigidbody2D rb2 = collider.transform.GetComponent<Rigidbody2D>();
        enemy = collider.GetComponent<Enemy>();
        enemy.TakeDamage(swordDamage);
        Debug.Log("1");
        rb2.AddForce(transform.up.normalized * pushForce,ForceMode2D.Impulse);
        impulse.GenerateImpulse();
        
    }
    if(collider.CompareTag("Bullet"))
    {
        Bullet bullet = collider.transform.GetComponent<Bullet>();
        bullet.rb.linearVelocity = bullet.rb.linearVelocity * -1 * reflectForce;
    }
   }
   void Update()
   {
    if(canDestroy)
    {
        Destroy(gameObject);
    }
    swordTimer += Time.deltaTime;
    if(swordTimer < swordWaitTime)
    {
        Vector3 playerRigidbody = new Vector3(player.playerRb.linearVelocity.x,player.playerRb.linearVelocity.y,0);
        rb.linearVelocity = playerRigidbody + (transform.up * swordForce) ;
    }
    if(swordTimer > swordWaitTime)
    {
        canDestroy = true;
    }
    
   

}
}
