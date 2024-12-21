using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeatureLevelUp : MonoBehaviour
{
    public Feature[] abilities;
    public Button[] buttons;

    void Start()
    {
        SetButtonImage(); // Ýlk görselleri yükle
        SetButtonText();  // Ýlk metinleri ayarla
        AssignButtonListeners(); // Týklama dinleyicilerini baðla
    }

    void SetButtonImage()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].image.sprite = abilities[i].sprites[0]; // Ýlk sprite atanýyor
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
        if (abilities[index].level < 3) // Max seviye kontrolü
        {
            abilities[index].level++;
            Debug.Log($"Feature {index} level upgraded to {abilities[index].level}");

            // Görseli güncelle
            UpdateButtonImage(index);

            // Metni güncelle
            UpdateButtonLevel(index);
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
}
