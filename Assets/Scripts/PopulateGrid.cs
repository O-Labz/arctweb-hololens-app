using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

// UnityWebRequest.Get example

// Access a website and use UnityWebRequest.Get to download a page.
// Also try to download a non-existing page. Display the error.


public class PopulateGrid : MonoBehaviour
{

	public GameObject prefab; // This is our prefab object that will be exposed in the inspector

	private int numberToCreate = 10; // number of objects to create. Exposed in inspector

	void Start()
	{
		Populate();
	}

	void Update()
	{

	}

	void Populate()
	{
		GameObject newObj; // Create GameObject instance

		for (int i = 0; i < numberToCreate; i++)
		{
			// Create new instances of our prefab until we've created as many as we specified
			newObj = (GameObject)Instantiate(prefab, transform);
			newObj.GetComponentInChildren<Text>().text = "testing: " + i;

			// Randomize the color of our image
			//newObj.GetComponent().color = Random.ColorHSV();
		}

		Debug.Log("Done Creating Images");

	}

}
