using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFW.LALLib
{
    public class WrapperProxy
    {
        public static ErrorNumberEnum FreeEvent(IntPtr evt)
        {
            return (ErrorNumberEnum)WrapperInterface.freeEvent(evt);
        }
        public static ErrorNumberEnum SetProperty(PropertyEnum key, string value)
        {
            return (ErrorNumberEnum)WrapperInterface.addProperty((int)key, value);
        }

        public static ErrorNumberEnum InstallCallback(AddEventCallback addEvent, DispatchEventsCallback dispatchEvents, AddLogCallback addLog,
                                            AddDeviceCallback addDevice, DisplayMediaStatisticsCallback displayMediaStatistics, DisplayCallStatisticsCallback displayCallStatistics,
                                            DisplayCodecCapabilities displayCodecNamesCallback, AddAppCallback addAppCallback)
        {
            return (ErrorNumberEnum)WrapperInterface.installCallback(addEvent, dispatchEvents, addLog, addDevice, displayMediaStatistics, displayCallStatistics, displayCodecNamesCallback, addAppCallback);
        }

        public static ErrorNumberEnum PreInitialize()
        {
            return (ErrorNumberEnum)WrapperInterface.preInitialize();
        }

        public static ErrorNumberEnum Initialize()
        {
            return (ErrorNumberEnum)WrapperInterface.initialize();
        }

        public static ErrorNumberEnum SetAudioDevice(string micHandle, string speakerHandle)
        {
            return (ErrorNumberEnum)WrapperInterface.setAudioDevice(micHandle, speakerHandle);
        }

        public static ErrorNumberEnum SetAudioDeviceForRingtone(string speakerHandle)
        {
            return (ErrorNumberEnum)WrapperInterface.setAudioDeviceForRingtone(speakerHandle);
        }

        public static ErrorNumberEnum SetVideoDevice(string cameraHandle)
        {
            return (ErrorNumberEnum)WrapperInterface.setVideoDevice(cameraHandle);
        }

        public static ErrorNumberEnum PlaceCall(string callee, ref int callHandle, CallModeEnum callMode)
        {
            return (ErrorNumberEnum)WrapperInterface.placeCall(callee, ref callHandle, (int)callMode);
        }

        public static ErrorNumberEnum TerminateCall(int callHandle)
        {
            return (ErrorNumberEnum)WrapperInterface.terminateCall(callHandle);
        }

        public static ErrorNumberEnum AnswerCall(int callHandle, CallModeEnum callMode, string authToken,  string cryptoSuiteType, string srtpKey, bool sutLiteEnable)
        {
            return (ErrorNumberEnum)WrapperInterface.answerCall(callHandle, (int)callMode, authToken, cryptoSuiteType, srtpKey, sutLiteEnable);
        }

        public static ErrorNumberEnum RejectCall(int callHandle)
        {
            return (ErrorNumberEnum)WrapperInterface.rejectCall(callHandle);
        }

        public static ErrorNumberEnum AttachStreamWnd(MediaTypeEnum mediaType, int streamId, int callHandle, IntPtr windowHandle, int x, int y, int width, int height)
        {
            return (ErrorNumberEnum)WrapperInterface.setStreamInfo((int)mediaType, streamId, callHandle, windowHandle, x, y, width, height);
        }

        public static ErrorNumberEnum DetachStreamWnd(MediaTypeEnum mediaType, int streamId, int callHandle)
        {
            return (ErrorNumberEnum)WrapperInterface.detachStreamWnd((int)mediaType, streamId, callHandle);
        }
        
        public static ErrorNumberEnum HoldCall(int callHandle)
        {
            return (ErrorNumberEnum)WrapperInterface.holdCall(callHandle);
        }

        public static ErrorNumberEnum ResumeCall(int callHandle)
        {
            return (ErrorNumberEnum)WrapperInterface.resumeCall(callHandle);
        }

        public static ErrorNumberEnum MuteMic(int callhandle, bool isMute)
        {
            return (ErrorNumberEnum)WrapperInterface.muteMic(callhandle, isMute);
        }

        public static ErrorNumberEnum MuteSpeaker(bool isMute)
        {
            return (ErrorNumberEnum)WrapperInterface.muteSpeaker(isMute);
        }
        
        public static ErrorNumberEnum SetMicVolume(int volume)
        {
            return (ErrorNumberEnum)WrapperInterface.setMicVolume((uint)volume);
        }

        public static int GetMicVolume()
        {
            return (int)WrapperInterface.getMicVolume();
        }

        public static ErrorNumberEnum SetSpeakerVolume(int volume)
        {
            return (ErrorNumberEnum)WrapperInterface.setSpeakerVolume((uint)volume);
        }
        
        public static int GetSpeakerVolume()
        {
            return (int)WrapperInterface.getSpeakerVolume();
        }
        public static ErrorNumberEnum RegisterClient()
        {
            return (ErrorNumberEnum)WrapperInterface.registerClient();
        }
        public static void UnregisterClient()
        {
            WrapperInterface.unregisterClient();
        }
        
        public static ErrorNumberEnum UpdateConfig()
        {
            return (ErrorNumberEnum)WrapperInterface.updateConfig();
        }

        public static ErrorNumberEnum StartShareContent(int callhandle, string deviceHandle, IntPtr appWndHandle)
        {
            return (ErrorNumberEnum)WrapperInterface.startShareContent(callhandle, deviceHandle, appWndHandle);
        }

         public static ErrorNumberEnum StartBFCPContent(int callhandle)
        {
            return (ErrorNumberEnum)WrapperInterface.startBFCPContent(callhandle);
        }
        
        public static ErrorNumberEnum StopShareContent(int callhandle)
        {
            return (ErrorNumberEnum)WrapperInterface.stopShareContent(callhandle);
        }
        
        public static ErrorNumberEnum SetContentBuffer(BFCPFormatEnum  format, int width, int height)
        {
            return (ErrorNumberEnum)WrapperInterface.setContentBuffer((int)format, width, height);
        }
        
        public static ErrorNumberEnum DestroyExit()
        {
            return (ErrorNumberEnum)WrapperInterface.destroyExit();
        }
        
        public static ErrorNumberEnum GetMediaStatistics(int callhandle)
        {
            return (ErrorNumberEnum)WrapperInterface.getMediaStatistics(callhandle);
        }
        
        public static ErrorNumberEnum GetCallStatistics()
        {
            return (ErrorNumberEnum)WrapperInterface.getCallStatistics();
        }
        
        public static string GetVersion()
        {
            var intPtrVersion= WrapperInterface.getVersion();
            return IntPtrHelper.IntPtrToUTF8string(intPtrVersion);
        }

        public static ErrorNumberEnum SendFECCKey(int callhandle, FECCKeyEnum key, FECCActionEnum action)
        {
            return (ErrorNumberEnum)WrapperInterface.sendFECCKey(callhandle, (int)key, (int)action);
        }
        
        public static ErrorNumberEnum SendDTMFKey(int callHandle, DTMFKeyEnum key)
        {
            return (ErrorNumberEnum)WrapperInterface.sendDTMFKey(callHandle, (int)key);
        }

        public static ErrorNumberEnum GetDeviceEnum(DeviceTypeEnum deviceType)
        {
            return (ErrorNumberEnum)WrapperInterface.getDeviceEnum((int)deviceType);
        }

        public static ErrorNumberEnum GetSupportedCapabilities()
        {
            return (ErrorNumberEnum)WrapperInterface.getSupportedCapabilities();
        }
        
        public static ErrorNumberEnum SetCapabilitiesEnable(int size, string type, string name, string tagID)
        {
            return (ErrorNumberEnum)WrapperInterface.setCapabilitiesEnable(size, type, name, tagID);
        }
        
        public static ErrorNumberEnum SetPreferencesCapabilities(int size, string type, string name, string tagID)
        {
            return (ErrorNumberEnum)WrapperInterface.setPreferencesCapabilities(size, type, name, tagID);
        }
        
        public static ErrorNumberEnum MuteLocalVideo(bool isMute)
        {
            return (ErrorNumberEnum)WrapperInterface.MuteLocalVideo(isMute);
        }
        
        public static ErrorNumberEnum GetApplicationInfo()
        {
            return (ErrorNumberEnum)WrapperInterface.getApplicationInfo();
        }
        
        public static ErrorNumberEnum StartCamera()
        {
            return (ErrorNumberEnum)WrapperInterface.startCamera();
        }
        
        public static ErrorNumberEnum StopCamera()
        {
            return (ErrorNumberEnum)WrapperInterface.stopCamera();
        }
        
        public static ErrorNumberEnum StartRecord(int callHandle, PipeTypeEnum pipeType, string filePath)
        {
            return (ErrorNumberEnum)WrapperInterface.startRecord(callHandle, (int)pipeType, filePath);
        }
        
        public static ErrorNumberEnum StopRecord(int callHandle)
        {
            return (ErrorNumberEnum)WrapperInterface.stopRecord(callHandle);
        }
        
        public static ErrorNumberEnum PauseRecord(int callHandle)
        {
            return (ErrorNumberEnum)WrapperInterface.pauseRecord(callHandle);
        }

        public static ErrorNumberEnum ResumeRecord(int callHandle)
        {
            return (ErrorNumberEnum)WrapperInterface.resumeRecord(callHandle);
        }
        
        public static ErrorNumberEnum StartPlayback(string filePath)
        {
            return (ErrorNumberEnum)WrapperInterface.startPlayback(filePath);
        }
        
        public static ErrorNumberEnum StopPlayback()
        {
            return (ErrorNumberEnum)WrapperInterface.stopPlayback();
        }
        
        public static ErrorNumberEnum PausePlayback()
        {
            return (ErrorNumberEnum)WrapperInterface.pausePlayback();
        }
        
        public static ErrorNumberEnum ResumePlayback()
        {
            return (ErrorNumberEnum)WrapperInterface.resumePlayback();
        }
        
        public static ErrorNumberEnum SetPlayPosition(int percent)
        {
            return (ErrorNumberEnum)WrapperInterface.setPlayPosition(percent);
        }
        
        public static ErrorNumberEnum GetPlayPosition(ref int percent)
        {
            return (ErrorNumberEnum)WrapperInterface.getPlayPosition(ref percent);
        }
        
        public static ErrorNumberEnum GetFileDuration(ref int seconds)
        {
            return (ErrorNumberEnum)WrapperInterface.getFileDuration(ref seconds);
        }


        public static ErrorNumberEnum GetRecordStatus(int callhandle, ref int status, ref int pipeType, string fileName)
        {
            return (ErrorNumberEnum)WrapperInterface.getRecordStatus(callhandle, ref status,  ref pipeType, fileName);
        }
        
        public static ErrorNumberEnum SetRemoteOneSVCVideoStream(int callhandle, int selectMode, int streamId, bool isActiveSpeaker)
        {
            return (ErrorNumberEnum)WrapperInterface.setRemoteOneSVCVideoStream(callhandle, selectMode, streamId, isActiveSpeaker);
        }


        public static ErrorNumberEnum SetRemoteVideoStreamNumber(int callhandle, int selectMode, int streamNumber)
        {
            return (ErrorNumberEnum)WrapperInterface.setRemoteVideoStreamNumber(callhandle, selectMode, streamNumber);
        }


        public static ErrorNumberEnum StartPlayAlert(string filePath, bool isLoop, int interval)
        {
            return (ErrorNumberEnum)WrapperInterface.startPlayAlert(filePath, isLoop, interval);
        }
        
        public static ErrorNumberEnum StopPlayAlert()
        {
            return (ErrorNumberEnum)WrapperInterface.stopPlayAlert();
        }
        
        public static ErrorNumberEnum ChangeCallMode(int callHandle, CallModeEnum callmode)
        {
            return (ErrorNumberEnum)WrapperInterface.changeCallMode(callHandle, (int)callmode);
        }
        
        public static ErrorNumberEnum PopupCameraPropertyAdvancedSettings(IntPtr winHandle)
        {
            return (ErrorNumberEnum)WrapperInterface.popupCameraPropertyAdvancedSettings(winHandle);
        }
        
        public static ErrorNumberEnum SetCertificateChoice(string certFingerPrint, int confirmId, CertChoiceEnum userChoice)
        {
            return (ErrorNumberEnum)WrapperInterface.setCertificateChoice(certFingerPrint, confirmId,(int) userChoice);
        }
        
        public static ErrorNumberEnum SetConfigFilePath(string filePath)
        {
            return (ErrorNumberEnum)WrapperInterface.setConfigFilePath(filePath);
        }
        
        public static IntPtr StartTranscoder(int audioOnly, TranscoderLayoutEnum layoutType, int resoFormat, int bitRate,
                 int frameRate, int keyFrameInterval, string inputFileName, string outputFileName, ref int errNo)
        {
            return WrapperInterface.startTranscoder(audioOnly, (int)layoutType, resoFormat, bitRate, frameRate, keyFrameInterval, inputFileName, outputFileName, ref errNo);
        }

        public static ErrorNumberEnum StopTranscoder(IntPtr taskHandle)
        {
            return (ErrorNumberEnum)WrapperInterface.stopTranscoder(taskHandle);
        }

        public static ErrorNumberEnum GetProgressOfTranscoder(IntPtr taskHandle, ref int percentage)
        {
            return (ErrorNumberEnum)WrapperInterface.getProgressOfTranscoder(taskHandle, ref percentage);
        }
        
        public static ErrorNumberEnum SetCallStream(CallStreamTypeEnum type, string filePath)
        {
            return (ErrorNumberEnum)WrapperInterface.setCallStream((int)type, filePath);
        }
        
        public static ErrorNumberEnum ClearCallStream(CallStreamTypeEnum type)
        {
            return (ErrorNumberEnum)WrapperInterface.clearCallStream((int)type);
        }
        
        public static ErrorNumberEnum EnableRecordAudioStreamCallback(int callHandle, RecordAudioStreamCallback callBack, int format, int interval)
        {
            return (ErrorNumberEnum)WrapperInterface.enableRecordAudioStreamCallback(callHandle, callBack, format, interval);
        }


        public static ErrorNumberEnum DisableRecordAudioStreamCallback(int callHandle)
        {
            return (ErrorNumberEnum)WrapperInterface.disableRecordAudioStreamCallback(callHandle);
        }
        
        public static ErrorNumberEnum SetStaticImage(IntPtr buffer, int length, int width, int height)
        {
            return (ErrorNumberEnum)WrapperInterface.setStaticImage(buffer, length, width, height);
        }

        public static ErrorNumberEnum EnableMediaQoE(VideoDataCapturedCallback videoDataCaptured,
                                            VideoDataRenderedCallback videoDataRendered,
                                            SpeakerDataReceivedCallback speakerDataReceived,
                                            MicrophoneDataSentCallback microphoneDataSent,
                                            DataEncodedCallback dataEncoded,
                                            DataDecodedCallback dataDecoded,
                                            RtpPacketReceivedCallback rtpPacketReceived,
                                            RtpPacketSentCallback rtpPacketSent,
                                            QoeTypeEnum type)
        {
            return (ErrorNumberEnum)WrapperInterface.enableMediaQoE(videoDataCaptured, videoDataRendered, speakerDataReceived, microphoneDataSent, dataEncoded, dataDecoded, rtpPacketReceived, rtpPacketSent, (int)type);
        }

        public static ErrorNumberEnum DisableMediaQoE(QoeTypeEnum type)
        {
            return (ErrorNumberEnum)WrapperInterface.disableMediaQoE((int)type);
        }
        public static ErrorNumberEnum startHttpTunnelAutoDiscovery(string destAddress, string destPort, string regId, string destUser)
        {
            return (ErrorNumberEnum)WrapperInterface.startHttpTunnelAutoDiscovery(destAddress, destPort, regId, destUser);

        }
    }    
}
