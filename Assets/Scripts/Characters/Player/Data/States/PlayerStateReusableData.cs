using System.Collections.Generic;
using UnityEngine;

public class PlayerStateReusableData
{
    public Vector2 MovementInput { get; set; }
    public float MovementSpeedModifier { get; set; }
    public float MovementDecelerationForce { get; set; }
    
    public List<PlayerCameraRecenteringData> SidewaysCameraRecenteringData { get; set; }
    public List<PlayerCameraRecenteringData> BackwardsCameraRecenteringData { get; set; }
    public bool ShouldWalk { get; set; }
    public bool ShouldSprint { get; set; }

    private Vector3 _currentTargetRotation;
    private Vector3 _timeToReachRotation;
    private Vector3 _dampedTargetRotationCurrentVelocity;
    private Vector3 _dampedTargetRotationPassedTime;

    public ref Vector3 CurrentTargetRotation
    {
        get
        {
            return ref _currentTargetRotation;
        }
        
    }public ref Vector3 TimeToReachRotation
    {
        get
        {
            return ref _timeToReachRotation;
        }
        
    }public ref Vector3 DampedTargetRotationCurrentVelocity
    {
        get
        {
            return ref _dampedTargetRotationCurrentVelocity;
        }
        
    }public ref Vector3 DampedTargetRotationPassedTime
    {
        get
        {
            return ref _dampedTargetRotationPassedTime;
        }
        
    }
    
    public Vector3 CurrentJumpForce { get; set; }

    public PlayerRotationData RotationData { get; set; }
}
