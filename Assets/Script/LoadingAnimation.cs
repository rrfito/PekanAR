using System.Collections;
using UnityEngine;
using TMPro;

public class LoadingTextDot : MonoBehaviour
{
    public TextMeshProUGUI loadingText;

    void Start()
    {
        StartCoroutine(AnimateDots());
    }

    IEnumerator AnimateDots()
    {
        string baseText = "Loading";
        while (true)
        {
            loadingText.text = baseText + ".";
            yield return new WaitForSeconds(0.2f);
            loadingText.text = baseText + "..";
            yield return new WaitForSeconds(0.2f);
            loadingText.text = baseText + "...";
            yield return new WaitForSeconds(0.2f);
        }
    }
}
