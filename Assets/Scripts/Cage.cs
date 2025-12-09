using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{

    [SerializeField] private Transform ironBars;
    [SerializeField] private float openedHeight, closedHeight, movingSpeed;
    [SerializeField] private bool isMoving;
    [SerializeField] private int neededLampAmount;
    [SerializeField] private int currentLampAmount;
    private Vector3 desiredPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving) MoveIronBars();
    }

    public void StartMovingIronBars(bool open)
    {
        if (open)
        {
            isMoving = true;
            desiredPosition = new Vector3(ironBars.position.x, openedHeight, ironBars.position.z);
        }
        else
        {
            isMoving = true;
            desiredPosition = new Vector3(ironBars.position.x, closedHeight, ironBars.position.z);
        }
    }

    private void MoveIronBars()
    {
        if (ironBars.position != desiredPosition)
        {
            ironBars.position = Vector3.MoveTowards(ironBars.position, desiredPosition, movingSpeed * Time.deltaTime);
        }
        else
        {
            isMoving = false;
        }
    }


    public void AddRemoveAmountOfConditions(int amount)
    {
        currentLampAmount += amount;
        Mathf.Clamp(currentLampAmount, 0, neededLampAmount);
        if (currentLampAmount >= neededLampAmount)
        {
            StartMovingIronBars(true);
        }
        else
        {
            StartMovingIronBars(false);
        }
    }

}
