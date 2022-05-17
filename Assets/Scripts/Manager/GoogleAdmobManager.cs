using UnityEngine;
using System;
using System.Collections;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

public class GoogleAdmobManager : SimpleSingleton<GoogleAdmobManager> {

#if UNITY_EDITOR
	// 広告ユニット ID を記述します
	private string appId = "unexpected_platform";// テストらしい
	private string adUnitId = "unexpected_platform";// テストらしい
#elif UNITY_ANDROID
	private string appId = "ca-app-pub-4566588771611947~4318273907";
	//private string adUnitId = "ca-app-pub-4566588771611947/8396054051";// 本番
	private string adUnitId = "ca-app-pub-3940256099942544/5224354917";// テスト
#elif UNITY_IPHONE
	private string appId = "ca-app-pub-4566588771611947~6248688279";
	//private string adUnitId = "ca-app-pub-4566588771611947/9398144538";// 本番
	private string adUnitId = "ca-app-pub-3940256099942544/1712485313";// テスト
#endif

	private RewardedAd rewardedAd;
/*	private RewardedAdAd RewardedAd;

	private Action<bool> RewardCallback = null;
	private Action<bool> RequestVideoCallback = null;

	private bool IsInitializedFlag = false;
*/
	//public bool IsInitialized() {
	//	return IsInitializedFlag;
	//}

	public void Initialize() {
		MobileAds.Initialize(
			initStatus => {
				CreateAndLoadRewardedAd();
			}
		);
	}

	public void CreateAndLoadRewardedAd()
    {
		this.rewardedAd = new RewardedAd(adUnitId);

		// Called when an ad request has successfully loaded.
		this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
		// Called when an ad request failed to load.
		this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
		// Called when an ad is shown.
		this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
		// Called when an ad request failed to show.
		this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
		// Called when the user should be rewarded for interacting with the ad.
		this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
		// Called when the ad is closed.
		this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		
		// Load the rewarded ad with the request.
		this.rewardedAd.LoadAd(request);
    }

	/*
	public void RequestRewardedAd(Action<bool> requestVideoCallback)
	{
#if UNITY_EDITOR
		if (requestVideoCallback != null) {
			requestVideoCallback(true);
		}
#else
		RequestVideoCallback = requestVideoCallback;

	 	// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the rewarded video ad with the request.
		RewardedAd.LoadAd(request, adUnitId);
#endif
	}

	public bool IsVideoLoaded() {
		return RewardedAd.IsLoaded();
	}

	public void ShowVideo(Action<bool> rewardCallback) {
#if UNITY_EDITOR
		if (rewardCallback != null) {
			rewardCallback(true);
		}
#else
		RewardCallback = rewardCallback;
		RewardedAd.Show();
#endif
	}*/

	public void UserChoseToWatchAd()
	{
		if (this.rewardedAd.IsLoaded()) {
			this.rewardedAd.Show();
		}
	}

	// 広告の読み込みが完了すると呼び出されます。
	public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        LogManager.Instance.Log("HandleRewardedAdLoaded event received");
    }

	// 広告の読み込みが失敗すると呼び出されます。提供される AdErrorEventArgs の Message プロパティは、発生した障害のタイプを示します。
    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //LogManager.Instance.Log(
        //    "HandleRewardedAdFailedToLoad event received with message: "
        //                     + args.Message);
    }

	// 広告がデバイスの画面いっぱいに表示されると呼び出されます。必要な場合は、ここでアプリの音声出力やゲームループを一時停止します。
    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        LogManager.Instance.Log("HandleRewardedAdOpening event received");
    }

	// 広告の表示に失敗すると呼び出されます。提供される AdErrorEventArgs の Message プロパティは、発生した障害のタイプを示します。
    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        LogManager.Instance.Log(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

	// 動画を視聴したユーザーに報酬を付与するときに呼び出されます。Reward は、ユーザーに付与される報酬を説明するパラメータです。
    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
		CreateAndLoadRewardedAd();
        LogManager.Instance.Log("HandleRewardedAdClosed event received");
    }

	// ユーザーが「閉じる」アイコンまたは「戻る」ボタンをタップして、動画リワード広告を閉じると呼び出されます。アプリで音声出力やゲームループを一時停止している場合は、ここで再開すると効果的です。
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        LogManager.Instance.Log(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
    }
}

