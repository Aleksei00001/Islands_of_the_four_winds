using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlpool : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up, 1000 * Time.fixedDeltaTime);
    }
}
