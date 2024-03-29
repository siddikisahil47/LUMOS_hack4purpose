using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Android;
using TMPro;
public class PedometerNew : MonoBehaviour
{
    public InputAction stepAction;
    public int steps;
    public TMP_Text textMesh;
    // Start is called before the first frame update
    void Start()
    {
        InputSystem.AddDevice<StepCounter>();
        if (StepCounter.current != null)
        {
            Debug.Log("Sensor found!");
            InputSystem.EnableDevice(StepCounter.current);
            StepCounter.current.MakeCurrent();
        }
        InputSystem.AddDevice<Accelerometer>();
        if (Accelerometer.current != null)
        {
            Debug.Log("Acc. Sensor found!");
            InputSystem.EnableDevice(Accelerometer.current);
        }
        steps = 0;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (StepCounter.current != null && StepCounter.current.enabled)
        {
            if (StepCounter.current.stepCounter.ReadValue() > steps)
            {
                steps = StepCounter.current.stepCounter.ReadValue();
                Debug.Log(steps);
            }
            textMesh.text = steps.ToString() + ", " + Accelerometer.current.acceleration.ReadValue();
            Debug.Log(steps.ToString() + ", " + Accelerometer.current.acceleration.ReadValue());
        }
        else
        {
            Debug.Log("No step counter found!");
        }
    }
}
