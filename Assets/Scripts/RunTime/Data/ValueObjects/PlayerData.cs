using System;
using Unity.Mathematics;
using UnityEngine;



[Serializable]
public class PlayerData
{
    public PlayerMovementData movementData; 
    public PlayerMovementMesh meshData;

    public PlayerForceDaty forceData;


}


[Serializable]
public class PlayerForceDaty
{
    public float3 ForceParameters;
}


[Serializable]
public class PlayerMovementMesh
{ 
    public float ScaleCount;
}


[Serializable]
public class PlayerMovementData
{   
    public float ForwardSpeed;
    public float SidewaySpeed;
}