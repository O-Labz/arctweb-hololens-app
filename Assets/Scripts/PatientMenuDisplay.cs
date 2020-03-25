﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PatientMenuDisplay : MonoBehaviour
{
    public Patient patient;
    private string ipAddress;
    private string patientId;

	// Items Displayed in UI
	public Image profilePic;
	public Text nameText;
	public Text ageText;
	public Text genderText;
	public Text lastVisit;

	Texture newTexture;

	// Start is called before the first frame update
	void Start()
    {
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
		// Fetch image from server

		Debug.Log("Your profile picture is: "+patient.profilePicture);
		StartCoroutine(GetImage(patient.profilePicture));
		nameText.text = "Name: " + patient.firstName + " " + patient.lastName;
		ageText.text = "Age: " + patient.age;
		genderText.text = "Gender: " + patient.gender;
		lastVisit.text = "Last Visit: 02/03/2020";
		// profilePic = (Image)Instantiate(profilePic, transform);
		// profilePic.sprite = patient.profileSprite;
		// profilePic.GetComponent<Image>().sprite = patient.profileSprite;
		// profilePic.GetComponent<Image>().material.mainTexture = newTexture;

		Debug.Log("patient " + patient.id +" data loaded");
	}

	IEnumerator GetImage(string pictureName)
	{
		using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture("http://" + ipAddress + "/arctweb/images/" + pictureName))
		{
			yield return uwr.SendWebRequest();

			if (uwr.isNetworkError || uwr.isHttpError)
			{
				Debug.Log(uwr.error);
			}
			else
			{
				// Get downloaded asset bundle

				Texture2D myTexture;

				myTexture = DownloadHandlerTexture.GetContent(uwr);

				profilePic.sprite  = Sprite.Create(myTexture, new Rect(0.0f, 0.0f, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f), 100.0f);


			}
		}
	}
}
