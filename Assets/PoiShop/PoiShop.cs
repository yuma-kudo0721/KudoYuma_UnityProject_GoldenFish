using System.Collections.Generic;
using UnityEngine;

public class PoiShop : MonoBehaviour
{
    [Header("ショップに並べるポイデータ")]
    public List<PoiData> shopPois; // インスペクターで登録する

    [Header("UI設定")]
    public Transform contentParent;   // ScrollViewのContentにあたるTransform
    public GameObject shopItemPrefab; // PoiShopItemプレハブ

    // プレイヤーが既に持っているポイを管理
    public List<PoiData> ownedPois = new List<PoiData>();

    void Start()
    {
        GenerateShopItems();
    }

    // UIに並べるための生成処理
    void GenerateShopItems()
    {
        // 既存の子オブジェクトはクリア
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // shopPoisリストを順番に見てアイテムを作成
        foreach (PoiData poi in shopPois)
        {
            GameObject itemObj = Instantiate(shopItemPrefab, contentParent);
            PoiShopItem item = itemObj.GetComponent<PoiShopItem>();
            item.Setup(poi, this);
        }
    }

    // ポイの購入処理
    public void BuyPoi(PoiData poi)
    {
        if (ownedPois.Contains(poi))
        {
            Debug.Log(poi.poiName + "は既に所持しています！");
            return;
        }

        // お金が足りるかのチェック（仮）
        // if(playerMoney < poi.price) {
        //   Debug.Log("お金が足りません！");
        //   return;
        // }

        // 所持リストに追加
        ownedPois.Add(poi);

        Debug.Log(poi.poiName + " を購入しました。");

        // 必要に応じてUIの更新や所持ポイの表示更新をここに追加
    }
}
