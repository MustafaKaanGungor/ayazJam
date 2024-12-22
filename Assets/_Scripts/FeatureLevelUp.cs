using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FeatureLevelUp : MonoBehaviour
{
    public static FeatureLevelUp instance { get; set; }
    public Feature[] abilities;
    public Button[] buttons;
    public FadeOutPanel fadeOutPanel;
    public Player player;
    private bool isClickable = true;
    public int timer;
    public int num = 0;
    [HideInInspector]
    public  float elapsedTime = 0;
    [HideInInspector]
    public bool stop = false;
    public TextMeshProUGUI textMeshProUGUI;
    public bool isNormal = false;
    public int  butonindex = 0;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        instance = this;
    }
    void Start()
    {
        SetNormalLevel();
        if(fadeOutPanel == null)
        {
            fadeOutPanel = GetComponent<FadeOutPanel>();
        }
        
        //SetButtonImage(); // Ýlk görselleri yükle
        //SetButtonText();  // Ýlk metinleri ayarla
        AssignButtonListeners(); // Týklama dinleyicilerini baðla
    }

    private void Update()
    {

        Timer();
        //if (Input.GetKeyUp(KeyCode.Space)) 
        //{
        //    fadeOutPanel.FadeIn();
        //}
    }


    void UpdateButtonImage(int index)
    {

        buttons[index].image.sprite = abilities[index].sprites[player.attackPower - 1];

    }
    void SetButtonImage()
    {
        for (int i = 0; i < abilities.Length; i++) {
            buttons[i].image.sprite = abilities[i].sprites[0];
        }
    }
    void SetButtonImage(int index)
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            buttons[i].image.sprite = abilities[i].sprites[0];
        }
    }

    void SetButtonText()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            TextMeshProUGUI buttonText = buttons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = (abilities[i].level).ToString();
            }
            else
            {
                Debug.LogWarning($"Button {i} has no TextMeshProUGUI component!");
            }
        }
    }

    void UpdateButtonLevel(int index)
    {
        TextMeshProUGUI buttonText = buttons[index].GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            buttonText.text = abilities[index].level.ToString();
        }
    }
    //


    void SetZero(int index)
    {
        buttons[index].image.sprite = abilities[index].sprites[0];
    }
    public void UpgradeAbility(int index)
    {

        if (isClickable & abilities[index].level < 3)
        {
            butonindex = index;

            Time.timeScale = 1;
            stop = false;
            //
            SetZero(index);
            player.SetAbilityZero(index);
            abilities[index].level += 1;
            //
            fadeOutPanel.FadeOut();
            //
            
        }
        else
            textMeshProUGUI.text = ("MAX!!");


    }
    void AssignButtonListeners()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Lambda'da kullanýlacak doðru index
            buttons[i].onClick.AddListener(() => UpgradeAbility(index));

        }

    }

    //fade outdan sonra çalýþacak
    void SetPlayerValues()
    {
        for (int i = 0; i<abilities.Length;i++)
        {
            if(i == 0)
            player.attackP = abilities[i].level;
            if(i == 1)
            player.speedP = abilities[i].level;
            if (i == 2)
            player.healthP = abilities[i].level;
        }
    }
    //her fadeinden sonra çalýþacak
    void SetNormalLevel()
    {
        
        for (int i = 0;i < buttons.Length;i++)
        {
           
                TextMeshProUGUI buttonText = buttons[i].GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = (abilities[i].level).ToString();
                buttons[i].image.sprite = abilities[i].sprites[abilities[i].level];
        }
    }
    public void Timer()
    {
        if (!stop)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= timer)
            {

                SetNormalLevel();
                fadeOutPanel.FadeIn();
                player.SetAbilityNormal(butonindex, abilities[butonindex].level);
                elapsedTime = 0f;
                stop = true;
            }
        }
    }

    [System.Serializable]
    public class Feature
    {
        public string name;
        public int level;
        public Sprite[] sprites;

        public int GetAbilityLevel()
        {
            return level;
        }
    }

    IEnumerator HidePanel()
    {
        yield return new WaitForSeconds(0.5f);
        fadeOutPanel.FadeOut();
        SetPlayerValues();
        Time.timeScale = 1f;
        yield return new WaitForSeconds(2f);
        isClickable = true;
    }
}
