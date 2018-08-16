using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AnimalMove : MonoBehaviour
{
    public List<GameObject> AnimalsObj = new List<GameObject>();
    public static Dictionary<string, float[]> AnimalStartEndPosition_dj = new Dictionary<string, float[]>();
    // Use this for initialization
    void Start()
    {
        AddAnimal();
        StartCoroutine(Init());
    }


    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Init()
    {
        yield return new WaitForSeconds(5f);
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
                    }
                    else
                    {

                        //播放动画,防止不动
                        if (!obj.transform.GetChild(0).GetComponent<Animation>().isPlaying)
                        {
                            if (obj.transform.GetChild(0).GetComponent<GetModelCompent>().JudgeAniListOrderIsLastOne())
                                obj.transform.GetChild(0).GetComponent<GetModelCompent>().PlayAniDefaultOrderImmediate();
                        }
                    }
                }
            }

            /*if (obj.transform.childCount > 2)
            {
                //print(obj.name);
                if (obj.transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture == null)
                {
                    obj.transform.GetChild(2).gameObject.SetActive(false);
                }
            }*/
            if (obj.transform.childCount >= 3)
            {
                //print(obj.name);                             
                if (obj.transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture == null)
                {
                    obj.transform.GetChild(3).gameObject.SetActive(false);
                }
            }
        }

    }

    void AddAnimal()
    {
        /*AnimalStartEndPosition_dj.Add("yingwuyu", new float[3] { 10f, -4f, 0.008f });
        AnimalStartEndPosition_dj.Add("citun", new float[3] { 10f, -4f, 0.006f });
        AnimalStartEndPosition_dj.Add("shayu", new float[3] { 10f, -4f, 0.017f });
        AnimalStartEndPosition_dj.Add("haima", new float[3] { 10f, -4f, 0.006f });
        AnimalStartEndPosition_dj.Add("pangxie", new float[3] { 10f, -4f, 0.003f });
        AnimalStartEndPosition_dj.Add("wugui", new float[3] { 10f, -4f, 0.004f });
        AnimalStartEndPosition_dj.Add("fancheyu", new float[3] { 10f, -4f, 0.0154f });
        AnimalStartEndPosition_dj.Add("haitun", new float[3] { 10f, -4f, 0.02f });
        AnimalStartEndPosition_dj.Add("ankangyu", new float[3] { 10f, -4f, 0.008f });
        AnimalStartEndPosition_dj.Add("jianyu", new float[3] { 10f, -4f, 0.011f });
        AnimalStartEndPosition_dj.Add("moxiangjing", new float[3] { 18f, -4f, 0.006f });
        AnimalStartEndPosition_dj.Add("shibanyu", new float[3] { 10f, -4f, 0.011f });
        AnimalStartEndPosition_dj.Add("people", new float[3] { 10, -4f, 0.005f });
        AnimalStartEndPosition_dj.Add("qianshuiting", new float[3] { 10, -4f, 0.012f });*/
        AnimalStartEndPosition_dj.Add("waixingren02", new float[3] { 17f, -17f, 0.011f });
        AnimalStartEndPosition_dj.Add("waixingren05", new float[3] { 17f, -17f, -0.017f });
    }
}
