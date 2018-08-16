using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class slide : MonoBehaviour {
    public GameObject slideObj;
    public GameObject slideObjClone;
    public GameObject AllSlideObj;
    public List<GameObject> AnimalsObj = new List<GameObject>();
    public float objWeight = 24.025f;

    private float limitLeftX;
    private float limitRightX;
    private int recordPositionCount = 0;//记录父级走了多少格
    private int recordPositionCountObj = 0;
    private int recordPositionCountObjClone = 0;
    // Use this for initialization
    void Awake()
    {
        //slideObjClone.transform.localPosition = new Vector3(-objWeight, 0, 0);
    }
    void Start() {
        //print(AnimalsObj.Count);
        foreach (GameObject obj in AnimalsObj)
        {
           
            Renderer[] rendererComponents = obj.GetComponentsInChildren<Renderer>();
            foreach (Renderer component in rendererComponents)
            {
                //print(component.material.name);
                if (component.material.name.Contains(obj.transform.GetChild(0).name))
                {
                    if (component.material.mainTexture == null)
                    {
                        obj.gameObject.SetActive(false);
                        //print("123213213");
                    }                
                }
            }

            if (obj.transform.childCount > 2)
            {
                //print(obj.name);
                if (obj.transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture == null)
                {
                    obj.transform.GetChild(2).gameObject.SetActive(false);
                }

            }
            if (obj.transform.childCount >= 3)
            {
                //print(obj.name);
                obj.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
    }

    void Init()
    {

        foreach (GameObject obj in AnimalsObj)
        {
            Renderer[] rendererComponents = obj.GetComponentsInChildren<Renderer>();
            foreach (Renderer component in rendererComponents)
            {
                if (obj.transform.childCount > 3)
                {
                    //print(component.material.name);
                    if (component.material.name.Contains(obj.transform.GetChild(0).name))
                    {
                        if (component.material.mainTexture == null)
                        {
                            obj.gameObject.SetActive(false);
                            /*if (!Directory.Exists(Application.dataPath + "/MapDepot" + "/" + obj.transform.GetChild(0).name))
                            {

                            }*/
                        }
                    }
                }
            }

            if (obj.transform.childCount > 2)
            {
                //print(obj.name);
                if (obj.transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture == null)
                {
                    obj.transform.GetChild(2).gameObject.SetActive(false);
                }

            }
            if (obj.transform.childCount > 3)
            {
                obj.transform.GetChild(3).gameObject.SetActive(false);
            }
        }

    }

    // Update is called once per frame
    void Update() {
        //move();
        // transposition();
    }

    private void transposition()
    {
        if (AllSlideObj.transform.localPosition.x >= objWeight / 2 + recordPositionCount * objWeight - 10)
        {
            if (recordPositionCount % 2 == 0)
            {
                slideObjClone.transform.localPosition = new Vector3(-objWeight - recordPositionCount * objWeight, 0, 0);
            }
            else
            {
                slideObj.transform.localPosition = new Vector3(-objWeight - recordPositionCount * objWeight, 0, 0);
            }
            recordPositionCount++;
        }
        if (AllSlideObj.transform.localPosition.x <= -objWeight / 2 + recordPositionCount * objWeight - 10)
        {

            if (recordPositionCount % 2 == 0)
            {
                slideObj.transform.localPosition = new Vector3((objWeight * 2) - recordPositionCount * objWeight, 0, 0);
            }
            else
            {
                slideObjClone.transform.localPosition = new Vector3((objWeight * 2) - recordPositionCount * objWeight, 0, 0);
            }
            recordPositionCount--;
        }
    }

    private void move()
    {
        AllSlideObj.transform.Translate(0.001f, 0, 0);
    }
  
}
