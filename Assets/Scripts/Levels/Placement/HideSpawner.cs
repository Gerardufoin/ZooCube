using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HideSpawner : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GetComponent<SpriteRenderer>().enabled = false;
	}
}
