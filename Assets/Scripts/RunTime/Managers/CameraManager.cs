using System;
using Cinemachine;
using Unity.IO.LowLevel.Unsafe;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private float3 _firstPosition;


    void Start()
    {
        Init();
    }

    private void Init()
    {
        _firstPosition = transform.position;
    }

    void OnEnable()
    {
        SubscribeEvents();
    }

    void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void UnSubscribeEvents()
    {
        CameraSignals.Instance.onSetCameraTarget -= onSetCameraTarget;
        CoreGameSignals.Instance.onReset -= OnReset;
    }

    private void SubscribeEvents()
    {
        CameraSignals.Instance.onSetCameraTarget += onSetCameraTarget;
        CoreGameSignals.Instance.onReset += OnReset;
    }

    private void OnReset()
    {
        transform.position = _firstPosition;
    }

    private void onSetCameraTarget()
    {
       // var player = FindObjectsByType<PlayerManager>().transform;
        // virtualCamera.Follow = player;  
        // virtualCamera.LookAt = player;  
    }
}