using UnityEngine;
using UnityEngine.Events;

public class OnDeathEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent _onDeathEvent;

    public void OnDeath()
    {
        _onDeathEvent.Invoke();
    }
}
