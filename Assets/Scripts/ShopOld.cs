using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopOld : MonoBehaviour {
	PlayerStats stats;
	public List<Cosmetic> cosmetics = new List<Cosmetic> ();
	public Sprite baseRope,baseHook;
	public GameObject basePlayer,baseMap;

	private List<Cosmetic> ropeCosmetics = new List<Cosmetic> ();
	private List<Cosmetic> hookCosmetics = new List<Cosmetic> ();
	private List<Cosmetic> playerCosmetics = new List<Cosmetic> ();
	private List<Cosmetic> mapCosmetics = new List<Cosmetic> ();
	public RectTransform ropeScroll, hookScroll, playerScroll, mapScroll;
	public GameObject ropeListing,hookListing,playerListing,mapListing;
	// Use this for initialization
	void Start () {
		stats = FindObjectOfType<PlayerStats> ();
		#region CosmeticDefinitons
		cosmetics.Add (new Cosmetic (
			"Golden Rope",
			Resources.Load<Sprite>("GoldRope"),
			"Nice and shiny",
			100,
			Cosmetic.CosmeticType.Rope,
			Resources.Load<Sprite>("GoldRope")
		));
		#endregion
		//===================================================================
		//Sorting the general list into seperate ones for display on the shop.
		foreach (var item in cosmetics) {
			switch (item.type) {
			case Cosmetic.CosmeticType.Rope: 
				ropeCosmetics.Add (item);
				break;
			case Cosmetic.CosmeticType.Hook: 
				hookCosmetics.Add (item);
				break;
			case Cosmetic.CosmeticType.Player: 
				playerCosmetics.Add (item);
				break;
			case Cosmetic.CosmeticType.Map: 
				mapCosmetics.Add (item);
				break;
			default:
				break;
			}
		}
		//===================================================================
		//Instantiate the UI Listings onto their respective scroll views
		foreach (var item in ropeCosmetics) {
			GameObject cur = (GameObject)Instantiate (ropeListing,Vector3.zero, Quaternion.identity,ropeScroll);
			Text[] texts = cur.GetComponentsInChildren<Text> ();
			foreach (var text in texts) {
				if (text.text == "name") {
					text.text = item.name;
				}
				if (text.text == "price") {
					text.text = item.price.ToString ();
				}
			}
			Image[] images = cur.GetComponentsInChildren<Image> ();
			foreach (var image in images) {
				if (image.name == "Sprite") {
					image.sprite = item.shopDisplaySprite;
					image.sprite.texture.filterMode = FilterMode.Point;
				}
			}
		}

		foreach (var item in hookCosmetics){
			//TODO: Fill this in	
		}

		foreach (var item in playerCosmetics){
			//TODO: Fill this in	
		}

		foreach (var item in mapCosmetics){
			//TODO: Fill this in	
		}


	}

	public bool BuyCosmetic(Cosmetic item){
		switch (item.type) {

		case Cosmetic.CosmeticType.Rope: 
			if (stats.totalCoins >= item.price)
				ApplyRopeSkin ((Sprite)item.skinParameters);
			else
				return false;
			break;

		default:
			break;
		}
		return true;
	}

	public void ApplyRopeSkin(Sprite skin){

	}

	public void ApplyHookSkin(Sprite skin){

	}

	public void ApplyPlayerSkin(Sprite[] skins){

	}

	public void ApplyMapSkin(Sprite[] skins){

	}


}

[System.Serializable]
public class Cosmetic{
	public string name;
	public Sprite shopDisplaySprite;
	public Object skinParameters;
	public string description;
	public int price;
	public enum CosmeticType
	{
		Rope,
		Player,
		Hook,
		Map
	}
	public CosmeticType type;
	public Cosmetic(string _name, Sprite _sprite, string _description, int _price, CosmeticType _type, Object _skinParameters){
		name = _name;
		shopDisplaySprite = _sprite;
		description = _description;
		price = _price;
		type = _type;
		skinParameters = _skinParameters;
	}
}
