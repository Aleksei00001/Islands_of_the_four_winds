using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [SerializeField] Slider slider;

    void Start()
    {
        audioSource.Play();
    }

    private void Update()
    {
        audioSource.volume = slider.value;
    }

}
