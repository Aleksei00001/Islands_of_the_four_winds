using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    [SerializeField] GameObject[] islandPrefab;

    private void Start()
    {
        GameObject newIsland = Instantiate(islandPrefab[Random.Range(0, 3)]);
        newIsland.transform.SetParent(this.transform);
        newIsland.transform.localScale = new Vector3(Random.Range(30, 60), Random.Range(30, 60), Random.Range(30, 60));
        newIsland.transform.Rotate(Vector3.forward, Random.Range(0, 360));
    }
}
