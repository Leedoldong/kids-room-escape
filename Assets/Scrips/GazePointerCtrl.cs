using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GazePointerCtrl : MonoBehaviour
{
    public Video360Play vp360;
    public Transform uiCanvas;
    public Image gazeImg;

    Vector3 defaultScale;
    public float uiScaleVal = 1f;
    bool isHitObj;
    GameObject prevHitObj;
    GameObject curHitObj;
    float curGazeTime = 0;
    public float gazeChargeTime = 3f;

    void Start()
    {
        defaultScale = uiCanvas.localScale;
        curGazeTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = transform.TransformPoint(Vector3.forward);
        Ray ray = new Ray(transform.position, dir);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo))
        {
            uiCanvas.localScale = defaultScale * uiScaleVal * hitInfo.distance;
            uiCanvas.position = transform.forward * hitInfo.distance;
            if(hitInfo.transform.tag == "GazeObj")
            {
                isHitObj = true;
            }
            curHitObj = hitInfo.transform.gameObject;
        }
        else
        {
            uiCanvas.localScale = defaultScale * uiScaleVal;
            uiCanvas.position = transform.position + dir;
        }
        uiCanvas.forward = transform.forward * -1;

        if(isHitObj)
        {
            if(curHitObj == prevHitObj)
            {
                curGazeTime += Time.deltaTime;
                if(curGazeTime > 3)
                {
                    HitObjChecker(curHitObj, true);
                }
            }
            else
            {
                prevHitObj = curHitObj;
            }
            
        }
        else
        {
            if (prevHitObj != null)
            {
                HitObjChecker(prevHitObj, false);
                prevHitObj = null;
            }
            curGazeTime = 0;
        }
        curGazeTime = Mathf.Clamp(curGazeTime, 0, gazeChargeTime);
        gazeImg.fillAmount = curGazeTime / gazeChargeTime;

        isHitObj = false;
        curHitObj = null;
    }

    void HitObjChecker(GameObject hitObj, bool isActive)
    {
        if (hitObj.GetComponent<VideoPlayer>())
        {
            if (isActive)
            {
                hitObj.GetComponent<VideoFrame>().CheckVideoFrame(true);
            }
            else
            {
                hitObj.GetComponent<VideoFrame>().CheckVideoFrame(false);
            }
        }
        if(gazeImg.fillAmount >= 1)
        {
            //if(hitObj.name.Contains("Right"))
            //{
            //    vp360.SwapVideoClip(true);
            //}
            //else if (hitObj.name.Contains("Left"))
            //{
            //    vp360.SwapVideoClip(false);
            //}
            //else
            //{
            //    vp360.SetVideoPlay(hitObj.transform.GetSiblingIndex());
            //}
            //curGazeTime = 0;
        }
    }
}
