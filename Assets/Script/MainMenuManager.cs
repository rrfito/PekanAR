using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject PembelajaranMenu;
    public GameObject TentangAplikasiMenu;
    public Button BtnPembelajaran;
    public Button BtnQuiz;
    public Button BtnTentangAplikasi;
    public Button BtnKeluar;
    public Button BtnScanGambar;
    public Button BtnJelajahiLokasi;
    public Button prev;

    void Start()
    {
        MainMenu.SetActive(true);
        PembelajaranMenu.SetActive(false);
        prev.gameObject.SetActive(false);
        TentangAplikasiMenu.SetActive(false);

        BtnPembelajaran.onClick.AddListener(Pembelajaran);
        BtnQuiz.onClick.AddListener(Quiz);
        BtnTentangAplikasi.onClick.AddListener(TentangAplikasi);
        BtnKeluar.onClick.AddListener(Keluar);
        BtnScanGambar.onClick.AddListener(ScanGambar);
        BtnJelajahiLokasi.onClick.AddListener(JelajahiLokasi);
        prev.onClick.AddListener(Kembali);
    }

    void Pembelajaran()
    {
        MainMenu.SetActive(false);
        PembelajaranMenu.SetActive(true);
        prev.gameObject.SetActive(true);
        TentangAplikasiMenu.SetActive(false);

    }

    void Kembali()
    {
        MainMenu.SetActive(true);
        PembelajaranMenu.SetActive(false);
        prev.gameObject.SetActive(false);
        TentangAplikasiMenu.SetActive(false);
    }

    void ScanGambar()
    {
        SceneManager.LoadScene("Marker-based");
    }

    void JelajahiLokasi()
    {
        SceneManager.LoadScene("JelajahiLokasi");
    }

    void Quiz()
    {
        SceneManager.LoadScene("Quiz");
    }

    void TentangAplikasi()
    {
        prev.gameObject.SetActive(true);
        MainMenu.SetActive(false);
        PembelajaranMenu.SetActive(false);
        TentangAplikasiMenu.SetActive(true);
    }

    void Keluar()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
