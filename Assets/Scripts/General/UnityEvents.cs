using UnityEngine;
using UnityEngine.Events;

public class UnityEvents : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _OnAwake, _OnStart, _OnEnable, _OnDisable, _OnDestroy;

    private void Awake()
    {
        _OnAwake.Invoke();
    }

    private void Start()
    {
        _OnStart.Invoke();
    }

    private void OnEnable()
    {
        _OnEnable.Invoke();
    }

    private void OnDisable()
    {
        _OnDisable.Invoke();
    }

    private void OnDestroy()
    {
        _OnDestroy.Invoke();
    }
}
