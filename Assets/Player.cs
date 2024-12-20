using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private Vector2 wsadInput;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private void Awake() {
        playerRb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {   
        
    }

    void Update()
    {
        wsadInput.x = Input.GetAxis("Horizontal");
        wsadInput.y = Input.GetAxis("Vertical");
        Debug.Log(wsadInput);
        playerRb.AddForce(wsadInput.x * Time.deltaTime * moveSpeed * Vector2.right, ForceMode2D.Force);

        if(wsadInput.y >= 0) {
            playerRb.AddForce(new Vector2(0, wsadInput.y) * jumpForce, ForceMode2D.Impulse);
        }
    }
}
