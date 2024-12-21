using Unity.VisualScripting;
using UnityEngine;

public class RangedGroundedEnemy : Enemy
{

    private bool isPlayerİnGround;
    [SerializeField] private Transform forwardObject;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            isPlayerİnRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            isPlayerİnRange = false;
        }
    }

    void Update()
    {
        if(isPlayerİnRange)
        {
            FacePlayer();
            isPlayerOnGround();
        }
        Debug.Log(isPlayerİnGround);
    }

    void isPlayerOnGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(forwardObject.position,transform.right);
        if(hit.collider.name == "Player")
        {
            isPlayerİnGround = true;
        }
        else
        {
            isPlayerİnGround = false;
        }
        
    }

    

}

