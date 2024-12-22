using UnityEngine;

public class FadeOutPanel : MonoBehaviour
{
    
    public CanvasGroup panelCanvasGroup; // Canvas Group bile�eni atanacak
    public float fadeDuration = 1.0f;   // Solukla�ma s�resi
    void Start()
    {
        
        if (panelCanvasGroup == null)
        {
            panelCanvasGroup = GetComponent<CanvasGroup>();
            
        }
    }

    
    public void FadeOut()
    {
        
        StartCoroutine(FadeOutCoroutine());
    }

    private System.Collections.IEnumerator FadeOutCoroutine()
    {
        float startAlpha = panelCanvasGroup.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            panelCanvasGroup.alpha = Mathf.Lerp(startAlpha, 0, elapsedTime / fadeDuration);
            yield return null;
        }

        panelCanvasGroup.alpha = 0;
        panelCanvasGroup.interactable = false;  // Paneli etkisiz hale getir
        panelCanvasGroup.blocksRaycasts = false; // Panelin t�klamalar� engellemesini durdur
    }


    private System.Collections.IEnumerator FadeInCoroutine()
    {
        float startAlpha = panelCanvasGroup.alpha;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            panelCanvasGroup.alpha = Mathf.Lerp(startAlpha, 1, time / fadeDuration);
            yield return null;
        }

        panelCanvasGroup.alpha = 1;
        panelCanvasGroup.interactable = true;
        panelCanvasGroup.blocksRaycasts = true;
    }
    public void OpenPanel()
    {

    }
}
