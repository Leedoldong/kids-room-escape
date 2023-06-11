using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;

public class MusicCheckAndMake : MonoBehaviour
{
    public static int startedZ = 0;
    public GameObject musicMaker;


    private void Start()
    {
        PlayerPrefs.SetInt("GamePlaying", 0);
        
        if (startedZ == 0)
        {
            Instantiate(musicMaker);
            DontDestroyOnLoad(gameObject);
            startedZ = 1;
        }
        else if (startedZ == 1)
        {
            Destroy(gameObject);
        }
        
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Video")
        {
            Destroy(GameObject.Find("MusicBoiPrefab(Clone)"));
        }
    }



}
