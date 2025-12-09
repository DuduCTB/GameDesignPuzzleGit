using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverWall : MonoBehaviour
{

    public bool active;

    public Vector3 rotationA;
    public Vector3 rotationB;

    public float speed = 5f;

    private void Update()
    {
        Quaternion target = Quaternion.Euler(active ? rotationB : rotationA);

        transform.rotation = Quaternion.Lerp(transform.rotation,target,Time.deltaTime * speed);
    }

    public void changeRotation(bool on)
    {
        active = on;
    }
}
