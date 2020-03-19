using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Patient", menuName = "Patient")]
public class Patient : ScriptableObject
{
    public string id;
    public string firstName;
    public string lastName;
    public string age;
    public string gender;
    public string doctorNotes;
    public string diagnosis;
    public string allergies;
    public string scans;
    public string serverIp;
    public string profilePicture;
}
