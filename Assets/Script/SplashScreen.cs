using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public Image logoImage;             
    public float fadeDuration = 2f;     
    public string nextSceneName;        

    void Start()
    {
        
        Color color = logoImage.color;
        color.a = 0f;
        logoImage.color = color;
        StartCoroutine(FadeInLogo());
    }

    IEnumerator FadeInLogo()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = timer / fadeDuration;

            Color color = logoImage.color;
            color.a = alpha;
            logoImage.color = color;

            yield return null;
        }
        Color finalColor = logoImage.color;
        finalColor.a = 1f;
        logoImage.color = finalColor;
        yield return new WaitForSeconds(2f);

        
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next scene name belum diisi di inspector.");
        }
    }
}
