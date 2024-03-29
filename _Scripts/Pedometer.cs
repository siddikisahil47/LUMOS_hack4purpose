
using UnityEngine;

using UnityEngine.InputSystem;

using UnityEngine.InputSystem.Android;

using UnityEngine.UI;
using TMPro;

public class Pedometer : MonoBehaviour
{
    [SerializeField] public TMP_Text text;
    [SerializeField] public int steps;
    public static Pedometer instance; 
    private void Awake()

    {
        AndroidRuntimePermissions.RequestPermission("android.permission.ACTIVITY_RECOGNITION");
    }

    void Start()

    {

        InputSystem.EnableDevice(AndroidStepCounter.current);

        AndroidStepCounter.current.MakeCurrent();

    }

    void Update()

    {
        steps = AndroidStepCounter.current.stepCounter.ReadValue();
        text.text = steps.ToString();
    }
}
