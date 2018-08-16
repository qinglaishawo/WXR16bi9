using UnityEngine;
using System.Collections;

interface IModecControlInterface
{
    /// <summary>
    /// 播放动画
    /// </summary>
    bool PlayAnimation(string sAniClipName);
    /// <summary>
    /// 重载播放动画，可穿是否循环函数
    /// </summary>
    bool PlayAnimation(string sAniClipName, bool IsLoop);
    /// <summary>
    /// 清空待播放的动画队列
    /// </summary>
    void ClearAniOrderList();
    /// <summary>
    /// 停止动画
    /// </summary>
    void StopAnimation();
    /// <summary>
    /// 暂停动画
    /// </summary>
    void PauseAnimation();
    /// <summary>
    /// 恢复动画
    /// </summary>
    void ResumeAnimation();
    /// <summary>
    /// 默认播放顺序播放动画
    /// </summary>
    void PlayAniDefaultOrder(bool IsSinglePlay);
    /// <summary>
    /// 默认播放顺序播放动画,不等待循环结束
    /// </summary>
    void PlayAniDefaultOrderImmediate(bool IsSinglePlay);
    /// <summary>
    /// 自定义顺序播放动画,把播放序列中的第一个以后的动画删除，添加一个指定动画(可设置循环)
    /// </summary>
    void PlayAniDefineOrder(string sAniClipName, bool IsLoop);
    /// <summary>
    /// 自定义顺序播放动画,把播放序列中的第一个以后的动画删除，添加一个指定动画
    /// </summary>
    void PlayAniDefineOrder(string sAniClipName);
    /// <summary>
    /// 判断播放链表是否播放到最后一个动画
    /// </summary>
    bool JudgeAniListOrderIsLastOne();
    /// <summary>
    /// 模型丢失之后的处理
    /// </summary>
    void AfterModelUnTrackable();
    /// <summary>
    /// 直接播放动画
    /// </summary>
    bool PlayAnimationImmediate(string sAniClipName,  bool bIsLoop);
    /// <summary>
    /// 摇杆控件播放的动画
    /// <summary>
    void PlayAnimationForJoyStick(string sAniClipName, float fPlaySpeed);
    /// <summary>
    /// 返回当前播放动画剪辑的名称
    /// <summary>
    string ReturnCurAniClipName();
}
