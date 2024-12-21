using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Player player;

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Hit");
        if(player.isAttacked)
        {
            if(collider.tag == "Enemy")
            {
                Destroy(collider.gameObject);
            }
        }
    }


}
