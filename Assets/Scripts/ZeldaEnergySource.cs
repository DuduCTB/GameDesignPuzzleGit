using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeldaEnergySource : MonoBehaviour
{
    public bool sourceChargedUp;
    public bool isAnInfiniteSource;
    public bool conectedToAPipeChargedConector;
    public bool dischargeIsDisabled;
    [SerializeField] private int touchingConnectorsAmount;
    [SerializeField] private EnergyConector connectorToDischarge;


    [SerializeField] private LayerMask pipesLayer;
    [SerializeField] private float detectionRadius = 0.5f;
    [SerializeField] private float amountNeedOfBoxAreas = 1f;
    [SerializeField] private Vector3 boxDetectionArea = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] private Vector3 aBoxLDetectionArea = new Vector3(1f, 0.5f, 0.5f);
    [SerializeField] private Vector3 aBoxLOffset = new Vector3(1f, 0.5f, 0.5f);
    [SerializeField] private Vector3 bBoxLDetectionArea = new Vector3(0.5f, 0.5f, 1f);
    [SerializeField] private Vector3 bBoxLOffset = new Vector3(1f, 0.5f, 0.5f);
    [SerializeField] private Transform alternatePivot;
    // Listas reutilizables para evitar GC
    private readonly List<Collider> previous = new();
    private readonly List<Collider> current = new();

    // Buffer para el OverlapSphere
    private static readonly Collider[] buffer = new Collider[32];

    private void Start()
    {
        if (isAnInfiniteSource)
        {
            sourceChargedUp = true;
        }
    }


    private void Update()
    {
        CheckIfShouldKeepBeingCharged();
        Detect();
        CompareLists();
    }

    private void Detect()
    {
        current.Clear();
        int hits = 0;
        switch (amountNeedOfBoxAreas)
        {
            case 1: hits = Physics.OverlapBoxNonAlloc(transform.position, boxDetectionArea, buffer, transform.rotation, pipesLayer, QueryTriggerInteraction.Collide);break;
            case 2:
                hits += Physics.OverlapBoxNonAlloc(alternatePivot.position + aBoxLOffset, aBoxLDetectionArea, buffer, transform.rotation, pipesLayer, QueryTriggerInteraction.Collide);
                hits += Physics.OverlapBoxNonAlloc(alternatePivot.position + bBoxLOffset, bBoxLDetectionArea, buffer, transform.rotation, pipesLayer, QueryTriggerInteraction.Collide);
                break;
        }

        for (int i = 0; i < hits; i++)
            current.Add(buffer[i]);
    }

    private void CompareLists()
    {
        // --- Detectar ENTRADAS ---
        foreach (Collider col in current)
        {
            if (!previous.Contains(col))
            {
                OnEnter(col);
            }
        }

        // --- Detectar SALIDAS ---
        foreach (Collider col in previous)
        {
            if (!current.Contains(col))
            {
                OnExit(col);
            }
        }

        // Copiar current -> previous
        previous.Clear();
        previous.AddRange(current);
    }

    private void OnEnter(Collider collision)
    {
        if (collision.transform.CompareTag("energyConnector"))
        {
            //Debug.Log("Source contactando con recibing End");
            EnergyConector conector = collision.transform.GetComponent<EnergyConector>();

            if (sourceChargedUp && !conector.energyFromPipe)
            {
                conector.conectorCharged = true;
                conector.energyFromSource = true;
                conector.touchingEnergySource = false;
                conector.ConectorUpdateAnotherPipeRedEndCharge();

            }

            if (!conector.energyFromPipe)
            {
                connectorToDischarge = conector;
            }


            touchingConnectorsAmount++;
        }
    }

    private void OnExit(Collider collision)
    {
        if (collision.transform.CompareTag("energyConnector"))
        {
            //Debug.Log("Source contactando con recibing End");
            EnergyConector conector = collision.transform.GetComponent<EnergyConector>();

            if (sourceChargedUp && !conector.energyFromPipe)
            {
                conector.conectorCharged = false;
                conector.energyFromSource = false;
                conector.touchingEnergySource = false;
                conector.ConectorUpdateAnotherPipeRedEndCharge();

            }
            touchingConnectorsAmount--;

            if (conector == connectorToDischarge)
            {
                connectorToDischarge = null;
            }
        }
    }
    private void CheckIfShouldKeepBeingCharged()
    {
        if (sourceChargedUp && !conectedToAPipeChargedConector && !isAnInfiniteSource)
        {
            if (connectorToDischarge != null)
            {
                connectorToDischarge.energyFromSource = false;
                connectorToDischarge.conectorCharged = false;
            }

            sourceChargedUp = false;
        }

        if (sourceChargedUp && connectorToDischarge != null && connectorToDischarge.touchingEnergySource && !connectorToDischarge.conectorCharged)
        {
            connectorToDischarge.energyFromSource = true;
            connectorToDischarge.conectorCharged = true;
            connectorToDischarge.ConectorUpdateAnotherPipeRedEndCharge();
        }

        if (touchingConnectorsAmount == 0 && !isAnInfiniteSource)
        {
            sourceChargedUp = false;
            conectedToAPipeChargedConector = false;
            
        }

        if (!sourceChargedUp && touchingConnectorsAmount >= 1 && !dischargeIsDisabled && connectorToDischarge != null)
        {
            connectorToDischarge.energyFromSource = false;
            connectorToDischarge.conectorCharged = false;
            connectorToDischarge.ConectorUpdateAnotherPipeRedEndCharge();
            dischargeIsDisabled = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        switch (amountNeedOfBoxAreas)
        { 
            case 1: Gizmos.DrawWireCube(transform.position, boxDetectionArea * 2);break;
            case 2:
                Gizmos.DrawWireCube(alternatePivot.position + aBoxLOffset, aBoxLDetectionArea * 2);
                Gizmos.DrawWireCube(alternatePivot.position + bBoxLOffset, bBoxLDetectionArea * 2);
                break;
        }  
    }
}
