using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRb;

    private Vector2 wsadInput;
    private bool isFacingRight;

    private void Awake() 
    {
        playerRb = GetComponent<Rigidbody2D>();    
    }

    void Start()
    {
        isFacingRight = true;
    }

    void Update()
    {
        wsadInput.x = Input.GetAxis("Horizontal");
        wsadInput.y = Input.GetAxis("Vertical");
    }
}
