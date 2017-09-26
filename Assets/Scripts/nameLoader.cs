using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class nameLoader : MonoBehaviour {
	
	public class NameObject
	{
		public string firstName;
		public string lastName;

		public NameObject(string fname, string lname)
		{
			firstName = fname;
			lastName = lname;
		}
	}
	
	private List<NameObject> nameList = new List<NameObject>(99);

	void Start () 
	{
		nameList.Capacity = 500;
		TextAsset theFile = Resources.Load ("names_long.txt") as TextAsset;
		Load (theFile);	
	}

	void Load(TextAsset file)
	{
		string[] fAndlName = file.text.Split('\n');
		foreach (string s in fAndlName) {
			string[] names = s.Split('	');
			try{
				//Debug.Log (names[0] +" "+ names[1]);	
				nameList.Add(new NameObject(names[0], names[1].Substring(0, names[1].Length-1)));
			}catch{}
		}

	}

	public NameObject getName(){
		NameObject theName = nameList[Random.Range (0, nameList.Count)];
		return theName;
	}


}
