using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void PlayButtonCheck()
    {
        SceneManager.LoadScene("Inner_Room");
    }
    public void ExitButtonCheck()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
    

}
