using System;
using System.Collections.Generic;

[Serializable]
public class PatientData
{
	public List<PatientList> patientData;
}


[Serializable]
public class PatientList
{
	public string id;
	public string firstName;
	public string lastName;
	public string age;
	public string gender;
	public string doctorNotes;
	public string diagnosis;
	public string allergies;
	public string scans;
	public string profilePicture;
}