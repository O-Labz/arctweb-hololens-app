using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using Dicom;
using Dicom.Imaging;
using UnityEngine;
using UnityEngine.UI;

public class DicomScrub : MonoBehaviour
{
    private Texture2D DicomTexture;
    public Image dicom2D;
    public Slider ScrubSlider;
    private Sprite mySprite;
    private string[] fileBundle;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            // Only get files that begin with the letter "c".
            fileBundle = Directory.GetFiles(@"C:\Users\Omri\Downloads\trauma\");
            ScrubSlider = ScrubSlider.GetComponent<Slider>();
            ScrubSlider.maxValue = fileBundle.Length;
            ShowImage(0);
            //Debug.Log(dirs.Length);
            //foreach (string dir in dirs)
            //{
            //    Debug.Log(dir);
            //}
        }
        catch (Exception e)
        {
            Debug.Log("Failed to get files: " + e.ToString());
        }
    }

    //fetch images as the slider moves
    public void ScrubDicom() 
    {
        int value = (int)ScrubSlider.value;
        ShowImage(value);
    }

    //Standard function to move images
    void ShowImage(int dicomIndex)
    {
        var stream = File.OpenRead(@"" + fileBundle[dicomIndex]);
        var file = DicomFile.Open(stream);
        DicomTexture = new DicomImage(file.Dataset).RenderImage().AsTexture2D();

        mySprite = Sprite.Create(DicomTexture, new Rect(0.0f, 0.0f, DicomTexture.width, DicomTexture.height), new Vector2(0.5f, 0.5f), 100.0f);

        dicom2D.GetComponent<Image>().sprite = mySprite;
    }
}
