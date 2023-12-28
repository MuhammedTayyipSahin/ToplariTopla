using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class UIPanelController : MonoBehaviour
{
    
    [SerializeField] private List<Transform> layers = new List<Transform>();
    
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
        CoreUISignals.Instance.onClosePanel += OnClosePanel;
        CoreUISignals.Instance.onOpenPanel += OnOpenPanel;
        CoreUISignals.Instance.onCloseALlPanels += OnCloseAllPanel;
    }

    private void UnSubscribeEvents(){
        CoreUISignals.Instance.onClosePanel -= OnClosePanel;
        CoreUISignals.Instance.onOpenPanel -= OnOpenPanel;
        CoreUISignals.Instance.onCloseALlPanels -= OnCloseAllPanel;
    }
    
    private void OnOpenPanel(UIPanelType panelType, int value)
    {
        OnClosePanel(value);
        Instantiate(
        Resources.Load<GameObject>($"Screens/{panelType}Panel"),layers[value] 
        ); 
    }


    private void OnClosePanel(int value)
    {
        if(layers[value].childCount > 0){
            #if UNITY_EDITOR
                DestroyImmediate(layers[value].gameObject);
            #else
            Destroy(layers[value].gameObject); 
            #endif
        }
    }

    private void OnCloseAllPanel()
    {
        foreach(var layer in layers){
            if(layer.childCount > 0){
            #if UNITY_EDITOR
                DestroyImmediate(layer.GetChild(0).gameObject);
            #else
            Destroy(layer.GetChild(0).gameObject); 
            #endif
        }
        }
    }


    
}