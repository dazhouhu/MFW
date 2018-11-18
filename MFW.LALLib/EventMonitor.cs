using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MFW.LALLib
{
    public delegate void EventMonitorEvent(Event evt);
    public class EventMonitor
    {
        object synObject = new object();
        private bool isRunning = false;
        private Queue<Event> queue = new Queue<Event>();
        private static CallManager callManager = CallManager.GetInstance();
        private static DeviceManager deviceManager = DeviceManager.GetInstance();
        private static LALProperties lalProperties = LALProperties.GetInstance();
        private static ILog log = LogManager.GetLogger("LAL: EventMonitor");
        private static AutoResetEvent autoEvent;
        public  event EventMonitorEvent MonitorEvent;
        public System.Windows.Forms.Control Dispatcher { get; set; }        

        #region Constructors
        /**
	     * Constructor sets EventMonitor as running.
	     */
        private static EventMonitor instance = null;

        public static EventMonitor GetInstance()
        {
            if (instance == null)
            {
                instance = new EventMonitor();
            }
            return instance;
        }
        private EventMonitor()
        {
            start();
        }
        #endregion

        /**
	     * Stop EventMonitor, and dispatch all events left in queue. After this, EventMonitor thread will become TERMINATED.
	     */
        public void StopEventMonitor()
        {
            isRunning = false;
            DispatchEvents();
        }

        /**
	     * Dispatch events in queue for proper handling.
	     */
        public void DispatchEvents()
        {
            //lock (synObject)
            {
                log.Info("notify evt monitor to proceed the events");
                //queue.notify();
                autoEvent.Set();
            }
        }

        public void start()
        {
            autoEvent = new AutoResetEvent(false);
            isRunning = true;
            var thread = new Thread(() =>
            {
                while (isRunning)
                {
                    log.Info("handle the evt");
                    if (queue.Count() == 0)
                    {
                        lock (synObject)
                        {
                            log.Info("No evt, wait..");
                            //queue.wait();
                            autoEvent.WaitOne();
                        }
                    }
                    log.Info("EventQ getEvent: total = " + queue.Count());
                    while (queue.Count() > 0)
                    {
                        Event evt = null;
                        lock (synObject)
                        {
                            evt = queue.Dequeue();
                        }
                        // dispatch Event to proper modules
                        if (evt == null)
                        {
                            log.Error("Event is null!");
                            continue;
                        }
                        if (null != Dispatcher)
                        {
                            try
                            {
                                Dispatcher.Invoke(new Action(() =>
                                {
                                    DoEvent(evt);
                                }));
                            }catch(Exception ex)
                            {
                                log.Error(ex.Message);
                            }
                        }
                        WrapperProxy.FreeEvent(evt.EventHandle);
                    }
                }
            });
            thread.Start();
        }

        public void AddEvents(IList<Event> list)
        {
            //lock (synObject)
            {
                foreach (var evt in list)
                {
                    queue.Enqueue(evt);
                }
                log.Info("Add events: current total = " + queue.Count());
                //autoEvent.Set();
            }
        }

        /**
         * Add single evt to queue.
         * 
         * @param evt
         *            Event instance.
         */
        public void AddEvent(Event evt)
        {
            log.Info("addEvent, type is" + evt.EventType.ToString());
            //lock (synObject)
            {
                queue.Enqueue(evt);
                log.Info("Add evt: current total = " + queue.Count());
                //autoEvent.Set();
            }
        }

        /**
	     * Event dispatch operation. When there's no evt to dispatch, simply wait. When EventMonitor is woke up, it dispatches all evt in queue.
	     */
        private void DoEvent(Event evt)
        {
            var c=callManager.GetCall(evt.CallHandle);
            if(null == c)
            {
                c = new Call(-1);
            }
            evt.Call = c;
            switch (evt.EventType)
            {
                case EventTypeEnum.SIP_REGISTER_SUCCESS:  /* with SIP server from CC*/
                    log.Info("register success");
                    MonitorEvent(evt);
                    break;

                case EventTypeEnum.SIP_REGISTER_FAILURE:  /* from CC */
                    log.Info("register failure");
                    MonitorEvent(evt);
                    break;

                case EventTypeEnum.PLCM_MFW_SIP_REGISTER_UNREGISTERED:
                    log.Info("unregister");
                    MonitorEvent(evt);
                    break;

                case EventTypeEnum.SIP_CALL_INCOMING: /* UAS received INVITE, from CC */
                    {
                        var call = new Call(evt.CallHandle)
                        {
                            DisplayCallName = evt.CallerName + "(id" + callManager.GetCallCounter() + ")",
                            CallMode = evt.CallMode,
                            CallState = CallStateEnum.SIP_INCOMING_INVITE
                        };
                        callManager.AddCall(call);
                        evt.Call = call;
                        call.CallEventState = CallEventStateEnum.INCOMING_INVITE;
                    }
                    break;
                case EventTypeEnum.SIP_CALL_TRYING:
                    {
                        log.Info("place id is: " + evt.PlaceId);
                        log.Info("[SIP_CALL_TRYING] sipCallId is" + evt.SipCallId);
                        var call = new Call(evt.CallHandle)
                        {
                            DisplayCallName = evt.CallerName + "(id" + callManager.GetCallCounter() + ")",
                            CallMode = evt.CallMode,
                            CallState = CallStateEnum.SIP_OUTGOING_TRYING
                        };
                        callManager.AddCall(call);
                        evt.Call = call;
                        call.CallEventState = CallEventStateEnum.SIP_CALL_TRYING;
                    }
                    break;
                case EventTypeEnum.SIP_CALL_RINGING: /* UAC get 180 from CC */
                    log.Info("[SIP_CALL_RINGING] sipCallId is" + evt.SipCallId);
                    evt.Call.CallState = CallStateEnum.SIP_OUTGOING_RINGING;
                    evt.Call.CallEventState = CallEventStateEnum.OUTGOING_RINGING;
                    break;
                case EventTypeEnum.SIP_CALL_FAILURE: /* from CC */
                    log.Info("[SIP_CALL_FAILURE] sipCallId is" + evt.SipCallId);
                    evt.Call.CallEventState = CallEventStateEnum.OUTGOING_FAILURE;
                    break;
                case EventTypeEnum.SIP_CALL_CLOSED: /* UAS get terminated from CC */ /* may close any call, in connected, held or others */
                    log.Info("[SIP_CALL_CLOSED] sipCallId is" + evt.SipCallId);
                    log.Info("Closed Call evt callhandle: " + evt.CallHandle);
                    evt.Call.Reason = string.IsNullOrEmpty(evt.Reason) ? "unknown reason" : evt.Reason;
                    evt.Call.CallState = CallStateEnum.SIP_CALL_CLOSED;
                    evt.Call.CallEventState = CallEventStateEnum.INCOMING_CLOSED;
                    break;
                case EventTypeEnum.SIP_CALL_UAS_CONNECTED:   /* from CC */
                    log.Info("UAS Connected Call handle: " + evt.CallHandle);
                    log.Info("[SIP_CALL_UAS_CONNECTED] sipCallId is" + evt.SipCallId);
                    evt.Call.CallState = CallStateEnum.SIP_INCOMING_CONNECTED;
                    evt.Call.CallEventState = CallEventStateEnum.INCOMING_CONNECTED;
                    LAL.MuteMic(evt.Call, false);
                    break;
                case EventTypeEnum.SIP_CALL_UAC_CONNECTED:  /* from CC */
                    log.Info("[SIP_CALL_UAC_CONNECTED] sipCallId is" + evt.SipCallId);
                    log.Info("UAC Connected Call handle: " + evt.CallHandle);
                    evt.Call.CallState = CallStateEnum.SIP_OUTGOING_CONNECTED;
                    evt.Call.CallEventState = CallEventStateEnum.OUTGOING_CONNECTED;
                    LAL.MuteMic(evt.Call, false);
                    break;
                case EventTypeEnum.SIP_CALL_HOLD:
                    log.Info("[SIP_CALL_HOLD] sipCallId is" + evt.SipCallId);
                    log.Info("Hold Call handle: " + evt.CallHandle);

                    evt.Call.CallState = CallStateEnum.SIP_CALL_HOLD;
                    evt.Call.CallEventState = CallEventStateEnum.INCOMING_HOLD;
                    //LAL.resumeCallInQueue();
                    break;
                case EventTypeEnum.SIP_CALL_HELD:
                    log.Info("[SIP_CALL_HELD] sipCallId is" + evt.SipCallId);
                    log.Info("held Call handle: " + evt.CallHandle);
                    evt.Call.CallState = CallStateEnum.SIP_CALL_HELD;
                    evt.Call.CallEventState = CallEventStateEnum.INCOMING_HELD;
                    break;
                case EventTypeEnum.SIP_CALL_DOUBLE_HOLD:
                    log.Info("[SIP_CALL_DOUBLE_HOLD] sipCallId is" + evt.SipCallId);
                    log.Info("double hold Call handle: " + evt.CallHandle);
                    evt.Call.CallState = CallStateEnum.SIP_CALL_DOUBLE_HOLD;
                    evt.Call.CallEventState = CallEventStateEnum.INCOMING_DOUBLE_HOLD;
                    //LAL.resumeCallInQueue();
                    break;
                case EventTypeEnum.DEVICE_VIDEOINPUTCHANGED:
                    string deviceName = evt.PlugDeviceName;
                    string newDeviceName = null;
                    string deviceHandle = evt.DeviceHandle;
                    Device device = null;
                    if (true == evt.PlugDeviceStatus)
                    {/*plug in a device*/
                        if ((null != deviceName) && (null != deviceHandle))
                        {
                            newDeviceName = deviceName + "(" + deviceManager.GetDeviceCount() + ")";
                            device = new Device(DeviceTypeEnum.VIDEO_INPUT, deviceHandle, newDeviceName, IntPtr.Zero);
                            deviceManager.AddDevice(device);
                        }
                    }
                    else
                    {
                        if (null != deviceHandle)
                        {
                            device = deviceManager.GetDevice(deviceHandle);
                            if (!deviceManager.RemoveDevice(deviceHandle))/*remove device from device manager*/
                            {
                                log.Error("No such video input device to be removed");
                            }
                        }
                    }
                    break;   /* from MP */
                case EventTypeEnum.DEVICE_AUDIOINPUTCHANGED:
                    deviceName = evt.PlugDeviceName;
                    deviceHandle = evt.DeviceHandle;
                    device = null;
                    newDeviceName = null;
                    if (true == evt.PlugDeviceStatus)
                    {/*plug in a device*/
                        if ((null != deviceName) && (null != deviceHandle))
                        {
                            newDeviceName = deviceName + "(" + deviceManager.GetDeviceCount() + ")";
                            device = new Device(DeviceTypeEnum.AUDIO_INPUT, deviceHandle, newDeviceName, IntPtr.Zero);
                            deviceManager.AddDevice(device);
                        }
                    }
                    else
                    {
                        if (null != deviceHandle)
                        {
                            device = deviceManager.GetDevice(deviceHandle);
                            if (true == deviceManager.RemoveDevice(deviceHandle))/*remove device from device manager*/
                            {
                                log.Error("No such video input device to be removed");
                            }
                        }
                    }
                    break;  /* from MP */
                case EventTypeEnum.DEVICE_AUDIOOUTPUTCHANGED:
                    deviceName = evt.PlugDeviceName;
                    deviceHandle = evt.DeviceHandle;
                    device = null;
                    newDeviceName = null;
                    if (true == evt.PlugDeviceStatus)
                    {/*plug in a device*/
                        if ((null != deviceName) && (null != deviceHandle))
                        {
                            newDeviceName = deviceName + "(" + deviceManager.GetDeviceCount() + ")";
                            device = new Device(DeviceTypeEnum.AUDIO_OUTPUT, deviceHandle, newDeviceName, IntPtr.Zero);
                            deviceManager.AddDevice(device);
                        }
                    }
                    else
                    {
                        if (null != deviceHandle)
                        {
                            device = deviceManager.GetDevice(deviceHandle);
                            if (!deviceManager.RemoveDevice(deviceHandle))
                            {
                                log.Error("No such video input device to be removed");
                            }
                        }
                    }
                    break; /* from MP */
                case EventTypeEnum.DEVICE_VOLUMECHANGED: break;  /* from MP */
                case EventTypeEnum.DEVICE_MONITORINPUTSCHANGED:
                    deviceName = evt.PlugDeviceName;
                    deviceHandle = evt.DeviceHandle;
                    device = null;
                    newDeviceName = null;
                    if (true == evt.PlugDeviceStatus)
                    {/*plug in a device*/
                        if ((null != deviceName) && (null != deviceHandle))
                        {
                            newDeviceName = deviceName + "(" + deviceManager.GetDeviceCount() + ")";
                            device = new Device(DeviceTypeEnum.DEV_MONITOR, deviceHandle, newDeviceName, IntPtr.Zero);
                            deviceManager.AddDevice(device);
                        }
                    }
                    else
                    {
                        if (null != deviceHandle)
                        {
                            device = deviceManager.GetDevice(deviceHandle);
                            if (!deviceManager.RemoveDevice(deviceHandle))/*remove device from device manager*/
                            {
                                log.Error("No such video input device to be removed");
                            }
                        }
                    }
                    break;  /* from MP */
                case EventTypeEnum.STREAM_VIDEO_LOCAL_RESOLUTIONCHANGED:
                    log.Info("debug: STREAM_VIDEO_LOCAL_RESOLUTIONCHANGED: WIDTH:" + evt.WndWidth + "   HEIGHT:" + evt.WndHeight);
                    evt.Call.SetChannelSize(0, evt.WndWidth, evt.WndHeight);
                    evt.Call.LocalWidth = evt.WndWidth;
                    evt.Call.LocalHeight = evt.WndHeight;
                    evt.Call.CallEventState = CallEventStateEnum.LOCAL_RESOLUTION_CHANGED;
                    break;
                case EventTypeEnum.STREAM_VIDEO_REMOTE_RESOLUTIONCHANGED:
                    log.Info("debug: STREAM_VIDEO_REMOTE_RESOLUTIONCHANGED: WIDTH:" + evt.WndWidth + "   HEIGHT:" + evt.WndHeight);
                    evt.Call.RemoteWidth = evt.WndWidth;
                    evt.Call.RemoteHeight = evt.WndHeight;
                    evt.Call.SetChannelSize(evt.StreamId, evt.WndWidth, evt.WndHeight);
                    evt.Call.CallEventState = CallEventStateEnum.REMOTE_RESOLUTION_CHANGED;
                    break;
                case EventTypeEnum.SIP_CONTENT_INCOMING:
                    evt.Call.ContentWidth = evt.WndWidth;
                    evt.Call.ContentHeight = evt.WndHeight;
                    evt.Call.AddChannel(evt.StreamId, ChannelType.Content);
                    evt.Call.SetChannelSize(evt.StreamId, evt.WndWidth, evt.WndHeight);
                    evt.Call.CallEventState = CallEventStateEnum.INCOMING_CONTENT;
                    break;
                case EventTypeEnum.SIP_CONTENT_CLOSED:
                    //log.Info("[SIP_CONTENT_CLOSED] sipCallId is" + evt.SipCallId);
                    log.Info("stop share content of Call handle: " + evt.CallHandle);

                    evt.Call.CallEventState = CallEventStateEnum.CONTENT_CLOSED;
                    break;
                case EventTypeEnum.SIP_CONTENT_SENDING:
                    log.Info("content debug:evt monitor receive SIP_CONTENT_SENDING");
                    evt.Call.CallEventState = CallEventStateEnum.CONTENT_SENDING;
                    break;
                case EventTypeEnum.SIP_CONTENT_UNSUPPORTED:
                    evt.Call.IsContentSupported = false;
                    evt.Call.CallEventState = CallEventStateEnum.CONTENT_UNSUPPORTED;
                    break;
                case EventTypeEnum.SIP_CONTENT_IDLE:
                    evt.Call.IsContentSupported = true;
                    evt.Call.ContentWidth = evt.WndWidth;
                    evt.Call.ContentHeight = evt.WndHeight;
                    evt.Call.CallEventState = CallEventStateEnum.CONTENT_IDLE;
                    break;
                case EventTypeEnum.PLCM_MFW_SIP_CALL_MODE_CHANGED:
                    log.Info(" PLCM_MFW_SIP_CALL_MODE_CHANGED, callModeEnum = " + CallModeEnum.PLCM_MFW_AUDIOVIDEO_CALL);
                    evt.Call.CallMode = evt.CallMode;
                    if (evt.CallMode == CallModeEnum.PLCM_MFW_AUDIOVIDEO_CALL)
                    {
                        evt.Call.IsAudioOnly = false;
                        evt.Call.CallEventState = CallEventStateEnum.CALL_AUDIO_ONLY_FALSE;
                    }
                    else
                    {
                        evt.Call.IsAudioOnly = true;
                        evt.Call.IsContentSupported = false;
                        evt.Call.CallEventState = CallEventStateEnum.CALL_AUDIO_ONLY_TRUE;
                    }
                    break;
                case EventTypeEnum.NETWORK_CHANGED: /* when network is changed or lost. */
                    evt.Call.NetworkIP = evt.IPAddress;
                    evt.Call.CallEventState = CallEventStateEnum.NETWORK_CHANGED;
                    MonitorEvent(evt);
                    break;
                case EventTypeEnum.MFW_INTERNAL_TIME_OUT:
                    evt.Call.CallEventState = CallEventStateEnum.MFW_INTERNAL_TIME_OUT;
                    MonitorEvent(evt);
                    break;
                case EventTypeEnum.UNKNOWN:
                    break;
                case EventTypeEnum.REFRESH_ACTIVE_SPEAKER:
                    {
                        log.Info("[svc debug] SVC_REFRESH_ACTIVE_SPEAKER");
                        log.Info("[svc debug] ViewID:" + evt.StreamId);
                        log.Info("[svc debug] ChanName:" + evt.RemoteChannelDisplayName);
                        log.Info("[svc debug] isActiveSpeaker:" + evt.IsActiveSpeaker);
                        evt.Call.ActiveSpeakerId = evt.ActiveSpeakerStreamId;
                        evt.Call.CallEventState = CallEventStateEnum.REFRESH_ACTIVE_SPEAKER;
                    }
                    break;
                case EventTypeEnum.REMOTE_VIDEO_REFRESH:
                    {
                        log.Info("[svc debug] REMOTE_VIDEO_REFRESH");
                        log.Info("[svc debug] chanNumber:" + evt.RemoteVideoChannelNum);
                        log.Info("[svc debug] activeSpeaker_id:" + evt.ActiveSpeakerStreamId);
                        evt.Call.DisplayCallName = evt.CallerName + "(id" + callManager.GetCallCounter() + ")";
                        
                        evt.Call.ChannelNumber = evt.RemoteVideoChannelNum;
                       // evt.Call.ActiveSpeakerId = evt.ActiveSpeakerStreamId;
                        evt.Call.CallEventState = CallEventStateEnum.REFRESH_LAYOUT;
                    }
                    break;
                case EventTypeEnum.REMOTE_VIDEO_CHANNELSTATUS_CHANGED:
                    {
                        log.Info("[LAL event] SVC_CHANNEL_STATUS_UPDATE" + " StreamId=" + evt.StreamId + " callHandle=" + evt.CallHandle);
                        evt.Call.AddChannel(evt.StreamId,ChannelType.Remote);
                        if(evt.IsActiveSpeaker){
                            evt.Call.ActiveSpeakerId=evt.StreamId;
                        }
                        evt.Call.CallEventState = CallEventStateEnum.CHANNEL_STATUS_UPDATE;
                    }
                    break;
                case EventTypeEnum.REMOTE_VIDEO_DISPLAYNAME_UPDATE:
                    {
                        log.Info("[LAL event] REMOTE_VIDEO_DISPLAYNAME_UPDATE" + " StreamId=" +evt.StreamId + " Displayname=" + evt.RemoteChannelDisplayName +" callHandle=" + evt.CallHandle);
                        evt.Call.SetChannelName(evt.StreamId, evt.RemoteChannelDisplayName);
                        evt.Call.CallEventState = CallEventStateEnum.DISPLAYNAME_UPDATE;
                    }
                    break;
                case EventTypeEnum.PLCM_MFW_SIP_CALL_MODE_UPGRADE_REQ:
                    log.Info("[LAL event] PLCM_MFW_SIP_CALL_MODE_UPGRADE_REQ" + " StreamId=" +
                                evt.StreamId + " ChanName=" + evt.RemoteChannelDisplayName +
                                " callHandle=" + evt.CallHandle);
                    evt.Call.CallEventState = CallEventStateEnum.CALL_MODE_UPGRADE_REQ;
                    break;
                case EventTypeEnum.PLCM_MFW_IS_TALKING_STATUS_CHANGED:
                    {
                        log.Info("PLCM_MFW_IS_TALKING_STATUS_CHANGED, SSRC is" + evt.StreamId + "; status is " + evt.IsTalkingFlag);
                        log.Info("PLCM_MFW_IS_TALKING_STATUS_CHANGED, call handle is " + evt.CallHandle);
                        if (1 == evt.IsTalkingFlag)
                        {
                            //  LALProperties.getInstance().displayProperty(PropertyEnum.IS_TALKING_LIST_SSRC, Integer.toHexString(evt.getStreamId()));
                        }
                        else if (0 == evt.IsTalkingFlag)
                        {
                            //  LALProperties.getInstance().removeProperty(PropertyEnum.IS_TALKING_LIST_SSRC, Integer.toHexString(evt.getStreamId()));
                        }
                        MonitorEvent(evt);
                    }
                    break;
                case EventTypeEnum.PLCM_MFW_CERTIFICATE_VERIFY:
                    // LAL.setCertRelated(evt);
                    //cur_call = LAL.createNullCallInstance();
                    evt.Call.CallEventState = CallEventStateEnum.CERTIFICATE_VERIFY;
                    MonitorEvent(evt);
                    break;
                case EventTypeEnum.PLCM_MFW_TRANSCODER_FINISH:
                    //cur_call = LAL.createNullCallInstance();
                    //LAL.setTranscoderFinshPara(event.getTranscoderTaskId(),event.getTranscoderInputFileName());

                    evt.Call.CallEventState = CallEventStateEnum.TRANSCODER_FINISH;
                    MonitorEvent(evt);
                    break;
                case EventTypeEnum.PLCM_MFW_ICE_STATUS_CHANGED:
                    //cur_call = LAL.createNullCallInstance();
                    //LAL.setICEStatus(evt.getICEStatus());
                    evt.Call.CallEventState = CallEventStateEnum.ICE_STATUS_CHANGED;
                    MonitorEvent(evt);
                    break;
                case EventTypeEnum.PLCM_MFW_SUTLITE_INCOMING_CALL:
                    log.Info("SUTLite message is " + evt.SUTLiteMessage);
                    log.Info("SUTLite place id is " + evt.PlaceId);
                    break;
                case EventTypeEnum.PLCM_MFW_SUTLITE_TERMINATE_CALL:
                    log.Info("SUTLite message is " + evt.SUTLiteMessage);
                    log.Info("SUTLite place id is " + evt.PlaceId);
                    break;
                case EventTypeEnum.PLCM_MFW_BANDWIDTH_LIMITATION:
                    log.Info("BANDWIDTH LIMITATION  callHandle=" + evt.CallHandle);
                    log.Info("BANDWIDTH LIMITATION  isvideo OK:" + evt.IsVideoOK);
                    break;
                case EventTypeEnum.PLCM_MFW_AUTODISCOVERY_STATUS_CHANGED:
                    log.Info("auto discovery status=" + evt.AutoDiscoveryStatus);
                    if (evt.AutoDiscoveryStatus == AutoDiscoveryStatusEnum.PLCM_MFW_AUTODISCOVERY_ERROR)
                    {
                        log.Info("error reason is:" + evt.Reason);
                        evt.Call.EventRegID = evt.RegID;
                        evt.Call.CallEventState = CallEventStateEnum.AUTODISCOVERY_ERROR;
                    }
                    else if (evt.AutoDiscoveryStatus == AutoDiscoveryStatusEnum.PLCM_MFW_AUTODISCOVERY_FAILURE)
                    {
                        evt.Call.EventRegID = evt.RegID;
                        evt.Call.CallEventState = CallEventStateEnum.AUTODISCOVERY_FAILURE;
                    }
                    else
                    {
                        evt.Call.EventRegID = evt.RegID;
                        evt.Call.CallEventState = CallEventStateEnum.AUTODISCOVERY_SUCCESS;
                    }
                    MonitorEvent(evt);
                    break;
            }
        }
    }
}
