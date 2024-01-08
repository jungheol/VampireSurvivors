using System;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdsManager : MonoBehaviour {

    string adUnitId;

    private BannerView bannerView;
    private AdRequest request;

    public static AdsManager instance;

    private void Awake() {
	    instance = this;
    }

    public void Start() {
        #if UNITY_ANDROID
                adUnitId = "ca-app-pub-8380134935999475/4093872226";
        #elif UNITY_IOS
                adUnitId = "ca-app-pub-3940256099942544/2934735716";
        #else
                adUnitId = "unexpected_platform";
        #endif
				// 광고 초기화
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        request = new AdRequest.Builder().Build();
    }

    public void LoadAd() {
	    bannerView.LoadAd(request);
    }
}
