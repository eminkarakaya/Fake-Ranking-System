using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class You : MonoBehaviour
{
    User user = new User();
    [Header("UI")]

    [SerializeField] private TextMeshProUGUI nickText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private Image flagImage;
    private void Start()
    {
        RankingListManager.instance.YouData(user);
        goldText.text = user.gold.ToString();
        nickText.text = user.nick.ToString();
        flagImage.sprite = user.flag;
    }
    public void SetRank(int rank)
    {
        user.rank = rank;
        if(rankText != null)
        {
            rankText.text = "#"+ rank.ToString();
        }
    }
}
