using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu, creditPanel,howToPlayPanel,settingPanel, settingBtn;

    public AudioSource audioSource;
    public AudioClip btnClip, loadingScreenClip;

    public Animator settingAnim;
    public GameObject fadeInOut;
    public GameObject loadingScreen;
    public Slider musicSlider;

    void Start()
    {
        Cursor.visible = true;
        mainMenu.SetActive(true);
        creditPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
        settingPanel.SetActive(false);
    }

    private void Update()
    {
        audioSource.volume = musicSlider.value;
    }
    public void SettingBtnClicked()
    {
        mainMenu.SetActive(false);
        settingPanel.SetActive(true);
        settingAnim.SetTrigger("SlideIn");
        PlayButtonSOund();
        settingBtn.SetActive(false);
    }

    public void CreditBtnClicked()
    {
        mainMenu.SetActive(false);
        creditPanel.SetActive(true);
        PlayButtonSOund();
    }

    public void HowToPlayBtnClicked()
    {
        mainMenu.SetActive(false);
        howToPlayPanel.SetActive(true);
        PlayButtonSOund();
    }

    public void BackBtnClicked()
    {
        mainMenu.SetActive(true);
        creditPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
        // settingPanel.SetActive(false);
        settingAnim.SetTrigger("SlideOut");
        StartCoroutine(DisableSettingPanel());
        PlayButtonSOund();
    }

    public IEnumerator DisableSettingPanel()
    {
        yield return new WaitForSeconds(1f);     
        settingPanel.SetActive(false);
        settingBtn.SetActive(true);
    }

    public void PlayBtnClicked()
    {
        audioSource.Stop();
        audioSource.clip = loadingScreenClip;
        audioSource.Play();

        loadingScreen.SetActive(true);
        PlayerPrefs.SetInt("LevelNum", 0);
        StartCoroutine(WaitFadeIn());
    }

    IEnumerator WaitFadeIn()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(1);

    }
    public void PlaySOund()
    {
        audioSource.Play();
    }

    public void PlayButtonSOund()
    {
        audioSource.PlayOneShot(btnClip);
    }

    public void FadeIn()
    {
        fadeInOut.GetComponent<Animator>().SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        fadeInOut.GetComponent<Animator>().SetTrigger("FadeOut");
    }

    public void MusicOn()
    {
        audioSource.Play();
    }

    public void MusicOff()
    {
        audioSource.Stop();
    }
}
