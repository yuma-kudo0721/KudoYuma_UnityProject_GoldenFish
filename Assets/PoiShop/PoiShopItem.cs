using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshProを使う場合

public class PoiShopItem : MonoBehaviour
{
    public Image iconImage;          // ポイのアイコン表示用
    public TMP_Text nameText;        // ポイの名前表示用
    public TMP_Text priceText;       // 価格表示用
    public TMP_Text description;       // 価格表示用
    public Button buyButton;         // 購入ボタン

    private PoiData poiData;
    private PoiShop poiShop;

    // ショップから呼ばれて、UIの中身をセットアップする
    public void Setup(PoiData data, PoiShop shop)
    {
        poiData = data;
        poiShop = shop;

        iconImage.sprite = poiData.icon;
        nameText.text = poiData.poiName;
        description.text = poiData.description;
        priceText.text = poiData.price.ToString();

        // ボタンのクリックイベントをクリアしてから新しく登録する
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(OnBuyClicked);
    }

    void OnBuyClicked()
    {
        // ボタンが押されたらショップの購入処理を呼び出す
        poiShop.BuyPoi(poiData);
    }
}
