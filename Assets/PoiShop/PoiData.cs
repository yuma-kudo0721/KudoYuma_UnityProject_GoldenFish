using UnityEngine;
[CreateAssetMenu(fileName = "NewPoiData", menuName = "Poi/Poi Data")]
public class PoiData : ScriptableObject
{
    public string poiName;      // ポイの名前
    public Sprite icon;         // アイコン画像
    public int price;           // 購入価格
    public GameObject prefab;   // 実際に使うPrefab
    [TextArea]
    public string description;
}
