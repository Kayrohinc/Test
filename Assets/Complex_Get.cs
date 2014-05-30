using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;
using com.pygmymonkey.tools;
public class Complex_Get : EditorWindow {
    #region GlobalVar
    Complex_Serializeable_Parametrs parametrs;
    public bool[] Open = new bool[0];

	private  int Index_of_Build;
    Vector2 scrollPos = Vector2.zero;
    public int count;
   #endregion
    #region StreamingAsset
    public const string AssetPath="Assets_Dont_Use";

    public void StreamingAsset_GUI(int j)
    {
        GUILayout.Label("Streaming Assets");
        GUILayout.Space(10f);
      
                for (int i = 0; i < StreamingAsset_path.Length; i++)
                {

                Complex_Parametrs.StreamingAsset_Toggle[j, i] = EditorGUILayout.Toggle(StreamingAsset_path[i],Complex_Parametrs.StreamingAsset_Toggle[j, i]);

                }

        
    }
    public static void StreamingAsset_Back()
    {

        for (int i = 0; i < StreamingAsset_path.Length; i++)
            if (!File.Exists("Assets/StreamingAssets/" + StreamingAsset_path[i]))
            {


                try
                {
                    File.Move(AssetPath+"/" + StreamingAsset_path[i], "Assets/StreamingAssets/" + StreamingAsset_path[i]);
				    File.Move(AssetPath+"/" + StreamingAsset_path[i], "Assets/StreamingAssets/" + StreamingAsset_path[i]+".meta");
			}
                catch (Exception e)
                {
                    Debug.Log("Move failed: " + e.Message);
                }

            }

        AssetDatabase.Refresh();
    }
    public static void StreamingAsset_Save(int j)
    {

        for (int i = 0; i < StreamingAsset_path.Length; i++)
        {
            if ((!Complex_Parametrs.StreamingAsset_Toggle[j, i]) && (!File.Exists(AssetPath + StreamingAsset_path[i])))
            {
                try
                {
					File.Move("Assets/StreamingAssets/" + StreamingAsset_path[i], AssetPath+"/" + StreamingAsset_path[i]);
					File.Move("Assets/StreamingAssets/" + StreamingAsset_path[i], AssetPath+"/" + StreamingAsset_path[i]+".meta");

                }
                catch (Exception e)
                {
                    Debug.Log("Move failed: " + e.Message);
                }
            }

            if ((Complex_Parametrs.StreamingAsset_Toggle[j, i]) && (!Directory.Exists("Assets/StreamingAssets/" + StreamingAsset_path[i].Split('/')[StreamingAsset_path[i].Split('/').Length - 1])))
            {


                try
                {
                    Directory.Move(AssetPath + StreamingAsset_path[i].Split('/')[StreamingAsset_path[i].Split('/').Length - 1], "Assets/StreamingAssets/" + StreamingAsset_path[i].Split('/')[StreamingAsset_path[i].Split('/').Length - 1]);
                }
                catch (Exception e)
                {
                    Debug.Log("Move failed: " + e.Message);
                }

            }
        }
        AssetDatabase.Refresh();

    }
   
   
    public static string[] StreamingAsset_path = new string[1];
    public bool[] StreamingAssets_Open;
    #endregion
    /// <summary>
   /// Streaming Asset pack 
    ///   The output folder is "Assets_Don't_Use" directed in the project folder
   ///  
   /// </summary>
    #region Resources
    public const string ResourcesPath="Resources_Don't_Use";
    public bool[] Resources_Open;
    public static string[] Resources_path = new string[1];
  
    
    public static void Resouces_Save(int j)
    {

        for (int i = 0; i < Resources_path.Length; i++)
        {
			if ((!Complex_Parametrs.Resources_Toggle[j, i]) && (!File.Exists(ResourcesPath+Resources_path[i])))
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

            if ((Complex_Parametrs.Resources_Toggle[j, i]) && (!File.Exists("Assets/Resources/" + Resources_path[i])))
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
        AssetDatabase.Refresh();

    }
    public static void Resouces_Back()
    {
        for (int i = 0; i < Resources_path.Length; i++)
            if (!File.Exists("Assets/Resources/" + Resources_path[i]))
            {


                try
                {
                    File.Move(ResourcesPath+"/" + Resources_path[i], "Assets/Resources/" + Resources_path[i]);
				    File.Move(ResourcesPath+"/"+Resources_path[i]+".meta", "Assets/Resources/" + Resources_path[i]+".meta");

                }
                catch (Exception e)
                {
                    Debug.Log("Move failed: " + e.Message);
                }

            }
        AssetDatabase.Refresh();
    }
    public void Resources_GUI(int j)
    {
       

        GUILayout.Label("Resources");
        GUILayout.Space(10f);
      
                for (int i = 0; i < Resources_path.Length; i++)
                {

                   Complex_Parametrs.Resources_Toggle[j, i] = EditorGUILayout.Toggle(Resources_path[i],Complex_Parametrs.Resources_Toggle[j, i]);

                }

        }
    
    #endregion
    /// <summary>
    /// / Resources Asset pack
    ///  The output folder is 
    /// </summary>
    #region Scene
	public static bool[,] Scene_Toogle;
	public void Scene_GUI(int j)
	{
		GUILayout.Label("Scenes");
		GUILayout.Space(10f);
		for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
		{
			
			Complex_Parametrs.Scene_Toggle[j, i] = EditorGUILayout.Toggle(EditorBuildSettings.scenes[i].path.Split('/')[EditorBuildSettings.scenes[i].path.Split('/').Length - 1],Complex_Parametrs.Scene_Toggle[j, i]);
			
		}
		
	}
	
	
	public static void Scene_Save(int j)
	{
		EditorBuildSettingsScene[] or = EditorBuildSettings.scenes;
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            or[i] = new EditorBuildSettingsScene(EditorBuildSettings.scenes[i].path,Complex_Parametrs.Scene_Toggle[j, i]);

        }
        EditorBuildSettings.scenes = or;

    }
    public bool[] Scene_Open;
    
    #endregion
    /// <summary>
    ///  Change scene count
    /// </summary>
    #region  Icons
    public bool[] Icon_Open;
    public  static int Icon_Count_Android;
    public static int Icon_Count_IOS;
  
   
    private bool[] Android;
    private bool[] IOS;

    public void Icon_GUI( int j)
    {
        GUILayout.BeginHorizontal();
        if  (GUILayout.Button("Android",GUILayout.Width(100)))
       {Android[j]=true;
        IOS[j]= false;
       }
        if (GUILayout.Button("Ios", GUILayout.Width(100)))
        {
            Android[j] = false;
            IOS[j] = true;
        }
        GUILayout.EndHorizontal();
        #region Android
        if (Android[j])
         {
        GUILayout.Space(10f);
        GUILayout.Label(" Android_Icons");
        GUILayout.Space(10f);
        for (int i = 0; i<Icon_Count_Android; i++)
        { 
          Complex_Parametrs.Icon_List_Android[j, i] = (Texture2D)EditorGUILayout.ObjectField((PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.Android)[i] + "x" + PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.Android)[i]).ToString(),Complex_Parametrs.Icon_List_Android[j,i], typeof(Texture2D), GUILayout.MaxWidth(200), GUILayout.MaxHeight(200));
				if (Complex_Parametrs.Icon_List_Android[j, i])
				{
					EditorGUI.DrawPreviewTexture(new Rect(225, GUILayoutUtility.GetLastRect().yMin, PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.Android)[i], PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.Android)[i]),Complex_Parametrs.Icon_List_Android[j, i]);
					if ((PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.Android)[i]) > 64)
						GUILayout.Space(PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.Android)[i] - 64 + 5);
					
				}
        }
       }
      #endregion
        #region IOS
        if (IOS[j])
        {
            GUILayout.Space(10f);
            GUILayout.Label("IOS_Icons");
            GUILayout.Space(10f);
            for (int i = 0; i < Icon_Count_IOS; i++)
            {
             Complex_Parametrs.Icon_List_IOS[j, i] = (Texture2D)EditorGUILayout.ObjectField((PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.iPhone)[i] + "x" + PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.iPhone)[i]).ToString(), Complex_Parametrs.Icon_List_IOS[j, i], typeof(Texture2D), GUILayout.MaxWidth(200), GUILayout.MaxHeight(200));
				if (Complex_Parametrs.Icon_List_IOS[j, i])
				{
					EditorGUI.DrawPreviewTexture(new Rect(225, GUILayoutUtility.GetLastRect().yMin, PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.iPhone)[i], PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.iPhone)[i]),Complex_Parametrs.Icon_List_IOS[j, i]);
					if ((PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.iPhone)[i]) > 64)
						GUILayout.Space(PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.iPhone)[i] - 64 + 5);
					
				}
            }
        }

        #endregion
    }

    public static void Icon_Save(int j, int Type)
    {
         if (Type==0){
        Texture2D[] Temp = new Texture2D[Icon_Count_Android];
        for (int i = 0; i < Icon_Count_Android; i++)
            Temp[i] = Complex_Parametrs.Icon_List_Android[j, i];
        PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Android, Temp);
         }
         if (Type == 1)
         {
             Texture2D[] Temp = new Texture2D[Icon_Count_IOS];
             for (int i = 0; i < Icon_Count_IOS; i++)
                 Temp[i] =Complex_Parametrs.Icon_List_IOS[j, i];
             PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.iPhone, Temp);
         
         }

    }

    #endregion
    /// <summary>
    /// Icons Android
    /// </summary>
    #region Splash

    public void Splash_Icon_GUI( int j)
    {
       }
    
    #endregion
    public bool Open_Builder;
    public int index_of_Platform;
    #region Function_Editor
    public void OnGUI()
    {
        
	scrollPos=	GUILayout.BeginScrollView(scrollPos, GUILayout.Height(position.height));
               
        #region Parametrs
        GUILayout.Label("Paramets of Releases");
        GUILayout.Space(10);
        for (int j = 0; j < count; j++)
        {

            if (GUILayout.Button(Enum.GetNames(typeof(AppParameters.ReleaseType))[j], GUILayout.Width(200)))
            {
                Open[j] = !Open[j];
            }
               
            if (Open[j])
			
			{ 
				GUILayout.BeginHorizontal();
				GUILayout.Label("Bundle_Version", GUILayout.MaxWidth(100));
				Complex_Parametrs.Bundle_Version[j]=GUILayout.TextField(Complex_Parametrs.Bundle_Version[j],20, GUILayout.MaxWidth(100));
				GUILayout.EndHorizontal();
				GUILayout.Space(20f);
                #region Scene
            if (GUILayout.Button("+Scene", GUILayout.Width(150)))
                {
                    Scene_Open[j] = !Scene_Open[j];
                }
                if (Scene_Open[j]) 
                {
                    Scene_GUI(j);
                }
                ////
                ///
                ///
            #endregion
                #region Resources
                GUILayout.Space(10f);
                if (GUILayout.Button("+Resources", GUILayout.Width(150)))
                {
                   Resources_Open[j] = !Resources_Open[j];
                }
                if (Resources_Open[j])
                {
                    Resources_GUI(j);
                }
                GUILayout.Space(10f);
                ///
                ///
                ///
                ///
                #endregion
                #region Stream
                if (GUILayout.Button("+StreamingAsset", GUILayout.Width(150)))
                {
                    StreamingAssets_Open[j]=!StreamingAssets_Open[j];
                }
                if (StreamingAssets_Open[j])
                {
                    StreamingAsset_GUI(j);
                }
                ///
                ///
                ///
                ///
                ///
                GUILayout.Space(10f);
                #endregion
                #region Icon
                if (GUILayout.Button("+Icon", GUILayout.Width(150)))
                {
                    Icon_Open[j] = !Icon_Open[j];
                    Android[j]= false;
                    IOS[j] = false;
                }
                if (Icon_Open[j])
                {
                    Icon_GUI(j);
                }
                GUILayout.Space(15f);
                #endregion 
            }
            GUILayout.Space(5f);
        }
        #endregion
        #region Builder
        GUILayout.Label("Custom Builder");
        GUILayout.Space(10f);
        if (GUILayout.Button("Build First Release",GUILayout.Width(300)))
        {
            Open_Builder = !Open_Builder;
        }
        if (Open_Builder)
        {   Rect temp= GUILayoutUtility.GetRect(40,40);
            temp.width=250;
           index_of_Platform  = EditorGUI.Popup(
             temp,
                "Platform:",
                index_of_Platform,
                Enum.GetNames(typeof(AppParameters.PlatformType)));
			Rect temp1=GUILayoutUtility.GetRect(40,40);
			temp1.width=250;
			Index_of_Build= EditorGUI.Popup(temp1,"Release",Index_of_Build,Enum.GetNames(typeof(AppParameters.ReleaseType)));
            if (GUILayout.Button("Start Build", GUILayout.Width(100)))
            {
                Scene_Save(Index_of_Build);
                Resouces_Save(Index_of_Build);
                StreamingAsset_Save(Index_of_Build);
                Icon_Save(Index_of_Build, index_of_Platform);
                List<string> Levels= new List<string>();
				ReleaseType m_releaseType;
				PlayerSettings.bundleVersion=Complex_Parametrs.Bundle_Version[Index_of_Build];
				PlayerSettings.bundleIdentifier = m_releaseType.bundleIdentifier;

				PlayerSettings.productName = m_releaseType.productName;
                foreach (EditorBuildSettingsScene temps in EditorBuildSettings.scenes)
                {
                 if (temps.enabled)
                 {
                     Levels.Add(temps.path);
                 }
                }
                Directory.CreateDirectory("Builds/" + DateTime.Now.Hour + "H" + DateTime.Now.Minute + "m" + "/");
                if (index_of_Platform==0){

					BuildPipeline.BuildPlayer(Levels.ToArray(), "Builds/" + DateTime.Now.Hour + "H" + DateTime.Now.Minute + "m" + "/" + Enum.GetNames(typeof(AppParameters.ReleaseType))[0] + ".apk", BuildTarget.Android, BuildOptions.None);
				    
				}
                 if (index_of_Platform==1)
                     BuildPipeline.BuildPlayer(Levels.ToArray(), "Builds/"+DateTime.Now.Hour+"H"+DateTime.Now.Minute+"m"+"/" + Enum.GetNames(typeof(AppParameters.ReleaseType))[0], BuildTarget.iPhone, BuildOptions.None);
			
				Complex_Get.Resouces_Back();
				Complex_Get.StreamingAsset_Back();
                 EditorWindow.GetWindow<AdvancedBuilderWindow>().Close();
               

            }

        }
        #endregion

        if (GUILayout.Button("Save", GUILayout.Width(80)))
        {
            Save();
        }
		GUILayout.EndScrollView();
}
     
    public   void Init_in_Other_Window(Complex_Get window)
    { 
         if (!Directory.Exists(AssetPath))
         {
             Directory.CreateDirectory(AssetPath);
         }
         if (!Directory.Exists(ResourcesPath))
         {
             Directory.CreateDirectory(ResourcesPath);
         }
        window.count = Enum.GetNames(typeof(AppParameters.ReleaseType)).Length;

        #region Saver
        Complex_Parametrs.Scene_Toggle = new bool[0, 0];
        Complex_Parametrs.Resources_Toggle = new bool[0, 0];
        Complex_Parametrs.StreamingAsset_Toggle = new bool[0, 0];
		window.Android= new bool[count];
		window.IOS= new bool[count];
		Complex_Get.Icon_Count_Android=PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.Android).Length;
		Complex_Get.Icon_Count_IOS=PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.iPhone).Length;

        window.parametrs = (Complex_Serializeable_Parametrs)AssetDatabase.LoadAssetAtPath("Assets/BowlingBuilder/Parameters.asset", typeof(Complex_Serializeable_Parametrs));

        if (window.parametrs == null)
        {
          window.parametrs = ScriptableObject.CreateInstance<Complex_Serializeable_Parametrs>();
			Complex_Parametrs.Bundle_Version= new string[window.count];
          Debug.Log("null");

			Complex_Parametrs.Icon_List_Android= new Texture2D[count,PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.Android).Length];
			Complex_Parametrs.Icon_List_IOS=  new Texture2D[count,PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.iPhone).Length];
			Complex_Get.Icon_Count_Android=PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.Android).Length;
			Complex_Get.Icon_Count_IOS=PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.iPhone).Length;
			for (int i=0;i<Complex_Parametrs.Bundle_Version.Length;i++)
			{

				Complex_Parametrs.Bundle_Version[i]= " ";
			}
		}

        else
        {
            int temp = this.parametrs.List.Length;
            Complex_Parametrs.Icon_List_IOS = new Texture2D[temp, this.parametrs.List[0].Icon_List_IOS.Length]; 
            Complex_Parametrs.Scene_Toggle = new bool[temp, this.parametrs.List[0].Scene_Toggle.Length];
            Complex_Parametrs.Icon_List_Android = new Texture2D[temp,this.parametrs.List[0].Icon_List_Android.Length];
            Complex_Parametrs.Resources_Toggle = new bool[temp, this.parametrs.List[0].Resources_Toggle.Length];
            Complex_Parametrs.StreamingAsset_Toggle = new bool[temp,this.parametrs.List[0].StreamingAsset_Toggle.Length];
	Complex_Parametrs.Bundle_Version	= new string[temp];

            for (int i = 0; i <temp; i++)
			{   Complex_Parametrs.Bundle_Version[i]=this.parametrs.List[i].Bindle_Version;
                for (int j = 0; j < this.parametrs.List[0].Resources_Toggle.Length; j++)
                {
                    Complex_Parametrs.Resources_Toggle[i, j] = this.parametrs.List[i].Resources_Toggle[j];
                }
                for (int j = 0; j < this.parametrs.List[0].Icon_List_Android.Length; j++)
                {
                    Complex_Parametrs.Icon_List_Android[i, j] = this.parametrs.List[i].Icon_List_Android[j];

                }
                for (int j = 0; j < this.parametrs.List[0].StreamingAsset_Toggle.Length; j++)
                {
                    Complex_Parametrs.StreamingAsset_Toggle[i, j] = this.parametrs.List[i].StreamingAsset_Toggle[j];
                }
                for (int j = 0; j < this.parametrs.List[0].Icon_List_IOS.Length; j++)
                {
                    Complex_Parametrs.Icon_List_IOS[i, j] = this.parametrs.List[i].Icon_List_IOS[j];
                }
                for (int j = 0; j < this.parametrs.List[0].Scene_Toggle.Length; j++)
                {
                        Complex_Parametrs.Scene_Toggle[i, j] = this.parametrs.List[i].Scene_Toggle[j];

                }

            }

   



        }
        #endregion
        /// Fuck my brain///
        #region Scene
        // Scene 
        bool[,] SceneTemp=new bool[Enum.GetNames(typeof(AppParameters.ReleaseType)).Length, EditorBuildSettings.scenes.Length];
        if ((Complex_Parametrs.Scene_Toggle.GetLength(0) != SceneTemp.GetLength(0)) || Complex_Parametrs.Scene_Toggle.GetLength(1) != SceneTemp.GetLength(1))
        {
            Complex_Parametrs.Scene_Toggle = SceneTemp;
        }
        window.Scene_Open = new bool[window.count];
        #endregion 
        #region Resources
        // Resources open

		Resources_path = new string[Directory.GetFiles("Assets/Resources/").Where(x=>!x.EndsWith(".meta")).ToArray().Length + Directory.GetFiles(ResourcesPath).Length];
		Directory.GetFiles("Assets/Resources/").Where(x=>!x.EndsWith(".meta")).ToArray().CopyTo(Resources_path, 0);
        window.count =Enum.GetNames(typeof(AppParameters.ReleaseType)).Length;
		Directory.GetFiles(ResourcesPath).CopyTo(Resources_path, Directory.GetFiles("Assets/Resources/").Where(x=>!x.EndsWith(".meta")).ToArray().Length);
		for (int i=0;i<Resources_path.Length;i++) 
		{
			Resources_path[i]=Resources_path[i].Split('/')[Resources_path[i].Split('/').Length - 1];

		}


		var ResourcesTemp= new bool[Enum.GetNames(typeof(AppParameters.ReleaseType)).Length,Resources_path.Length];
       if (Complex_Parametrs.Resources_Toggle.GetLength(0) != ResourcesTemp.GetLength(0) || Complex_Parametrs.Resources_Toggle.GetLength(1) != ResourcesTemp.GetLength(1))
       {
           Complex_Parametrs.Resources_Toggle = ResourcesTemp;
       }
        window.Open = new bool[Enum.GetNames(typeof(AppParameters.ReleaseType)).Length];
        window.Resources_Open = new bool[window.count];
        //Resources close
        #endregion 
        #region Streaming Assets
        // Streaming Assets open
		StreamingAsset_path = new string[Directory.GetFiles("Assets/StreamingAssets/").Where(x=>!x.EndsWith(".meta")).ToArray().Length + Directory.GetFiles(AssetPath).Length];
		Directory.GetFiles("Assets/StreamingAssets/").Where(x=>!x.EndsWith(".meta")).ToArray().CopyTo(StreamingAsset_path, 0);
		Directory.GetFiles(AssetPath).CopyTo(StreamingAsset_path, Directory.GetFiles("Assets/StreamingAssets/").Where(x=>!x.EndsWith(".meta")).ToArray().Length);
		for (int i=0;i<StreamingAsset_path.Length;i++) 
		{
			StreamingAsset_path[i]=StreamingAsset_path[i].Split('/')[StreamingAsset_path[i].Split('/').Length - 1];
		}

		
		var StreamingTemp  = new bool[Enum.GetNames(typeof(AppParameters.ReleaseType)).Length,StreamingAsset_path.Length];
       if (Complex_Parametrs.StreamingAsset_Toggle.GetLength(0) != StreamingTemp.GetLength(0) || Complex_Parametrs.StreamingAsset_Toggle.GetLength(1) != StreamingTemp.GetLength(1))
       {
           Complex_Parametrs.StreamingAsset_Toggle = StreamingTemp;
       }
        
        window.StreamingAssets_Open= new bool[window.count];
        // Streaming Assets Close
        #endregion
        #region Icons
        // Icons
        window.Icon_Open = new bool[window.count];
         
		  


	   #endregion 
		#region Bundlie 


			

        #endregion
        
		window.Show();
    }
    
    private void OnEnable()
    {
        hideFlags = HideFlags.HideAndDontSave;
    }

    public void Save()
    {
        this.parametrs.List = new Complex_Serializeable_Parametrs.Release[this.count];

        for (int i = 0; i < this.count; i++)
        {  

            this.parametrs.List[i] = new Complex_Serializeable_Parametrs.Release();
            this.parametrs.List[i].Scene_Toggle = new bool[Complex_Parametrs.Scene_Toggle.GetLength(1)];
			this.parametrs.List[i].Bindle_Version=" ";
			this.parametrs.List[i].Bindle_Version=Complex_Parametrs.Bundle_Version[i];
			for (int j = 0; j < Complex_Parametrs.Scene_Toggle.GetLength(1); j++)
            {
                this.parametrs.List[i].Scene_Toggle[j] = Complex_Parametrs.Scene_Toggle[i, j];
            }
            this.parametrs.List[i].Resources_Toggle = new bool[Complex_Parametrs.Resources_Toggle.GetLength(1)];
            for (int j = 0; j < Complex_Parametrs.Resources_Toggle.GetLength(1); j++)
            {
                this.parametrs.List[i].Resources_Toggle[j] = Complex_Parametrs.Resources_Toggle[i, j];
            }

            this.parametrs.List[i].StreamingAsset_Toggle = new bool[Complex_Parametrs.StreamingAsset_Toggle.GetLength(1)];
            for (int j = 0; j < Complex_Parametrs.StreamingAsset_Toggle.GetLength(1); j++)
            {
                this.parametrs.List[i].StreamingAsset_Toggle[j] = Complex_Parametrs.StreamingAsset_Toggle[i, j];
            }

            this.parametrs.List[i].Icon_List_Android = new Texture2D[Complex_Parametrs.Icon_List_Android.GetLength(1)];
            for (int j = 0; j < Complex_Parametrs.Icon_List_Android.GetLength(1); j++)
            {
                this.parametrs.List[i].Icon_List_Android[j] = Complex_Parametrs.Icon_List_Android[i, j];
            }
            this.parametrs.List[i].Icon_List_IOS = new Texture2D[Complex_Parametrs.Icon_List_IOS.GetLength(1)];
            for (int j = 0; j < Complex_Parametrs.Icon_List_IOS.GetLength(1); j++)
            {
                this.parametrs.List[i].Icon_List_IOS[j] = Complex_Parametrs.Icon_List_IOS[i, j];
            }


        }

        if (!AssetDatabase.Contains(parametrs))
            AssetDatabase.CreateAsset(parametrs, "Assets/BowlingBuilder/Parameters.asset");
        else
        {
            EditorUtility.SetDirty(parametrs);
          
            EditorApplication.SaveAssets();
            AssetDatabase.SaveAssets();
           
        }

    }
    #endregion


}

public class Complex_Parametrs : ScriptableObject
{  public static string[] Bundle_Version= new string[0];
    public static bool[,] Scene_Toggle= new bool[0,0];
    public static bool[,] Resources_Toggle= new bool[0,0];
    public static bool[,] StreamingAsset_Toggle= new bool[0,0];
    public static Texture2D[,] Icon_List_Android;
    public static Texture2D[,] Icon_List_IOS;
}

