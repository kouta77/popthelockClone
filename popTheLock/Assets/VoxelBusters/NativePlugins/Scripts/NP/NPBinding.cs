using UnityEngine;
using System.Collections;
using System.Reflection;
using VoxelBusters.DesignPatterns;
using VoxelBusters.NativePlugins;
using VoxelBusters.NativePlugins.Internal;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(PlatformBindingHelper))]
public class NPBinding : SingletonPattern <NPBinding>
{
	#region Static Fields

#if USES_ADDRESS_BOOK
	private 	static		AddressBook				addressBook;
#endif

#if USES_BILLING
	private 	static		Billing 				billing;
#endif

#if USES_GAME_SERVICES
	private 	static		GameServices 			gameServices;
#endif

#if USES_MEDIA_LIBRARY
	private 	static		MediaLibrary 			mediaLibrary;
#endif

#if USES_NETWORK_CONNECTIVITY
	private		static		NetworkConnectivity		networkConnectivity;
#endif

#if USES_NOTIFICATION_SERVICE
	private 	static		NotificationService 	notificationService; 	
#endif

#if USES_SHARING
	private 	static		Sharing 				sharing; 		
#endif

#if USES_TWITTER
	private 	static		Twitter 				twitter;
#endif

	private		static		UI 						userInterface;
	private		static		Utility 				utility;
	
#if USES_WEBVIEW
	private 	static		WebViewNative 			webview;
#endif

	#endregion

	#region Static Properties

#if USES_ADDRESS_BOOK
	/// <summary>
	/// Returns platform specific interface to access Address Book feature.
	/// </summary>
	public static AddressBook AddressBook 	
	{ 
		get
		{
			if (!NPSettings.Application.SupportedFeatures.UsesAddressBook)
			{
				Debug.LogError("[NPBinding] Address Book feature is currently marked not required. If you want to use this feature, then goto NPSettings/Application Settings and enable \"UsesAddressBook\" flag.");
				return null;
			}

			if (Instance == null)
				return null;

			if (addressBook == null)
				addressBook	= Instance.CachedGameObject.GetComponent<AddressBook>();
			
			return addressBook;
		}
	}
#endif

#if USES_BILLING
	/// <summary>
	/// Returns platform specific interface to access Billing (IAP) feature.
	/// </summary>
	public static Billing Billing 			
	{ 
		get
		{
			if (!NPSettings.Application.SupportedFeatures.UsesBilling)
			{
				Debug.LogError("[NPBinding] Billing feature is currently marked not required. If you want to use this feature, then goto NPSettings/Application Settings and enable \"UsesBilling\" flag.");
				return null;
			}
			
			if (Instance == null)
				return null;
			
			if (billing == null)
				billing	= Instance.CachedGameObject.GetComponent<Billing>();
			
			return billing;
		}
	}
#endif

#if USES_GAME_SERVICES
	/// <summary>
	/// Returns platform specific interface to access Game Services feature.
	/// </summary>
	public static GameServices GameServices 			
	{ 
		get
		{
			if (!NPSettings.Application.SupportedFeatures.UsesGameServices)
			{
				Debug.LogError("[NPBinding] GameServices feature is currently marked not required. If you want to use this feature, then goto NPSettings/Application Settings and enable \"UsesGameServices\" flag.");
				return null;
			}
			
			if (Instance == null)
				return null;
			
			if (gameServices == null)
				gameServices	= Instance.CachedGameObject.GetComponent<GameServices>();
			
			return gameServices;
		}
	}
#endif

#if USES_MEDIA_LIBRARY
	/// <summary>
	/// Returns platform specific interface to access Media Library feature.
	/// </summary>
	public static MediaLibrary MediaLibrary 	
	{ 
		get
		{
			if (!NPSettings.Application.SupportedFeatures.UsesMediaLibrary)
			{
				Debug.LogError("[NPBinding] Media Library feature is currently marked not required. If you want to use this feature, then goto NPSettings/Application Settings and enable \"UsesMediaLibrary\" flag.");
				return null;
			}
			
			if (Instance == null)
				return null;
			
			if (mediaLibrary == null)
				mediaLibrary	= Instance.CachedGameObject.GetComponent<MediaLibrary>();
			
			return mediaLibrary;
		}
	}
#endif

#if USES_NETWORK_CONNECTIVITY
	/// <summary>
	/// Returns platform specific interface to access Network Connectivity feature.
	/// </summary>
	public static NetworkConnectivity NetworkConnectivity 	
	{ 
		get
		{
			if (!NPSettings.Application.SupportedFeatures.UsesNetworkConnectivity)
			{
				Debug.LogError("[NPBinding] Network Connectivity feature is currently marked not required. If you want to use this feature, then goto NPSettings/Application Settings and enable \"UsesNetworkConnectivity\" flag.");
				return null;
			}
			
			if (Instance == null)
				return null;

			if (networkConnectivity == null)
				networkConnectivity	= Instance.CachedGameObject.GetComponent<NetworkConnectivity>();
			
			return networkConnectivity;
		}
	}
#endif

#if USES_NOTIFICATION_SERVICE
	/// <summary>
	/// Returns platform specific interface to access Notification Service feature.
	/// </summary>
	public static NotificationService NotificationService 	
	{ 
		get
		{
			if (!NPSettings.Application.SupportedFeatures.UsesNotificationService)
			{
				Debug.LogError("[NPBinding] Notification Service feature is currently marked not required. If you want to use this feature, then goto NPSettings/Application Settings and enable \"UsesNotificationService\" flag.");
				return null;
			}
			
			if (Instance == null)
				return null;
			
			if (notificationService == null)
				notificationService	= Instance.CachedGameObject.GetComponent<NotificationService>();
			
			return notificationService;
		}
	}
#endif

#if USES_SHARING
	/// <summary>
	/// Returns platform specific interface to access Sharing feature.
	/// </summary>
	public static Sharing Sharing 		
	{ 
		get
		{
			if (!NPSettings.Application.SupportedFeatures.UsesSharing)
			{
				Debug.LogError("[NPBinding] Sharing feature is currently marked not required. If you want to use this feature, then goto NPSettings/Application Settings and enable \"UsesSharing\" flag.");
				return null;
			}
			
			if (Instance == null)
				return null;
			
			if (sharing == null)
				sharing	= Instance.CachedGameObject.GetComponent<Sharing>();
				
			return sharing;
		}
	}
#endif

#if USES_TWITTER
	/// <summary>
	/// Returns platform specific interface to access Twitter feature.
	/// </summary>
	public static Twitter Twitter 		
	{ 
		get
		{
			if (!NPSettings.Application.SupportedFeatures.UsesTwitter)
			{
				Debug.LogError("[NPBinding] Twitter feature is currently marked not required. If you want to use this feature, then goto NPSettings/Application Settings and enable \"UsesTwitter\" flag.");
				return null;
			}
			
			if (Instance == null)
				return null;
			
			if (twitter == null)
				twitter	= Instance.CachedGameObject.GetComponent<Twitter>();
			
			return twitter;
		}
	}
#endif
	
	/// <summary>
	/// Returns platform specific interface to access Native UI feature.
	/// </summary>
	public static UI UI 		
	{ 
		get
		{
			if (Instance == null)
				return null;
			
			if (userInterface == null)
				userInterface	= Instance.CachedGameObject.GetComponent<UI>();
				
			return userInterface;
		}
	}
	
	/// <summary>
	/// Returns platform specific interface to access Utility functions.
	/// </summary>
	public static Utility Utility 		
	{ 
		get
		{
			if (Instance == null)
				return null;
			
			if (utility == null)
				utility	= Instance.CachedGameObject.GetComponent<Utility>();
				
			return utility;
		}
	}

#if USES_WEBVIEW
	/// <summary>
	/// Returns platform specific interface to access Native WebView feature.
	/// </summary>
	public static WebViewNative WebView 			
	{ 
		get
		{	
			if (Instance == null)
				return null;

			if (webview == null)
				webview	= Instance.CachedGameObject.GetComponent<WebViewNative>();
			
			return webview;
		}
	}
#endif
	
	#endregion
	
	#region Overriden Methods
	
	protected override void Init ()
	{
		base.Init ();
		
		// Not interested in non singleton instance
		if (instance != this)
			return;

		// Create compoennts 
#if USES_ADDRESS_BOOK
		addressBook				= AddComponentBasedOnPlatform<AddressBook>();
#endif

#if USES_BILLING
		billing					= AddComponentBasedOnPlatform<Billing>();
#endif
	
#if USES_GAME_SERVICES
		gameServices			= AddComponentBasedOnPlatform<GameServices>();
#endif

#if USES_MEDIA_LIBRARY
		mediaLibrary			= AddComponentBasedOnPlatform<MediaLibrary>();
#endif

#if USES_NETWORK_CONNECTIVITY
		networkConnectivity		= AddComponentBasedOnPlatform<NetworkConnectivity>();
#endif

#if USES_NOTIFICATION_SERVICE
		notificationService		= AddComponentBasedOnPlatform<NotificationService>();
#endif

#if USES_SHARING
		sharing					= AddComponentBasedOnPlatform<Sharing>();
#endif

#if USES_TWITTER
		twitter					= AddComponentBasedOnPlatform<Twitter>();
#endif
	
		userInterface			= AddComponentBasedOnPlatform<UI>();
		utility					= AddComponentBasedOnPlatform<Utility>();

#if USES_WEBVIEW
		webview					= AddComponentBasedOnPlatform<WebViewNative>();
#endif
	}
	
	#endregion
	
	#region Create Component Methods
	
	private T AddComponentBasedOnPlatform <T> () where T : MonoBehaviour
	{
		System.Type _basicType		= typeof(T);
		string 		_baseTypeName	= _basicType.ToString();
		
		// Check if we have free version specific class
		string 		_platformSpecificTypeName	= null;
		
#if UNITY_EDITOR
		_platformSpecificTypeName	= _baseTypeName + "Editor";	
#elif UNITY_IOS 
		_platformSpecificTypeName	= _baseTypeName + "IOS";	
#elif UNITY_ANDROID
		_platformSpecificTypeName	= _baseTypeName + "Android";
#endif

		if (!string.IsNullOrEmpty(_platformSpecificTypeName))
		{
#if !NETFX_CORE
			System.Type _platformSpecificClassType	= _basicType.Assembly.GetType(_platformSpecificTypeName, false);
#else
			System.Type _platformSpecificClassType	= _basicType;
#endif

			return CachedGameObject.AddComponent(_platformSpecificClassType) as T;
		}

		return CachedGameObject.AddComponent<T>();
	}

	#endregion
}