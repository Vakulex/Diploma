using UnityEngine;
using UnityEngine.Serialization;

//TODO: a lot.

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [FormerlySerializedAs("Data")]
    [Header("References")]
    [field: SerializeField] public PlayerSO data;

    public PlayerInput Input { get; private set; }

    public Transform MainCameraTransform { get; private set; }
    
    public Rigidbody Rigidbody { get; private set; }
    
    private PlayerMovementStateMachine _movementStateMachine;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Input = GetComponent<PlayerInput>();
        
        MainCameraTransform = Camera.main.transform;
        _movementStateMachine = new PlayerMovementStateMachine(this);
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
