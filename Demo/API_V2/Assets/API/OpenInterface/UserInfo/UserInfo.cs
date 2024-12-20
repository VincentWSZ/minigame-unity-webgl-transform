using System;
using System.Collections;
using System.Threading;
using LitJson;
using UnityEngine;
using WeChatWASM;

public class UserInfo : Details
{
    private WXUserInfoButton _button;

    private readonly Action<WXUserInfoResponse> _onTap = (res) =>
    {
        var result = "onTap\n" + JsonMapper.ToJson(res);
        GameManager.Instance.detailsController.AddResult(
            new ResultData() { initialContentText = result }
        );
    };

    private void Start()
    {
        StartCoroutine(SetTimeout(1.0f));
    }

    IEnumerator SetTimeout(float delay)
    {
        yield return new WaitForSeconds(delay);

        Vector2 size = GameManager.Instance.detailsController.GetInitialButtonSize();
        Vector2 position = GameManager.Instance.detailsController.GetButtonPosition(-1);
        Debug.Log(position);
        Debug.Log(size);
        var windowInfo = GameManager.Instance.WindowInfo;
        var canvasWidth = (int)(windowInfo.screenWidth * windowInfo.pixelRatio);
        var canvasHeight = (int)(windowInfo.screenHeight * windowInfo.pixelRatio);
        _button = WX.CreateUserInfoButton(
            Math.Abs((int)position.x),
            Math.Abs((int)position.y),
            (int)(size.x * canvasWidth / 1080f),
            (int)(size.y * canvasWidth / 1080f),
            "en",
            true
        );
        _button.OnTap(_onTap);
    }

    // 测试 API
    protected override void TestAPI(string[] args) { }

    // 微信小游戏示例 demo
    public void getUserInfo()
    {
        WX.GetSetting(
            new GetSettingOption
            {
                success = (res) =>
                {
                    Debug.Log("GetSetting\n" + JsonMapper.ToJson(res));
                    if (res.authSetting["scope.userInfo"])
                    {
                        WX.GetUserInfo(
                            new GetUserInfoOption
                            {
                                success = (res) =>
                                {
                                    Debug.Log("getuserinfo\n" + JsonMapper.ToJson(res));
                                }
                            }
                        );
                    }
                    else
                    {
                        // 参数依次为 x, y, width, height, lang, withCredentials
                        WXUserInfoButton button = WX.CreateUserInfoButton(
                            20,
                            20,
                            400,
                            200,
                            "en",
                            true
                        );
                        button.OnTap(_onTap);
                    }
                }
            }
        );
    }

    private void OnDestroy()
    {
        _button.Hide();
        _button.Destroy();
    }
}
