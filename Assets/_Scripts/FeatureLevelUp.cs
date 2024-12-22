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

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        instance = this;
    }
    void Start()
    {
        if(fadeOutPanel == null)
        {
            fadeOutPanel = GetComponent<FadeOutPanel>();
        }
        SetOne();
        //SetButtonImage(); // Ýlk görselleri yükle
        //SetButtonText();  // Ýlk metinleri ayarla
        AssignButtonListeners(); // Týklama dinleyicilerini baðla
    }

    private void Update()
    {
    }
    void SetOne()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].image.sprite = abilities[i].sprites[1];
            TextMeshProUGUI buttonText = buttons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = "1";
        }
    }
    void SetOne1(int index)
    {
        int level2 = abilities[index].level;
        buttons[index].image.sprite = abilities[index].sprites[level2];
        TextMeshProUGUI buttonText = buttons[index].GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = "1";
    }
    void SetTwo(int index)
    {
        int level2 = abilities[index].level;
            buttons[index].image.sprite = abilities[index].sprites[level2];
            TextMeshProUGUI buttonText = buttons[index].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = "2";
    }
    void SetThree(int index)
    {
        int level3 = abilities[index].level;
            buttons[index].image.sprite = abilities[index].sprites[level3];
            TextMeshProUGUI buttonText = buttons[index].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = "3";
        
    }
    public void UpgradeAbility(int index)
    {
        if (isClickable)
        {
            player.DecreaseAttack();
            if(num == 0)
            SetZero(index);
            if(num == 2)
                SetTwo(index);
            if (num == 3)
                SetThree(index);
            //UpdateButtonImage(index);
            //UpdateButtonLevel(index);
            fadeOutPanel.FadeOut();
        }

    }
    void SetZero(int index)
    {
        buttons[index].image.sprite = abilities[index].sprites[0];
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

    void AssignButtonListeners()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Lambda'da kullanýlacak doðru index
            buttons[i].onClick.AddListener(() => UpgradeAbility(index));

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
        yield return new WaitForSeconds(2f);
        isClickable = true;
    }
    public void Timer()
    {
        float elapsedTime = 0;
        elapsedTime += Time.deltaTime; 
        if (elapsedTime > timer)
        {
            Time.timeScale = 0f;
            //
        }
    }
    
}
