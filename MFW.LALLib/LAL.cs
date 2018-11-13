using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFW.LALLib
{
    public class LAL
    {
        #region Fields
        private static ILog log = LogManager.GetLogger("LAL");
        private static LALProperties lalProperties = LALProperties.GetInstance();
        private static ConfigurationManager confManager = ConfigurationManager.GetInstance();
        private static DeviceManager deviceManager = DeviceManager.GetInstance();
        private static CallManager callManager = CallManager.GetInstance();
        private static EventMonitor eventMonitor = EventMonitor.GetInstance();
        private const int invalidCallHandle = -1;
        #endregion

        #region Callback        
        private static AddEventCallback addEventCallback = new AddEventCallback(AddEventCallbackF);
        private static DispatchEventsCallback dispatchEventsCallback = new DispatchEventsCallback(DispatchEventsCallbackF);
        private static AddLogCallback addLogCallback = new AddLogCallback(AddLogCallbackF);
        private static AddDeviceCallback addDeviceCallback = new AddDeviceCallback(AddDeviceCallbackF);
        private static DisplayMediaStatisticsCallback displayMediaStatisticsCallback = new DisplayMediaStatisticsCallback(DisplayMediaStatisticsCallbackF);
        private static DisplayCallStatisticsCallback displayCallStatisticsCallback = new DisplayCallStatisticsCallback(DisplayCallStatisticsCallbackF);
        private static DisplayCodecCapabilities displayCodecCapabilities = new DisplayCodecCapabilities(DisplayCodecCapabilitiesF);
        private static AddAppCallback addAppCallback = new AddAppCallback(AddAppCallbackF);

        public static void AddEventCallbackF(IntPtr eventHandle, int callHandle, IntPtr placeId, int eventType, IntPtr callerName,
                IntPtr calleeName, int userCode, IntPtr reason, int wndWidth, int wndHeight, bool plugDeviceStatus, IntPtr plugDeviceName, IntPtr deviceHandle, IntPtr ipAddress, int callMode,
                int streamId, int activeSpeakerStreamId, int remoteVideoChannelNum, IntPtr remoteChannelDisplayName, bool isActiveSpeaker, int isTalkingFlag, IntPtr regID, IntPtr sipCallId, IntPtr version, IntPtr serialNumber, IntPtr notBefore, IntPtr notAfter,
                IntPtr issuer, IntPtr subject, IntPtr signatureAlgorithm, IntPtr fingerPrintAlgorithm, IntPtr fingerPrint, IntPtr publickey, IntPtr basicContraints, IntPtr keyUsage, IntPtr rxtendedKeyUsage,
                IntPtr subjectAlternateNames, IntPtr pemCert, bool isCertHostNameOK, int certFailReason, int certConfirmId, IntPtr transcoderTaskId, IntPtr transcoderInputFileName, int iceStatus, IntPtr sutLiteMessage, bool isVideoOK, IntPtr mediaIPAddr, int discoveryStatus)
        {
            // add Event to EventMonitor
            var strplaceId = IntPtrHelper.IntPtrTostring(placeId);
            var strcallerName = IntPtrHelper.IntPtrTostring(callerName);
            var strcalleeName = IntPtrHelper.IntPtrTostring(calleeName);
            var strreason = IntPtrHelper.IntPtrTostring(reason);
            var strplugDeviceName = IntPtrHelper.IntPtrTostring(plugDeviceName);
            var strdeviceHandle = IntPtrHelper.IntPtrTostring(deviceHandle);
            var stripAddress = IntPtrHelper.IntPtrTostring(ipAddress);
            var strremoteChannelDisplayName = IntPtrHelper.IntPtrTostring(remoteChannelDisplayName);
            var strregID = IntPtrHelper.IntPtrTostring(regID);
            var strsipCallId = IntPtrHelper.IntPtrTostring(sipCallId);
            var strVersion = IntPtrHelper.IntPtrTostring(version);
            var strSerialNumber = IntPtrHelper.IntPtrTostring(serialNumber);
            var strNotBefore = IntPtrHelper.IntPtrTostring(notBefore);
            var strNotAfter = IntPtrHelper.IntPtrTostring(notAfter);
            var strIssuer = IntPtrHelper.IntPtrTostring(issuer);
            var strSubject = IntPtrHelper.IntPtrTostring(subject);
            var strSignatureAlgorithm = IntPtrHelper.IntPtrTostring(signatureAlgorithm);
            var strFingerPrintAlgorithm = IntPtrHelper.IntPtrTostring(fingerPrintAlgorithm);
            var strFingerPrint = IntPtrHelper.IntPtrTostring(fingerPrint);
            var strPublickey = IntPtrHelper.IntPtrTostring(publickey);
            var strBasicContraints = IntPtrHelper.IntPtrTostring(basicContraints);
            var strKeyUsage = IntPtrHelper.IntPtrTostring(keyUsage);
            var strExtendedKeyUsage = IntPtrHelper.IntPtrTostring(rxtendedKeyUsage);
            var strSubjectAlternateNames = IntPtrHelper.IntPtrTostring(subjectAlternateNames);
            var strPemCert = IntPtrHelper.IntPtrTostring(pemCert);
            var strtranscoderInputFileName = IntPtrHelper.IntPtrTostring(transcoderInputFileName);
            var strSUTLiteMessage = IntPtrHelper.IntPtrTostring(sutLiteMessage);
            var strMediaIPAddr = IntPtrHelper.IntPtrTostring(mediaIPAddr);
            Event evt = new Event(eventHandle, callHandle, strplaceId, (EventTypeEnum)eventType, strcallerName,
                    strcalleeName, userCode, strreason,
                    wndWidth, wndHeight, plugDeviceStatus, strplugDeviceName, strdeviceHandle, stripAddress, (CallModeEnum)callMode,
                    streamId, activeSpeakerStreamId, remoteVideoChannelNum, strremoteChannelDisplayName, isActiveSpeaker, isTalkingFlag, strregID, strsipCallId,
                    strVersion,
                    strSerialNumber,
                    strNotBefore,
                    strNotAfter,
                    strIssuer,
                    strSubject,
                    strSignatureAlgorithm,
                    strFingerPrintAlgorithm,
                    strFingerPrint,
                    strPublickey,
                    strBasicContraints,
                    strKeyUsage,
                    strExtendedKeyUsage,
                    strSubjectAlternateNames,
                    strPemCert,
                    isCertHostNameOK,
                    certFailReason,
                    certConfirmId,
                    transcoderTaskId,
                    strtranscoderInputFileName,
                    (ICEStatusEnum)iceStatus,
                    strSUTLiteMessage,
                    isVideoOK,
                    strMediaIPAddr,
                    (AutoDiscoveryStatusEnum)discoveryStatus);
            eventMonitor.AddEvent(evt);
        }

        public static void DispatchEventsCallbackF()
        {
            // tell EventMonitor to dispatch Event
            eventMonitor.DispatchEvents();
        }

        public static void AddLogCallbackF(ulong timestamp, bool expired, int funclevel, ulong pid, ulong tid, IntPtr lev, IntPtr comp, IntPtr msg, int len)
        {
            var output = string.Empty;
            var level = IntPtrHelper.IntPtrToUTF8string(lev);
            var component = IntPtrHelper.IntPtrTostring(comp);
            var message = IntPtrHelper.IntPtrTostring(msg);
            if (string.IsNullOrEmpty(component))
            {
                component = "wrapper";
            }

            output += string.Format("[PID:{0}][TID:{1}] ", pid, tid); ;

            for (int i = 0; i < funclevel; i++)
            {
                output += "--";
            }
            output += level.ToString();

            if (level == "DEBUG")
            {
                log.Debug(output);
            }
            else if (level == "INFO")
            {
                log.Info(output);
            }
            else if (level == "WARN")
            {
                log.Warn(output);
            }
            else if (level == "ERROR")
            {
                log.Error(output);
            }
            else
            {
                log.Fatal(output);
            }
        }

        public static void AddAppCallbackF(IntPtr appHandle, IntPtr appNamePtr)
        {
            var appName = IntPtrHelper.IntPtrTostring(appNamePtr);
            Device device = null;
            string resultDeviceName = appName;
            device = new Device(DeviceTypeEnum.APPLICATIONS, null, resultDeviceName, appHandle);
            deviceManager.AddApp(device);/*add device to device manger's hash table*/
            log.Info("application name is " + device.DeviceName);
            // lalProperties.displayProperty(PropertyEnum.APPLICATIONS, device.getDeviceName());
        }


        public static void AddDeviceCallbackF(int deviceType, IntPtr deviceHandlePtr, IntPtr deviceNamePtr)
        {
            var deviceHandle = IntPtrHelper.IntPtrToUTF8string(deviceHandlePtr);
            var deviceName = IntPtrHelper.IntPtrToUTF8string(deviceNamePtr);
            log.Info("AddDeviceCallback: deviceType:" + deviceType + "  deviceHandle:" + deviceHandle + "  deviceName:" + deviceName);
            Device device = null;
            string resultDeviceName = deviceName + "(" + deviceManager.GetDeviceCount() + ")";
            device = new Device((DeviceTypeEnum)deviceType, deviceHandle, resultDeviceName, IntPtr.Zero);
            if (null != device)
            {
                deviceManager.AddDevice(device);/*add device to device manger's hash table*/
            }
            else
            {
                log.Error("add a null device");
            }
        }


        public static void DisplayMediaStatisticsCallbackF(IntPtr channelNamePtr, IntPtr strParticipantNamePtr, IntPtr remoteSystemIdPtr, IntPtr callRatePtr, IntPtr packetsLostPtr, IntPtr packetLossPtr,
                IntPtr videoProtocolPtr, IntPtr videoRatePtr, IntPtr videoRateUsedPtr, IntPtr videoFrameRatePtr, IntPtr videoPacketsLostPtr, IntPtr videoJitterPtr,
                IntPtr videoFormatPtr, IntPtr errorConcealmentPtr, IntPtr audioProtocolPtr, IntPtr audioRatePtr, IntPtr audioPacketsLostPtr, IntPtr audioJitterPtr,
                IntPtr audioEncryptPtr, IntPtr videoEncryptPtr, IntPtr feccEncryptPtr, IntPtr audioReceivedPacketPtr, IntPtr roundTripTimePtr,
                IntPtr fullIntraFrameRequestPtr, IntPtr intraFrameSentPtr, IntPtr packetsCountPtr, IntPtr overallCPULoadPtr, IntPtr channelNumPtr)
        {
            var channelName = IntPtrHelper.IntPtrTostring(channelNamePtr);
            var strParticipantName = IntPtrHelper.IntPtrTostring(strParticipantNamePtr);
            var remoteSystemId = IntPtrHelper.IntPtrTostring(remoteSystemIdPtr);
            var callRate = IntPtrHelper.IntPtrTostring(callRatePtr);
            var packetsLost = IntPtrHelper.IntPtrTostring(packetsLostPtr);
            var packetLoss = IntPtrHelper.IntPtrTostring(packetLossPtr);
            var videoProtocol = IntPtrHelper.IntPtrTostring(videoProtocolPtr);
            var videoRate = IntPtrHelper.IntPtrTostring(videoRatePtr);
            var videoRateUsed = IntPtrHelper.IntPtrTostring(videoRateUsedPtr);
            var videoFrameRate = IntPtrHelper.IntPtrTostring(videoFrameRatePtr);
            var videoPacketsLost = IntPtrHelper.IntPtrTostring(videoPacketsLostPtr);
            var videoJitter = IntPtrHelper.IntPtrTostring(videoJitterPtr);
            var videoFormat = IntPtrHelper.IntPtrTostring(videoFormatPtr);
            var errorConcealment = IntPtrHelper.IntPtrTostring(errorConcealmentPtr);
            var audioProtocol = IntPtrHelper.IntPtrTostring(audioProtocolPtr);
            var audioRate = IntPtrHelper.IntPtrTostring(audioRatePtr);
            var audioPacketsLost = IntPtrHelper.IntPtrTostring(audioPacketsLostPtr);
            var audioJitter = IntPtrHelper.IntPtrTostring(audioJitterPtr);
            var audioEncrypt = IntPtrHelper.IntPtrTostring(audioEncryptPtr);
            var videoEncrypt = IntPtrHelper.IntPtrTostring(videoEncryptPtr);
            var feccEncrypt = IntPtrHelper.IntPtrTostring(feccEncryptPtr);
            var audioReceivedPacket = IntPtrHelper.IntPtrTostring(audioReceivedPacketPtr);
            var roundTripTime = IntPtrHelper.IntPtrTostring(roundTripTimePtr);
            var fullIntraFrameRequest = IntPtrHelper.IntPtrTostring(fullIntraFrameRequestPtr);
            var intraFrameSent = IntPtrHelper.IntPtrTostring(intraFrameSentPtr);
            var packetsCount = IntPtrHelper.IntPtrTostring(packetsCountPtr);
            var overallCPULoad = IntPtrHelper.IntPtrTostring(overallCPULoadPtr);
            var channelNum = IntPtrHelper.IntPtrTostring(channelNumPtr);
            /*
            MediaStatistics mediaStatistics = new MediaStatistics(new string[] { channelName, strParticipantName, remoteSystemId, callRate, packetsLost, packetLoss,
                    videoProtocol, videoRate, videoRateUsed, videoFrameRate, videoPacketsLost, videoJitter,
                    videoFormat, errorConcealment, audioProtocol, audioRate, audioPacketsLost, audioJitter,
                    audioEncrypt, videoEncrypt, feccEncrypt, audioReceivedPacket, roundTripTime, fullIntraFrameRequest, intraFrameSent, packetsCount, overallCPULoad, channelNum });
            log.Info("media channel number is " + mediaStatistics.getChannelNum());
            if (0 != mediaStatistics.getChannelNum())
            {
                mediaStatisticsDisplay.displayMediaStatistics(mediaStatistics);
            }
            */
        }
        public static void DisplayCallStatisticsCallbackF(int timeInLastCall, int totalTime, int callPlaced, int callReceived, int callConnected)
        {
            /*
            CallStatistics callStatistics = new CallStatistics(timeInLastCall, totalTime, callPlaced, callReceived, callConnected);
            callStatisticsDisplay.displayCallStatistics(callStatistics);
            */
        }

        public static void DisplayCodecCapabilitiesF(IntPtr typePtr, IntPtr codecNamePtr)
        {
            /*
            var type = IntPtrHelper.IntPtrTostring(typePtr);
            var codecName = IntPtrHelper.IntPtrTostring(codecNamePtr);
            if (type == "audio")
            {
                codecNameDisplay.displayAudioCodec(codecName);
            }
            else
            {
                codecNameDisplay.displayVideoCodec(codecName);
            }
            */
        }
        #endregion

        #region Init
        public static bool Initialize()
        {
            var errNo = ErrorNumberEnum.PLCM_SAMPLE_OK;
            /*PreInitialize*/
            errNo = WrapperProxy.InstallCallback(addEventCallback, dispatchEventsCallback, addLogCallback, addDeviceCallback,
                    displayMediaStatisticsCallback, displayCallStatisticsCallback, displayCodecCapabilities, addAppCallback);
            if (ErrorNumberEnum.PLCM_SAMPLE_OK != errNo)
            {
                log.Error("Register callback functions failed. Error number = " + errNo.ToString());
                return false;
            }

            /*in wrapper, create instances, load library*/
            errNo = WrapperProxy.PreInitialize();
            if (ErrorNumberEnum.PLCM_SAMPLE_OK != errNo)
            {
                var msg = "Pre-initialization failed. Error number = " + errNo.ToString();
                log.Error(msg);
                throw new Exception(msg);
            }

            /* KVList*/
            confManager.LoadFromXML(@"conf\cfg.xml", @"conf\common.xml");
            LoadSettingsFromConfFile(true);


            errNo =WrapperProxy.Initialize();
            if (ErrorNumberEnum.PLCM_SAMPLE_OK != errNo)
            {
                log.Error("Initialize failed. Error number = " + errNo.ToString());
                return false;
            }
            var version = WrapperProxy.GetVersion();
            log.Info("**********************************************************************");
            log.Info("        PLCM MFW Sample App Initialized Successful ( version: " + version + " )");
            log.Info("**********************************************************************");

            /*get devices enumerate*/
            
            return true;
        }
        public static void GetDevices()
        {
            var errNo = WrapperProxy.GetDeviceEnum(DeviceTypeEnum.AUDIO_INPUT);
            if (ErrorNumberEnum.PLCM_SAMPLE_OK != errNo)
            {
                log.Error("Get audio input device failed. Error number = " + errNo.ToString());
            }
            errNo = WrapperProxy.GetDeviceEnum(DeviceTypeEnum.VIDEO_INPUT);
            if (ErrorNumberEnum.PLCM_SAMPLE_OK != errNo)
            {
                log.Error("Get video input device failed. Error number = " + errNo.ToString());
            }
            errNo = WrapperProxy.GetDeviceEnum(DeviceTypeEnum.AUDIO_OUTPUT);
            if (ErrorNumberEnum.PLCM_SAMPLE_OK != errNo)
            {
                log.Error("Get audio output device failed. Error number = " + errNo.ToString());
            }
            errNo = WrapperProxy.GetDeviceEnum(DeviceTypeEnum.DEV_MONITOR);
            if (ErrorNumberEnum.PLCM_SAMPLE_OK != errNo)
            {
                log.Error("Get monitor device failed. Error number = " + errNo.ToString());
            }
        }
        public static void LoadSettingsFromConfFile(bool isUpdateKVList)
        {
            var errNo = ErrorNumberEnum.PLCM_SAMPLE_OK;
            var propertyMaps = PropertyEnumMap.GetPropertiesMaps();
            var properties = lalProperties.GetProperties();
            foreach (var map in propertyMaps)
            {
                var key = map.Key;
                var val = confManager.GetPropertyFromConfigFile(key);
                if (null != val)
                {
                    if (PropertyEnum.NULL != key)
                    {

                        switch (key)
                        {
                            case PropertyEnum.SIP_PROXY_SERVER_ADDRESS:
                            case PropertyEnum.SIP_USERNAME:
                            case PropertyEnum.SIP_PASSWORD:
                            case PropertyEnum.PLCM_MFW_KVLIST_KEY_REG_ID:
                                {
                                    val = string.Empty;
                                }
                                break;
                        }
                        lalProperties.SetProperty(key, val);
                        if (isUpdateKVList)
                        {
                            errNo = WrapperProxy.SetProperty(key, properties[key]);
                            if (ErrorNumberEnum.PLCM_SAMPLE_OK != errNo)
                            {
                                log.Error("AddProperty failed. Error number = " + errNo.ToString());
                                throw new Exception("AddProperty failed");
                            }
                        }
                    }
                    else
                    {
                        log.Error("no such setting in PropertyEnum for key" + key);
                        throw new Exception("no such setting in PropertyEnum for key" + key);
                    }
                }
            }
        }
        #endregion

        #region Register
        public static bool RegisterClient(string sipServer,string username,string password)
        {
            var regID= username + "@" + sipServer;

            log.Info("invoke register client");
            // log.Info("PLCM_MF_PROP_LocalAddr is " + lalProperties.getProperty(PropertyEnum.PLCM_MF_PROP_LocalAddr));
            log.Info("SIP_PROXY_SERVER_ADDRESS is " + sipServer);
            log.Info("SIP_USERNAME is " + username);
            log.Info("PLCM_MFW_KVLIST_KEY_REG_ID is " + regID);
            
            WrapperProxy.SetProperty(PropertyEnum.SIP_PROXY_SERVER_ADDRESS, sipServer);
            WrapperProxy.SetProperty(PropertyEnum.SIP_USERNAME, username);
            WrapperProxy.SetProperty(PropertyEnum.SIP_PASSWORD, password);
            WrapperProxy.SetProperty(PropertyEnum.PLCM_MFW_KVLIST_KEY_REG_ID, regID);
            WrapperProxy.UpdateConfig();
            var errNo = WrapperProxy.RegisterClient();
            log.Info("registerClient, registerClient errNo=" + errNo);

            if (ErrorNumberEnum.PLCM_SAMPLE_OK != errNo)
            {               
                log.Error("Register failed. Error number = " + errNo.ToString());
                return false;
            }
            lalProperties.SetProperty(PropertyEnum.SIP_PROXY_SERVER_ADDRESS, sipServer);
            lalProperties.SetProperty(PropertyEnum.SIP_USERNAME, username);
            lalProperties.SetProperty(PropertyEnum.SIP_PASSWORD, password);
            lalProperties.SetProperty(PropertyEnum.PLCM_MFW_KVLIST_KEY_REG_ID, regID);
            return true;
        }
        public static void UnregisterClient()
        {
            log.Info("invoke unregister client");
            log.Info("PLCM_MFW_KVLIST_KEY_REG_ID is " + lalProperties.GetProperty(PropertyEnum.PLCM_MFW_KVLIST_KEY_REG_ID));
            WrapperProxy.UnregisterClient();
        }
        #endregion
        #region Call

        /**
         * User agent dial out a call.
         * @return successful or failed.
         */
        public static ErrorNumberEnum Dial(string callAddr, CallModeEnum callMode)
        {
            log.Info("place call: callername:" + callAddr);
            int callHandleReference = 0;
            var errno = WrapperProxy.SetProperty(PropertyEnum.PLCM_MF_PROP_CalleeAddr, callAddr);
            if (errno != ErrorNumberEnum.PLCM_SAMPLE_OK)
            {
                return errno;
            }
            lalProperties.SetProperty(PropertyEnum.PLCM_MF_PROP_CalleeAddr, callAddr);
            errno = WrapperProxy.PlaceCall(callAddr, ref callHandleReference, callMode);
            if (errno == ErrorNumberEnum.PLCM_SAMPLE_OK)
            {
            }
            else
            {
                log.Error("Dial a Call failed. ErrorNum = " + errno.ToString());
                return errno;
            }
            return errno;
        }

        /**
         * User agent hang up the current call.
         * @param isAuto: true: when the call is not established, incoming another call, automatically hang up the
         * not-established call; false: user normally hang up a connected call
         * @return true: successful; false: fail
         */
        public static bool Hangup(Call  call)
        {
            var errno = WrapperProxy.TerminateCall(call.CallHandle);
            if (errno != ErrorNumberEnum.PLCM_SAMPLE_OK)
            {
                log.Error("Hangup a Call failed. ErrorNum = " + errno.ToString());
                return false;
            }
            return true;
        }
        public static bool Hangup(bool isAuto)
        {
            if (false == isAuto)
            {
                var call = callManager.CurrentCall;
                if ( null == call)
                {
                    log.Error("Terminating a NULL call.");
                    return false;
                }
                log.Info("normally terminate a call");
                var errno = WrapperProxy.TerminateCall(call.CallHandle);
                if (errno != ErrorNumberEnum.PLCM_SAMPLE_OK)
                {
                    log.Error("Hangup a Call failed. ErrorNum = " + errno.ToString());
                    return false;
                }
            }
            else
            {//automatically hang up a call
                var unestablishedCalls = callManager.GetUnestablishedCalls();
                foreach (var unestablishedCall in unestablishedCalls)
                {
                    log.Info("automaticall hang up a call");
                    var errno = WrapperProxy.TerminateCall(unestablishedCall.CallHandle);
                    if (errno != ErrorNumberEnum.PLCM_SAMPLE_OK)
                    {
                        log.Error("automatically Hangup a Call failed. ErrorNum = " + errno.ToString());
                        return false;
                    }
                }
            }
            return true;
        }

        /**
         * User agent hold the current call.
         * @param isAuto: true, this call is hold automatically, 
         *        for EX. when answer an incoming call, automatically hold the active call;
         *        otherwise, hold the current call
         * @return true: successful; false: fail
         */
        public static bool Hold(Call call)
        {
            var errno = WrapperProxy.HoldCall(call.CallHandle);
            if (ErrorNumberEnum.PLCM_SAMPLE_OK != errno)
            {
                log.Error("hold a call failed." + "callHanlde is: " + call.CallHandle +
                        ".Error number = " + errno.ToString());
                return false;
            }
            return true;
        }
        public static bool Hold(bool isAuto)
        {
            List<Call> calls = new List<Call>();
            if (true == isAuto)
            {
                log.Info("automatically hold the active or held call now");
                calls.AddRange(callManager.GetActiveCalls());
                calls.AddRange(callManager.GetHeldCalls());
            }
            else
            {
                log.Info(" hold the current call now");
                var currentCall = callManager.CurrentCall;
                if (null != currentCall)
                {
                    calls.Add(currentCall);
                }
            }
            foreach (var holdCall in calls)
            {
                var errno = WrapperProxy.HoldCall(holdCall.CallHandle);
                if (ErrorNumberEnum.PLCM_SAMPLE_OK != errno)
                {
                    log.Error("hold a call failed." + "callHanlde is: " + holdCall.CallHandle +
                            ".Error number = " + errno.ToString());
                    return false;
                }
            }
            return true;
        }
        /**
         * User agent resume the held current call.
         * @return true: successful; false: fail
         */
        public static bool Resume(Call call)
        {
            var errno = WrapperProxy.ResumeCall(call.CallHandle);
            if (ErrorNumberEnum.PLCM_SAMPLE_OK != errno)
            {
                log.Error("resume a call failed." + "callHanlde is: " + call.CallHandle +
                        ".Error number = " + errno.ToString());
                return false;
            }
            return true;
        }
        public static bool Resume()
        {
            var heldCalls = callManager.GetHeldCalls();
            foreach (var call in heldCalls)
            {
                var errno = WrapperProxy.ResumeCall(call.CallHandle);
                if (ErrorNumberEnum.PLCM_SAMPLE_OK != errno)
                {
                    log.Error("resume a call failed." + "callHanlde is: " + call.CallHandle +
                            ".Error number = " + errno.ToString());
                    return false;
                }
            }
            return true;
        }
        /**
         * User agent answer the incoming call.
         * @return true: successful; false: fail
         */
        public static bool Answer(Call call,CallModeEnum callMode, bool sutLiteEnable)
        {
            return Answer(call.CallHandle, callMode, sutLiteEnable);
        }
        public static bool Answer(int callHandle,CallModeEnum callMode, bool sutLiteEnable)
        {
            var errno = ErrorNumberEnum.PLCM_SAMPLE_OK;

            string CryptoSuiteType = "AES_CM_128_HMAC_SHA1_80";
            string SRTPKey = "HfVGG79oW5XStt9DewUYrdngYlV/QqDBGIDNFB7m";
            var m_authToken = "AApzdG1lZXRpbmcxAAdzdHVzZXIxAAABPcJe1o4CsXgvirq1RQys3JCU0U8RvJ4uoA==";
            log.Debug("[answer], CryptoSuiteType=" + CryptoSuiteType + "  SRTPKey:" + SRTPKey);
            errno = WrapperProxy.AnswerCall(callHandle, callMode, m_authToken, CryptoSuiteType, SRTPKey, sutLiteEnable);

            if (errno != ErrorNumberEnum.PLCM_SAMPLE_OK)
            {
                log.Error("Answer a Call failed. ErrorNum = " + errno.ToString());
                return false;
            }
            return true;
        }
        public static bool Answer(bool isVideo, bool sutLiteEnable)
        {
            var currentCall = callManager.CurrentCall;
            if (currentCall == null)
            {
                log.Error("answer a NULL call.");
                return false;
            }
            var errno = ErrorNumberEnum.PLCM_SAMPLE_OK;

            string CryptoSuiteType = "AES_CM_128_HMAC_SHA1_80";
            string SRTPKey = "HfVGG79oW5XStt9DewUYrdngYlV/QqDBGIDNFB7m";
            var m_authToken = "AApzdG1lZXRpbmcxAAdzdHVzZXIxAAABPcJe1o4CsXgvirq1RQys3JCU0U8RvJ4uoA==";
            log.Debug("[answer], CryptoSuiteType=" + CryptoSuiteType + "  SRTPKey:" + SRTPKey);
            if (isVideo)
            {
                errno = WrapperProxy.AnswerCall(currentCall.CallHandle, CallModeEnum.PLCM_MFW_AUDIOVIDEO_CALL, m_authToken, CryptoSuiteType, SRTPKey, sutLiteEnable);
            }
            else
            {
                errno = WrapperProxy.AnswerCall(currentCall.CallHandle, CallModeEnum.PLCM_MFW_AUDIO_CALL, m_authToken, CryptoSuiteType, SRTPKey, sutLiteEnable);
            }
            if (errno == ErrorNumberEnum.PLCM_SAMPLE_OK)
            {
                /*add the answered call to calllist*/
                callManager.AddCall(currentCall);
            }
            else
            {
                log.Error("Answer a Call failed. ErrorNum = " + errno.ToString());
                return false;
            }
            return true;
        }

        #endregion
        #region Mute
        /***
         * mute/unmute the mirophone
         * @para mute: true-mute the microphone; false-unmute the mircophone
         * @return true: successful; false-fail
         * */
        public static bool MuteMic(Call call, bool isMute)
        {
            log.Debug("[muteMic]");
            if (null == call)
            {
                log.Error("unmute/mute a NULL call.");
                return false;
            }
            var errno = WrapperProxy.MuteMic(call.CallHandle, isMute);
            if (errno != ErrorNumberEnum.PLCM_SAMPLE_OK)
            {
                log.Error("mute a Call failed. ErrorNum = " + errno.ToString());
                return false;
            }
            call.IsMute = isMute;
            return true;
        }

        public static bool MuteSpeaker(bool isMute)
        {
            log.Debug("[muteSpeaker]");
            var errno = WrapperProxy.MuteSpeaker(isMute);
            if (errno != ErrorNumberEnum.PLCM_SAMPLE_OK)
            {
                log.Error("mute speaker for a Call failed. ErrorNum = " + errno.ToString());
                return false;
            }
            return true;
        }

        /***
         * mute/unmute the localvideo
         * @para hide: true-hide the local video; false-display the local video
         * @return true: successful; false-fail
         * */
        public static bool MuteLocalVideo(bool isHide)
        {
            log.Debug("[muteLocalVideo]" + isHide);
            //isMuteLocalVideo = hide;
            var errno = WrapperProxy.MuteLocalVideo(isHide);
            if (errno != ErrorNumberEnum.PLCM_SAMPLE_OK)
            {
                log.Error("mute local video failed. ErrorNum = " + errno.ToString());
                return false;
            }
            return true;
        }
        #endregion

        #region Stream
        public static bool AttachStreamToWindow(IntPtr wndHandle, int callHandle, MediaTypeEnum mediaType, int streamId, int width, int height)
        {
            log.Debug("[attachStreamToWindow] callHandle:" + callHandle + " mediatype:" + mediaType.ToString() + " streamId:" + streamId + " to window(width: " + width + " height: " + height + " )");
            var errNo = WrapperProxy.AttachStreamWnd(mediaType, streamId, callHandle, wndHandle, 0, 0, width, height);
            if (ErrorNumberEnum.PLCM_SAMPLE_OK != errNo)
            {
                log.Error("attach stream to window failed. Error number = " + errNo.ToString());
                return false;
            }
            return true;
        }
        /**
         * detach stream from video window
         * @param mediaType: stream mediaType- local, remote, PIP and content
         * @return true: successfully detached; false: fail to detach
         * */
        public static bool DetachStreamFromWindow(MediaTypeEnum mediaType, int streamId, int callHandle)
        {
            log.Debug("[detachStreamFromWindow] mediatype " + mediaType.ToString() + " from video window.");
            if (mediaType != MediaTypeEnum.PLCM_MF_STREAM_LOCAL)
            {
                if (invalidCallHandle != callHandle)
                {
                    var errNo = WrapperProxy.DetachStreamWnd(mediaType, streamId, callHandle);
                    if (ErrorNumberEnum.PLCM_SAMPLE_OK != errNo)
                    {
                        log.Error("detach stream from window failed. Error number = " + errNo.ToString());
                        return false;
                    }
                }
                else
                {
                    log.Error("no such call to detach stream window");
                    return false;
                }
            }
            else
            {//for local preview
             //when the media type is local, set the call handle to -1 (invalid call handle)
                var errNo = WrapperProxy.DetachStreamWnd(mediaType, streamId, invalidCallHandle);
                if (ErrorNumberEnum.PLCM_SAMPLE_OK != errNo)
                {
                    log.Error("detach stream from window failed. Error number = " + errNo.ToString());
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region Camera
        /**
         * used to start camera
         * */
        public static bool StartCamera()
        {
            log.Debug("[startCamera]");
            var errNo = WrapperProxy.StartCamera();
            if (ErrorNumberEnum.PLCM_SAMPLE_OK != errNo)
            {
                log.Error("start camera failed. ErrorNum = " + errNo.ToString());
                return false;
            }
            return true;
        }
        /**
         * used to stop camera
         * */
        public static bool StopCamera()
        {
            log.Debug("[stopCamera]");
            var errNo = WrapperProxy.StopCamera();
            if (ErrorNumberEnum.PLCM_SAMPLE_OK != errNo)
            {
                log.Error("stop camera failed. ErrorNum = " + errNo.ToString());
                return false;
            }
            return true;
        }
        #endregion

    }
}
