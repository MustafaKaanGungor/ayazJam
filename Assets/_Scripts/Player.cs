using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UI;
using TMPro;
using Unity.Cinemachine;
using JetBrains.Annotations;

public class Player : MonoBehaviour
{
    #region "Hareket"
    public Rigidbody2D playerRb;
    
    public Camera cam;
    private bool isSwordMax;
    private float swordMaxValue;
    public Vector3 mousePos;
    public int swordİnTheScenes = 0;
    public GameObject swordPrefab;
    public float swordMaxTime;
    private Vector3 dashRotation;
    public Vector3 swordDirection;
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
    public bool swordTrigger = false;
    [SerializeField] private float swordForce;
    [SerializeField] private float expectedSwordTimer;
    #endregion

    #region "UI"
    [SerializeField] Slider dashSlider;
    #endregion
    #region "Health"
    public int healt;
    public int maxHealth;
    public Slider slider;
    public GameObject endGameImage;
    #endregion

    #region "Player deger"
    public int attackP = 1;
    public int speedP = 1;
    public int healthP = 1;
    #endregion
    private void Awake() {
    playerRb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    
    }
    void Start()
    {   
        slider.value = healthP;
    }


    void Update()
    {
        Die();
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
        if(Input.GetKeyDown(KeyCode.Mouse0) && !isAttacked)
        {
            Attack(movement);
            
        }
        if(isAttacked)
        {
            x = 0;
            y = 0;
        }

        playerRb.AddForce(movement * speedP * Time.deltaTime);
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        swordDirection = mousePos - transform.position;
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
        isAttacked = true;
        GameObject sword = Instantiate(swordPrefab, transform.position, Quaternion.identity);
        sword.transform.SetParent(transform);
        SwordDashForce();
        StartCoroutine("AttackTime");
        
    }
    void SwordDashForce()
    {
        playerRb.AddForce(swordDirection.normalized * attackDashForce, ForceMode2D.Impulse);
    }

    public void TakeDamage(int damage)
    {
        healthP -= damage;
        animator.SetTrigger("GotDamaged");
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

    public void SetAbilityZero(int index)
    {
        if (index == 0) 
        attackP = 5;
        if (index == 1)
        speedP = 0;
        if (index == 2)
        {
            healthP = 5;
            slider.value = healthP;
        }
    }
    //fadeoutdan sonra çıksın
    public void SetAbilityNormal(int index , int seviye)
    {
        if(index == 0)
        {
            if (seviye == 1)
                attackP = 20;
            if (seviye == 2)
                attackP = 25;
            if (seviye == 3)
                attackP = 50;
        }
        if (index == 1) 
        {
            if (seviye == 1)
                speedP = 1000;
            if (seviye == 2)
                speedP = 1300;
            if (seviye == 3)
                speedP = 1500;
        }
        if(index == 2)
        {
            if (seviye == 1)
                healthP = 100;
            if (seviye == 2)
                healthP = 150;
            if (seviye == 3)
                healthP = 200;
            slider.value = healthP;
        }
    }
    public void SetSpeedP()
    {
        speedP = 0;
    }

    public void SetHealt()
    {
        healthP = 5;
        slider.value = healthP;
    }
    void Die()
    {
        if(healthP <= 0)
        {
            endGameImage.SetActive(true);
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    
}
