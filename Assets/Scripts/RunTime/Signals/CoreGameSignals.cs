using System;
using UnityEngine;
using UnityEngine.Events;


public class CoreGameSignals : MonoBehaviour
{
    
    public static CoreGameSignals Instance;

    void Awake()
    {
        if(Instance != null && Instance != this){
            Destroy(gameObject);
            
        } else{
            Instance = this;
        }   
    }
    

   public UnityAction<int> onLevelInitialize = delegate { };
   public UnityAction onClearActiveLevel = delegate { };
   public UnityAction onNextLevel = delegate { };
   public UnityAction onRestartLevel = delegate { };

   public UnityAction onReset = delegate {};
   public UnityAction onLevelSuccessful = delegate {};
   public UnityAction onLevelFailed = delegate {};
   public Func<int> onGetLevelValue = delegate { return 0; };

    public UnityAction<byte> onStageAreaSuccessful = delegate { };
        public UnityAction onStageAreaEntered = delegate { };
        public UnityAction onFinishAreaEntered = delegate { };
        public UnityAction onMiniGameAreaEntered = delegate { };
        public UnityAction onMultiplierAreaEntered = delegate { };


}