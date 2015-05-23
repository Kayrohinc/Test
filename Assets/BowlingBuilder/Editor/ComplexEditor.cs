using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

public class ComplexEditor :EditorWindow
{
 Complex_Serializeable_Parametrs parametrs;
	Vector2 scrollPos = Vector2.zero;
    public string ReleaseName=" ";
    public bool Add;
    public bool Open_Builder;
    public int index_of_Platform;
     public int Index_of_Build;
		[MenuItem("Window/Bulder")]
public static 	void Init()
		{  
		ComplexEditor window=(ComplexEditor)GetWindow(typeof(ComplexEditor));
        window.parametrs = (Complex_Serializeable_Parametrs)AssetDatabase.LoadAssetAtPath("Assets/BowlingBuilder/Parameters.asset", typeof(Complex_Serializeable_Parametrs));
				if (window.parametrs == null) {
						window.parametrs = new Complex_Serializeable_Parametrs ();
				}
            if (!Directory.Exists(window.parametrs.ResourcesPath))
            {
                Directory.CreateDirectory(window.parametrs.ResourcesPath);
            }
            if (!Directory.Exists(window.parametrs.StreamingPath))
            {
                Directory.CreateDirectory(window.parametrs.StreamingPath);
            }
				if   (!Directory.Exists ("Assets/StreamingAssets/"))
				{
						Directory.CreateDirectory ("Assets/StreamingAssets/");
				}
				AssetDatabase.Refresh (); 
			
      
	window.parametrs.Refresh();
		
	}
	#region GUIParametrs
	public void OnGUI()
    {
      scrollPos=	GUILayout.BeginScrollView(scrollPos, GUILayout.Height(position.height));     
       GUILayout.Label("Paramets of Releases");
       GUILayout.Space(10);
        for (int j = 0; j <parametrs.Count_of_Release; j++)
        { 
        if (GUILayout.Button(parametrs.Lister[j].Release_Name, GUILayout.Width(200)))
            {
                parametrs.Lister[j].Open = !parametrs.Lister[j].Open;
            }
        if (this.parametrs.Lister[j].Open)
        {
            #region String
            GUILayout.BeginHorizontal();
            GUILayout.Label("Bundle_Version", GUILayout.MaxWidth(100));
            parametrs.Lister[j].Bindle_Version =GUILayout.TextField(parametrs.Lister[j].Bindle_Version, 20, GUILayout.MaxWidth(100));
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Product_Name", GUILayout.MaxWidth(100));
            parametrs.Lister[j].Product_Name= GUILayout.TextField(parametrs.Lister[j].Product_Name, 20, GUILayout.MaxWidth(100));
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Bundle_Name", GUILayout.MaxWidth(100));
            parametrs.Lister[j].Bindle_Name = GUILayout.TextField(parametrs.Lister[j].Bindle_Name, 20, GUILayout.MaxWidth(100));
			GUILayout.EndHorizontal();
			GUILayout.Space(10f);
			GUILayout.BeginHorizontal();
			GUILayout.Label("Define_Symbols", GUILayout.MaxWidth(100));
			parametrs.Lister[j].Define_Symbols = GUILayout.TextField(parametrs.Lister[j].Define_Symbols, 20, GUILayout.MaxWidth(100));
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
            #endregion
            #region Scene
            if (GUILayout.Button("+Scene", GUILayout.Width(150)))
            {
                parametrs.Lister[j].Scene_Open = !parametrs.Lister[j].Scene_Open;
            }
            if (parametrs.Lister[j].Scene_Open)
            {
                GUILayout.Label("Scenes");
                GUILayout.Space(10f);
                for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
                {

                    parametrs.Lister[j].Scenes[i] = EditorGUILayout.Toggle(EditorBuildSettings.scenes[i].path.Split('/')[EditorBuildSettings.scenes[i].path.Split('/').Length - 1], parametrs.Lister[j].Scenes[i]);

                }
            }
            #endregion
            #region Resouce
             if (GUILayout.Button("+Resources", GUILayout.Width(150)))
                {
                   parametrs.Lister[j].Resource_Open= !parametrs.Lister[j].Resource_Open;
                }
             if (parametrs.Lister[j].Resource_Open)
             {
                 GUILayout.Label("Resources");
                 GUILayout.Space(10f);

                 for (int i = 0; i < parametrs.Lister[j].Resource.Length; i++)
                 {

												parametrs.Lister[j].Resource[i] = EditorGUILayout.Toggle(parametrs.Resources_path[i]+"/Resouces", parametrs.Lister[j].Resource[i]);

                 }
             }
            #endregion
            #region Streaming
            if (GUILayout.Button("+StreamingAsset", GUILayout.Width(150)))
                {
                    parametrs.Lister[j].Streaming_Open = !parametrs.Lister[j].Streaming_Open;
                }
            if (parametrs.Lister[j].Streaming_Open)
            {
                GUILayout.Label("Streaming Assets");
                GUILayout.Space(10f);

                for (int i = 0; i <parametrs.Streaming_path.Length; i++)
                {

                    parametrs.Lister[j].Streaming[i] = EditorGUILayout.Toggle(parametrs.Streaming_path[i], parametrs.Lister[j].Streaming[i]);

                }
            }
#endregion        
            #region Icon
          if (GUILayout.Button("+Icon", GUILayout.Width(150)))
                {
                    parametrs.Lister[j].Icon=!parametrs.Lister[j].Icon;
                    parametrs.Lister[j].IOS_Open=false;
                    parametrs.Lister[j].Android_Open= false;
                }
                if (parametrs.Lister[j].Icon)
                {
                    GUILayout.BeginHorizontal();
        if  (GUILayout.Button("Android",GUILayout.Width(100)))
       {parametrs.Lister[j].Android_Open=true;
        parametrs.Lister[j].IOS_Open= false;
       }
        if (GUILayout.Button("Ios", GUILayout.Width(100)))
        {
           parametrs.Lister[j].Android_Open=false;
           parametrs.Lister[j].IOS_Open=true;
        }
        GUILayout.EndHorizontal();
        #region Android
        if (parametrs.Lister[j].Android_Open)
         {
        GUILayout.Space(10f);
        GUILayout.Label(" Android_Icons");
        GUILayout.Space(10f);
        for (int i = 0; i <parametrs.Lister[j].Android.Length; i++)
        { 
          parametrs.Lister[j].Android[i]= (Texture2D)EditorGUILayout.ObjectField((PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.Android)[i] + "x" + PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.Android)[i]).ToString(),parametrs.Lister[j].Android[i], typeof(Texture2D), GUILayout.MaxWidth(200), GUILayout.MaxHeight(200));
				if (parametrs.Lister[j].Android[i])
				{
					EditorGUI.DrawPreviewTexture(new Rect(225, GUILayoutUtility.GetLastRect().yMin, PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.Android)[i], PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.Android)[i]),parametrs.Lister[j].Android[i]);
					if ((PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.Android)[i]) > 64)
						GUILayout.Space(PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.Android)[i] - 64 + 5);
					
				}
        }
       }
      #endregion
        #region IOS
        if (parametrs.Lister[j].IOS_Open)
        {
            GUILayout.Space(10f);
            GUILayout.Label("IOS_Icons");
            GUILayout.Space(10f);
            for (int i = 0; i <parametrs.Lister[j].IOS.Length; i++)
            {
             parametrs.Lister[j].IOS[i] = (Texture2D)EditorGUILayout.ObjectField((PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.iPhone)[i] + "x" + PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.iPhone)[i]).ToString(), parametrs.Lister[j].IOS[i], typeof(Texture2D), GUILayout.MaxWidth(200), GUILayout.MaxHeight(200));
				if (parametrs.Lister[j].IOS[i])
				{
					EditorGUI.DrawPreviewTexture(new Rect(225, GUILayoutUtility.GetLastRect().yMin, PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.iPhone)[i], PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.iPhone)[i]),parametrs.Lister[j].IOS[i]);
					if ((PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.iPhone)[i]) > 64)
						GUILayout.Space(PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.iPhone)[i] - 64 + 5);
					
				}
            }
        }

        #endregion
  }
                GUILayout.Space(15f);
            #endregion
          
            if (GUILayout.Button("Save",GUILayout.MaxWidth(200)))
            {
            parametrs.Save();
            }
            if (GUILayout.Button("Delete",GUILayout.MaxWidth(200)))
            {
                    parametrs.Count_of_Release--;
                    parametrs.Lister.Remove(parametrs.Lister[j]);
                    this.Repaint();
                }
            GUILayout.Space(20f);
        } 
        
        }

        #region Builder
        GUILayout.Label("Custom Builder");
        GUILayout.Space(10f);
        if (GUILayout.Button("BuildRelease", GUILayout.Width(300)))
        {
         Open_Builder= !Open_Builder;
        }
        if (Open_Builder)
        {   Rect temp = GUILayoutUtility.GetRect(40, 40);
            temp.width = 250;
            index_of_Platform = EditorGUI.Popup(
              temp,
                 "Platform:",
                 index_of_Platform,
           parametrs.Lister.Select(x=>x.Release_Name).ToArray());
            Rect temp1 = GUILayoutUtility.GetRect(40, 20);
            temp1.width = 250;
             string[] target= new string[2]{"Android","IOS"};
            Index_of_Build = EditorGUI.Popup(temp1, "Release", Index_of_Build,target);
            if (GUILayout.Button("Start Build", GUILayout.Width(100)))
            {
                parametrs.Build(index_of_Platform);
                PlayerSettings.bundleVersion =parametrs.Lister[index_of_Platform].Bindle_Version;
                PlayerSettings.bundleIdentifier =parametrs.Lister[index_of_Platform].Bindle_Name;
				
                PlayerSettings.productName =parametrs.Lister[index_of_Platform].Product_Name;
              

                 Directory.CreateDirectory("Builds/" + DateTime.Now.Hour + "H" + DateTime.Now.Minute + "m" + "/");
                  string[] levels= EditorBuildSettings.scenes.Where(x=>(x.enabled==true)).Select(y=>y.path).ToArray();

                 if (Index_of_Build == 0)
                {
					PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android,parametrs.Lister[index_of_Platform].Define_Symbols);
                    PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Android, parametrs.Lister[index_of_Platform].Android);
					BuildPipeline.BuildPlayer(levels, "Builds/" + DateTime.Now.Hour + "H" + DateTime.Now.Minute + "m" + "/" +parametrs.Lister[index_of_Platform].Release_Name+ ".apk", BuildTarget.Android, BuildOptions.None);

                }
                  if (Index_of_Build == 1)
                  {
					PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iPhone,parametrs.Lister[index_of_Platform].Define_Symbols);
                      PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.iPhone, parametrs.Lister[index_of_Platform].IOS);
                      BuildPipeline.BuildPlayer(levels, "Builds/" + DateTime.Now.Hour + "H" + DateTime.Now.Minute + "m" + "/" + parametrs.Lister[index_of_Platform].Release_Name, BuildTarget.iPhone, BuildOptions.None);
                  }
                parametrs.Reset();

							
            }
            
        }
        #endregion


        #region AddBuilder
        if (Add==false)
        {
	if (GUILayout.Button("Add Release",GUILayout.MaxWidth(200)))
    { Add=!Add;
    this.parametrs.Lister.Add(new Complex_Serializeable_Parametrs.Release());   
    }
    }
       if (Add==true)
     {
		  GUILayout.BeginHorizontal();
	    GUILayout.Label( "Release Name",GUILayout.MaxWidth(100));
        ReleaseName = GUILayout.TextField(ReleaseName, GUILayout.MaxWidth(70));
       GUILayout.EndHorizontal();
           GUILayout.BeginHorizontal();
      if (GUILayout.Button("Save",GUILayout.MaxWidth(100)))
      {
          this.parametrs.Count_of_Release++;
          this.parametrs.Lister[this.parametrs.Lister.Count - 1].Release_Name = ReleaseName;
          this.parametrs.Refresh();
          this.parametrs.Save();
       
        Add = false;
      }
      if (GUILayout.Button("Cancel",GUILayout.MaxWidth(100)))
      {
          Add = false;
      }
      GUILayout.EndHorizontal();
        #endregion
     }
       GUILayout.EndScrollView();

	}

 
    #endregion
    private void OnDisable()
    {
        parametrs.Refresh();
    }

    private void OnEnable()
    {
        
        hideFlags = HideFlags.HideAndDontSave;
    }


  
}
