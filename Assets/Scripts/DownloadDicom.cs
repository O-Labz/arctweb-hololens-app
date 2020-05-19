using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class DownloadDicom : MonoBehaviour
{

    public TextMeshPro text;

    public GlobalIp global;

    private string serverIpAddress;

    void Start()
    {
        // A non-existing page.
        serverIpAddress = global.applicationIpaddress;
        StartCoroutine(GetRequest("http://" + serverIpAddress + "/arctweb/getdicom.php"));
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
                processDicomJsonData(webRequest.downloadHandler.text);
            }
        }
    }

    private void processDicomJsonData(string _json)
    {
        string foldername;
        string dicomfile;

        DicomFileData dicomData = JsonUtility.FromJson<DicomFileData>(_json);

        Debug.Log(dicomData);
        Debug.Log(dicomData.dicomFileData);


        foreach (DicomFileDataList data in dicomData.dicomFileData)
        {
            foldername = data.foldername;
            dicomfile = data.dicomfile;
            StartCoroutine(DownloadFile(dicomfile));
        }
    }

    IEnumerator DownloadFile(string filename)
    {
        var uwr = new UnityWebRequest("http://" + serverIpAddress + "/arctweb/dicom/headct/" + filename + "", UnityWebRequest.kHttpVerbGET);
        string path = Path.Combine(Application.persistentDataPath, "Dicom/headct/" + filename);
        uwr.downloadHandler = new DownloadHandlerFile(path);
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError || uwr.isHttpError)
            Debug.LogError(uwr.error);
        else
            text.text = path;
        Debug.Log(text);
        Debug.Log("File successfully downloaded and saved to " + path);
    }
}