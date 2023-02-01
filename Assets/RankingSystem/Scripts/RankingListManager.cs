using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class RankingListManager : MonoBehaviour
{
    public static RankingListManager instance;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Transform contentParent;
    [SerializeField] private GameObject userPrefab;
    [SerializeField] private FlagData flagData;
    [SerializeField] private Vector2Int goldRangeMinMax;
    [SerializeField] private int currentRank;
    [SerializeField] private Vector2Int fromTo;
    [SerializeField] private UserList root = new UserList();
    [SerializeField] private GameObject youPrefab;
    private UserData targetUserdata;
    private int tempRank, bottomOffset = 2, topOffset = 6;
    public int targetRank;
    private float lineThickness;
    private void Awake()
    {
        instance = this;
        string path = "UserData.json";
        string filePath = path.Replace(".json", "");

        TextAsset targetFile = Resources.Load<TextAsset>(filePath);
        root = JsonUtility.FromJson<UserList>(targetFile.text);
    }
    private void Start()
    {
        lineThickness = userPrefab.GetComponent<RectTransform>().rect.height;
        tempRank = fromTo.x;
        for (int i = 0; i < fromTo.y - fromTo.x + topOffset ; i++)
        {
            CreateUser();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Scrolling();
        }
    }
    GameObject CreateYou()
    {
        return Instantiate(youPrefab, transform.position, Quaternion.identity, this.transform);
    }
    public void YouData(User user)
    {
        user.flag = flagData.flags[Random.Range(0, flagData.flags.Count)];
        user.nick = "You";
        user.gold = 1000;
    }
    public void RandomData(User user)
    {
        user.flag = flagData.flags[Random.Range(0, flagData.flags.Count)];
        user.nick = root.users[Random.Range(0, root.users.Count)].nick;
        user.gold = Random.Range(goldRangeMinMax.x, goldRangeMinMax.y);
    }
    public void CreateUser()
    {
        var user = Instantiate(userPrefab, Vector3.zero, Quaternion.identity, contentParent);
        user.GetComponent<UserData>().SetRank(tempRank);
        tempRank++;
    }
    public void BuyumeAnimasyonu(System.Action onComplate , GameObject you)
    {
        you.transform.DOScale(Vector3.one * 1.1f,.5f).OnComplete(()=> onComplate.Invoke());
    }
    public void BuyumeAnimasyonu(GameObject you)
    {
        you.transform.DOScale(Vector3.one * 1.1f, .5f);
    }
    public void KuculmeAnimasyonu(System.Action onComplate, GameObject you)
    {
        you.transform.DOScale(Vector3.one, .5f).OnComplete(()=> onComplate.Invoke());
    }
    public void KuculmeAnimasyonu(GameObject you)
    {
        you.transform.DOScale(Vector3.one, .5f);
    }
    public void YerineOturma(GameObject you,Transform target)
    {
        you.transform.DOMove(target.transform.position, 1f);
    }
    //private UserData FindTarget()
    //{
    //    for (int i = 0; i < contentParent.childCount; i++)
    //    {

    //    }
    //}
    public void Scrolling()
    {
        var you = CreateYou();
        scrollRect.verticalNormalizedPosition = 0;
        you.transform.position = contentParent.GetChild(contentParent.childCount - bottomOffset -1).transform.position; 

        DOTween.To(() => scrollRect.verticalNormalizedPosition, x => scrollRect.verticalNormalizedPosition = x, 1f, 1f);
        BuyumeAnimasyonu(() => DOTween.To(() => scrollRect.verticalNormalizedPosition, x => scrollRect.verticalNormalizedPosition = x, 1f, 1f).OnComplete(() => { YerineOturma(you, contentParent.transform.GetChild(6)); KuculmeAnimasyonu(()=>you.GetComponent<You>().SetRank(fromTo.x + topOffset),you); }), you);
        //scrollRect.verticalNormalizedPosition = 1; // value range (0 to 1)
    }
}