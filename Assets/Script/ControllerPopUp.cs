using UnityEngine;
using TMPro; // Penting untuk TextMeshPro
using System.Collections.Generic; // Untuk List<string>

public class InfoPopupController : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro _popupTextJudul; // Seret komponen TextMeshPro untuk JUDUL di Inspector

    [SerializeField]
    private TextMeshPro _popupTextIsi; // Seret komponen TextMeshPro untuk ISI di Inspector

    private PopupContentData _currentPopupData; // Data pop-up yang sedang ditampilkan
    private int _currentPageIndex = 0; // Melacak halaman teks saat ini

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
        // OnEnable akan dipanggil setiap kali diaktifkan.
        // Data harus diset setelah pop-up di-instantiate dan diaktifkan.
        // ResetToFirstPage() akan dipanggil setelah data diset.
    }

    // Metode baru untuk mengatur data pop-up dari ImageTrackingManager
    public void SetPopupData(PopupContentData data)
    {
        _currentPopupData = data;
        ResetToFirstPage(); // Reset dan tampilkan halaman pertama dengan data baru
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
            _currentPageIndex = 0; // Kembali ke halaman pertama jika sudah habis
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
        // Update teks judul
        if (_popupTextJudul != null && _currentPopupData != null)
        {
            _popupTextJudul.text = _currentPopupData.popupTitle;
        }
        else if (_popupTextJudul != null)
        {
            _popupTextJudul.text = "Judul Tidak Ada";
        }

        // Update teks isi berdasarkan halaman saat ini
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