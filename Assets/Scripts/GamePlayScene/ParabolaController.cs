using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
	public float speed = 1f;

	public GameObject parabolaRoot;
	public bool autoStart = true;
	public bool animation = true;
	internal bool nextParabola = false;
	protected float animationTime = float.MaxValue;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public class Parabola3D{
		public float Length{get; private set;}
		public Vector3 a;
		public Vector3 b;
		public Vector3 c;

	}

	public class Parabola2D{
		public float a{get; private set;}

		public float b{get; private set;}

		public float c{get; private set;}
	}
}
