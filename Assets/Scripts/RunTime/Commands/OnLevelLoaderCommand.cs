
using UnityEngine;

public class OnLevelLoaderCommand{

    Transform _levelHolder;
   public OnLevelLoaderCommand(Transform levelHolder){

    _levelHolder = levelHolder;
   }
    public void Execute(int levelIndex){
        GameObject levelObject = Object.Instantiate(Resources.Load<GameObject>($"Prefabs/LevelPrefabs/level {levelIndex}"));

    }
    
}