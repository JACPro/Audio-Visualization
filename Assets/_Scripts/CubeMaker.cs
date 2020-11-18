using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMaker : MonoBehaviour
{
    [SerializeField] GameObject sampleCube;
    [SerializeField] GameObject sampleCylinder;
    GameObject[] cubes = new GameObject[512];
    [SerializeField] float maxScale;

    GameObject[] cylinders = new GameObject[8];
    [SerializeField] float startScale, scaleMultiplier;

    public bool useBuffer;



    
    void Start()
    {   /* For a ring of 512 cubes
        for (int i = 0; i < 512; i++) {
            GameObject sampleInstance = Instantiate(sampleCube);
            sampleInstance.transform.position = transform.position;
            sampleInstance.transform.parent = transform;
            sampleInstance.name = "Cube: " + i;
            this.transform.eulerAngles = new Vector3(0, -0.703125f * i, 0);
            sampleInstance.transform.position = Vector3.forward * 100f;
            cubes[i] = sampleInstance;
        }
        */

        /* For a ring of 8 cubes
        */
        for (int i = 0; i < 8; i++) {
            GameObject sampleInstance = Instantiate(sampleCylinder);
            sampleInstance.transform.position = transform.position;
            sampleInstance.transform.parent = transform;
            sampleInstance.name = "Cube: " + i;
            this.transform.eulerAngles = new Vector3(0, -45f * i, 0);
            sampleInstance.transform.position = Vector3.forward * 100f;
            cylinders[i] = sampleInstance;
        }
    }

    void Update()
    {
        /* For a ring of 512 cubes
        for (int i = 0; i < 512; i++) {
            cubes512[i].transform.localScale = new Vector3(10, Audio.samples[i] * maxScale + 2, 10);
        }
        */
        for (int i = 0; i < 8; i++)
        {
            if (useBuffer) {
                cylinders[i].transform.localScale = new Vector3(50, Audio.bandBuffer[i] * scaleMultiplier + startScale, 50);
            } else {
                cylinders[i].transform.localScale = new Vector3(50, Audio.bands[i] * scaleMultiplier + startScale, 50);
            }
        }
    }

    private void FixedUpdate() {
        /*
        Make the cubes spin
        */

        for (int i = 0; i < 8; i++) {
            cylinders[i].transform.RotateAround(Vector3.zero, Vector3.up, 30 * Time.deltaTime);
        }
    }
}

