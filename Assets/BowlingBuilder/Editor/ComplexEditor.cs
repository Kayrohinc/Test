using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;


public class ComplexEditor :EditorWindow
{
 Complex_Serializeable_Parametrs parametrs;
	Vector2 scrollPos = Vector2.zero;


	[MenuItem("Window/Bulder")]
public static 	void Init()
	{
		ComplexEditor window=(ComplexEditor)GetWindow(typeof(ComplexEditor));
	if (window.parametrs==null)
		{
			window.parametrs= new Complex_Serializeable_Parametrs();
		}
	window.parametrs.Refresh();

	}

	#region GUIParametrs
	public void OnGUI()
	{
	if (GUILayout.Button("Add Release"))
		{
		this.parametrs.Count_of_Release++;
	    this.parametrs.Lister.Add(new Complex_Serializeable_Parametrs.Release());
		this.parametrs.Save();
		}

	}

 
    #endregion


}
