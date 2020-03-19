using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientMenuDisplay : MonoBehaviour
{
    public Patient patient;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(patient.firstName);
    }
}
