using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    void OnEnable()
    {
        SubscribeEvents();
    }

    void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialize;
        CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
        CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
        CoreGameSignals.Instance.onReset += OnReset;
    }

    private void UnSubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize -= OnLevelInitialize;
        CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
        CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
        CoreGameSignals.Instance.onReset -= OnReset;
    }


     private void OnLevelInitialize(int arg0)
    {
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelType.Level,0);
        UISignals.Instance.onSetLevelValue?.Invoke((int)CoreGameSignals.Instance.onGetLevelValue?.Invoke());
    }

       private void OnLevelSuccessful()
    {
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelType.Win,2);
    }

    private void OnLevelFailed()
    {
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelType.Fail,2);
    }



    private void OnReset(){
        CoreUISignals.Instance.onCloseALlPanels?.Invoke();
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelType.Start,1);
    }


    public void NextLevel(){
        CoreGameSignals.Instance.onNextLevel?.Invoke();
       // CoreGameSignals.Instance.onReset?.Invoke();
    }

    public void RestartLevel(){
        CoreGameSignals.Instance.onRestartLevel?.Invoke();
      //  CoreGameSignals.Instance.onReset?.Invoke();

    }   

    public void Play(){
        Debug.LogWarning("UI Manager Tetiklendi");

        UISignals.Instance.onPlay?.Invoke();
        CoreUISignals.Instance.onClosePanel?.Invoke(1); 
        InputSignals.Instance.onEnableInput?.Invoke();
        //CameraSignals.Instance.onSetCameraTarget?.Invoke(); 
    }


}