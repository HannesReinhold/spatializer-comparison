using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResonanceOcclusion : MonoBehaviour
{
    Transform cam;
    private FMOD.Studio.EventInstance instance;


    public FMODUnity.EventReference fmodEvent;


    [SerializeField]
    [Range(0, 10)]
    private float occlusion;

    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
        instance.release();
    }

    // Update is called once per frame
    void Update()
    {
        float currentOcclusion = 0;
        if (cam == null) cam = FindObjectOfType<Camera>().transform;

        RaycastHit hit;
        Vector3 dir = (cam.position - transform.position).normalized;
        if (Physics.Raycast(transform.position + dir * 1f, dir, out hit, Vector3.Distance(cam.position, transform.position) - 2))
        {
            currentOcclusion = 10;
        }

        occlusion = Mathf.Lerp(occlusion, currentOcclusion, Time.deltaTime);

        instance.setParameterByName("Occlusion", occlusion);
    }



}
