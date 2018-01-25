/*
	Fractal Function written by Christopher Rollins
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour {
	//shape set for Fractal
	public Mesh mesh;
	//Material for all Fractals
	public Material material;
	//number of iterations
	[Range(0,10)]
	public int maxIterations;
	//size of Child objects
	[Range(0,1)]
	public float childScale;

	private int depth;

	void Start (){
		//Set components Mesh and Materia
		gameObject.AddComponent<MeshFilter> ().mesh = mesh;
		gameObject.AddComponent<MeshRenderer> ().material = material;
		if (depth < maxIterations) {
			StartCoroutine (CreateChildren ());	
		}
	}
	//set array of Vectors for directions the children can spawn
	private static Vector3[] childDirections = {
		Vector3.up,
		Vector3.right,
		Vector3.left,
		Vector3.forward,
		Vector3.back
	};
	//rotations of child objects to make then face properly
	private static Quaternion[] childOrientations = {
		Quaternion.identity,
		Quaternion.Euler (0f, 0f, -90f),
		Quaternion.Euler (0f, 0f, 90f),
		Quaternion.Euler (90f, 0f, 0f),
		Quaternion.Euler (-90f, 0f, 0f),
	};

	//creates gameobject child
	private IEnumerator CreateChildren(){
		for (int i = 0; i < childDirections.Length; i++) {
			yield return new WaitForSeconds (0.5f);
			new GameObject ("Fractal Child").AddComponent<Fractal> ().Initialize(this, i);
		}

	}

	//overall function that creates child object with unique statistics
	private void Initialize (Fractal parent, int childIndex){
		mesh = parent.mesh;
		material = parent.material;
		maxIterations = parent.maxIterations;
		depth = parent.depth + 1;
		childScale = parent.childScale;
		transform.parent = parent.transform;
		transform.localScale = Vector3.one * childScale;
		transform.localPosition = childDirections[childIndex] * (0.5f + 0.5f * childScale);
		transform.localRotation = childOrientations[childIndex];
	}
}
