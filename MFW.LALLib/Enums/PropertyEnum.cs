using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MFW.LALLib
{
    public enum PropertyEnum
    {
        NULL, /* 0 Just ignore this enum. */
              //PLCM_MF_PROP_MINSYS("MINSYS"),
        SIP_PROXY_SERVER_ADDRESS, /*1 used by PLCM_MF_Initialize and PLCM_MF_SetUserInfo, format is ipaddr, example:172.21.120.124 */
        PLCM_MFW_KVLIST_KEY_SIP_Transport,  /*2 used by PLCM_MF_Initialize and PLCM_MF_SetUserInfo, format is UDP,TCP,TLS,AUTO */
        PLCM_MFW_KVLIST_KEY_SIP_ServerType, /*3 used by PLCM_MF_Initialize and PLCM_MF_SetUSerInfo, format is STANDARD */
        PLCM_MFW_KVLIST_KEY_SIP_Register_Expires_Interval, /*4, determine time interval of register in CC*/
        SIP_USERNAME,/* 5 used by PLCM_MF_Initialize and PLCM_MF_SetUserInfo, format example is sam@example.com */
        SIP_DOMAIN,/*6 used by PLCM_MF_Initialize and PLCM_MF_SetUserInfo */
        SIP_AUTHORIZATION_NAME,  /*7 used by PLCM_MF_Initialize and PLCM_MF_SetUserInfo */
        SIP_PASSWORD,/*8 used by PLCM_MF_Initialize and PLCM_MF_SetUserInfo */
        PLCM_MFW_KVLIST_KEY_SIP_CookieHead,/*9 cookie head */
        PLCM_MFW_KVLIST_KEY_SIP_Base_Cred,/*10 Base authentication head */
        PLCM_MFW_KVLIST_KEY_SIP_AnonymousToken_Cred, /*11,Anonymous-Token cred*/
        PLCM_MFW_KVLIST_KEY_SIP_Anonymous_Cred, /*12, Anonymous cred*/
        PLCM_MFW_KVLIST_KEY_CallSettings_MaxCallNum, /*13 The max support of call number at a time, max number is 16. */
        CALL_SETTINGS_NETWORK_CALLRATE,/*14 used by PLCM_MF_Initialize and PLCM_MFW_PlaceCall, example format is 384 */
        CALL_SETTINGS_AES_ENCRIPTION,/*15 used by PLCM_MF_Initialize */

        //PLCM_MFW_KVLIST_KEY_CallSettings_CryptoSuiteType,/*13 < Set SRTP crypto suite type if AesEcription is "on" or "auto". */
        //PLCM_MFW_KVLIST_KEY_CallSettings_SrtpKey,/*14 < Set SRTP crypto key if AesEcription is "on" or "auto" and CryptoSuiteType has value. */
        CALL_SETTINGS_DEFAULT_Audio_START_PORT,/*16 used by PLCM_MF_Initialize, example is 3230 */
        CALL_SETTINGS_DEFAULT_Audio_END_PORT,/*17 used by PLCM_MF_Initialize, example is 3550 */
        CALL_SETTINGS_DEFAULT_Video_START_PORT,/*18 used by PLCM_MF_Initialize, example is 3230 */
        CALL_SETTINGS_DEFAULT_Video_END_PORT,/*19 used by PLCM_MF_Initialize, example is 3550 */
        PLCM_MFW_KVLIST_KEY_CallSettings_SIPClientListeningPort, /*20*/
        PLCM_MFW_KVLIST_KEY_CallSettings_SIPClientListeningTLSPort, /*21*/

        CALL_SETTINGS_ENABLE_SVC, /*22 used by PLCM_MF_Initialize, example is enable */
        SYSTEM_CONFIGURATION_LOGLEVEL, /*23 used by PLCM_MF_Initialize, default value is INFO*/
        SYSTEM_USER_AGENT, /*24 Specifies the user agent name, default is MFW_SDK*/

        PLCM_MFW_KVLIST_ICE_UserName,    /*25 ICE username*/
        PLCM_MFW_KVLIST_ICE_Password,    /*26 ICE password*/
        PLCM_MFW_KVLIST_ICE_TCPServer,  /*27 ICE TCP server like domain/domain:port/ip:port*/
        PLCM_MFW_KVLIST_ICE_UDPServer,  /*28 ICE UDP server like domain/domain:port/ip:port*/
        PLCM_MFW_KVLIST_ICE_TLSServer,  /*29 ICE TLS server */
        PLCM_MFW_KVLIST_ICE_Enable,        /*30 ICE enable, default is true*/
        PLCM_MFW_KVLIST_ICE_AUTHTOKEN_Enable, /*31*/
        PLCM_MFW_KVLIST_ICE_INIT_AUTHTOKEN, /*32*/
        PLCM_MFW_KVLIST_ICE_RTO,                  /*33 ICE RTO parameter. */
        PLCM_MFW_KVLIST_ICE_RC,                    /*34 ICE RC parameter */
        PLCM_MFW_KVLIST_ICE_RM,                    /*35 ICE RM parameter */
        PLCM_MFW_KVLIST_QOS_ServiceType,  /*36 < Qos service type. The value can be "IP_PRECEDENCE", "DIFFSERV". Not supported on Windows. */
        PLCM_MFW_KVLIST_QOS_Audio,              /*37 < Qos audio value. The value can be 0~255. Not supported on Windows. */
        PLCM_MFW_KVLIST_QOS_Video,              /*38 < Qos video value. The value can be 0~255. Not supported on Windows. */
        PLCM_MFW_KVLIST_QOS_Fecc,                /*39< Qos FECC value. The value can be 0~255. Not supported on Windows. */
        PLCM_MFW_KVLIST_QOS_Enable,            /*40 < Enable/ Disable Qos. The value can be "true" or "false". */
        PLCM_MFW_KVLIST_DBA_Enable,            /*41 < Enable/ Disable DBA.. The value can be "true" or "false". */
        PLCM_MFW_KVLIST_KEY_REG_ID,                /*42 */
        PLCM_MFW_KVLIST_LPR_Enable,            /*43*/
        PLCM_MFW_KVLIST_CERT_PATH,                                             /*44 Path of certificates. */
        PLCM_MFW_KVLIST_CERT_CHECKFQDN,                                        /*45  Check fqdn of certificate or not. */
        PLCM_MFW_KVLIST_HttpConnect_Enable,                            /*46 Enable/Disable Http connect. */
        PLCM_MFW_KVLIST_SIP_HttpProxyServer,                          /*47 SIP http proxy server. */
        PLCM_MFW_KVLIST_SIP_HttpProxyPort,                              /*48 SIP http proxy port.  */
        PLCM_MFW_KVLIST_SIP_HttpProxyUserName,                      /*49 SIP http proxy user name.  */
        PLCM_MFW_KVLIST_SIP_HttpPassword,                                /*50 SIP http proxy password.  */
        PLCM_MFW_KVLIST_ICE_HttpProxyServer,                          /*51 ICE http proxy server. */
        PLCM_MFW_KVLIST_ICE_HttpProxyPort,                              /*52 ICE http proxy port.  */
        PLCM_MFW_KVLIST_ICE_HttpProxyUserName,                      /*53 ICE http proxy user name.  */
        PLCM_MFW_KVLIST_ICE_HttpPassword,                                /*54 ICE http proxy password.  */
        PLCM_MFW_KVLIST_MEDIA_HttpProxyServer,                      /*55 MEDIA http proxy server. */
        PLCM_MFW_KVLIST_MEDIA_HttpProxyPort,                          /*56 MEDIA http proxy port. */
        PLCM_MFW_KVLIST_MEDIA_HttpProxyUserName,                  /*57 MEDIA http proxy username. */
        PLCM_MFW_KVLIST_MEDIA_HttpPassword,                       /*58 MEDIA http proxy password. */
        PLCM_MFW_PROCUT,                                                      /*59 Product name.  */
        PLCM_MFW_KVLIST_AutoZoom_Enable,                                  /*60 Enable/ Disable auto zoom. The value can be "true" or "false". */
        PLCM_MFW_KVLIST_TLSOffLoad_Enable,                              /*61 Enable*/
        PLCM_MFW_KVLIST_TLSOffLoad_Host,                                  /*62 TLS off-load host*/
        PLCM_MFW_KVLIST_TLSOffLoad_Port,                                  /*63 TLS off-load port*/
        PLCM_MFW_KVLIST_HttpTunnel_Enable,                              /*64 Enable/ Disable http tunnel. The value can be "true" or "false".*/
        PLCM_MFW_KVLIST_SIP_HttpTunnelProxyServer,              /*65 SIP http tunnel proxy server. */
        PLCM_MFW_KVLIST_SIP_HttpTunnelProxyPort,                  /*66 SIP http tunnel proxy port. */
        PLCM_MFW_KVLIST_MEDIA_HttpTunnelProxyServer,          /*67 Meida http tunnel proxy server. */
        PLCM_MFW_KVLIST_MEDIA_HttpTunnelProxyPort,              /*68 Meida http tunnel proxy port. */
        PLCM_MFW_KVLIST_RTPMode,                                                 /*69 RTP mode. The value can be TCP/RTP/AVP or RTP/AVP or All. */
        PLCM_MFW_KVLIST_TCPBFCPForced,                                     /*70 Enable/ Disable TCP/BFCP forced. The value can be "true" or "false".*/
        PLCM_MFW_KVLIST_G729B_Enable,                                              /*71< Enable/Disable G729B codec. The value can be "true" or "false".  */
        PLCM_MFW_KVLIST_SAML_Enable,                                                /*72 SAML*/
        PLCM_MFW_KVLIST_iLBCFrame,                                             /*73< Microsecond Frame for iLBC codec. The value can be "20" or "30".  Default value is 30. */
        PLCM_MFW_KVLIST_BFCP_CONTENT_Enable,                              /*74  Enable/Disable BFCP content. The value can be "true" or "false".*/
        PLCM_MFW_KVLIST_SUPPORT_PORTRAIT_MODE,                             /*75 Enable/Disable support portrait mode*/
        PLCM_MFW_KVLIST_KEY_DisplayName,                                     /*76 Display name for sip call */
        PLCM_MFW_KVLIST_FECC_Enable,                                          /*77 Enable/Disable FECC function. */
        PLCM_MFW_KVLIST_Comfortable_Noise_Enable,                 /*78 comfortable noise function */
        PLCM_MFW_KVLIST_SIP_Header_Compact_Enable,                /*79 SIP header compact function. */

        PLCM_MF_PROP_MAXSYS, /*72*/


        /*the followings are used by LAL*/
        PLCM_MF_PROP_LocalAddr,
        PLCM_MF_PROP_CalleeAddr,
        CALL_STATUS,
        TEST_string,
        AUDIO_INPUT_DEVICE,
        AUDIO_OUTPUT_DEVICE,
        AUDIO_OUTPUT_DEVICE_FOR_RINGTONE,
        VIDEO_INPUT_DEVICE,
        MONITOR_DEVICE,
        CALL_LIST_DISPLAY_NAME,
        IS_TALKING_LIST_SSRC,
        LOCAL_WINDOW_WIDTH,
        LOCAL_WINDOW_HEIGHT,
        REMOTE_WINDOW_WIDTH,
        REMOTE_WINDOW_HEIGHT,
        PIP_WINDOW_WIDTH,
        PIP_WINDOW_HEIGHT,
        CONTENT_WINDOW_WIDTH,
        CONTENT_WINDOW_HEIGHT,
        APPLICATIONS,

        /*Sound Effects*/
        SOUND_INCOMING,
        SOUND_CLOSED,
        SOUND_RINGING,
        SOUND_HOLD,

        //ICE token
        ICE_AUTH_TOKEN
    }

    public class PropertyEnumMap
    {
        private static ILog log = LogManager.GetLogger("PropertyEnumMap");

        private static Dictionary<PropertyEnum, string> _map = new Dictionary<PropertyEnum, string>()
        {
            {PropertyEnum.NULL,"null"}, /* 0 Just ignore this enum. */
	        //{PropertyEnum.PLCM_MF_PROP_MINSYS,"MINSYS"},
	        {PropertyEnum.SIP_PROXY_SERVER_ADDRESS,"sipProxyServer"}, /*1 used by PLCM_MF_Initialize and PLCM_MF_SetUserInfo, format is ipaddr, example:172.21.120.124 */
	        {PropertyEnum.PLCM_MFW_KVLIST_KEY_SIP_Transport,"transportProtocol"},  /*2 used by PLCM_MF_Initialize and PLCM_MF_SetUserInfo, format is UDP,TCP,TLS,AUTO */
	        {PropertyEnum.PLCM_MFW_KVLIST_KEY_SIP_ServerType,"sipServerType"}, /*3 used by PLCM_MF_Initialize and PLCM_MF_SetUSerInfo, format is STANDARD */
	        {PropertyEnum.PLCM_MFW_KVLIST_KEY_SIP_Register_Expires_Interval,"sipRegisterExpires"}, /*4, determine time interval of register in CC*/
	        {PropertyEnum.SIP_USERNAME,"sipUserName"},/* 5 used by PLCM_MF_Initialize and PLCM_MF_SetUserInfo, format example is sam@example.com */
	        {PropertyEnum.SIP_DOMAIN,"sipUserDomain"},/*6 used by PLCM_MF_Initialize and PLCM_MF_SetUserInfo */
	        {PropertyEnum.SIP_AUTHORIZATION_NAME,"sipAuthorizationName"},  /*7 used by PLCM_MF_Initialize and PLCM_MF_SetUserInfo */
	        {PropertyEnum.SIP_PASSWORD,"sipPassword"},/*8 used by PLCM_MF_Initialize and PLCM_MF_SetUserInfo */
	        {PropertyEnum.PLCM_MFW_KVLIST_KEY_SIP_CookieHead,"sipCookieHead"},/*9 cookie head */
	        {PropertyEnum.PLCM_MFW_KVLIST_KEY_SIP_Base_Cred,"sipBaseCred"},/*10 Base authentication head */
	        {PropertyEnum.PLCM_MFW_KVLIST_KEY_SIP_AnonymousToken_Cred,"sipAnonyTokenCred"}, /*11,Anonymous-Token cred*/
	        {PropertyEnum.PLCM_MFW_KVLIST_KEY_SIP_Anonymous_Cred,"sipAnonyCred"}, /*12, Anonymous cred*/
	        {PropertyEnum.PLCM_MFW_KVLIST_KEY_CallSettings_MaxCallNum,"callSettingsMaxSupportCallNumber"}, /*13 The max support of call number at a time, max number is 16. */
	        {PropertyEnum.CALL_SETTINGS_NETWORK_CALLRATE,"callSettingsNetworkCallRate"},/*14 used by PLCM_MF_Initialize and PLCM_MFW_PlaceCall, example format is 384 */
	        {PropertyEnum.CALL_SETTINGS_AES_ENCRIPTION,"callSettingsAESEncription"},/*15 used by PLCM_MF_Initialize */

	        //{PropertyEnum.PLCM_MFW_KVLIST_KEY_CallSettings_CryptoSuiteType,"CryptoSuiteType"},/*13 < Set SRTP crypto suite type if AesEcription is "on" or "auto". */
            //{PropertyEnum.PLCM_MFW_KVLIST_KEY_CallSettings_SrtpKey,"SrtpKey"},/*14 < Set SRTP crypto key if AesEcription is "on" or "auto" and CryptoSuiteType has value. */
	        {PropertyEnum.CALL_SETTINGS_DEFAULT_Audio_START_PORT,"callSettingsDefaultAudioStartPort"},/*16 used by PLCM_MF_Initialize, example is 3230 */
	        {PropertyEnum.CALL_SETTINGS_DEFAULT_Audio_END_PORT,"callSettingsDefaultAudioEndPort"},/*17 used by PLCM_MF_Initialize, example is 3550 */
	        {PropertyEnum.CALL_SETTINGS_DEFAULT_Video_START_PORT,"callSettingsDefaultVideoStartPort"},/*18 used by PLCM_MF_Initialize, example is 3230 */
	        {PropertyEnum.CALL_SETTINGS_DEFAULT_Video_END_PORT,"callSettingsDefaultVideoEndPort"},/*19 used by PLCM_MF_Initialize, example is 3550 */
	        {PropertyEnum.PLCM_MFW_KVLIST_KEY_CallSettings_SIPClientListeningPort,"callSettingsSIPClientListeningPort"}, /*20*/
	        {PropertyEnum.PLCM_MFW_KVLIST_KEY_CallSettings_SIPClientListeningTLSPort,"callSettingsSIPClientListeningTLSPort"}, /*21*/

	        {PropertyEnum.CALL_SETTINGS_ENABLE_SVC,"callSettingsEnableSVC"}, /*22 used by PLCM_MF_Initialize, example is enable */
	        {PropertyEnum.SYSTEM_CONFIGURATION_LOGLEVEL,"systemConfiglogLevel"}, /*23 used by PLCM_MF_Initialize, default value is INFO*/
	        {PropertyEnum.SYSTEM_USER_AGENT,"systemUserAgent"}, /*24 Specifies the user agent name, default is MFW_SDK*/

	        {PropertyEnum.PLCM_MFW_KVLIST_ICE_UserName,"ICEUserName"}, 	/*25 ICE username*/
	        {PropertyEnum.PLCM_MFW_KVLIST_ICE_Password,"ICEPassword"}, 	/*26 ICE password*/
	        {PropertyEnum.PLCM_MFW_KVLIST_ICE_TCPServer,"ICETCPServer"}, 	/*27 ICE TCP server like domain/domain:port/ip:port*/
	        {PropertyEnum.PLCM_MFW_KVLIST_ICE_UDPServer,"ICEUDPServer"}, 	/*28 ICE UDP server like domain/domain:port/ip:port*/
	        {PropertyEnum.PLCM_MFW_KVLIST_ICE_TLSServer,"ICETLSServer"},	/*29 ICE TLS server */
	        {PropertyEnum.PLCM_MFW_KVLIST_ICE_Enable,"ICEEnable"},		/*30 ICE enable, default is true*/
	        {PropertyEnum.PLCM_MFW_KVLIST_ICE_AUTHTOKEN_Enable,"ICETokenEnableId"}, /*31*/
	        {PropertyEnum.PLCM_MFW_KVLIST_ICE_INIT_AUTHTOKEN,"sipICEInitAuthToken"}, /*32*/
	        {PropertyEnum.PLCM_MFW_KVLIST_ICE_RTO,"ICERto"},					/*33 ICE RTO parameter. */
	        {PropertyEnum.PLCM_MFW_KVLIST_ICE_RC,"ICERc"},					/*34 ICE RC parameter */
	        {PropertyEnum.PLCM_MFW_KVLIST_ICE_RM,"ICERm"},					/*35 ICE RM parameter */	
	        {PropertyEnum.PLCM_MFW_KVLIST_QOS_ServiceType,"QOSServiceType"},	/*36 < Qos service type. The value can be "IP_PRECEDENCE", "DIFFSERV". Not supported on Windows. */
	        {PropertyEnum.PLCM_MFW_KVLIST_QOS_Audio,"QOSAudio"},				/*37 < Qos audio value. The value can be 0~255. Not supported on Windows. */
	        {PropertyEnum.PLCM_MFW_KVLIST_QOS_Video,"QOSVideo"}, 				/*38 < Qos video value. The value can be 0~255. Not supported on Windows. */
	        {PropertyEnum.PLCM_MFW_KVLIST_QOS_Fecc,"QOSFecc"}, 				/*39< Qos FECC value. The value can be 0~255. Not supported on Windows. */
	        {PropertyEnum.PLCM_MFW_KVLIST_QOS_Enable,"QOSEnable"},			/*40 < Enable/ Disable Qos. The value can be "true" or "false". */
	        {PropertyEnum.PLCM_MFW_KVLIST_DBA_Enable,"DBAEnable"},			/*41 < Enable/ Disable DBA.. The value can be "true" or "false". */
	        {PropertyEnum.PLCM_MFW_KVLIST_KEY_REG_ID,"RegId"},				/*42 */
	        {PropertyEnum.PLCM_MFW_KVLIST_LPR_Enable,"LPREnable"}, 			/*43*/
            {PropertyEnum.PLCM_MFW_KVLIST_CERT_PATH,"CrertPath"},                                           	/*44 Path of certificates. */
            {PropertyEnum.PLCM_MFW_KVLIST_CERT_CHECKFQDN,"CheckFQDN"},                                     	/*45  Check fqdn of certificate or not. */
            {PropertyEnum.PLCM_MFW_KVLIST_HttpConnect_Enable,"HttpConnectEnable"},                            /*46 Enable/Disable Http connect. */
            {PropertyEnum.PLCM_MFW_KVLIST_SIP_HttpProxyServer,"SIPHttpProxyServer"},                          /*47 SIP http proxy server. */
            {PropertyEnum.PLCM_MFW_KVLIST_SIP_HttpProxyPort,"SIPHttpProxyPort"},                              /*48 SIP http proxy port.  */
            {PropertyEnum.PLCM_MFW_KVLIST_SIP_HttpProxyUserName,"SIPHttpProxyUserName"},                      /*49 SIP http proxy user name.  */
            {PropertyEnum.PLCM_MFW_KVLIST_SIP_HttpPassword,"SIPHttpPassword"},                                /*50 SIP http proxy password.  */
            {PropertyEnum.PLCM_MFW_KVLIST_ICE_HttpProxyServer,"ICEHttpProxyServer"},                          /*51 ICE http proxy server. */
            {PropertyEnum.PLCM_MFW_KVLIST_ICE_HttpProxyPort,"ICEHttpProxyPort"},                              /*52 ICE http proxy port.  */
            {PropertyEnum.PLCM_MFW_KVLIST_ICE_HttpProxyUserName,"ICEHttpProxyUserName"},                      /*53 ICE http proxy user name.  */
            {PropertyEnum.PLCM_MFW_KVLIST_ICE_HttpPassword,"ICEHttpPassword"},                                /*54 ICE http proxy password.  */
            {PropertyEnum.PLCM_MFW_KVLIST_MEDIA_HttpProxyServer,"MediaHttpProxyServer"},						/*55 MEDIA http proxy server. */
            {PropertyEnum.PLCM_MFW_KVLIST_MEDIA_HttpProxyPort,"MediaHttpProxyPort"},							/*56 MEDIA http proxy port. */
            {PropertyEnum.PLCM_MFW_KVLIST_MEDIA_HttpProxyUserName,"MediaHttpProxyUserName"},                  /*57 MEDIA http proxy username. */           
            {PropertyEnum.PLCM_MFW_KVLIST_MEDIA_HttpPassword,"MediaHttpProxyPassword"},                       /*58 MEDIA http proxy password. */            
            {PropertyEnum.PLCM_MFW_PROCUT,"MFWProduct"},														/*59 Product name.  */
            {PropertyEnum.PLCM_MFW_KVLIST_AutoZoom_Enable,"AutoZoomEnable"},									/*60 Enable/ Disable auto zoom. The value can be "true" or "false". */
            {PropertyEnum.PLCM_MFW_KVLIST_TLSOffLoad_Enable,"TLSOffLoadEnable"},                              /*61 Enable*/                      
            {PropertyEnum.PLCM_MFW_KVLIST_TLSOffLoad_Host,"TLSOffLoadHost"},                                  /*62 TLS off-load host*/
            {PropertyEnum.PLCM_MFW_KVLIST_TLSOffLoad_Port,"TLSOffLoadPort"},                                  /*63 TLS off-load port*/
            {PropertyEnum.PLCM_MFW_KVLIST_HttpTunnel_Enable,"HttpTunnelEnable"},								/*64 Enable/ Disable http tunnel. The value can be "true" or "false".*/
            {PropertyEnum.PLCM_MFW_KVLIST_SIP_HttpTunnelProxyServer,"SIPHttpTunnelProxyServer"},				/*65 SIP http tunnel proxy server. */
            {PropertyEnum.PLCM_MFW_KVLIST_SIP_HttpTunnelProxyPort,"SIPHttpTunnelProxyPort"},					/*66 SIP http tunnel proxy port. */
            {PropertyEnum.PLCM_MFW_KVLIST_MEDIA_HttpTunnelProxyServer,"MediaHttpTunnelProxyServer"},			/*67 Meida http tunnel proxy server. */
            {PropertyEnum.PLCM_MFW_KVLIST_MEDIA_HttpTunnelProxyPort,"MediaHttpTunnelProxyPort"},				/*68 Meida http tunnel proxy port. */
            {PropertyEnum.PLCM_MFW_KVLIST_RTPMode,"RTPMode"},													/*69 RTP mode. The value can be TCP/RTP/AVP or RTP/AVP or All. */
            {PropertyEnum.PLCM_MFW_KVLIST_TCPBFCPForced,"TCPBFCPForced"},										/*70 Enable/ Disable TCP/BFCP forced. The value can be "true" or "false".*/
            {PropertyEnum.PLCM_MFW_KVLIST_G729B_Enable,"G729B"},												/*71< Enable/Disable G729B codec. The value can be "true" or "false".  */
            {PropertyEnum.PLCM_MFW_KVLIST_SAML_Enable,"SAML"},												/*72 SAML*/
            {PropertyEnum.PLCM_MFW_KVLIST_iLBCFrame,"iLBCFrame"},                                             /*73< Microsecond Frame for iLBC codec. The value can be "20" or "30".  Default value is 30. */ 											
 	        {PropertyEnum.PLCM_MFW_KVLIST_BFCP_CONTENT_Enable,"CONTENT_Enable"},								/*74  Enable/Disable BFCP content. The value can be "true" or "false".*/
            {PropertyEnum.PLCM_MFW_KVLIST_SUPPORT_PORTRAIT_MODE,"PORTRAIT_MODE"},								/*75 Enable/Disable support portrait mode*/
            {PropertyEnum.PLCM_MFW_KVLIST_KEY_DisplayName,"DisplayName"},    									/*76 Display name for sip call */
            {PropertyEnum.PLCM_MFW_KVLIST_FECC_Enable,"FECCEnable"},											/*77 Enable/Disable FECC function. */
            {PropertyEnum.PLCM_MFW_KVLIST_Comfortable_Noise_Enable,"ComfortableNoiseEnable"},					/*78 comfortable noise function */
            {PropertyEnum.PLCM_MFW_KVLIST_SIP_Header_Compact_Enable,"SIPHeaderCompactEnable"},				/*79 SIP header compact function. */

            {PropertyEnum.PLCM_MF_PROP_MAXSYS,"MAXSYS"}, /*72*/


            /*the followings are used by LAL*/
            {PropertyEnum.PLCM_MF_PROP_LocalAddr,"LocalAddr"},
            {PropertyEnum.PLCM_MF_PROP_CalleeAddr,"Callee"},
            {PropertyEnum.CALL_STATUS,"CALL_STATUS"},
            {PropertyEnum.TEST_string,"TEST_string"},
            {PropertyEnum.AUDIO_INPUT_DEVICE,"AudioInputDevice"},
            {PropertyEnum.AUDIO_OUTPUT_DEVICE,"AudioOutputDevice"},
            {PropertyEnum.AUDIO_OUTPUT_DEVICE_FOR_RINGTONE,"AudioOutputDeviceForRingtone"},
            {PropertyEnum.VIDEO_INPUT_DEVICE,"VideoInputDevice"},
            {PropertyEnum.MONITOR_DEVICE,"monitorDevice"},
            {PropertyEnum.CALL_LIST_DISPLAY_NAME,"CallListDisplayName"},
            {PropertyEnum.IS_TALKING_LIST_SSRC,"IsTalkingListSSRC"},
            {PropertyEnum.LOCAL_WINDOW_WIDTH,"localWindowWidth"},
            {PropertyEnum.LOCAL_WINDOW_HEIGHT,"localWindowHeight"},
            {PropertyEnum.REMOTE_WINDOW_WIDTH,"remoteWindowWidth"},
            {PropertyEnum.REMOTE_WINDOW_HEIGHT,"remoteWindowHeight"},
            {PropertyEnum.PIP_WINDOW_WIDTH,"PIPWindowWidth"},
            {PropertyEnum.PIP_WINDOW_HEIGHT,"PIPWindowHeight"},
            {PropertyEnum.CONTENT_WINDOW_WIDTH,"contentWindowWidth"},
            {PropertyEnum.CONTENT_WINDOW_HEIGHT,"contentWindowHeight"},
            {PropertyEnum.APPLICATIONS,"applocations"},

	        /*Sound Effects*/
	        {PropertyEnum.SOUND_INCOMING,"IncomingSound"},
            {PropertyEnum.SOUND_CLOSED,"ClosedSound"},
            {PropertyEnum.SOUND_RINGING,"RingingSound"},
            {PropertyEnum.SOUND_HOLD,"HoldSound"},

	        //ICE token
	        {PropertyEnum.ICE_AUTH_TOKEN,"ICEAuthToken"}
    };

        public static string GetPropertyEnumText(PropertyEnum propertyEnum)
        {
            if (_map.ContainsKey(propertyEnum))
            {
                return _map[propertyEnum];
            }
            throw new Exception("Not Found!");
        }

        public static PropertyEnum GetPropertyEnum(string text)
        {
            if (_map.Values.Contains(text))
            {
                var kv = _map.Where(m => m.Value == text).First();
                return kv.Key;
            }

            throw new Exception("Not Found!");
        }
        

        public static Dictionary<PropertyEnum, string> GetPropertiesMaps()
        {
            return _map;
        }
    }
}
