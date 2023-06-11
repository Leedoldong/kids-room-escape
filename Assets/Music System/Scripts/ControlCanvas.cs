using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlCanvas : MonoBehaviour
{

    public GameObject settingsCan;
    public AudioClip[] clips;
    public AudioSource audioSource;

    public void BackButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(01);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    private AudioClip GetRandomClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }


}
