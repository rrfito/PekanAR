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
        if (currentIndex < info.Length - 1) 
        {
            currentIndex++;
            UpdateNavigationUI();
        }
        else
        {
          
            SceneManager.LoadScene("Quiz");
        }
    }

    void OnPrevTutorial()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateNavigationUI();
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

   
    void UpdateNavigationUI()
    {
       
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