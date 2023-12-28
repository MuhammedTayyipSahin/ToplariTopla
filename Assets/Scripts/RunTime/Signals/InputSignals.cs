using UnityEngine;
using UnityEngine.Events;

public class InputSignals : MonoBehaviour
{
    public static InputSignals Instance; 

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else{
            Instance = this;
        }
    }

    public UnityAction onFirstTimeTouchTaken = delegate{ };

    public UnityAction onInputTaken = delegate{}; 

    public UnityAction onInputReleased = delegate{   };
    public UnityAction onEnableInput = delegate{   };
    public UnityAction onDisableInput = delegate{   };
 
    public UnityAction<HorizontalInputParams> onInputDragged = delegate{ };

}