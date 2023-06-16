using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceHover : MonoBehaviour
{
    public Transform Center;

    public Vector3 frequencies;
    public Vector3 amplitudes;

    private Vector3 timeVector = Vector3.zero;



    // Start is called before the first frame update
    void Start()
    {
        timeVector = new Vector3(Random.Range(0,10), Random.Range(0, 10), Random.Range(0, 10));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Center.position + new Vector3(Mathf.Sin(timeVector.x)* amplitudes.x, Mathf.Sin(timeVector.y)* amplitudes.y, Mathf.Cos(timeVector.z)* amplitudes.z);
        timeVector.x += Time.deltaTime * frequencies.x;
        timeVector.y += Time.deltaTime * frequencies.y;
        timeVector.z += Time.deltaTime * frequencies.z;
    }
}
