using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class ListMarker : MonoBehaviour
{
    public AudioSource sfx;
    public Button[] Marker;
    public GameObject[] modelPrefabs; 
    public Transform previewContainer; 

    public float duration = 0.3f;
    public float selectedY = 50f;
    public float normalY = 0f;
    private Tween rotationTween;

    private GameObject currentModel;
    private Vector2 lastMousePosition;

    void Start()
    {
        for (int i = 0; i < Marker.Length; i++)
        {
            int index = i; 
            Marker[i].onClick.AddListener(() => OnItemClick(index));
        }
    }

    public void OnItemClick(int index)
    {
        sfx.Play();
        for (int i = 0; i < Marker.Length; i++)
        {
            RectTransform rt = Marker[i].GetComponent<RectTransform>();
            float targetY = (i == index) ? selectedY : normalY;
            rt.DOAnchorPosY(targetY, duration).SetEase(Ease.OutQuad);
        }

      
        if (currentModel != null)
        {
            Destroy(currentModel);  
        }
        if (rotationTween != null)
        {
            rotationTween.Kill();
        }
        if (index >= 0 && index < modelPrefabs.Length)
        {
            currentModel = Instantiate(modelPrefabs[index], Vector3.zero, Quaternion.identity);
            currentModel.transform.SetParent(previewContainer, false);
            currentModel.transform.localPosition = Vector3.zero;    
            currentModel.transform.localScale = Vector3.one * 5;
            
            rotationTween = currentModel.transform.DORotate(new Vector3(0, 360, 0), 10f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
        }
        else
        {
            Debug.LogWarning("Model prefab untuk index ini tidak ditemukan!");
        }
    }

    
}
