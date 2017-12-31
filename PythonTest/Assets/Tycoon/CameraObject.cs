using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraObject : MonoBehaviour {

	private Action<CameraObject> onUpdateCall;

	public void SetUpdateFunction(Action<CameraObject> fnc) {
		onUpdateCall = fnc;
	}

	void Update () {
		if (onUpdateCall != null)
			onUpdateCall.Invoke (this);
	}

	public Vector3 GetPos() {
		return gameObject.transform.position;
	}

	public void SetPos(Vector3 vec) {
		gameObject.transform.position = vec;
	}
}
