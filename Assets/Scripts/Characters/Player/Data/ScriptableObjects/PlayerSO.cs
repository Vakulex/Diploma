using UnityEngine;

namespace MovementSystem
{
    [CreateAssetMenu(fileName = "Player", menuName = "Genshin Impact Movement System/Characters/Player")]
    public class PlayerSo : ScriptableObject
    {
        [field: SerializeField] public PlayerGroundedData GroundedData { get; private set; }
        [field: SerializeField] public PlayerAirborneData AirborneData { get; private set; }
    }
}