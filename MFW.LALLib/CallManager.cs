using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFW.LALLib
{
    public class CallManager:BaseModel
    {
        #region Constructors
        private static CallManager instance = null;
        private CallManager() { }
        public static CallManager GetInstance()
        {
            if (instance == null)
            {
                instance = new CallManager();
                instance._callList.CollectionChanged+= (sender, args) => {
                    instance.CallsChanged?.Invoke(sender, args);
                };
            }
            return instance;
        }
        #endregion

        #region Properties
        public  Call CurrentCall
        {
            get { return _callList.FirstOrDefault(f => f.IsCurrent); }
            set
            {
                var currentCall= _callList.FirstOrDefault(f => f.IsCurrent);
                Call call = null;
                if(null != value)
                {
                   call= GetCall(value.CallHandle);
                }
                if(currentCall != call)
                {
                    if(null !=currentCall)
                    {
                        currentCall.IsCurrent = false;
                    }
                    if(null != call)
                    {
                        call.IsCurrent = true;
                    }
                    NotifyPropertyChanged("CurrentCall");
                }
            }
        }
        private ObservableCollection<Call> _callList = new ObservableCollection<Call>() {};
        public ObservableCollection<Call> CallList { get { return _callList; }}
        #endregion

        #region Events
        public event NotifyCollectionChangedEventHandler CallsChanged;
        #endregion

        #region Get Call
        public Call GetCall(int callHandle)
        {
            return _callList.FirstOrDefault(c => c.CallHandle == callHandle);
        }

        public IList<Call> GetActiveCalls()
        {
            return _callList.Where(c => c.CallState == CallStateEnum.SIP_OUTGOING_CONNECTED
                                    || c.CallState == CallStateEnum.SIP_INCOMING_CONNECTED).ToList();
        }
        public IList<Call> GetUnestablishedCalls()
        {
            return _callList.Where(c => c.CallState == CallStateEnum.SIP_UNKNOWN).ToList();
        }
        public IList<Call> GetHeldCalls()
        {
            return _callList.Where(c => c.CallState == CallStateEnum.SIP_CALL_HELD).ToList();
        }
        public IList<Call> GetIncomingCall()
        {
            return _callList.Where(c => c.CallState == CallStateEnum.SIP_INCOMING_INVITE).ToList();
        }

        public Call GetCallByName(string callDisplayName)
        {
            return _callList.Where(c => c.DisplayCallName == callDisplayName).FirstOrDefault();
        }

        #endregion
        public void AddCall(Call call)
        {
            if (call.CallHandle != -1)
            {
                var c = GetCall(call.CallHandle);
                if (null == c)
                {
                    _callList.Add(call);
                    CurrentCall = call;
                }
            }
        }
        public void RemoveCall(Call call)
        {
            if (Contains(call.CallHandle))
            {
                _callList.Remove(call);
                CurrentCall = _callList.FirstOrDefault();
            }
        }
        
        public void ClearCalls()
        {
            _callList.Clear();
        }

        public bool Contains(int callHandle)
        {
            return _callList.Any(c => c.CallHandle == callHandle);
        }
        public int GetCallCounter()
        {
            return _callList.Count;
        }
    }
}
