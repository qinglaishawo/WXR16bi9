using UnityEngine;
using System.Collections;

public class test1 : MonoBehaviour {
    public Texture2D sourceTex;
    public Rect sourceRect;
    public GameObject cube;
	// Use this for initialization
	void Start () {

        int x = Mathf.FloorToInt(sourceRect.x);
        int y = Mathf.FloorToInt(sourceRect.y);
        int width = Mathf.FloorToInt(sourceRect.width);
        int height = Mathf.FloorToInt(sourceRect.height);

        Color[] pix = sourceTex.GetPixels(x, y, width, height);
        Texture2D destTex = new Texture2D(width, height);
        destTex.SetPixels(pix);
        destTex.Apply();

        // Set the current object's texture to show the
        // extracted rectangle.
        cube.GetComponent<Renderer>().material.mainTexture = destTex;

    }

    // Update is called once per frame
    void Update () {
	
	}
}
