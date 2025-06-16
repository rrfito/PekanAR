using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public class ListMarker : MonoBehaviour
{
    public AudioSource sfx;
    public Button[] Marker;
    public TextMeshProUGUI name;  
    public float duration = 0.3f;
    public float selectedY = 50f;
    public float normalY = 0f;

    void Start()
    {
        foreach (var button in Marker)
        {
            
            button.onClick.AddListener(() => OnItemClick(button));
        }
    }

    public void OnItemClick(Button btn)
    {
        
        name.SetText(btn.name);
        sfx.Play();
        foreach (var button in Marker)
        {
            RectTransform rt = button.GetComponent<RectTransform>();
            float targetY = (button == btn) ? selectedY : normalY;
            rt.DOAnchorPosY(targetY, duration).SetEase(Ease.OutQuad);
        }
    }
}
