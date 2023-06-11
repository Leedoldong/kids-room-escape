using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Check : MonoBehaviour
{
    public Text password;
    int[] passw = new int[4];
    int i = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int j = 0; j < passw.Length; j++)
            passw[j] = 0;
    }

    // Update is called once per frame
    void Update()
    {
        password.text = passw[0].ToString() + passw[1].ToString()
            + passw[2].ToString() + passw[3].ToString();
    }

    public void ButtonCheck()
    {
        if (passw[0] == 1 && passw[1] == 2 && passw[2] == 3 && passw[3] == 4)
        {
            if (GameManager.bath_finish == true)
            {
                SceneManager.LoadScene("Video");
            }
            else
            {
                SceneManager.LoadScene("Bath_Room");
            }
        }
            
    }
    public void ButtonExit()
    {
        gameObject.SetActive(false);
        for (int j = 0; j < passw.Length; j++)
            passw[j] = 0;
        i = 0;
    }


    public void ButtonCheck1()
    {
        if(i< passw.Length)
            passw[i] = 1;
        i++;
    }
    public void ButtonCheck2()
    {
        if (i < passw.Length)
            passw[i] = 2;
        i++;
    }
    public void ButtonCheck3()
    {
        if (i < passw.Length)
            passw[i] = 3;
        i++;
    }
    public void ButtonCheck4()
    {
        if (i < passw.Length)
            passw[i] = 4;
        i++;
    }
    public void ButtonCheck5()
    {
        if (i < passw.Length)
            passw[i] = 5;
        i++;
    }
    public void ButtonCheck6()
    {
        if (i < passw.Length)
            passw[i] = 6;
        i++;
    }
    public void ButtonCheck7()
    {
        if (i < passw.Length)
            passw[i] = 7;
        i++;
    }
    public void ButtonCheck8()
    {
        if (i < passw.Length)
            passw[i] = 8;
        i++;
    }
    public void ButtonCheck9()
    {
        if (i < passw.Length)
            passw[i] = 9;
        i++;
    }

}
