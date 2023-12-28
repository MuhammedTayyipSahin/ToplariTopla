using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class LevelData 
{
    public List<PoolData> pools;
    

    public LevelData(List<PoolData> poolList){
        pools = poolList; 
    }

}