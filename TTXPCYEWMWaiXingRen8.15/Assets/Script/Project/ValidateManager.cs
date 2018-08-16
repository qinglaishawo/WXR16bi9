using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using UnityEngine;

/*Info
 * GENG:2016年12月13日16:18:31
 *  -更换服务器地址为https
 *  -添加证书忽略方法
 * GENG：2016年12月5日11:30:37
 *  -删除委托定义
 *  -隐藏公开单例
 *  -修改二维码网络查询方法为静态方法
 * json示例：
 *   {"result":"success","code":"USER_NORMAL_LOGIN","message":null,"left_times":0,"product_code":"TTX_1"}
 * json keys:
 *  -left_times(剩余激活次数)
 *  -left_hours（剩余使用时长；-1为无限时长）
 *  -product_code（产品码）
 */
public class ValidateManager : MonoBehaviour
{
    private static string GetDeviceUID()
    {
        //正确
        //return System.Guid.NewGuid().ToString();
        return SystemInfo.deviceUniqueIdentifier;
        /*#if UNITY_ANDROID || UNITY_EDITOR
                return SystemInfo.deviceUniqueIdentifier;
        #elif UNITY_IPHONE
                return ZLQ_GetMac.getMac1();
        #else
                return SystemInfo.deviceUniqueIdentifier;
        #endif*/
    }

    /// <summary>
    /// 查询当前设备时间是否到达给定日期(格式:"yyyyMMdd"，例:"20160819")。（在给定日期之前返回False，当天及之后返回True）
    /// </summary>
    /// <param name="time">格式:"yyyyMMdd"</param>
    /// <returns>在给定日期之前返回False，当天及之后返回True</returns>
    public static bool QueryApplicationState(string endtime)
    {
        if (DateTime.Now >= DateTime.ParseExact(endtime, "yyyyMMdd", CultureInfo.CurrentCulture))
            return true;
        return false;
    }


    /// <summary>
    /// 查询产品激活状态
    /// </summary>
    /// <param name="productCode"></param>
    /// <returns></returns>
    public static bool QueryProductLocal(string productCode)
    {
        string key = CalculateSerialNumber(productCode, GetDeviceUID());

        /**MARK_NEWADD*/
        string value;
        if (PlayerPrefs.HasKey(key))
        {
            /*MARK_TEMP*/
            /*return true;*/


            /*MARK*/
            value = PlayerPrefs.GetString(key);
            try
            {
                if (DateTime.Now < DateTime.ParseExact(value, "yyyyMMddHHmmss", CultureInfo.CurrentCulture))
                    return true;
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("ERROR! QueryProductLocal: " + ex.ToString());
                PlayerPrefs.DeleteKey(key);
                return false;
            }
        }
        else
        {
            return false;
        }


    }

    /// <summary>
    /// 查询二维码(网络异步)
    /// </summary>
    /// <param name="qrcode">二维码字符串</param>
    /// <param name="appCode">应用码</param>
    /// <param name="resultReceiveFunc">查询结果接收方法</param>
    public static void QueryProductSever(string qrcode, string appCode, string version, Action<ValidateResult> resultReceiveFunc)
    {
        ValidateManager.Instance.QueryMultiplatform(qrcode, appCode, version, resultReceiveFunc);
    }

    /// <summary>
    /// 清除本地激活信息（取消产品激活状态）
    /// </summary>
    /// <param name="productCode"></param>
    public static void CleanUpActivationInfo(string productCode)
    {
        string key = CalculateSerialNumber(productCode, GetDeviceUID());
        PlayerPrefs.DeleteKey(key);
    }



    #region

    //private const string ServerUrl = "http://api.armagicschool.com/user/active";
    private const string ServerUrl = "https://activate.armagicschool.com/active";
    private string UID = "";
    private string QRCodeStr = "";
    private string AppCode = "";
    private string Version = "";
    private Action<ValidateResult> ResultEvent;
    private static ValidateManager instance;

    private static ValidateManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject();
                DontDestroyOnLoad(obj);
                obj.name = "VALIDATEMANAGER";
                instance = obj.AddComponent<ValidateManager>();
            }
            return instance;
        }
    }

    private void QueryMultiplatform(string qrcode, string appCode, string version, Action<ValidateResult> resultEvent)
    {
        UID = GetDeviceUID();
        QRCodeStr = qrcode;
        AppCode = appCode;
        Version = version;
        ResultEvent = resultEvent;

        ValidateResult r = new ValidateResult();
        r.state = ValidateResult.State.Checking;
        SendCheckResult(r);

        //统一方法，全平台通用。
#if UNITY_IOS000
        //ZLQ_GetMac.getRequestString("VALIDATEMANAGER", "BackString", ServerUrl, "code=" + QRCodeStr + "&wfAddress=" + UID + "&productCode=" + AppCode);
#else
        StartCoroutine(StartCheck(ServerUrl, QRCodeStr, UID, AppCode, Version));
#endif
    }

    public bool MyRemoteCertificateValidationCallback(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        bool isOk = true;
        // If there are errors in the certificate chain, look at each error to determine the cause.
        if (sslPolicyErrors != SslPolicyErrors.None)
        {
            for (int i = 0; i < chain.ChainStatus.Length; i++)
            {
                if (chain.ChainStatus[i].Status != X509ChainStatusFlags.RevocationStatusUnknown)
                {
                    chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                    chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                    chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
                    chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                    bool chainIsValid = chain.Build((X509Certificate2)certificate);
                    if (!chainIsValid)
                    {
                        isOk = false;
                    }
                }
            }
        }
        return isOk;
    }

    private IEnumerator StartCheck(string serverUrl, string qrcode, string wfAddress, string appCode, string version)
    {
        yield return new WaitForSeconds(.5f);

        try
        {
            ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;

            Encoding encoding = Encoding.UTF8;//.GetEncoding("utf-8");
            byte[] data = encoding.GetBytes("code=" + qrcode + "&identifier=" + wfAddress + "&app=" + appCode + "&ver=" + version);

            HttpWebRequest request = WebRequest.Create(serverUrl) as HttpWebRequest;
            //request.Proxy = null;
            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            using (Stream outstream = request.GetRequestStream())
            {
                outstream.Write(data, 0, data.Length);
            }

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream instream = response.GetResponseStream();
            StreamReader sr = new StreamReader(instream, encoding);
            string content = sr.ReadToEnd();

            BackString(content);


        }
        catch (Exception ex)
        {
            Debug.Log("EXCEPTION: " + ex.ToString());

            ValidateResult rst = new ValidateResult();
            rst.state = ValidateResult.State.ErrorRequestStream;
            SendCheckResult(rst);
        }
    }

    private void BackString(string content)
    {
        //Guidebug.Log("BACKSTRING CONTENT: " + content);

        try
        {
            if (content == "timeout")
            {
                /*Result TimeOut*/
                ValidateResult rst = new ValidateResult();
                rst.state = ValidateResult.State.ErrorTimeOut;
                SendCheckResult(rst);
                return;
            }

            Hashtable jsonObj = (Hashtable)MiniJSON.jsonDecode(content);
            if (jsonObj["result"].Equals("success"))
            {
                /*Result ShowNum*/
                ValidateResult rst = new ValidateResult();
                rst.state = ValidateResult.State.Success;
                rst.RemainingNum = Convert.ToInt32(jsonObj["left_times"].ToString());
                rst.productCode = jsonObj["product_code"].ToString();

                /**MARK_NEEDADD 2016年8月30日10:19:46*/
                if (jsonObj.ContainsKey("left_hours"))
                    rst.ValidHours = Convert.ToInt32(jsonObj["left_hours"].ToString());

                SendCheckResult(rst);
                return;
            }
            else if (jsonObj["result"].Equals("fail"))
            {
                object errorCode = jsonObj["code"];
                if (errorCode.Equals("MORE_THAN_THE_NUMBER_OF_ACTIVATION"))
                {
                    /*Result MaxNum*/
                    ValidateResult rst = new ValidateResult();
                    rst.state = ValidateResult.State.KeyMaxCount;
                    rst.productCode = jsonObj["product_code"].ToString();
                    SendCheckResult(rst);
                    return;
                }
                else if (errorCode.Equals("USER_BARCODE_NOT_EXIST") || errorCode.Equals("USER_PARAMS_ERROR") || errorCode.Equals("PRODUCT_DOES_NOT_MATCH"))
                {
                    /*Result KeyError*/
                    ValidateResult rst = new ValidateResult();
                    rst.state = ValidateResult.State.KeyError;
                    SendCheckResult(rst);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("BACKSTRING EX: " + ex.ToString());

            ValidateResult rst = new ValidateResult();
            rst.state = ValidateResult.State.ErrorBackContent;
            SendCheckResult(rst);
            return;
        }
    }

    private void SendCheckResult(ValidateResult result)
    {
        /*Guidebug.Log("SENDING RESULT! : "
            + " RST: " + result.state
            + " receiveProductCode: " + result.productCode
            + (result.RemainingNum == -1 ? "" : " RemainingNum: " + result.RemainingNum.ToString()));*/


        if (result.state == ValidateResult.State.Success)
        {
            string code = result.productCode;
            string key = CalculateSerialNumber(code, UID);
            string value = CalculateValue(code, UID, result.ValidHours);

            PlayerPrefs.SetString(key, value);
            //Guidebug.Log("key " + key + "  value " + value);
        }

        //SEND RESULT
        if (ResultEvent != null)
            ResultEvent(result);
    }

    private static string CalculateSerialNumber(string productCode, string deviceID)
    {
        string serialNumber = "";
        serialNumber += productCode;
        serialNumber += deviceID.Substring(2, 2) + deviceID.Substring(3, 1) + deviceID.Substring(0, 2) + deviceID.Substring(4, 2);
        return serialNumber;
    }

    private static string CalculateValue(string productCode, string deviceID, int hours)
    {
        if (hours == -1)
            return "21160830153000";

        /************************MARK_NEEDMODIFY*/
        IFormatProvider ifp = new CultureInfo("zh-CN", false);
        DateTime dt = DateTime.Now.AddHours(hours);
        return dt.ToString("yyyyMMddHHmmss", ifp);
    }

    #endregion
}

public class ValidateResult
{
    public string productCode = "";

    public enum State
    {
        None,
        Checking,
        //开始联网
        ErrorRequestStream,
        //Android/Editor RequestStreamError
        ErrorBackContent,
        //错误
        ErrorTimeOut,
        //网络超时
        KeyError,
        //二维码错误
        KeyMaxCount,
        //超过激活次数
        Success,
        //验证成功
    }

    public State state = State.None;
    public int RemainingNum = -1;
    public int ValidHours = -1;

    /// <summary>
    /// 输出为中文信息
    /// </summary>
    public override string ToString()
    {
        switch (state)
        {
            case State.None:
            case State.Checking:
                return "网络连接中\n请稍候...";
            case State.ErrorRequestStream:
                return "网络不给力啊！\n请重试.(ERS)";
            case State.ErrorBackContent:
                return "网络不给力啊！\n请重试.(EBC)";
            case State.ErrorTimeOut:
                return "网络不给力啊！\n请重试.(ETO)";
            case State.KeyError:
                return "验证二维码不存在！\n请重试.";
            case State.KeyMaxCount:
                return "超过激活次数！";
            case State.Success:
                return "成功激活!\n剩余激活台数:" + RemainingNum;
            default:
                return "000";
        }
    }
}
