using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelPanelController : MonoBehaviour
{
    [SerializeField] private List<Image> levelImages =  new List<Image>();
    [SerializeField] private List<TextMeshProUGUI> levelTexts = new List<TextMeshProUGUI>(); 


    void OnEnable()
    {
        SubscribeEvent();
    }

    void OnDisable()
    {
        UnSubscribeEvent(); 
    }


    private void SubscribeEvent()
    {
        UISignals.Instance.onSetLevelValue += OnSetLevelValue;
        UISignals.Instance.onSetStageColor += OnSetStageColor;
    }

     private void UnSubscribeEvent()
    {
        UISignals.Instance.onSetLevelValue -= OnSetLevelValue;
        UISignals.Instance.onSetStageColor -= OnSetStageColor;
    }

    private void OnSetStageColor(int stageValue)
    {
       levelImages[stageValue].DOColor(new Color(0.99f, 0.42f, 0.02f ), 0.3f);
    }

    private void OnSetLevelValue(int levelValue)
    {
        var additionalValue = ++ levelValue;
        levelTexts[0].text = additionalValue.ToString(); 
        additionalValue++;
        levelTexts[1].text = additionalValue.ToString();
    }
}