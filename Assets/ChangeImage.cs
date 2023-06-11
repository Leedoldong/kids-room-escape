using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChangeImage : MonoBehaviour
{
    public Sprite middle_Power;
    public Sprite full_Power;
    Image thisImg;
    GameObject GrabObj;
    public Text Power;
    private float time = 14.0f;
    bool ok = true;

    // Start is called before the first frame update
    void Start()
    {
        thisImg = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ok)
            Powerd();
            

    }
    public void Powerd()
    {
        if (GameObject.Find("Player").GetComponent<GrabObject>().isCharge == true)
        {
            thisImg.sprite = middle_Power;
            if (time > 0)
            {
                time -= Time.deltaTime;
                Power.text = "충전중\n입니다...\n" + Mathf.Round(time).ToString();
            }
            else
            {
                thisImg.sprite = full_Power;
                Power.text = "비밀번호는\n\"1234\"";
                Time.timeScale = 1.0f;
                ok = false;
            }
            
        }
    }
}
