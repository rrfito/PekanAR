using UnityEngine;
using TMPro; 
using System.Collections.Generic; 

public class InfoPopupController : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro _popupTextJudul; 

    [SerializeField]
    private TextMeshPro _popupTextIsi;

    private PopupContentData _currentPopupData; 
    private int _currentPageIndex = 0; 

    void Awake()
    {
        if (_popupTextJudul == null)
        {
            Debug.LogError("Popup Text Judul (TextMeshPro) belum di-assign di Inspector!");
        }
        if (_popupTextIsi == null)
        {
            Debug.LogError("Popup Text Isi (TextMeshPro) belum di-assign di Inspector!");
        }
    }

    void OnEnable()
    {
        
    }

   
    public void SetPopupData(PopupContentData data)
    {
        _currentPopupData = data;
        ResetToFirstPage(); 
    }

    public void GoToNextPage()
    {
        if (_currentPopupData == null || _currentPopupData.pagesOfTextIsi == null || _currentPopupData.pagesOfTextIsi.Count == 0)
        {
            Debug.LogWarning("Tidak ada teks isi untuk ditampilkan di pop-up saat ini!");
            return;
        }

        _currentPageIndex++;
        if (_currentPageIndex >= _currentPopupData.pagesOfTextIsi.Count)
        {
            _currentPageIndex = 0; 
        }
        UpdatePopupText();
        Debug.Log($"Pindah ke halaman teks isi: {_currentPageIndex + 1}");
    }

    public void ResetToFirstPage()
    {
        _currentPageIndex = 0;
        UpdatePopupText();
    }

    private void UpdatePopupText()
    {
      
        if (_popupTextJudul != null && _currentPopupData != null)
        {
            _popupTextJudul.text = _currentPopupData.popupTitle;
        }
        else if (_popupTextJudul != null)
        {
            _popupTextJudul.text = "Judul Tidak Ada";
        }

     
        if (_popupTextIsi != null && _currentPopupData != null && _currentPopupData.pagesOfTextIsi != null && _currentPopupData.pagesOfTextIsi.Count > _currentPageIndex)
        {
            _popupTextIsi.text = _currentPopupData.pagesOfTextIsi[_currentPageIndex];
        }
        else if (_popupTextIsi != null)
        {
            _popupTextIsi.text = "Teks isi tidak tersedia.";
        }
    }
}