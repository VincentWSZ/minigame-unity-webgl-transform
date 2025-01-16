using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using WeChatWASM;

public class VDVideo : MonoBehaviour
{
    private WXVideoDecoder _videoDecoder;
    
    // Start is called before the first frame update
    void Start()
    {
        var btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);

        if (_videoDecoder == null)
        {
            _videoDecoder = WX.CreateVideoDecoder();
        }

        this.AutoPlayVideo();
    }

    private void AutoPlayVideo()
    {
        var windowInfo = GameManager.Instance.WindowInfo;
        _videoDecoder.Start(
            new VideoDecoderStartOption()
            {
                source =
                    "https://res.wx.qq.com/wechatgame/product/webpack/userupload/20190812/video.mp4",
                mode = 1,
                abortAudio = false,
                abortVideo = false,
            }
        );
        
    }

    private void OnClick()
    {
        Debug.Log("click");
        _videoDecoder.On("data",(result)=>
        {
            Debug.Log("data" + result);
        });
        
    }

    private void OnDestroy()
    {
        _videoDecoder.Remove();
    }
}