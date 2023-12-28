using UnityEngine;
using UnityEngine.Events;

public class CoreUISignals: MonoBehaviour{

    public static CoreUISignals Instance;


    private void Awake()
    {
        if(Instance != null && Instance != this){
            Destroy(gameObject);
        }

        Instance = this;

    }

    public UnityAction<UIPanelType, int> onOpenPanel = delegate{};
    public UnityAction<int> onClosePanel = delegate{};

    public UnityAction onCloseALlPanels = delegate{};
  







}