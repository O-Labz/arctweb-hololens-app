using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;
using Dicom;
using Dicom.Imaging;

// UnityWebRequest.Get example

// Access a website and use UnityWebRequest.Get to download a page.
// Also try to download a non-existing page. Display the error.


public class PopulateGrid : MonoBehaviour
{

	public GameObject prefab; // This is our prefab object that will be exposed in the inspector

	private int numberToCreate = 0; // number of objects to create. Exposed in inspector

	private Texture2D DicomTexture;
	private Sprite mySprite;
	private string[] fileBundle;

	private string path;

	void Start()
	{

		GameObject newObj;

		// Only get files that begin with the letter "c".
		path = Path.Combine(Application.persistentDataPath, "Dicom/headct/");

		Debug.Log(path);

		fileBundle = Directory.GetFiles(path);

		numberToCreate = fileBundle.Length;

		//Debug.Log(dirs.Length);
		foreach (string dir in fileBundle)
		{
			//    Debug.Log(dir);
			Debug.Log(numberToCreate);

			//Debug.Log(dir.ToString());

			newObj = (GameObject)Instantiate(prefab, transform);

			var stream = File.OpenRead(dir.ToString());

			var file = DicomFile.Open(stream);

			DicomTexture = new DicomImage(file.Dataset).RenderImage().AsTexture2D();

			mySprite = Sprite.Create(DicomTexture, new Rect(0.0f, 0.0f, DicomTexture.width, DicomTexture.height), new Vector2(0.5f, 0.5f), 100.0f);

			// Set Text
			//newObj.GetComponentInChildren<Text>().text = "000112.dcm";
			//// Set Image
			newObj.GetComponent<Image>().sprite = mySprite;

		}

	}

}
