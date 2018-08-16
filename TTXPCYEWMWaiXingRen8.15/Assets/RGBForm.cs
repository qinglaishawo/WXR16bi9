using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RGBForm : MonoBehaviour {
    public List<Vector3> RGB = new List<Vector3>();

	// Use this for initialization
	void Start () {
        RGB.Add(new Vector3(0,0,0));
        RGB.Add(new Vector3(41,36,33));
        RGB.Add(new Vector3(192,192,192));
        RGB.Add(new Vector3(240,255,255));
        RGB.Add(new Vector3(255,255,0));
        RGB.Add(new Vector3(227,207,87));
        RGB.Add(new Vector3(255,153,18));
        RGB.Add(new Vector3(255,97,0));
        RGB.Add(new Vector3(176,224,230));
        RGB.Add(new Vector3(65,105,225));
        RGB.Add(new Vector3(56,94,15));
        RGB.Add(new Vector3(8,46,84));
        RGB.Add(new Vector3(0,255,0));
        RGB.Add(new Vector3(0,201,87));
        RGB.Add(new Vector3(128,42,42));
        RGB.Add(new Vector3(210,105,30));
        RGB.Add(new Vector3(255,0,0));
        RGB.Add(new Vector3(255,127,80));
        RGB.Add(new Vector3(160,32,240));
        RGB.Add(new Vector3(218,112,214));
        RGB.Add(new Vector3(135,38,87));
        RGB.Add(new Vector3(0,0,255));
        RGB.Add(new Vector3(25,25,112));
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
