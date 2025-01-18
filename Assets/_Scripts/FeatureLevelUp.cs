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
        Time.timeScale = 0;
        SetNormalLevel();
        if(fadeOutPanel == null)
        {
            fadeOutPanel = GetComponent<FadeOutPanel>();
        }
        
        AssignButtonListeners(); // Týklama dinleyicilerini baðla
    }

    private void Update()
    {
        Timer();
    }
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
    }
}
