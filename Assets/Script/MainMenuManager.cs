using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public AudioSource btnSound;
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
    IEnumerator Delay(string name)
    {
        btnSound.Play();
        yield return new WaitForSeconds(btnSound.clip.length);
        ChangeScene(name);
    }
    void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    void Pembelajaran()
    {
        btnSound.Play();
        MainMenu.SetActive(false);
        PembelajaranMenu.SetActive(true);
        prev.gameObject.SetActive(true);
        TentangAplikasiMenu.SetActive(false);

    }

    void Kembali()
    {
        btnSound.Play();
        MainMenu.SetActive(true);
        PembelajaranMenu.SetActive(false);
        prev.gameObject.SetActive(false);
        TentangAplikasiMenu.SetActive(false);
    }

    void ScanGambar()
    {
        StartCoroutine(Delay("Marker-based"));
    }

    void JelajahiLokasi()
    {
        StartCoroutine(Delay("JelajahiLokasi"));
    }

    void Quiz()
    {
        StartCoroutine(Delay("TutorialQuiz"));
    }
    

    void TentangAplikasi()
    {
        btnSound.Play();
        prev.gameObject.SetActive(true);
        MainMenu.SetActive(false);
        PembelajaranMenu.SetActive(false);
        TentangAplikasiMenu.SetActive(true);
    }

    void Keluar()
    {
        btnSound.Play();
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
