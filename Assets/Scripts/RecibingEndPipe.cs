using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecibingEndPipe : MonoBehaviour
{

    [SerializeField] private TriggerPipeManager parentPipe;
    public bool isCharged;
    public bool connectedToAnotherPipe;
    private bool detectingPipes = true;
    [SerializeField] private float detectionRadius = 0.1f;
    [SerializeField] private LayerMask pipesLayer;
    private GivingEnergyEnd adjacentGivingEnd;
    private EnergyConector adjacentConnector;


    // Start is called before the first frame update
    private void Awake()
    {
        
        parentPipe = GetComponentInParent<TriggerPipeManager>();
    }

    private void Start()
    {
        StartCoroutine(UpdateAdjacentStuff());

    }

    private IEnumerator UpdateAdjacentStuff()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        while (detectingPipes)
        {
            DetectOtherGivingEnds();
            if (adjacentGivingEnd != null)  RecibingPartGetsCharged(adjacentGivingEnd.isSendingEnergy);
            if (adjacentConnector != null)  RecibingPartGetsCharged(adjacentConnector.conectorCharged);
            yield return wait;
        }
    }

    private void DetectOtherGivingEnds()
    {
        Collider[] detectedStuff = Physics.OverlapSphere(transform.position, detectionRadius, pipesLayer);
        adjacentGivingEnd = null;
        adjacentConnector = null;
        foreach (Collider detectedObject in detectedStuff)
        {

            if (detectedObject.CompareTag("givingEnd"))
            {
                //Debug.Log("Parte que recibe  azul detecta parte roja ajena");

                adjacentGivingEnd = detectedObject.GetComponent<GivingEnergyEnd>();
                connectedToAnotherPipe = true;

            }


            if (detectedObject.CompareTag("energyConnector"))
            {
                adjacentConnector = detectedObject.GetComponent<EnergyConector>();
            }
        }
    }


    public void RecibingPartGetsCharged(bool value)
    {
        isCharged = value;
        parentPipe.recibingGettingEnergy = value;
    }
}
