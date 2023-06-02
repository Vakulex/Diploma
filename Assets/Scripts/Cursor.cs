using UnityEngine;

public class Cursor : MonoBehaviour
{

    private void Start()
    {
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetCursorState(bool isEnabled)
    {
        UnityEngine.Cursor.visible = isEnabled;
        if (isEnabled)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
    }

}
