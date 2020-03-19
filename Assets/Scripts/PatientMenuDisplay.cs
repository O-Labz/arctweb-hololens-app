using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PatientMenuDisplay : MonoBehaviour
{
    public Patient patient;
    private SearchPatients getPatient;
    private string ipAddress;
    private string patientId;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(patient.firstName);
        patientId = patient.id;
		ipAddress = patient.serverIp;
        StartCoroutine(GetRequest("http://" + ipAddress + "/arctweb/getuser.php?user=" + patientId));
    }

	IEnumerator GetRequest(string uri)
	{
		using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
		{
			// Request and wait for the desired page.
			yield return webRequest.SendWebRequest();

			if (webRequest.isNetworkError)
			{
				Debug.Log(": Error: " + webRequest.error);
			}
			else
			{
				processPatientJsonData(webRequest.downloadHandler.text);
			}
		}
	}

	private void processPatientJsonData(string _json)
	{

		PatientData patients = JsonUtility.FromJson<PatientData>(_json);

		foreach (PatientList data in patients.patientData)
		{
			patient.id = data.id;
			patient.firstName = data.firstName;
			patient.lastName = data.lastName;
			patient.age = data.age;
			patient.gender = data.gender;
			patient.doctorNotes = data.doctorNotes;
			patient.diagnosis = data.diagnosis;
			patient.allergies = data.allergies;
			patient.scans = data.scans;
			patient.profilePicture = data.profilePicture;
		}

		Debug.Log("patient " + patient.id +" data loaded");
	}
}
