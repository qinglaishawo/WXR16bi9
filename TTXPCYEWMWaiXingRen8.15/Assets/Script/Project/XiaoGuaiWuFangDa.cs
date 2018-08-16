using UnityEngine;
using System.Collections;


public class XiaoGuaiWuFangDa : MonoBehaviour {
    //[HideInInspector]
    public bool isStartFangDa;
    private bool isFangDaFinish;
    //[HideInInspector]
    public bool isCanClick;
    public float FangDaSpeed;

	// Use this for initialization
	void Start () {
        FangDaSpeed = 0;

    }
	
	// Update is called once per frame
	void Update () {
        if (isStartFangDa)
        {
            if (FangDaSpeed <= 1)
            {
                FangDaSpeed = FangDaSpeed + 0.01f;
                this.transform.localScale = new Vector3(FangDaSpeed, FangDaSpeed, FangDaSpeed);
                //this.transform.Rotate(0,0, XuanZhuangSpeed*Time.deltaTime);               
            }
            else
            {
                ReStart();
                isCanClick = true;
                this.transform.GetChild(0).GetComponent<Animator>().enabled=true;
            }
        }
	}

    private void ReStart()
    {
        isFangDaFinish = false;
        isStartFangDa = false;
        FangDaSpeed = 0;
    }
}
