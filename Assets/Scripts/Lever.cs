using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    public bool isLeverOn = false; 
    public UnityEvent OnLeverTurnedOn;
    public UnityEvent OnLeverTurnedOff;

    [SerializeField] private GameObject onModel, offModel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) //Debug
        {
            ActivateLever();
        }
    }

    public void ActivateLever()
    {
        if (!isLeverOn)
        {
            OnLeverTurnedOn.Invoke();
            onModel.SetActive(true);
            offModel.SetActive(false);
            isLeverOn = true;
        }
        else
        {
            OnLeverTurnedOff.Invoke();
            onModel.SetActive(false);
            offModel.SetActive(true);
            isLeverOn = false;
        }
    }
}
