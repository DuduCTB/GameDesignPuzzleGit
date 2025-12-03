using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPipeManager : MonoBehaviour
{
    //Este script tiene un nombre incorrecto, no es un manager general, es como funciona cada cable individualmente
    [SerializeField] private bool recibingConnected, givingConnected;
    [SerializeField] private Material pipeOnMaterial, pipeOffMaterial;

    private Renderer myRenderer;
    public bool recibingGettingEnergy, sendingGettingEnergy;
    [SerializeField] public GivingEnergyEnd givingPipeEnd;


    private void Awake()
    {
        myRenderer = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (sendingGettingEnergy)
        {
            givingPipeEnd.isSendingEnergy = true;
            ChangeMaterialBasedOnEnergy(true);
        }
        else
        {

        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            //CheckIfPipeIsConected();
        }
    }

    private void ChangeMaterialBasedOnEnergy(bool isConnectedToEnergy)
    {
        if (isConnectedToEnergy)
        {
            myRenderer.material = pipeOnMaterial;
        }
        else
        {
            myRenderer.material = pipeOffMaterial;
        }
    }


    private void OnDrawGizmos()
    {

    }
}
