using UnityEngine;
using System.Collections;

public class AnimalIndividuality : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        if (!gameObject.GetComponent<Animation>().isPlaying && gameObject.transform.parent.gameObject.activeInHierarchy)
        {
            if (this.gameObject.GetComponent<GetModelCompent>().JudgeAniListOrderIsLastOne())
                this.gameObject.GetComponent<GetModelCompent>().PlayAniDefaultOrderImmediate();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (this.gameObject.GetComponent<Animation>().IsPlaying("dj_0") && AnimalMove.AnimalStartEndPosition_dj.ContainsKey(name))
            AnimalMove_dj(gameObject, AnimalMove.AnimalStartEndPosition_dj[name][0], AnimalMove.AnimalStartEndPosition_dj[name][1], AnimalMove.AnimalStartEndPosition_dj[name][2]);
        if (this.gameObject.name.Equals("DB") && this.gameObject.GetComponent<Animation>().IsPlaying("dj_0"))
        {
            transform.parent.Translate(-0.02f, 0, 0);
            if (transform.parent.localPosition.x > 17)
            {
                transform.parent.localPosition = new Vector3(-17, transform.parent.localPosition.y, transform.parent.localPosition.z);
            }
            if (transform.parent.localPosition.x < -17)
            {
                transform.parent.localPosition = new Vector3(17, transform.parent.localPosition.y, transform.parent.localPosition.z);
            }
        }
    }

    private void AnimalMove_dj(GameObject obj, float startPostion, float endPostion, float speed)
    {
        obj.transform.parent.Translate(speed, 0, 0);
        if (obj.transform.parent.localPosition.x > startPostion)
        {
            obj.transform.parent.localPosition = new Vector3(endPostion, obj.transform.parent.localPosition.y, obj.transform.parent.localPosition.z);
        }
        if (obj.transform.parent.localPosition.x < endPostion)
        {
            obj.transform.parent.localPosition = new Vector3(startPostion, obj.transform.parent.localPosition.y, obj.transform.parent.localPosition.z);
        }
    }
}
