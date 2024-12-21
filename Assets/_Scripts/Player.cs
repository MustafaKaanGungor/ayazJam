using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region "Hareket"
    private Rigidbody2D playerRb;
    private Vector3 dashRotation;
    public int healt;
    Vector3 movement;
    private List<KeyCode> pressedKeys = new List<KeyCode>();

    float x,y;
    private KeyCode lastKeyPressed;
    [SerializeField] private float timeBetweenDashes;
    private float dashTimer;
    [SerializeField] private float attackDuration;
    public bool isAttacked = false;
    private bool canDash = true;

    [SerializeField] private int dashForce;
    private bool IsFacingRight;
    [SerializeField] private float moveSpeed;
    #endregion

    #region "Saldiri"
    [SerializeField] private float attackDashForce; 
    private Animator animator;
    public int attackPower = 1;
    #endregion

    #region "UI"
    [SerializeField] Slider dashSlider;
    #endregion

    
    private void Awake() {
    playerRb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    
    }
    void Start()
    {   
        
    }


    void Update()
    {
        dashTimer += Time.deltaTime;
        dashSlider.value = dashTimer;
        if(dashTimer > timeBetweenDashes) {
            canDash = true;
        }

        x = 0;
        y = 0;
        LastKeyPressedByPlayer();
        if(pressedKeys.Count != 0) {
            lastKeyPressed = pressedKeys.Last();
            animator.SetBool("isWalking", true);
        } else {
            lastKeyPressed = KeyCode.None;
            animator.SetBool("isWalking", false);
        }
        if(lastKeyPressed == KeyCode.A)
        {
            x = -1;
            y = 0;
            if(!isAttacked && !IsFacingRight) {
                Turn();
                dashRotation = -transform.right.normalized;
            }
        }
        if(lastKeyPressed == KeyCode.D)
        {
            x = 1;
            y = 0;
            if(!isAttacked && IsFacingRight) {
                Turn();
                dashRotation = transform.right.normalized;
            }
        }
        if(lastKeyPressed == KeyCode.W)
        {
            x = 0;
            y = 1;
            dashRotation = transform.up.normalized;
            
        }
        if(lastKeyPressed == KeyCode.S)
        {
            x = 0;
            y = -1;
            dashRotation = -transform.up.normalized;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            Dash();
            animator.SetTrigger("Dash");
        }
        

        movement  = new Vector2(x,y);
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack(movement);
            
        }
        if(isAttacked)
        {
            x = 0;
            y = 0;
        }

        playerRb.AddForce(movement * moveSpeed * Time.deltaTime);
        Debug.Log(healt);
    }
    void LastKeyPressedByPlayer()
    {
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                if (!pressedKeys.Contains(key) && (key == KeyCode.W || key == KeyCode.A || key == KeyCode.S || key == KeyCode.D))
                {
                    pressedKeys.Add(key);
                }
            }
            else if (Input.GetKeyUp(key))
            {
                pressedKeys.Remove(key);
            }
        }
    }
    public List<KeyCode> GetPressedKeys()
    {
        return new List<KeyCode>(pressedKeys);
    }

    void Dash()
    {
         
        playerRb.AddForce(dashRotation * dashForce, ForceMode2D.Impulse);
        //playerRb.linearVelocity = transform.right * Time.deltaTime * dashForce;
        canDash = false;
        dashTimer = 0;
    }
    void Attack(Vector2 direction)
    {
        if(!isAttacked) {
            isAttacked = true;
            if(dashRotation == transform.right.normalized)
            {
            animator.SetTrigger("AttackRight");
            }
            if(dashRotation == -transform.right.normalized)
            {
                animator.SetTrigger("AttackLeft");
            }
            if(dashRotation == transform.up.normalized)
            {
                animator.SetTrigger("AttackUp");
            }
            if(dashRotation == -transform.up.normalized)
            {
                animator.SetTrigger("AttackDown");
            }
            playerRb.linearVelocity = dashRotation * attackDashForce;
        }
        StartCoroutine("AttackTime");
    }

    public void TakeDamage(int damage)
    {
        healt -= damage;
    }

    IEnumerator WaitForDash()
    {
        yield return new WaitForSeconds(timeBetweenDashes);
        canDash = true;
    }

    IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(attackDuration);
        isAttacked = false;
    }

    private void Turn()
	{
		Vector3 scale = transform.localScale; 
		scale.x *= -1;
		transform.localScale = scale;

		IsFacingRight = !IsFacingRight;
	}
    

    
}
