using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Penting untuk bekerja dengan UI

public class TutorialQuizManager : MonoBehaviour
{
   
    public GameObject[] info;
    public Button prevButton; 
    public Button nextButton;
    public TextMeshProUGUI AyoMulai;

    private int currentIndex = 0;

    public AudioSource Sfx;
    void Start()
    {
        
        for (int i = 0; i < info.Length; i++)
        {
            info[i].SetActive(false);
        }
        nextButton.onClick.AddListener(OnNextTutorial);
        prevButton.onClick.AddListener(OnPrevTutorial); 
        UpdateNavigationUI();
    }
    IEnumerator Delay(string name)
    {
        Sfx.Play();
        yield return new WaitForSeconds(Sfx.clip.length);
        ChangeScene(name);
    }
    void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }


    void ShowTutorialPage(int index)
    {
        if (index >= 0 && index < info.Length)
        {
            for (int i = 0; i < info.Length; i++)
            {
                info[i].SetActive(i==index);
            }
        }
        
    }

  
    void OnNextTutorial()
    {
        Sfx.Play();
        if (currentIndex < info.Length - 1) 
        {
            currentIndex++;
            UpdateNavigationUI();
        }
        else
        {
            StartCoroutine(Delay("Quiz"));
           
        }
    }

    void OnPrevTutorial()
    {
        Sfx.Play();
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateNavigationUI();
        }
        else
        {
            StartCoroutine(Delay("MainMenu"));
          
        }
    }

   
    void UpdateNavigationUI()
    {
        prevButton.gameObject.SetActive(currentIndex !=0);
        ShowTutorialPage(currentIndex);
        if (AyoMulai != null) 
        {
            if (currentIndex == info.Length - 1) 
            {
                AyoMulai.text = "Ayo Mulai";
            }
            else
            {
                AyoMulai.text = "Berikutnya";
            }
        }
        

    }
}