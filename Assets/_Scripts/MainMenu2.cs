using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using JetBrains.Annotations;
using System.Collections;

public class MainMenu2 : MonoBehaviour
{
    public string sceneName;
    public float videoLenght;
    public GameObject Background;
    public GameObject mainİmage;
    public GameObject animationİmage;
    public GameObject videoİmage;
    public float animationLenght;
    public float timer;
    public bool isStarted;

    public void LoadGame()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void StartTimer()
    {
        isStarted = true;
    }
    void Update()
    {
        if(isStarted)
        {
            timer += Time.deltaTime;
        }
        if(timer > 0 && timer <= animationLenght)
        {
            mainİmage.SetActive(false);
            animationİmage.SetActive(true);
        }
        if(timer > animationLenght && timer <= videoLenght)
        {
            Background.SetActive(false);
            animationİmage.SetActive(false);
            videoİmage.SetActive(true);
        }
        if(timer > videoLenght)
        {
            LoadGame();
        }
    }

    
}
