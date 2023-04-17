using System;
using UnityEngine;

//TODO: a lot.

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField]
    public PlayerSO Data { get; private set; }

    [field: Header("Collisions")]
    [field: SerializeField] public CapsuleColliderUtility ColliderUtility { get; private set; }
    [field: SerializeField] public PlayerLayerData LayerData { get; private set; }
    
    public PlayerInput Input { get; private set; }

    public Transform MainCameraTransform { get; private set; }
    
    public Rigidbody Rigidbody { get; private set; }
    
    private PlayerMovementStateMachine _movementStateMachine;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Input = GetComponent<PlayerInput>();
        
        ColliderUtility.Initialize(gameObject);
        ColliderUtility.CalculateCapsuleColliderDimensions();
        
        MainCameraTransform = Camera.main.transform;
        _movementStateMachine = new PlayerMovementStateMachine(this);
    }

    private void OnValidate()
    {
        ColliderUtility.Initialize(gameObject);
        ColliderUtility.CalculateCapsuleColliderDimensions();
    }

    private void Start()
    {
        _movementStateMachine.ChangeState(_movementStateMachine.IdleState);
    }

    private void Update()
    {
        _movementStateMachine.HandleInput();
        _movementStateMachine.Update();
    }

    private void FixedUpdate()
    {
        _movementStateMachine.PhysicsUpdate();
    }
}
