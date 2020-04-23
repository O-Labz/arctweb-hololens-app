using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New GlobalIp", menuName = "GlobalIp")]
public class GlobalIp : ScriptableObject
{
    public string applicationIpaddress;
    public string centralPatientId;
}
