using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class slideScript : MonoBehaviour
{

    public slide slide;
    public List<GameObject> AnimalsObj = new List<GameObject>();
    public List<ParticleSystem> hdParticle = new List<ParticleSystem>();
    public Camera mainCamera;
    public GameObject ShakeCamera;

    public static Dictionary<string, float[]> AnimalStartEndPosition_dj = new Dictionary<string, float[]>();
    public static Dictionary<string, float[]> AnimalStartEndPosition_hd = new Dictionary<string, float[]>();
    public static Dictionary<string, float[]> AnimalStartEndPosition_fixation = new Dictionary<string, float[]>();

    private bool IsShake = false;
    private float timer = 0;
    private float hdtime = 0;
    private bool ishdtime = false;
    private float specificHdtime = 0;

    void Awake()
    {
        AnimalsObj=slide.AnimalsObj;
    }
    void Start()
    {
        //ShakeCamera = GameObject.Find("Main Camera");
        if (AnimalStartEndPosition_dj.Count == 0)
        {
            AnimalStartEndPosition_dj.Add("daxiang", new float[3] { 20f, -20f, 0.005f });
            AnimalStartEndPosition_dj.Add("dujiaoshou", new float[3] { 20f, -20f, -0.01f });
            AnimalStartEndPosition_dj.Add("laohu", new float[3] { 20f, -20f, 0.015f });
            AnimalStartEndPosition_dj.Add("yilong", new float[3] { 12.25f, -11.74f, -0.02f });
            AnimalStartEndPosition_dj.Add("jianlong", new float[3] { 12.25f, -11.74f, -0.002f });
            AnimalStartEndPosition_dj.Add("jielong", new float[3] { 13.38f, -10.61f, 0.002f });
            AnimalStartEndPosition_dj.Add("cimulong", new float[3] { 12.25f, -11.74f, 0.004f });
            AnimalStartEndPosition_dj.Add("waixingren05", new float[3] { 25f, -20f, -0.01f });
            //AnimalStartEndPosition_dj.Add("waixingren10", new float[3] { 12.87f,-13.58f,0.02f });
            AnimalStartEndPosition_dj.Add("DB", new float[3] { 25f, -20f, -0.02f });
            AnimalStartEndPosition_hd.Add("waixingren02", new float[4] { 25f, -20f, -0.02f, 2f });
        }

        //AnimalStartEndPosition_hd.Add("dujiaoshou", new float[6] { 34.655f, 10.63f, 11.024f, 19.78f, 29.39f, 2f });
        //AnimalStartEndPosition_fixation.Add("dujiaoshou", new float[4] { 26.77f, 18.79f, 11.024f, 1.8f });

    }

    void Update()
    {
        ClickEvent();
       /* if (ishdtime == true)
        {
            hdtime = hdtime + Time.deltaTime;
            if (hdtime >= specificHdtime)
            {
                IsShake = true;
                ishdtime = false;
                //Handheld.Vibrate();
                hdtime = 0;
            }
        }*/
        //shakeCamera();
    }

    private void ClickEvent()
    {
        //if TV
       /* int count = Input.touchCount;
        for (int i = 0; i < count; i++)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.GetTouch(i).position);
        }*/
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo))
            {
                GameObject gameObj = hitinfo.collider.gameObject;
                if (gameObj.GetComponent<AnimalIndividuality>() != null)
                {
                    if (gameObj.GetComponent<GetModelCompent>().JudgeAniListOrderIsLastOne())
                    {
                        gameObj.GetComponent<GetModelCompent>().PlayAniDefaultOrderImmediate();
                    }
                }
                gameObj = null;
            }
        }
        #region
        /*if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo))
            {
                GameObject gameObj = hitinfo.collider.gameObject;
                if (gameObj.GetComponent<AnimalIndividuality>() != null)
                {
                    //obj.name.Substring(0, obj.name.IndexOf("_"));
                    if (!gameObj.name.Contains("ND"))
                    {
                        if (gameObj.GetComponent<GetModelCompent>().JudgeAniListOrderIsLastOne())
                        {
                            gameObj.GetComponent<GetModelCompent>().PlayAniDefaultOrderImmediate();
                         
                        }
                        string[] tempText = gameObj.transform.parent.name.Split('_');

                        if (tempText.Length <= 2)
                        {
                            for (int i = 0; i < AnimalsObj.Count; i++)
                            {
                                if (AnimalsObj[i].name == gameObj.transform.parent.name + "_clone")
                                {
                                    if (AnimalsObj[i].transform.GetChild(0).GetComponent<GetModelCompent>().JudgeAniListOrderIsLastOne())
                                    {
                                        //Debug.Log("sss");
                                        AnimalsObj[i].transform.GetChild(0).GetComponent<GetModelCompent>().PlayAniDefaultOrderImmediate();
                                        //Debug.Log(AnimalsObj[i].transform.GetChild(0).GetComponent<GetModelCompent>().l_AniPlayOrder.Count);
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < AnimalsObj.Count; i++)
                            {
                                if (AnimalsObj[i].name + "_clone" == gameObj.transform.parent.name)
                                {
                                    if (AnimalsObj[i].transform.GetChild(0).GetComponent<GetModelCompent>().JudgeAniListOrderIsLastOne())
                                    {
                                        //Debug.Log("qqq");
                                        AnimalsObj[i].transform.GetChild(0).GetComponent<GetModelCompent>().PlayAniDefaultOrderImmediate();
                                    }
                                }
                            }
                        }
                    }


                    else
                    {
                        if (gameObj.GetComponent<GetModelCompent>().JudgeAniListOrderIsLastOne())
                        {
                            gameObj.GetComponent<GetModelCompent>().PlayAniDefaultOrderImmediate();
                        }
                    }

                }
                else if (gameObj.name.Contains("_hd"))
                {
                    foreach (ParticleSystem hd in hdParticle)
                    {
                        if (gameObj.name == hd.name)
                        {
                            if (!hd.isPlaying)
                                hd.Play();
                            continue;
                        }
                    }
                }
            }
        }*/
        #endregion
    }


    private void shakeCamera()
    {
        if (IsShake)
        {
            timer = timer + Time.deltaTime;
            if (timer < 0.3f)
            {
                float newPosition = Random.Range(-1, 1);

                ShakeCamera.transform.localPosition = new Vector3(newPosition * 0.1f, 1.36f, 14.8f);
            }

        }
        if (timer > 0.3f)
        {
            ShakeCamera.transform.localPosition = new Vector3(0.24f, 1.36f, 14.8f);
            IsShake = false;
            timer = 0;
        }
    }
}
