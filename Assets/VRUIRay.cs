using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRUIRay : MonoBehaviour
{
    // 1. �켱 �ش� ��ũ��Ʈ�� �̱������� �������.
    public static VRUIRay instance;


    // 2. ���콺 ������ ������ ���ӿ�����Ʈ�� �غ��Ѵ�.
    // VR ������ ��Ʈ�ѷ�
    public Transform rightHand;
    // ���콺 �����͸� ��ü�� �̹���
    public Transform dot;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        // 3. Ray�� ����ؼ� dot(���콺������)�� Ȱ��ȭ�Ѵ�.
        Ray ray = new Ray(rightHand.position, rightHand.forward);
        Debug.DrawRay(rightHand.position, rightHand.forward * 100f, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //if (hit.transform.gameObject.layer == LayerMask.NameToLayer("UI"))
            //{
            //    dot.gameObject.SetActive(true);
            //}
            //else
            //{
            //    dot.gameObject.SetActive(false);
            //}

            dot.position = hit.point;


            // 4. dot�� �浹 �� �� �� Ŭ���� �� �ֵ��� �Ѵ�.
            // ���� ���� Ȱ��ȭ ���¸�
            if (dot.gameObject.activeSelf)
            {
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    // ��ư ��ũ��Ʈ�� �����´�
                    Button btn = hit.transform.GetComponent<Button>();
                    // ���� btn�� null�� �ƴ϶��
                    if (btn != null)
                    {
                        btn.onClick.Invoke();
                    }
                }
            }
        }
        else
        {
            dot.gameObject.SetActive(false);
        }
    }
}