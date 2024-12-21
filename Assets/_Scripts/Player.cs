using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using System.Collections;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    private Rigidbody2D playerRb;
    Vector3 movement;
    private List<KeyCode> pressedKeys = new List<KeyCode>();

    float x,y;
    private KeyCode lastKeyPressed;
    [SerializeField] private float timeBetweenDashes;
    [SerializeField] private float attackDuration;
    public bool isAttacked = false;
    private bool canDash = true;

    [SerializeField] private int dashForce;
    private void Awake() {
    playerRb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {   
        
    }


    void Update()
    {
        x = 0;
        y = 0;
        LastKeyPressedByPlayer();
        lastKeyPressed = pressedKeys.Last();
        if(lastKeyPressed == KeyCode.A)
        {
            x = -1;
            y = 0;
            transform.rotation = Quaternion.Euler(new Vector3(0,0,180));
        }
        if(lastKeyPressed == KeyCode.D)
        {
            x = 1;
            y = 0;
            transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        }
        if(lastKeyPressed == KeyCode.W)
        {
            x = 0;
            y = 1;
            transform.rotation = Quaternion.Euler(new Vector3(0,0,90));
        }
        if(lastKeyPressed == KeyCode.S)
        {
            x = 0;
            y = -1;
            transform.rotation = Quaternion.Euler(new Vector3(0,0,270));
        }
        if(lastKeyPressed == KeyCode.LeftShift && canDash)
        {
            Dash();
        }
        if(lastKeyPressed == KeyCode.Mouse0)
        {
            Attack();
            
        }
        if(isAttacked)
        {
            x = 0;
            y = 0;
        }

        movement  = new Vector2(x,y);
        

        playerRb.AddForce(movement * 500 * Time.deltaTime);
    }
    void LastKeyPressedByPlayer()
    {
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                if (!pressedKeys.Contains(key))
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
        playerRb.linearVelocity = transform.right * Time.deltaTime * dashForce;
        canDash = false;
        StartCoroutine("WaitForDash");
    }
    void Attack()
    {
        if(!isAttacked)
        {
        isAttacked = true;
        }
        StartCoroutine("AttackTime");
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

    
}
