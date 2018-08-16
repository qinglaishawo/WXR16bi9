

using System.Collections;
/**
* Copyright (c) 2015-2016 VisionStar Information Technology (Shanghai) Co., Ltd. All Rights Reserved.
* EasyAR is the registered trademark or trademark of VisionStar Information Technology (Shanghai) Co., Ltd in China
* and other countries for the augmented reality technology developed by VisionStar Information Technology (Shanghai) Co., Ltd.
*/
using UnityEngine;
using System.Collections.Generic;
namespace EasyAR
{
    public class ARIsEasyBehaviour : MonoBehaviour
    {
        public BarCodeScannerBehaviour bcb;
        //public static List<ImageTargetBaseBehaviour> tempImageTargetBaseBehaviour=new List<ImageTargetBaseBehaviour>();
        private const string title = "Please enter KEY first!";
        private const string boxtitle = "===PLEASE ENTER YOUR KEY HERE===";
        private const string keyMessage = ""
            + "Steps to create the key for this sample:\n"
            + "  1. login www.easyar.com\n"
            + "  2. create app with\n"
            + "      Name: HelloARQRCode (Unity)\n"
            + "      Bundle ID: cn.easyar.samples.unity.helloarqrcode\n"
            + "  3. find the created item in the list and show key\n"
            + "  4. replace all text in TextArea with your key";

        private bool startShowMessage;
        private bool isShowing;
        private string textMessage;

        private void Awake()
        {
            var EasyARBehaviour = FindObjectOfType<EasyARBehaviour>();
            if (EasyARBehaviour.Key.Contains(boxtitle))
            {
#if UNITY_EDITOR
                UnityEditor.EditorUtility.DisplayDialog(title, keyMessage, "OK");
#endif
                Debug.LogError(title + " " + keyMessage);
            }
            EasyARBehaviour.Initialize();
            foreach (var behaviour in ARBuilder.Instance.AugmenterBehaviours)
            {
                behaviour.TargetFound += OnTargetFound;
                behaviour.TargetLost += OnTargetLost;
                //behaviour.TextMessage += OnTextMessage;
            }
           /* foreach (var behaviour in ARBuilder.Instance.TrackerBehaviours)
            {
                behaviour.TargetLoad += OnTargetLoad;
                behaviour.TargetUnload += OnTargetUnload;
            }*/
        }
        private Manager manager;
        void Start()
        {
            /* foreach (var behaviour in ARBuilder.Instance.AugmenterBehaviours)
             {
                 behaviour.TextMessage -= OnTextMessage;
             }*/
            manager = GameObject.Find("Manager").GetComponent<Manager>();
        }
        void OnTargetFound(AugmenterBaseBehaviour augmenterBehaviour, ImageTargetBaseBehaviour targetBehaviour, Target target)
        {
            manager.Found(targetBehaviour.gameObject);
        }

        void OnTargetLost(AugmenterBaseBehaviour augmenterBehaviour, ImageTargetBaseBehaviour targetBehaviour, Target target)
        {
            manager.Lost(targetBehaviour.gameObject);
        }

        void OnTargetLoad(ImageTrackerBaseBehaviour trackerBehaviour, ImageTargetBaseBehaviour targetBehaviour, Target target, bool status)
        {
            //tempImageTargetBaseBehaviour.Add(targetBehaviour);
            print(targetBehaviour);
            Debug.Log("<Global Handler> Load target (" + status + "): " + target.Id + " (" + target.Name + ") " + " -> " + trackerBehaviour);
        }

        void OnTargetUnload(ImageTrackerBaseBehaviour trackerBehaviour, ImageTargetBaseBehaviour targetBehaviour, Target target, bool status)
        {
            Debug.Log("<Global Handler> Unload target (" + status + "): " + target.Id + " (" + target.Name + ") " + " -> " + trackerBehaviour);
        }

        private void OnTextMessage(AugmenterBaseBehaviour augmenterBehaviour, string text)
        {
            textMessage = text;
            startShowMessage = true;
            Debug.Log("got text: " + text);
        }

        IEnumerator ShowMessage()
        {
            isShowing = true;
            yield return new WaitForSeconds(2f);
            isShowing = false;
        }

       /* private void OnGUI()
        {
            if (startShowMessage)
            {
                if (!isShowing)
                    StartCoroutine(ShowMessage());
                startShowMessage = false;
            }

            if (isShowing)
                GUI.Box(new Rect(10, Screen.height / 2, Screen.width - 20, 30), textMessage);
        }*/
    }
}
