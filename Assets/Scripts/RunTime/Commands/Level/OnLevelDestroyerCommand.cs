
using UnityEngine;

public class OnLevelDestroyerCommand
{
    private Transform _levelHolder;
    public OnLevelDestroyerCommand(Transform levelHolder){
        _levelHolder = levelHolder;    
    }


    public void Destroy(){ 
        if(_levelHolder.childCount > 0){
        Object.Destroy(_levelHolder.transform.GetChild(0).gameObject);

        }
    }




}