using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FeatureLevelUp : MonoBehaviour
{
    public Feature[] abilities;
    public Button[] buttons;
    public FadeOutPanel fadeOutPanel;
    private bool isClickable = true;
    public bool isSaved = false;
    void Start()
    {
        GetData();

        if (!isSaved) {
            SetButtonImage(); // Ýlk görselleri yükle
            SetButtonText();  // Ýlk metinleri ayarla
            
        }
        AssignButtonListeners(); // Týklama dinleyicilerini baðla
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Fade iþlemini boþluk tuþuyla tetikle
        {
            SaveData();
        }if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerPrefs.DeleteAll();
        }
    }
    void SetButtonImage()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].image.sprite = abilities[i].sprites[i]; // Ýlk sprite atanýyor
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

    public void UpgradeAbility(int index)
    {
        if (abilities[index].level < 3 & isClickable) // Max seviye kontrolü
        {
            abilities[index].level++;
            Debug.Log($"Feature {index} level upgraded to {abilities[index].level}");

            // Görseli güncelle
            UpdateButtonImage(index);

            // Metni güncelle
            UpdateButtonLevel(index);
            isClickable = false;
            StartCoroutine(HidePanel());

        }
        else
        {
            Debug.Log($"Feature {index} has reached max level");
            
        }

    }

    void UpdateButtonImage(int index)
    {
        if (abilities[index].level < abilities[index].sprites.Length)
        {
            buttons[index].image.sprite = abilities[index].sprites[abilities[index].level];
            Debug.Log($"Button {index} image updated for level {abilities[index].level}");
        }
        else
        {
            Debug.LogWarning($"No sprite defined for level {abilities[index].level}");
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

    void SaveData()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            PlayerPrefs.SetString($"Name_{i}", abilities[i].name);
            PlayerPrefs.SetInt($"Level_{i}", abilities[i].level);
        }
        PlayerPrefs.Save();
        isSaved = true;
        Debug.Log("Data saved!");
    }
    void GetData()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            // Veri yükleme
            abilities[i].name = PlayerPrefs.GetString($"Name_{i}", abilities[i].name);
            abilities[i].level = PlayerPrefs.GetInt($"Level_{i}", abilities[i].level);

            // Yüklenen veriyi butonlara yansýt
            buttons[i].image.sprite = abilities[i].sprites[abilities[i].level];
            TextMeshProUGUI buttonText = buttons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = abilities[i].level.ToString();
            }
        }
    }
    IEnumerator HidePanel()
    {
        yield return new WaitForSeconds(0.5f);
        fadeOutPanel.FadeOut();
        yield return new WaitForSeconds(2f);
        isClickable = true;
    }
    
}
