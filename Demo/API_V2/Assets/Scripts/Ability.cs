using System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ability类：管理API能力的UI显示和交互
/// </summary>
public class Ability : MonoBehaviour
{
    [Header("Ability Data")]
    [SerializeField]
    private AbilitySO abilitySO; // 能力数据ScriptableObject

    [Header("References")]
    [SerializeField]
    private Text abilityText; // 能力名称文本组件

    [SerializeField]
    private Image abilityImage; // 能力图标图像组件

    [Header("Abilities")]
    [SerializeField]
    private GameObject abilityPrefab; // 能力预制体

    [SerializeField]
    private GameObject abilities; // 能力容器对象

    [Header("Expand")]
    [SerializeField]
    private float unfoldAlpha = 0.5f; // 展开状态下的透明度
    private bool _isExpanded = false; // 当前是否展开

    [Header("Button")]
    [SerializeField]
    private Button button; // 能力按钮组件

    private RectTransform _contentRectTransform; // 父对象的RectTransform，用于布局重建

    private void Awake()
    {
        // 获取父对象的RectTransform组件，用于后续布局更新
        _contentRectTransform = transform.parent.GetComponent<RectTransform>();
    }

    private void Start()
    {
        // 添加按钮点击事件监听器
        button.onClick.AddListener(OnClick);
    }

    // 初始化 AbilitySO,设置对应的 AbilitySO 和条目
    public void Init(AbilitySO so)
    {
        abilitySO = so;

        // 设置游戏对象名称和UI显示
        gameObject.name = abilitySO.abilityName;
        abilityText.text = abilitySO.abilityName;
        abilityImage.sprite = abilitySO.abilitySprite;
        so.abilityList.Sort((x, y) => x.abilityOrder.CompareTo(y.abilityOrder));
        // 为每个条目实例化一个预制体并初始化
        foreach (var ability in abilitySO.abilityList)
        {
            var abilityObj = Instantiate(abilityPrefab, abilities.transform);
            abilityObj.GetComponent<Ability>().Init(ability);
        }
    }

    private static Color SetColorWithAlpha(Color color, float alpha)
    {
        // 返回保持RGB不变但修改Alpha值的新颜色
        return new Color(color.r, color.g, color.b, alpha);
    }

    public void OnClick()
    {
        if (abilitySO.abilityList == null || abilitySO.abilityList.Count == 0)
        {
            GameManager.Instance.LoadScene(abilitySO.AbilitySceneName);
        }
        else
        {
            _isExpanded = !_isExpanded;

            // 更新展开状态的UI
            abilityText.color = SetColorWithAlpha(abilityText.color, _isExpanded ? unfoldAlpha : 1f);
            abilityImage.color = SetColorWithAlpha(abilityImage.color, _isExpanded ? unfoldAlpha : 1f);
            abilities.SetActive(_isExpanded);

            // 强制重建布局
            LayoutRebuilder.ForceRebuildLayoutImmediate(_contentRectTransform);
        }
    }
}