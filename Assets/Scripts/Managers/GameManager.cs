using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject curtains;

    private Vector3 curtainsDest;

    private float startTimer = 0.0f;

	// Use this for initialization
	void Start ()
    {
        curtainsDest = curtains.transform.position;
        curtainsDest.y = 11.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (startTimer < 1.0f)
        {
            startTimer += Time.deltaTime;
            return ;
        }

	    if (curtains.transform.position != curtainsDest)
        {
            curtains.transform.position = Vector3.MoveTowards(curtains.transform.position, curtainsDest, 8.0f * Time.deltaTime);
        }
	}
}
