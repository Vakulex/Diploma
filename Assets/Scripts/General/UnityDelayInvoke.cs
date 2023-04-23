using UnityEngine;
using UnityEngine.Events;

public class UnityDelayInvoke : MonoBehaviour
{
    [SerializeField]
    private UnityEvent Event;

    public void Delay(float time)
    {
        Invoke("DelayInvoke", time);
    }

    private void DelayInvoke()
    {
        Event.Invoke();
    }
}
