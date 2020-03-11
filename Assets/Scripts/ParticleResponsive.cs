using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleResponsive : MonoBehaviour
{
    public GameObject button;
    void Start()
    {
        Vector3 temp = button.GetComponent<RectTransform>().position;
        temp.z = 1;
        this.GetComponent<Transform>().position = temp;
    }
}
