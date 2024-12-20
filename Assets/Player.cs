using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private void Awake() {
        playerRb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {   
        
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(x,y);

        playerRb.AddForce(movement * 500 * Time.deltaTime);
    }
}
