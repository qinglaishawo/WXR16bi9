using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
    private float moveSpeed;
    private float zuo;
    private float you;
    private bool way = false;
    // Use this for initialization
    void Start () {
        moveSpeed = 0.5f;
        zuo = 10f;
        you = -16f;
        way = false;
}
	
	// Update is called once per frame
	void Update () {
        CameraM();

    }
    private void CameraM()
    {

        if (this.transform.position.x <= zuo && way == false)
        {
            gameObject.transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        }
        if (gameObject.transform.position.x >= zuo)
        {
            way = true;
        }
        if (way == true && gameObject.transform.position.x >= you)
        {
            gameObject.transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        }
        if (gameObject.transform.position.x <= you)
        {
            way = false;
        }
    }
}
