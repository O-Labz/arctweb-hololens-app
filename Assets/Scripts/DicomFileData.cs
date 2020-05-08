using System;
using System.Collections.Generic;

[Serializable]
public class DicomFileData
{
	public List<DicomFileDataList> dicomFileData;
}



[Serializable]
public class DicomFileDataList
{
	public string foldername;
	public string dicomfile;
}