using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class yunsenlin : MonoBehaviour {
   
	void Start () {
   
	}
	
	// Update is called once per frame
	void Update () {
        // AnimalMove_dj(gameObject, 0.326f, -23.9224f);
        AnimalMove_dj(gameObject, 0.326f, -0.3f);
    }

    private void AnimalMove_dj(GameObject obj, float startPostion, float endPostion)
    {     
        obj.transform.Translate(0.003f, 0, 0);
        if (obj.transform.localPosition.x > startPostion)
        {
            obj.transform.localPosition = new Vector3(endPostion, obj.transform.localPosition.y, obj.transform.localPosition.z);
        }
        if (obj.transform.localPosition.x < endPostion)
        {
            obj.transform.localPosition = new Vector3(startPostion, obj.transform.localPosition.y, obj.transform.localPosition.z);
        }
    }
}
