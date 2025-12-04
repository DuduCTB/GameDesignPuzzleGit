using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecibingEndPipe : MonoBehaviour
{

    [SerializeField] private TriggerPipeManager parentPipe;
    public bool isCharged;
    public bool connectedToAnotherPipe;

    // Start is called before the first frame update
    private void Awake()
    {
        parentPipe = GetComponentInParent<TriggerPipeManager>();
    }


    private void OnTriggerEnter(Collider detection)
    {
        if (detection.CompareTag("givingEnd"))
        {
            Debug.Log("Parte que recibe  azul detecta parte roja ajena");

            GivingEnergyEnd givingEnd = detection.GetComponent<GivingEnergyEnd>();
            connectedToAnotherPipe = true;
            //if (givingEnd.isSendingEnergy)
            //{
            //    isCharged = true;
            //    parentPipe.recibingGettingEnergy = true;
            //}
            //else
            //{
            //    parentPipe.recibingGettingEnergy = false;
            //    isCharged = false;
            //}
        }

    }

    public void RecibingPartGetsCharged(bool value)
    {
        isCharged = value;
        parentPipe.recibingGettingEnergy = value;
    }
}
