using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Enemy enemy;
    [SerializeField] private int swordDamage;
   void OnTriggerEnter2D(Collider2D collider)
   {
    if(collider.tag == "Enemy")
    {
        enemy = collider.GetComponent<Enemy>();
        enemy.TakeDamage(swordDamage);
        
    }
   }

}
