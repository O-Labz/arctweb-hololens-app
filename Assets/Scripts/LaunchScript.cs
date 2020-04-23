using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchScript : MonoBehaviour
{
    public GlobalIp globalIp;

    public string applicationIpAddress = "192.168.1.12";

    // Start is called before the first frame update
    void Start()
    {
        globalIp.applicationIpaddress = applicationIpAddress;
    }
}
