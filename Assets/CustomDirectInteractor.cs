using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomDirectInteractor : XRDirectInteractor
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnSelectEntered(XRBaseInteractable interactable)
    {
        Debug.Log("잡았다.");
    }
    protected override void OnSelectExited(XRBaseInteractable interactable)
    {
        Debug.Log("놓았다.");
    }
}
