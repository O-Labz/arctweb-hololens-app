using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SearchPatients : MonoBehaviour
{

	public GameObject prefab; // This is our prefab object that will be exposed in the inspector

	private Sprite mySprite;

	public string serverIpAddress = "192.168.1.12";

	// Start is called before the first frame update
	void Start()
	{
		// A correct website page.
		//StartCoroutine(GetRequest("http://localhost:1234/arctweb/"));

		// A non-existing page.
		StartCoroutine(GetRequest("http://"+serverIpAddress+"/arctweb/getusers.php"));

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
		string name;
		string pic;

		PatientData patients = JsonUtility.FromJson<PatientData>(_json);

		Debug.Log(patients);
		Debug.Log(patients.patientData);


		foreach (PatientList data in patients.patientData)
		{
			Debug.Log(data.firstName + " " + data.lastName);
			name = data.firstName + " " + data.lastName;
			pic = data.profilePicture;
			StartCoroutine(Populate(name, pic));
		}
	}


	IEnumerator Populate(string cardName, string pictureName)
	{
		using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture("http://"+serverIpAddress+"/arctweb/images/" + pictureName))
		{
			string url = "http://"+serverIpAddress+"/arctweb/images/" + pictureName;
			yield return uwr.SendWebRequest();

			if (uwr.isNetworkError || uwr.isHttpError)
			{
				Debug.Log(uwr.error);
			}
			else
			{
				// Get downloaded asset bundle
				GameObject newObj; // Create GameObject instance
				Texture2D myTexture;
				myTexture = DownloadHandlerTexture.GetContent(uwr);
				newObj = (GameObject)Instantiate(prefab, transform);


				mySprite = Sprite.Create(myTexture, new Rect(0.0f, 0.0f, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
				// Set Text
				newObj.GetComponentInChildren<Text>().text = cardName;
				// Set Image
				newObj.GetComponent<Image>().sprite = mySprite;
				// Set button action dynamically
				newObj.GetComponent<Button>().onClick.AddListener(() => loadScene("PatientInfoScene"));
				Debug.Log("Done Creating: " + pictureName);
			}
		}
	}

	//public void OpenURL(string url)
	//{
	//	Application.OpenURL(url);
	// 	Debug.Log("is this working?");
	// }

	public void loadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

}