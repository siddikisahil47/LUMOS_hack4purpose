using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject formObject;
    public Form form;
    public static GameManager instance;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    public void OpenForm()
    {
        if (form.setUpComplete)
        {
            formObject.SetActive(false);
        }
        else
        {
            formObject.SetActive(true);
        }
    }

}
