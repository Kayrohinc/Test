using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


[Serializable]
public struct Str<T>
{ [NonSerializedAttribute]
	public bool Open;
	[SerializeField]
	public T[] Parametrs;
}

[Serializable]
public class Complex_Serializeable_Parametrs : ScriptableObject
{  
	public Complex_Serializeable_Parametrs()
	{
		Lister= new List<Release>(); 
	}

	public int AssetsCount;
	public string[] Resources_path;
	public string StreamingPath="Assets/Dont_Used/StreamingAssets/";
	public string  ResourcesPath="Assets/Dont_Used/Resources/";
	[SerializeField]
	public int Count_of_Release;
   
	[SerializeField]
    public List<Release> Lister;
    

	[Serializable] 
    public class Release {
		[NonSerialized]
	public 	bool Open;
     public Release()
        {
          
		   Bindle_Version= "";
		   Bindle_Name="";
		   Product_Name="";
		   Release_Name="";
        }
		#region Parameters
	[SerializeField]
	Str<bool> Scenes;
	[SerializeField]
	Str<bool> Resource;
    [SerializeField]
    Str<bool> Streaming;
	[SerializeField]
    Str<Texture2D> Android;
	[SerializeField]
	Str<Texture2D> IOS;
		#endregion
		#region BuildParametrs
	[SerializeField]
	public string Release_Name;
	[SerializeField]
    public string Bindle_Version;
	[SerializeField]
	public string Product_Name;
	[SerializeField]
	public string Bindle_Name;
		#endregion
	  }


public void Refresh()
	{
		if (this==null)
		{
		    Lister= new List<Release>();
		}
		//////
		//////
	    Resources_path= new string[Directory.GetFiles("Assets/Resources/").Where(x=>!x.EndsWith(".meta")).ToArray().Length + Directory.GetFiles(ResourcesPath).Length];
		Directory.GetFiles("Assets/Resources/").Where(x=>!x.EndsWith(".meta")).ToArray().CopyTo(Resources_path, 0);
		Directory.GetFiles(ResourcesPath).CopyTo(Resources_path, Directory.GetFiles("Assets/Resources/").Where(x=>!x.EndsWith(".meta")).ToArray().Length);
		for (int i=0;i<Resources_path.Length;i++) 
		{
			Resources_path[i]=Resources_path[i].Split('/')[Resources_path[i].Split('/').Length - 1];
			
		}
		/// <summary>
		/// /*Resources path  Saver*/
		/// </summary>





	}
public void Save()
	{
		if (!AssetDatabase.Contains(this))
			AssetDatabase.CreateAsset(this, "Assets/BowlingBuilder/Parameters.asset");
		else
		{
			EditorUtility.SetDirty(this);
			
			EditorApplication.SaveAssets();
			AssetDatabase.SaveAssets();
			
		}

	}
public void Build()
{



}

}
