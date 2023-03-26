using System;
using UnityEngine;

[Serializable]
public class CapsuleColliderUtility : MonoBehaviour
{
    public CapsuleColliderData CapsuleColliderData { get; private set; }
    [field: SerializeField]
    public DefaultColliderData DefaultColliderData { get; private set; }
    [field: SerializeField]
    public SlopeData SlopeData { get; private set; }
}
