using UnityEngine;
using System.Collections;

public class ScrollingBackground : MonoBehaviour {

	public float speed;

	void Start () {

	}

	void Update () {
		Vector2 offset = new Vector2(0, Time.time * speed);
		GetComponent<Renderer>().material.mainTextureOffset = offset;       
	}
}﻿