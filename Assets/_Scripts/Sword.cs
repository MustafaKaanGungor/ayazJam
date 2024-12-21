using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Enemy enemy;
    [SerializeField] private int swordDamage;
    [SerializeField] private CinemachineImpulseSource impulse;

    private void Start() {
    }
   void OnTriggerEnter2D(Collider2D collider)
   {
    if(collider.CompareTag("Enemy"))
    {
        enemy = collider.GetComponent<Enemy>();
        enemy.TakeDamage(swordDamage);
        impulse.GenerateImpulse();
        
        
    }
   }

}
