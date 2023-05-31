using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    //�ʿ� �Ӽ� : ��ü�� ��� �ִ��� ����, ��� �ִ� ��ü, ���� ��ü�� ����, ���� �� �ִ� �Ÿ�
    // ��ü�� ��� �ִ����� ����
    bool isGrabbing = false;
    //��� �ִ� ��ü
    GameObject grabbedObject;
    //���� ��ü�� ����
    public LayerMask grabbedLayer;
    //���� �� �ִ� �Ÿ�
    public float grabRange = 0.2f;
    //���� ��ġ
    Vector3 prevPos;
    // ���� ��
    float throwPower = 10;
    //���� ȸ��
    Quaternion prevRot;
    // ȸ����
    public float rotPower = 5;
    // ���Ÿ����� ��ü�� ��� ��� Ȱ��ȭ ����
    public bool isRemoteGrab = false;
    // ���Ÿ����� ��ü�� ���� �� �ִ� �Ÿ�
    public float remoteGrabDistance = 20;

    static bool[] istouch = new bool[2];

    public GameObject[] sign = new GameObject[2];
    public GameObject phone;
    public GameObject phoneImage;
    public GameObject warningImage;
    public GameObject okImage;



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //��ü ���
        //1. ��ü�� ���� �ʰ� ���� ���
        if (isGrabbing == false)
        {
            //���õ�
            TryGrab();
        }
        else
        {
            //��ü ����
            TryUngrab();
        }
        if (ARAVRInput.GetDown(ARAVRInput.Button.HandTrigger, ARAVRInput.Controller.LTouch))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            if(hit.collider.tag == "Phone")
            {
                Debug.Log("��");
                phoneImage.SetActive(true);
                phone.SetActive(false);
                

            }
        }
    }

    private void TryUngrab()
    {
        //��������
        Vector3 throwDirection = (ARAVRInput.RHandPosition - prevPos);
        //��ġ���
        prevPos = ARAVRInput.RHandPosition;

        Quaternion deltaRotation = ARAVRInput.RHand.rotation * Quaternion.Inverse(prevRot);
        prevRot = ARAVRInput.RHand.rotation;

        //��ư�� ���Ҵٸ�
        if (ARAVRInput.GetUp(ARAVRInput.Button.HandTrigger, ARAVRInput.Controller.RTouch))
        {
            //���� ���� ���·� ��ȯ
            isGrabbing = false;
            //���� ��� Ȱ��ȭ
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            //�տ��� ��ź �����
            grabbedObject.transform.parent = null;
            //������
            grabbedObject.GetComponent<Rigidbody>().velocity = throwDirection * throwPower;
            //���ӵ� = (1/dt) * d(Ư�� �� ���� ���� ����)
            float angle;
            Vector3 axis;
            deltaRotation.ToAngleAxis(out angle, out axis);
            Vector3 angularVelocity = (1.0f / Time.deltaTime) * angle * axis;
            grabbedObject.GetComponent<Rigidbody>().angularVelocity = angularVelocity;
            //���� ��ü�� ������ ����
            grabbedObject = null;


        }
    }

    private void TryGrab()
    {
        //[Grab]��ư�� ������ ���� ���� �ȿ� �ִ� ��ź�� ��´�
        //1. [Grab] ��ư�� �����ٸ� 
        if (ARAVRInput.GetDown(ARAVRInput.Button.HandTrigger, ARAVRInput.Controller.RTouch))
        {
            // ���Ÿ� ��ü ��⸦ ����Ѵٸ�
            if (isRemoteGrab)
            {
                // �� �������� Ray ����
                Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
                RaycastHit hitInfo;
                
                // SphereCast�� �̿��� ��ü �浹�� üũ
                if (Physics.SphereCast(ray, 0.5f, out hitInfo, remoteGrabDistance, grabbedLayer))
                {
                    // ���� ���·� ��ȯ
                    isGrabbing = true;

                    // ���� ��ü�� ���� ���
                    grabbedObject = hitInfo.transform.gameObject;
                    // ��ü�� �������� ��� ����
                    StartCoroutine(GrabbingAnimation());
                }

                //2. ���������ȿ� ��ź�� ������
                // �����ȿ� �ִ� ��� ��ź ����
                Collider[] hitObjects = Physics.OverlapSphere(ARAVRInput.RHandPosition, grabRange, grabbedLayer);
                //���� ����� ��ź �ε���
                int closest = 0;
                //�հ� ���� ����� ��ü ����
                for (int i = 1; i < hitObjects.Length; i++)
                {
                    //�հ� ���� ����� ��ü���� �Ÿ�
                    Vector3 closestPos = hitObjects[closest].transform.position;
                    float closestDistance = Vector3.Distance(closestPos, ARAVRInput.RHandPosition);
                    // ���� ��ü�� ���� �Ÿ�
                    Vector3 nextPos = hitObjects[i].transform.position;
                    float nextDistance = Vector3.Distance(nextPos, ARAVRInput.RHandPosition);
                    //���� ��ü���� �Ÿ��� �� �����ٸ�
                    if (nextDistance < closestDistance)
                    {
                        //���� ����� ��ü �ε��� ��ü
                        closest = i;
                    }
                }
                //3. ��ź�� ��´�
                //����� ��ü�� �������
                if (hitObjects.Length > 0)
                {
                    //���� ���·� ��ȯ
                    isGrabbing = true;
                    //������ü�� ���� ���
                    grabbedObject = hitObjects[closest].gameObject;
                    if (!istouch[0] || !istouch[1])
                    {
                        if (grabbedObject.tag.Equals("NoChair"))
                        {
                            warningImage.SetActive(true);
                            sign[0].SetActive(false);
                            Destroy(warningImage, 4);
                            istouch[0] = true;
                        }
                        else if (grabbedObject.tag.Equals("Chair"))
                        {
                            okImage.SetActive(true);
                            sign[1].SetActive(false);
                            Destroy(okImage, 4);
                            istouch[1] = true;
                        }
                    }
                    
                    //���� ��ü�� ���� �ڽ����� ���
                    grabbedObject.transform.parent = ARAVRInput.RHand;
                    // ���� �������
                    grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                    //�ֱ� ��ġ �� ����
                    prevPos = ARAVRInput.RHandPosition;
                    //�ʱ� ȸ���� ����
                    prevRot = ARAVRInput.RHand.rotation;
                }
            }
        }

    }
    IEnumerator GrabbingAnimation()
    {
        // ���� ��� ����
        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
        // �ʱ� ��ġ �� ����
        prevPos = ARAVRInput.RHandPosition;
        // �ʱ� ȸ�� �� ����
        prevRot = ARAVRInput.RHand.rotation;
        Vector3 startLocation = grabbedObject.transform.position;
        Vector3 targetLocation = ARAVRInput.RHandPosition + ARAVRInput.RHandDirection * 0.1f;

        float currentTime = 0;
        float finishTime = 0.2f;

        // �����
        float elapsedRate = currentTime / finishTime;

        while (elapsedRate < 1)
        {
            currentTime += Time.deltaTime;
            elapsedRate = currentTime / finishTime;

            grabbedObject.transform.position = Vector3.Lerp(startLocation, targetLocation, elapsedRate);

            yield return null;
        }

        // ���� ��ü�� ���� �ڽ����� ���
        grabbedObject.transform.position = targetLocation;
        grabbedObject.transform.parent = ARAVRInput.RHand;



    }
}


