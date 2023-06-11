using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject Q1;
    public GameObject q1_warning;
    public GameObject q1_good;
    public GameObject[] signs;

    public GameObject Q2;
    public GameObject q2_warning;
    public GameObject q2_good;

    public GameObject toilet;
    public GameObject hands;
    public Text t_count;
    public Text h_count;
    private float toilet_time = 5.0f;
    private float handwash_time = 10.0f;
    bool istoilet = false;
    bool iswash = false;
    public GameObject Lock;

    public GameObject towel;

    public static bool bath_finish = false;

    void Start()
    {
        Q1.SetActive(true);
    }

    void Update()
    {
        if (ARAVRInput.GetDown(ARAVRInput.Button.HandTrigger, ARAVRInput.Controller.LTouch))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            if (hit.collider.tag == "Toothbrush")
            {
                Debug.Log("칫솔");
                signs[0].SetActive(false);
                Q2.SetActive(true);
            }
            if (hit.collider.tag == "Toilet")
            {
                Debug.Log("변기");
                signs[1].SetActive(false);
                toilet.SetActive(true);
                istoilet=true;
            }
            if (hit.collider.tag == "Vanity")
            {
                Debug.Log("세면대");
                hands.SetActive(true);
                iswash = true;
            }
            if (hit.collider.tag == "Door")
            {
                Debug.Log("문 - 잠금");
                bath_finish = true;
                Lock.SetActive(true);
            }
            if (hit.collider.tag == "Towel")
            {
                Debug.Log("수건");
                towel.SetActive(true);
                Destroy(towel, 3);
                signs[3].SetActive(false);
            }
        }
        if (iswash)
        {
            if (handwash_time > 0)
            {
                handwash_time -= Time.deltaTime;
                h_count.text = "남은 시간 : " + Mathf.Round(handwash_time).ToString();
            }
            else
            {
                h_count.text = "완료! -> 수건으로 가서 물을 닦으세요!";
                Time.timeScale = 1.0f;
                Destroy(hands, 2);
                signs[3].SetActive(true);
                iswash = false;
            }
        }
        if (istoilet)
        {
            if (toilet_time > 0)
            {
                toilet_time -= Time.deltaTime;
                t_count.text = "남은 시간 : " + Mathf.Round(toilet_time).ToString();
            }
            else
            {
                t_count.text = "완료!";
                Time.timeScale = 1.0f;
                Destroy(toilet, 2);
                signs[2].SetActive(true);
                istoilet = false;
            }
        }
    }
    
    public void Quest1_button1()
    {
        q1_good.SetActive(true);
        Destroy(q1_good, 4);
        Destroy(Q1, 4);
        signs[0].SetActive(true);
    }
    public void Quest1_button2()
    {
        q1_warning.SetActive(true);
        Destroy(q1_warning, 4);
    }
    public void Quest2_button1()
    {
        q2_warning.SetActive(true);
        Destroy(q2_warning, 4);
    }
    public void Quest2_button2()
    {
        q2_good.SetActive(true);
        Destroy(q2_good, 4);
        Destroy(Q2, 4);
        signs[1].SetActive(true);
    }
}
