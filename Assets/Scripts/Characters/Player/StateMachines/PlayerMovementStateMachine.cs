public class PlayerMovementStateMachine : StateMachine
{
    public Player Player { get; }
    public PlayerStateReusableData ReusableData { get; }
    public PlayerIdleState IdleState { get; }
    public PlayerWalkState WalkState { get; }
    public PlayerRunState RunState { get; }
    public PlayerSprintState SprintState { get; }
    public PlayerDashState DashState { get; }
    public PlayerLightStopState LightStopState { get; }
    public PlayerMediumStopState MediumStopState { get; }
    public PlayerHardStopState HardStopState { get; }

    public PlayerMovementStateMachine(Player player)
    {
        Player = player;
        ReusableData = new PlayerStateReusableData();
        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkState(this);
        RunState = new PlayerRunState(this);
        SprintState = new PlayerSprintState(this);
        DashState = new PlayerDashState(this);
        LightStopState = new PlayerLightStopState(this);
        MediumStopState = new PlayerMediumStopState(this);
        HardStopState = new PlayerHardStopState(this);
    }
}
