using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;




[Serializable]
public class Complex_Serializeable_Parametrs : ScriptableObject
{
   

	public Complex_Serializeable_Parametrs()
	{
		Lister= new List<Release>(); 
	}
    public string[] Resources_path;
    public string[] Streaming_path;
    public int SceneCount;
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
           Scenes = new bool[0];
           Resource = new bool[0];
           Streaming = new bool[0];
           Android = new Texture2D[0];
           IOS = new Texture2D[0];
		   Bindle_Version= "";
		   Bindle_Name="";
		   Product_Name="";
		   Release_Name="";
        }
	#region Parameters
public 	bool[] Scenes;
[NonSerialized]
public bool Scene_Open;
public	bool[] Resource;
[NonSerialized]
public bool Resource_Open;
public  bool[] Streaming;
[NonSerialized]
public bool Streaming_Open;
[NonSerialized]
public bool Icon;
public Texture2D[] Android;
[NonSerialized]
public bool Android_Open;
public	Texture2D[] IOS;
[NonSerialized]
public bool IOS_Open;
    #endregion
	#region BuildParametrs
	public string Release_Name;
	public string Bindle_Version;
	public string Product_Name;
	public string Bindle_Name;
	#endregion
	  }


public void Refresh()
    {
       
	  	if (this==null)
		{
		    Lister= new List<Release>();
            Lister.Add(new Release());
        }
        #region  Resources_Path
        Resources_path = new string[Directory.GetFiles("Assets/Resources/").Where(x=>!x.EndsWith(".meta")).ToArray().Length + Directory.GetFiles(ResourcesPath).Length];
		Directory.GetFiles("Assets/Resources/").Where(x=>!x.EndsWith(".meta")).ToArray().CopyTo(Resources_path, 0);
		Directory.GetFiles(ResourcesPath).CopyTo(Resources_path, Directory.GetFiles("Assets/Resources/").Where(x=>!x.EndsWith(".meta")).ToArray().Length);
		for (int i=0;i<Resources_path.Length;i++) 
		{
			Resources_path[i]=Resources_path[i].Split('/')[Resources_path[i].Split('/').Length - 1];

        }
        #endregion
        #region Streaming_Path
        Streaming_path = new string[Directory.GetFiles("Assets/StreamingAssets/").Where(x => !x.EndsWith(".meta")).ToArray().Length + Directory.GetFiles(StreamingPath).Length];
        Directory.GetFiles("Assets/StreamingAssets/").Where(x => !x.EndsWith(".meta")).ToArray().CopyTo(Streaming_path, 0);
        Directory.GetFiles(StreamingPath).CopyTo(Streaming_path, Directory.GetFiles("Assets/StreamingAssets/").Where(x => !x.EndsWith(".meta")).ToArray().Length);
        for (int i = 0; i < Streaming_path.Length; i++)
        {
            Streaming_path[i] = Streaming_path[i].Split('/')[Streaming_path[i].Split('/').Length - 1];
        }        
        #endregion
        #region Scene
        SceneCount =EditorBuildSettings.scenes.Length;
        
        #endregion
        for (int i = 0; i <Count_of_Release;i++)
        {
            
            if (Lister[i].Scenes.Length != SceneCount)
            {
                Lister[i].Scenes = new bool[SceneCount];
            }
            if (Lister[i].Resource.Length != Resources_path.Length)
            {
                Lister[i].Resource = new bool[Resources_path.Length];
            }
            if (Lister[i].Streaming.Length != Streaming_path.Length)
            {
                Lister[i].Streaming = new bool[Streaming_path.Length];
            }
            if (Lister[i].IOS.Length==0)
            {
                Lister[i].IOS = PlayerSettings.GetIconsForTargetGroup(BuildTargetGroup.iPhone);
            }
            if (Lister[i].Android.Length==0)
            {
                Lister[i].Android = PlayerSettings.GetIconsForTargetGroup(BuildTargetGroup.Android);
            }
        }

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
public void Build(int j)
{

#region Streaming
        for (int i = 0; i <Streaming_path.Length; i++)
        {
            if ((!Lister[j].Streaming[i]) && (!File.Exists(StreamingPath+Streaming_path[i])))
            {
                try
                {
					File.Move("Assets/StreamingAssets/" + Streaming_path[i], StreamingPath+"/" + Streaming_path[i]);
					File.Move("Assets/StreamingAssets/" + Streaming_path[i]+".meta",StreamingPath+"/" + Streaming_path[i]+".meta");

                }
                catch (Exception e)
                {
                    Debug.Log("Move failed: " + e.Message);
                }
            }

            if ((Lister[j].Streaming[i]) && (!Directory.Exists("Assets/StreamingAssets/" + Streaming_path[i])))
            {


                try
                {
                    File.Move(StreamingPath + Streaming_path[i], "Assets/StreamingAssets/" + Streaming_path[i]);
                     File.Move(StreamingPath + Streaming_path[i]+".meta", "Assets/StreamingAssets/" + Streaming_path[i]+".meta");

                }
                catch (Exception e)
                {
                    Debug.Log("Move failed: " + e.Message);
                }

            }
        }
    
#endregion
#region Resources
    for (int i = 0; i < Resources_path.Length; i++)
        {
			if ((!Lister[j].Resource[i]) && (!File.Exists(ResourcesPath+Resources_path[i])))
            {
                try
                {
                    File.Move("Assets/Resources/" + Resources_path[i], ResourcesPath+"/" + Resources_path[i]);
					File.Move("Assets/Resources/" + Resources_path[i]+".meta", ResourcesPath+"/" + Resources_path[i]+".meta");

                }
                catch (Exception e)
                {
                    Debug.Log("Move failed: " + e.Message);
                }
            }

            if ((Lister[j].Resource[i]) && (!File.Exists("Assets/Resources/" + Resources_path[i])))
            {


                try
                {
                    File.Move(ResourcesPath+"/"+ Resources_path[i], "Assets/Resources/" + Resources_path[i]);
					File.Move(ResourcesPath+"/" + Resources_path[i]+".meta", "Assets/Resources/" + Resources_path[i]+".meta");
                }
                catch (Exception e)
                {
                    Debug.Log("Move failed: " + e.Message);
                }

            }
        }
        
#endregion
#region Scene
EditorBuildSettingsScene[] or = EditorBuildSettings.scenes;
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            or[i] = new EditorBuildSettingsScene(EditorBuildSettings.scenes[i].path,Lister[j].Scenes[i]);

        }
        EditorBuildSettings.scenes = or; 

#endregion 

}
public void Reset()
{
    for (int i = 0; i <Streaming_path.Length; i++)
    {
          if (!Directory.Exists("Assets/StreamingAssets/" + Streaming_path[i]))
            {


                try
                {
                    File.Move(StreamingPath + Streaming_path[i] + ".meta", "Assets/StreamingAssets/" + Streaming_path[i] + ".meta");

                    File.Move(StreamingPath + Streaming_path[i], "Assets/StreamingAssets/" + Streaming_path[i]);
                    
                }
                catch (Exception e)
                {
                    Debug.Log("Move failed: " + e.Message);
                }

            }
    }
     for (int i = 0; i < Resources_path.Length; i++)
        {
	

             if (!File.Exists("Assets/Resources/" + Resources_path[i]))
             {


                try
                {
                    File.Move(ResourcesPath + "/" + Resources_path[i] + ".meta", "Assets/Resources/" + Resources_path[i] + ".meta");
                    File.Move(ResourcesPath+"/"+ Resources_path[i], "Assets/Resources/" + Resources_path[i]);
					
                }
                catch (Exception e)
                {
                    Debug.Log("Move failed: " + e.Message);
                }

            }
}
}

}
