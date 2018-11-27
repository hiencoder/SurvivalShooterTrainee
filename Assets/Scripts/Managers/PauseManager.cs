using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Audio;
public class PauseManager : MonoBehaviour
{
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unPaused;
    Canvas panelSetting;
    private bool isShowing;

    //Open/close panel setting

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        //panelSetting = GetComponent<Canvas>();

    }
    // Use this for initialization
    void Start()
    {
        panelSetting = GameObject.Find("SettingCanvas").GetComponent<Canvas>();
        panelSetting.GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (panelSetting.enabled == true)
            {
                panelSetting.GetComponent<Canvas>().enabled = false;
                ResumeGame();
            }
            else
            {
                panelSetting.GetComponent<Canvas>().enabled = true;
                //Pause game
                PauseGame();
            }
        }
    }

    public void QuitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();
    }

    public void ResumeGame()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        LowPass();
    }

    public void PauseGame()
    {
        //Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        LowPass();
    }

    public void LowPass()
    {
        if (Time.timeScale == 0)
        {
            paused.TransitionTo(0.1f);
        }
        else
        {
            unPaused.TransitionTo(0.1f);
        }
    }
    public void OpenSetting()
    {
        if (panelSetting.enabled == false)
        {
            panelSetting.GetComponent<Canvas>().enabled = true;
            PauseGame();
        }
        else
        {
            panelSetting.GetComponent<Canvas>().enabled = false;
            ResumeGame();
        }
    }
}
