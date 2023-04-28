using System;
using UnityEngine;

[Serializable]
public class PlayerCapsuleColliderUtility : CapsuleColliderUtility
{
    [field: SerializeField] public PlayerTriggerColliderData TriggerColliderData { get; private set; }

    protected override void OnInit()
    {
        base.OnInit();
        
        TriggerColliderData.Initialize();
    }
}
