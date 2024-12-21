using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeatureLevelUp : MonoBehaviour
{
    public Feature[] abilities;
    public Button[] buttons;

    void Start()
    {
        SetButtonImage();
        AssignButtonListeners();
    }

    void SetButtonImage()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].image.sprite = abilities[i].sprites[0]; // Ýlk sprite atanýyor
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

    public void UpdateButtonLevel(int index)
    {
        if (buttons.Length > index && buttons[index] != null)
        {
            TextMeshProUGUI buttonText = buttons[index].GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = abilities[index].level.ToString();
                Debug.Log($"Button {index} level updated to {abilities[index].level}");
            }
            else
            {
                Debug.LogWarning("Button does not have a TextMeshProUGUI component!");
            }
        }
        else
        {
            Debug.LogWarning("Button array is empty or null!");
        }
    }

    public void UpgradeAbility(int index)
    {
        if (abilities[index].level < 3) // Seviyeyi 3'ten büyük yapmýyoruz.
        {
            abilities[index].level++;
            Debug.Log($"Feature {index} level upgraded to {abilities[index].level}");
            UpdateButtonLevel(index);
        }
        else
        {
            Debug.Log($"Feature {index} has reached max level");
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
