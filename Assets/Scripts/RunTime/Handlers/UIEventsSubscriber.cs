using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIEventsSubscriber : MonoBehaviour
{

    [SerializeField] private UIEventSubscriptionType type;
    [SerializeField] private Button button;
    
    private UnityAction TextAction  = delegate{};
    private UnityEvent TestAction = new UnityEvent();

    private UIManager _manager;



    void Awake()
    {
        GetReferences();
    }

    private void GetReferences()
    {
        _manager = FindObjectOfType<UIManager>();
    }

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
        switch (type)
        {
            case UIEventSubscriptionType.OnPlay:
                button.onClick.AddListener(_manager.Play);
                // button.clicked += _manager.Play;
                break;
            case UIEventSubscriptionType.OnNextLevel:
                button.onClick.AddListener(_manager.NextLevel);
                break;
            case UIEventSubscriptionType.OnRestartLevel:
                button.onClick.AddListener(_manager.RestartLevel);
                break;
        }
    }

     private void UnSubscribeEvents()
    {
        switch (type)
        {
            case UIEventSubscriptionType.OnPlay:
                button.onClick.RemoveListener(_manager.Play);
                // button.clicked -= _manager.Play;
                break;
            case UIEventSubscriptionType.OnNextLevel:
                button.onClick.RemoveListener(_manager.NextLevel);
                break;
            case UIEventSubscriptionType.OnRestartLevel:
                button.onClick.RemoveListener(_manager.RestartLevel);
                break;
        }
    }



}