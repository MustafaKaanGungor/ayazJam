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
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class Player : MonoBehaviour
{
    #region "Hareket"
    public Rigidbody2D playerRb;
    
    public Camera cam;
    [HideInInspector]
    public Vector3 mousePos;
    [HideInInspector]
    public int swordİnTheScenes = 0;
    public GameObject swordPrefab;
    public float swordMaxTime;
    [HideInInspector]
    private Vector3 dashRotation;
    [HideInInspector]
    public Vector3 swordDirection;
    [HideInInspector]
    Vector3 movement;
    private List<KeyCode> pressedKeys = new List<KeyCode>();
    [HideInInspector]
    float x,y;
    [HideInInspector]
    private KeyCode lastKeyPressed;
    [SerializeField] private float timeBetweenDashes;
    private float dashTimer;
    [SerializeField] private float attackDuration;
    [HideInInspector]
    public bool isAttacked = false;
    private bool canDash = true;

    [SerializeField] private int dashForce;
    private bool IsFacingRight;
    //[SerializeField] private float moveSpeed;
    //private bool isSwordMax;
    //private float swordMaxValue;
    #endregion

    #region "Saldiri"
    [SerializeField] private float attackDashForce; 
    private Animator animator;
    [HideInInspector] 
    public bool swordTrigger = false;
    #endregion

    #region "UI"
    [SerializeField] Slider dashSlider;
    [SerializeField] RectTransform healthBar;
    [SerializeField] GameObject border;
    #endregion
    #region "Health"
    public int healt;
    public int maxHealth;
    [SerializeField] Slider slider;
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
        if (healthBar != null) {
            Debug.Log("var");        
        }
        else
            Debug.Log("yok");
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
        slider.value = healthP;
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
        speedP = 300;
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
                speedP = 700;
            if (seviye == 2)
                speedP = 900;
            if (seviye == 3)
                dashForce = 10;
                speedP = 900;
        }
        if(index == 2)
        {
            if (seviye == 1)
            {
                healthP = 100;
                maxHealth = healthP;
                slider.maxValue = maxHealth;
                slider.value = healthP;
            }
                

            if (seviye == 2)
            {
                healthP = 150;
                maxHealth = healthP;
                slider.maxValue = maxHealth;
                healthBar.sizeDelta = new Vector2(585, 178);
                border.gameObject.transform.localScale = new Vector3(1.05f, 1f, 1f);
                slider.value = healthP;
            }
                
            if (seviye == 3)
            {
                healthP = 200;
                maxHealth = healthP;
                healthBar.sizeDelta = new Vector2(610, 178);
                border.gameObject.transform.localScale = new Vector3(1.1f, 1f, 1f);
                slider.maxValue = maxHealth;
                slider.value = healthP;
            }
            

        }
    }
    //public void SetSpeedP()
    //{
    //    speedP = 300;
    //}

    //public void SetHealt()
    //{
    //    healthP = 5;
    //    slider.value = healthP;
    //}
    void Die()
    {
        if(healthP <= 0)
        {
            //endGameImage.SetActive(true);
            SceneManager.LoadScene(1);
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    
}
