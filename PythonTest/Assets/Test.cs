using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Scripting.Hosting;

public class Test : MonoBehaviour {



	void Start () {
		ScriptEngine scriptEngine = IronPython.Hosting.Python.CreateEngine();  
		ScriptScope scriptScope = scriptEngine.CreateScope();  
		scriptEngine.Runtime.LoadAssembly(typeof(GameObject).Assembly);

		TextAsset ta = Resources.Load<TextAsset>("MyScript");
		ScriptSource src = scriptEngine.CreateScriptSourceFromString (ta.text);
		src.Execute (scriptScope);
		var printme = scriptScope.GetVariable<Microsoft.Scripting.Utils.Func<object, object>>("printme");
		printme.Invoke (gameObject);
		scriptScope.GetVariable<Microsoft.Scripting.Utils.Func<object, object>> ("deleteme").Invoke (gameObject);
	}

}
