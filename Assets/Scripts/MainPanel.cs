using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainPanel : MonoBehaviour
{
    [Header("Options")]
    public Slider volumeFX;
    public Slider volumeMaster;
    public Toggle mute;
    public AudioMixer mixer;

    private float lastVolume;
    private AudioManager audioManager;

    [Header("Name Panel")]
    public TMP_InputField inputNickName;
    public Button continueButton;

    [Header("Game Options")]
    public Toggle fullScreen;
    public TMP_Dropdown qualities;
    public TMP_Dropdown resolutions;
    private Resolution[] localResolutions;

    [Header("Panels")]
    public GameObject[] panels;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        mute.isOn = PlayerPrefs.GetInt("mute") == 1;
        if (inputNickName)
        {
            inputNickName.text = PlayerPrefs.GetString("nickname", "");
        }
    }

    private void Awake()
    {
        volumeFX.value = PlayerPrefs.GetFloat("VolFX", volumeFX.value);
        volumeMaster.value = PlayerPrefs.GetFloat("VolMaster", volumeMaster.value);
        volumeFX.onValueChanged.AddListener(ChangeVolumeFX);
        volumeMaster.onValueChanged.AddListener(ChangeVolumeMaster);

        qualities.value = PlayerPrefs.GetInt("Quality", qualities.value);
        qualities.onValueChanged.AddListener(ChangeQuality);

        SetResolutions();
        resolutions.value = PlayerPrefs.GetInt("resolution", resolutions.value);
        resolutions.onValueChanged.AddListener(ChangeResolution);

        fullScreen.isOn = PlayerPrefs.GetInt("fullScreen") != 0;
        fullScreen.onValueChanged.AddListener(ChangeFullScreen);

        if (inputNickName)
        {
            inputNickName.text = PlayerPrefs.GetString("nickname", "");
            inputNickName.onValueChanged.AddListener(CheckInputNickname);
        }

    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        ClosePanels();
    }

    public void PauseGame(GameObject panel)
    {
        Time.timeScale = 0;
        OpenPanel(panel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void OpenPanel(GameObject panel)
    {

        foreach (GameObject p in panels)
        {
            p.SetActive(false);
        }

        panel.SetActive(true);
        PlaySoundButton();
    }

    public void ClosePanels()
    {

        foreach (GameObject p in panels)
        {
            p.SetActive(false);
        }
        PlaySoundButton();
    }

    public void ChangeVolumeMaster(float v)
    {
        mixer.SetFloat("VolMaster", v);
        PlayerPrefs.SetFloat("VolMaster", v);
    }

    public void ChangeVolumeFX(float v)
    {
        mixer.SetFloat("VolFX", v);
        PlayerPrefs.SetFloat("VolFX", v);
    }

    public void PlaySoundButton()
    {
        audioManager.PlaySFX(audioManager.clickButtom);
    }

    public void SetMute()
    {
        Debug.Log("Antes");
        if (mute.isOn)
        {
            mixer.GetFloat("VolMaster", out lastVolume);
            mixer.SetFloat("VolMaster", -80);
        }
        else
        {
            mixer.SetFloat("VolMaster", lastVolume);
        }
        PlayerPrefs.SetInt("mute", mute.isOn ? 1 : 0);

    }

    public void ChangeQuality(int q)
    {
        QualitySettings.SetQualityLevel(q);
        PlayerPrefs.SetInt("Quality", q);
    }

    public void SetResolutions()
    {
        localResolutions = Screen.resolutions;
        resolutions.ClearOptions();
        List<string> options = new List<string>();
        int count = 0;
        int currentResolution = 0;

        foreach (Resolution resolution in localResolutions)
        {
            string option = resolution.width + " x " + resolution.height;
            options.Add(option);

            if (Screen.fullScreen 
                && resolution.width == Screen.width 
                && resolution.height == Screen.height)
            {
                currentResolution = count;
            }
            count++;
        }

        resolutions.AddOptions(options);
        resolutions.value = currentResolution;
        resolutions.RefreshShownValue();

    }

    public void ChangeResolution(int r)
    {
        Resolution resolution = localResolutions[r];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("resolution", r);
    }

    public void ChangeFullScreen(bool b)
    {
        Screen.fullScreen = b;
        PlayerPrefs.SetInt("fullScreen", b ? 1 : 0);
    }

    public void CheckInputNickname(string t)
    {
        if (t.Length >= 3)
        {
            continueButton.interactable = true;
        }
        else
        {
            continueButton.interactable = false;
        }
        PlayerPrefs.SetString("nickname", t);
    }
}
