﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class Purchaser : MonoBehaviour, IStoreListener {

	private static IStoreController m_StoreController;          // The Unity Purchasing system.
	private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

	// Product identifiers for all products capable of being purchased: 
	// "convenience" general identifiers for use with Purchasing, and their store-specific identifier 
	// counterparts for use with and outside of Unity Purchasing. Define store-specific identifiers 
	// also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

	// General product identifiers for the consumable, non-consumable, and subscription products.
	// Use these handles in the code to reference which product to purchase. Also use these values 
	// when defining the Product Identifiers on the store. Except, for illustration purposes, the 
	// kProductIDSubscription - it has custom Apple and Google identifiers. We declare their store-
	// specific mapping to Unity Purchasing's AddProduct, below.
	[Header("Get coin when user buy product")]
	[SerializeField]
	int coinConsumable1;
	[SerializeField]
	int coinConsumable5;
	[SerializeField]
	int coinConsumable10;
	[SerializeField]
	int coinConsumable15;
	[SerializeField]
	int coinConsumable30;
	[SerializeField]
	int coinConsumable50;
	[SerializeField]
	int coinConsumable100;

	[Header("Id product consumable")]
	[HideInInspector]
	public string kProductIDConsumable1 = "com.tranphuocnhan.TestFingerWar.pack1";
	[HideInInspector]
	public string kProductIDConsumable5 = "com.tranphuocnhan.TestFingerWar.pack2";
	[HideInInspector]
	public string kProductIDConsumable10 = "com.tranphuocnhan.TestFingerWar.pack3";
	[HideInInspector]
	public string kProductIDConsumable15 = "com.tranphuocnhan.TestFingerWar.pack4";
	[HideInInspector]
	public string kProductIDConsumable30 = "com.tranphuocnhan.TestFingerWar.pack5";
	[HideInInspector]
	public string kProductIDConsumable50 = "com.tranphuocnhan.TestFingerWar.pack6";
	[HideInInspector]
	public string kProductIDConsumable100 = "com.tranphuocnhan.TestFingerWar.pack7";

	[Header("Id product non consumable")]
	[HideInInspector]
	public string kProductIDNonConsumable = "com.tranphuocnhan.TestFingerWar.removeads";
	//public static string kProductIDSubscription =  "subscription"; 

	// Apple App Store-specific product identifier for the subscription product.
	private static string kProductNameAppleSubscription =  "com.unity3d.subscription.new";

	// Google Play Store-specific product identifier subscription product.
	private static string kProductNameGooglePlaySubscription =  "com.unity3d.subscription.original"; 

	public static Purchaser intance;

	void Awake(){
		if(intance == null)
			intance = this;
	}

	void Start()
	{
		// If we haven't set up the Unity Purchasing reference
		if (m_StoreController == null)
		{
			// Begin to configure our connection to Purchasing
			InitializePurchasing();
		}
	}

	public void InitializePurchasing() 
	{
		// If we have already connected to Purchasing ...
		if (IsInitialized())
		{
			// ... we are done here.
			return;
		}

		// Create a builder, first passing in a suite of Unity provided stores.
		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

		// Add a product to sell / restore by way of its identifier, associating the general identifier
		// with its store-specific identifiers.
		builder.AddProduct(kProductIDConsumable1, ProductType.Consumable);
		builder.AddProduct(kProductIDConsumable5, ProductType.Consumable);
		builder.AddProduct(kProductIDConsumable10, ProductType.Consumable);
		builder.AddProduct(kProductIDConsumable15, ProductType.Consumable);
		builder.AddProduct(kProductIDConsumable30, ProductType.Consumable);
		builder.AddProduct(kProductIDConsumable50, ProductType.Consumable);
		builder.AddProduct(kProductIDConsumable100, ProductType.Consumable);
		// Continue adding the non-consumable product.
		builder.AddProduct(kProductIDNonConsumable, ProductType.NonConsumable);
		// And finish adding the subscription product. Notice this uses store-specific IDs, illustrating
		// if the Product ID was configured differently between Apple and Google stores. Also note that
		// one uses the general kProductIDSubscription handle inside the game - the store-specific IDs 
		// must only be referenced here. 
		/*builder.AddProduct(kProductIDSubscription, ProductType.Subscription, new IDs(){
			{ kProductNameAppleSubscription, AppleAppStore.Name },
			{ kProductNameGooglePlaySubscription, GooglePlay.Name },
		});*/

		// Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
		// and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
		UnityPurchasing.Initialize(this, builder);
	}


	private bool IsInitialized()
	{
		// Only say we are initialized if both the Purchasing references are set.
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}


	public void BuyConsumable1()
	{
		// Buy the consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(kProductIDConsumable1);
	}

	public void BuyConsumable5()
	{
		// Buy the consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(kProductIDConsumable5);
	}

	public void BuyConsumable10()
	{
		// Buy the consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(kProductIDConsumable10);
	}

	public void BuyConsumable15()
	{
		// Buy the consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(kProductIDConsumable15);
	}

	public void BuyConsumable30()
	{
		// Buy the consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(kProductIDConsumable30);
	}

	public void BuyConsumable50()
	{
		// Buy the consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(kProductIDConsumable50);
	}

	public void BuyConsumable100()
	{
		// Buy the consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(kProductIDConsumable100);
	}


	public void BuyNonConsumable()
	{
		// Buy the non-consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(kProductIDNonConsumable);
	}


	public void BuySubscription()
	{
		// Buy the subscription product using its the general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
		// Notice how we use the general product identifier in spite of this ID being mapped to
		// custom store-specific identifiers above.
		//BuyProductID(kProductIDSubscription);
	}


	void BuyProductID(string productId)
	{
		// If Purchasing has been initialized ...
		if (IsInitialized())
		{
			// ... look up the Product reference with the general product identifier and the Purchasing 
			// system's products collection.
			Product product = m_StoreController.products.WithID(productId);

			// If the look up found a product for this device's store and that product is ready to be sold ... 
			if (product != null && product.availableToPurchase)
			{
				Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
				// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
				// asynchronously.
				m_StoreController.InitiatePurchase(product);
			}
			// Otherwise ...
			else
			{
				// ... report the product look-up failure situation  
				Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
			}
		}
		// Otherwise ...
		else
		{
			// ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
			// retrying initiailization.
			Debug.Log("BuyProductID FAIL. Not initialized.");
		}
	}


	// Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
	// Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
	public void RestorePurchases()
	{
		// If Purchasing has not yet been set up ...
		if (!IsInitialized ()) {
			// ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
			Debug.Log ("RestorePurchases FAIL. Not initialized.");
			return;
		}


		// ... begin restoring purchases
		Debug.Log ("RestorePurchases started ...");

		// Fetch the Apple store-specific subsystem.
		var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions> ();
		// Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
		// the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
		apple.RestoreTransactions ((result) => {
			// The first phase of restoration. If no more responses are received on ProcessPurchase then 
			// no purchases are available to be restored.
			if (result) {
				//SaveManager.instance.state.isPurchaseRemoveAds = true;
				//SaveManager.instance.Save ();
			}
			Debug.Log ("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
		});
	}

	//  
	// --- IStoreListener
	//

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		// Purchasing has succeeded initializing. Collect our Purchasing references.
		Debug.Log("OnInitialized: PASS");

		// Overall Purchasing system, configured with products for this application.
		m_StoreController = controller;
		// Store specific subsystem, for accessing device-specific store features.
		m_StoreExtensionProvider = extensions;
	}


	public void OnInitializeFailed(InitializationFailureReason error)
	{
		// Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
		Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}


	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
	{
		// A consumable product has been purchased by this user.
		if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumable1, StringComparison.Ordinal))
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			if (SaveManager.instance.state.isX2pack1) {
				SaveManager.instance.state.TotalDiamond += (coinConsumable1 * 2);
				SaveManager.instance.state.isX2pack1 = false;
				SaveManager.instance.Save ();
			} else {
				SaveManager.instance.state.TotalDiamond += coinConsumable1;
				SaveManager.instance.Save ();
			}
			// The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
		}
		// A consumable product has been purchased by this user.
		if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumable5, StringComparison.Ordinal))
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			if (SaveManager.instance.state.isX2pack5) {
				SaveManager.instance.state.TotalDiamond += (coinConsumable5 * 2);
				SaveManager.instance.state.isX2pack5 = false;
				SaveManager.instance.Save ();
			} else {
				SaveManager.instance.state.TotalDiamond += coinConsumable5;
				SaveManager.instance.Save ();
			}
			SaveManager.instance.Save ();
			// The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
		}
		// A consumable product has been purchased by this user.
		if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumable10, StringComparison.Ordinal))
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			if (SaveManager.instance.state.isX2pack10) {
				SaveManager.instance.state.TotalDiamond += (coinConsumable10 * 2);
				SaveManager.instance.state.isX2pack10 = false;
				SaveManager.instance.Save ();
			} else {
				SaveManager.instance.state.TotalDiamond += coinConsumable10;
				SaveManager.instance.Save ();
			}
			// The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
		}
		// A consumable product has been purchased by this user.
		if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumable15, StringComparison.Ordinal))
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			if (SaveManager.instance.state.isX2pack15) {
				SaveManager.instance.state.TotalDiamond += (coinConsumable15 * 2);
				SaveManager.instance.state.isX2pack15 = false;
				SaveManager.instance.Save ();
			} else {
				SaveManager.instance.state.TotalDiamond += coinConsumable15;
				SaveManager.instance.Save ();
			}
			// The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
		}
		// A consumable product has been purchased by this user.
		if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumable30, StringComparison.Ordinal))
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			if (SaveManager.instance.state.isX2pack30) {
				SaveManager.instance.state.TotalDiamond += (coinConsumable30 * 2);
				SaveManager.instance.state.isX2pack30 = false;
				SaveManager.instance.Save ();
			} else {
				SaveManager.instance.state.TotalDiamond += coinConsumable30;
				SaveManager.instance.Save ();
			}
			// The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
		}
		// A consumable product has been purchased by this user.
		if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumable50, StringComparison.Ordinal))
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			if (SaveManager.instance.state.isX2pack50) {
				SaveManager.instance.state.TotalDiamond += (coinConsumable50 * 2);
				SaveManager.instance.state.isX2pack50 = false;
				SaveManager.instance.Save ();
			} else {
				SaveManager.instance.state.TotalDiamond += coinConsumable50;
				SaveManager.instance.Save ();
			}
			// The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
		}
		// A consumable product has been purchased by this user.
		if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumable100, StringComparison.Ordinal))
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			if (SaveManager.instance.state.isX2pack100) {
				SaveManager.instance.state.TotalDiamond += (coinConsumable100 * 2);
				SaveManager.instance.state.isX2pack100 = false;
				SaveManager.instance.Save ();
			} else {
				SaveManager.instance.state.TotalDiamond += coinConsumable100;
				SaveManager.instance.Save ();
			}
			// The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
		}
		// Or ... a non-consumable product has been purchased by this user.
		if (String.Equals(args.purchasedProduct.definition.id, kProductIDNonConsumable, StringComparison.Ordinal))
		{
			SaveManager.instance.state.isPurchaseRemoveAds = true;
			GoogleMobileAdsDemoScript.instance.HideBanner ();
			SaveManager.instance.Save ();
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			// TODO: The non-consumable item has been successfully purchased, grant this item to the player.
		}
		// Or ... a subscription product has been purchased by this user.
		/*else if (String.Equals(args.purchasedProduct.definition.id, kProductIDSubscription, StringComparison.Ordinal))
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			// TODO: The subscription item has been successfully purchased, grant this to the player.
		}*/
		// Or ... an unknown product has been purchased by this user. Fill in additional products here....
		else 
		{
			Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
		}

		// Return a flag indicating whether this product has completely been received, or if the application needs 
		// to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
		// saving purchased products to the cloud, and when that save is delayed. 
		return PurchaseProcessingResult.Complete;
	}


	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		// A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
		// this reason with the user to guide their troubleshooting actions.
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}

	public string GetPrice(string productID) {
		return m_StoreController.products.WithID(productID).metadata.localizedPrice.ToString();
	}
}
