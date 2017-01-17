using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {

	public float destroyTime;

	void Start ()
	{
		if (destroyTime != 0) {
			Invoke ("DestroyGameObject", destroyTime);
		} else
			return;
	}

	void DestroyGameObject() {
		Destroy (gameObject);
	}
}