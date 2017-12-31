using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Scripting.Hosting;
using System.IO;
using System;
using System.Reflection.Emit;
using System.Reflection;

public class Test : MonoBehaviour {

	private static object BindConstructor(Type type, Type[] parameters, Type delegateType) {
		ConstructorInfo ctor = type.GetConstructor (parameters);
		DynamicMethod dm = new DynamicMethod ("Constructor", type, parameters, type, true);
		ILGenerator il = dm.GetILGenerator ();
		for (int i = 0; i < parameters.Length; i++)
			il.Emit (OpCodes.Ldarg, i);
		il.Emit (OpCodes.Newobj, ctor);
		il.Emit (OpCodes.Ret);
		return dm.CreateDelegate (delegateType);
	}
	
	void Start () {
		// LOAD ASSEMBLY, NOT NEEDED
		//scriptEngine.Runtime.LoadAssembly(typeof(GameObject).Assembly);

		ScriptEngine scriptEngine = IronPython.Hosting.Python.CreateEngine();  
		ScriptScope scriptScope = scriptEngine.CreateScope();  


		scriptScope.SetVariable ("tycoon", new Tycoon());
		scriptScope.SetVariable ("Vector3", BindConstructor(typeof(Vector3), new[] {typeof(float), typeof(float), typeof(float)}, typeof(Func<float, float, float, Vector3>)));
		scriptScope.SetVariable ("log", new Action<object>(Debug.Log));

		List<string> paths = new List<string>();
		// to load python stdlib - not needed
		// paths.Add (Application.dataPath + "/StreamingAssets/pystdlib");
		scriptEngine.SetSearchPaths (paths);

		string scriptPath = Application.dataPath + "/StreamingAssets/MyScript.py";
		string script = File.ReadAllText (scriptPath);

		ScriptSource src = scriptEngine.CreateScriptSourceFromString (script);
		src.Execute (scriptScope);
		// var printme = scriptScope.GetVariable<Microsoft.Scripting.Utils.Func<object, object>>("printme");
		// printme.Invoke (gameObject);
	}

}
