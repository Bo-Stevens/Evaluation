using Cinemachine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CameraController : MonoBehaviour
{
    [HideInInspector] public Transform Follow;
    CinemachineVirtualCamera virtualCamera;
    CinemachineBrain brain;

    private void Awake()
    {
        brain = GetComponent<CinemachineBrain>();
        virtualCamera = (CinemachineVirtualCamera)brain.ActiveVirtualCamera;
        StartCoroutine(GrabVirtualCam());
    }

    IEnumerator GrabVirtualCam()
    {
        yield return null;
        virtualCamera = (CinemachineVirtualCamera)brain.ActiveVirtualCamera;
        virtualCamera.Follow = Follow;
        virtualCamera.LookAt = Follow;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
