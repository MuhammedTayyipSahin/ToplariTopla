using UnityEngine;
using Unity.Mathematics;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InputManger : MonoBehaviour
{
    private InputData _data;
    private bool _isAvailableForTouch, _isFirstTimeTouchTaken, _isTouching;

    private float _currentVelocity;
    private float3 _moveVector; 
    private Vector2? _mousePosition;


    void Awake()
    {
        _data = GetInputData();    
        
    }
    void OnEnable()
    {
        SubscribeEvents();       
    }

    void SubscribeEvents(){
        CoreGameSignals.Instance.onReset += OnReset;
        InputSignals.Instance.onEnableInput += OnEnableInput; 
        InputSignals.Instance.onDisableInput += OnDisableInput; 
    }

    void UnsubscribeEvents(){
        CoreGameSignals.Instance.onReset -= OnReset;
        InputSignals.Instance.onEnableInput -= OnEnableInput; 
        InputSignals.Instance.onDisableInput -= OnDisableInput; 
    }




    private void OnEnableInput(){
        _isAvailableForTouch = true;
    }
    private void OnDisableInput(){
        _isAvailableForTouch = false;
    }

    private InputData GetInputData()
    {
        return Resources.Load<CD_Input>("Data/CD_Input").InputData;
    }

    private  void OnDisable()
    {
        UnsubscribeEvents();
    }

    void Update()
    {
        if(_isAvailableForTouch){
            // elimizi çekme
            if(Input.GetMouseButtonUp(0) && !IsPointerOverUIElement() ){
                _isTouching = false;
                InputSignals.Instance.onInputReleased?.Invoke(); 
                Debug.LogWarning("Çalıştırıldı -- OnInputReleased"); 
            }

            // ilk tıklama
            if(Input.GetMouseButtonDown(0) && !IsPointerOverUIElement() ){
                _isTouching = true;
                InputSignals.Instance.onInputTaken?.Invoke();
                Debug.LogWarning("Çalıştırıldı -- OnInputTaken");
                if(!_isFirstTimeTouchTaken){
                    _isFirstTimeTouchTaken = true;
                    InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
                    Debug.LogWarning("Çalıştırıldı -- OnfirstTimeTouchTaken");
                } 
                
            }

            _mousePosition = Input.mousePosition;

            if(Input.GetMouseButton(0) && !IsPointerOverUIElement()){
                if(_isTouching){
                    if(_mousePosition != null){
                        Vector2 mouseDeltaPos = (Vector2)Input.mousePosition - _mousePosition.Value;
                        if(mouseDeltaPos.x > _data.HorizantalInputSpeed){
                            _moveVector.x = _data.HorizantalInputSpeed / 10f * mouseDeltaPos.x;
                        }
                        else if(mouseDeltaPos.x < _data.HorizantalInputSpeed){
                            _moveVector.x = -_data.HorizantalInputSpeed / 10f * mouseDeltaPos.x;
                        }
                        else{
                            _moveVector.x = Mathf.SmoothDamp(-_moveVector.x, 0f, ref _currentVelocity, _data.ClampSpeed); 
                        }
                        _mousePosition = Input.mousePosition;

                        InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams(){
                            HorizantalValue = _moveVector.x,
                            ClampValues = _data.ClampValues
            
                        });
                    }
                }

            }
             
        }
    }

    private bool IsPointerOverUIElement()
    {
        var evenData = new PointerEventData(EventSystem.current);
        evenData.position = Input.mousePosition;

        var result = new List<RaycastResult>();

        EventSystem.current.RaycastAll(evenData, result);

        return result.Count > 0;
        
    }

        private void OnReset()
    {
        _isAvailableForTouch = false;
        _isTouching = false;
    }

}