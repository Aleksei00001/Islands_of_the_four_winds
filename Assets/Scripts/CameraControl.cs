using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Grid grid;

    void Update()
    {
        transform.position = new Vector3(grid.levelRule.sizeX - 1, (grid.levelRule.sizeX + grid.levelRule.sizeY), -grid.levelRule.sizeY * 2);
    }
}
