using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tycoon {

	public CameraObject GetCamera() {
		return GameObject.Find ("Main Camera").GetComponent<CameraObject> ();
	}

}
