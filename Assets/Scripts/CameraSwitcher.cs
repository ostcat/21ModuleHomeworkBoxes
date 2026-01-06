using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> _virtualCameras;
    private Queue<CinemachineVirtualCamera> _camerasQueue;

    private void Awake()
    {
        _camerasQueue = new Queue<CinemachineVirtualCamera>(_virtualCameras);
        SwitchCamera();
    }

    private void Update()
    {
        if( _virtualCameras != null && Input.GetKeyDown(KeyCode.F))
            SwitchCamera();
    }

    private void SwitchCamera()
    {
        CinemachineVirtualCamera nextCamera = _camerasQueue.Dequeue();

        foreach (var camera in _virtualCameras)
        {
            camera.gameObject.SetActive(false);
        }

        nextCamera.gameObject.SetActive(true);

        _camerasQueue.Enqueue(nextCamera);
    }
}
