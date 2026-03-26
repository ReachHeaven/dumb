using GoogleMobileAds.Api;
using UnityEngine;


namespace Game.Core
{
    public class AdManager : MonoBehaviour
    {
#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
        private BannerView _bannerView;

        private void Start()
        {
            MobileAds.Initialize(status => { });
            RequestBanner();
        }

        private void RequestBanner()
        {
#if UNITY_EDITOR
            string unityAds = "ca-app-pub-3940256099942544/6300978111";
#else
            string unityAds = "ca-app-pub-6730059161566120/2009151702";
#endif
            var adSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
            _bannerView = new BannerView(unityAds, adSize, AdPosition.Bottom);
            var adRequest = new AdRequest();
            _bannerView.LoadAd(adRequest);
        }

        private void OnDestroy()
        {
            _bannerView?.Destroy();
        }
#endif
    }
}
