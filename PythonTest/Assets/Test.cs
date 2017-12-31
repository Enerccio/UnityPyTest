using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Scripting.Hosting;
using System.IO;

public class Test : MonoBehaviour {



	void Start () {
		ScriptEngine scriptEngine = IronPython.Hosting.Python.CreateEngine();  
		ScriptScope scriptScope = scriptEngine.CreateScope();  
		scriptEngine.Runtime.LoadAssembly(typeof(GameObject).Assembly);

		List<string> paths = new List<string>();
		paths.Add (Application.dataPath + "/StreamingAssets/pystdlib");
		scriptEngine.SetSearchPaths (paths);

		string scriptPath = Application.dataPath + "/StreamingAssets/MyScript.py";
		string script = File.ReadAllText (scriptPath);

		ScriptSource src = scriptEngine.CreateScriptSourceFromString (script);
		src.Execute (scriptScope);
		var printme = scriptScope.GetVariable<Microsoft.Scripting.Utils.Func<object, object>>("printme");
		printme.Invoke (gameObject);
		scriptScope.GetVariable<Microsoft.Scripting.Utils.Func<object, object>> ("deleteme").Invoke (gameObject);
	}

}
