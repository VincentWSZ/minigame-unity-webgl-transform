using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "AbilitySO")]
public class AbilitySO : ScriptableObject
{
    public string abilityName;

    public Sprite abilitySprite;
    public List<AbilitySO> abilityList;

    // 运行时强制设置为name + "Scene"
    public string AbilitySceneName => name.Replace("SO", "Scene");

    [Tooltip("从小到大排序")]
    public int abilityOrder;
}
