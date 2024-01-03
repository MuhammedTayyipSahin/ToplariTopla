using UnityEngine;
using UnityEngine.Events;

public class CameraSignals : MonoBehaviour
{
    public static CameraSignals Instance;


    void Awake()
    {
        if(Instance != null && Instance != this){
            Destroy(gameObject);
        }
        else{
            Instance = this;
        }
    }

    public UnityAction onSetCameraTarget = delegate{};



}