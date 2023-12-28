using UnityEngine;
using UnityEngine.Events;

public class UISignals : MonoBehaviour
{
    public static UISignals Instance;

    private void Awake()
    {
        if(Instance != null && Instance != this){
            Destroy(gameObject); 
        }else{
            Instance = this;
        }

    }
    

    public UnityAction<int> onSetStageColor = delegate{}; 
    public UnityAction<int> onSetLevelValue = delegate{}; 

    public UnityAction onPlay  = delegate{};



}