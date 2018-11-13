
namespace MFW.LALLib
{
    public enum ErrorNumberEnum
    {
        UNKNOWN = -1,                                    //(-1),
        PLCM_SAMPLE_OK,                                    //(0), /*sample code error number representing OK*/

        /*SDK error number*/
        PLCM_MFW_ERR_INVALID_HANDLE,                                    //(1),
        PLCM_MFW_ERR_MOREDATA,                                    //(2),
        PLCM_MFW_ERR_INVALID_DEVICETYPE,                                    //(3),
        PLCM_MFW_ERR_NOTIMPLEMENTED,                                    //(4),
        PLCM_MFW_ERR_NOTSUPPORTED,                                    //(5),
        PLCM_MFW_ERR_INVALID_MESSAGEQ,                                    //(6),
        PLCM_MFW_ERR_INVALID_STREAMINFO,                                    //(7),
        PLCM_MFW_ERR_INVALID_WND,                                    //(8),
        PLCM_MFW_ERR_INTERNAL,                                    //(9),
        PLCM_MFW_ERR_INVALIDCALL,                                    //(10),
        PLCM_MFW_ERR_INVALID_PARAMETER,                                    //(11),
        PLCM_MFW_ERR_WARNING_SENDING_CONTENT,                                    //(12),
        PLCM_MFW_ERR_INVALID_DEVICE,                                    //(13),
        PLCM_MFW_ERR_INVALID_CAPS,                                    //(14),
        PLCM_MFW_ERR_INVALID_KVLIST,                                    //(15),
        PLCM_MFW_ERR_UNSUPPORT_NEON,                                    //(16),
        PLCM_MFW_ERR_GENERIC,                                    //(17),
        PLCM_MFW_ERR_ENCRYPTION_CONFIG,                                    //(18),
        PLCM_MFW_ERR_CALL_EXCEED_MAXIMUM_CALLS,                                    //(19),
        PLCM_MFW_ERR_CALL_IN_REGISTERING,                                    //(20),
        PLCM_MFW_ERR_CALL_INVALID_FORMAT,                                    //(21),
        PLCM_MFW_ERR_CALL_NO_CONNECT,                                    //(22),
        PLCM_MFW_ERR_CALL_HOST_UNKNOWN,                                    //(23),
        PLCM_MFW_ERR_CALL_EXIST,                                    //(24),
        PLCM_MFW_ERR_CALL_INVALID_OPERATION,                                    //(25),
        PLCM_MFW_ERR_INVOKEAPI_INCALLBACK,                                    //(26),
        PLCM_MFW_ERR_PLAYBACK_FILE_NON_EXIST,                                    //(27),
        //MORE...add SDK error code

        /*sample code error number, start from 129*/
        PLCM_SAMPLE_INVALID_CALLHANDLE= -1,                              //(0xffffffff),
        PLCM_SAMPLE_ERRNO_START = 128,                                    //(128),
        PLCM_SAMPLE_LOADDLL_FAIL,                                    //(PLCM_SAMPLE_ERRNO_START.getValue() + 1),
        PLCM_SAMPLE_LOADDLL_FUNC_FAIL,                                    //(PLCM_SAMPLE_ERRNO_START.getValue() + 2),
        PLCM_SAMPLE_KVLISTINST_NULL,                                    //(PLCM_SAMPLE_ERRNO_START.getValue() + 3),
        PLCM_SAMPLE_CALLBACK_NULL,                                    //(PLCM_SAMPLE_ERRNO_START.getValue() + 4),
        PLCM_SAMPLE_REGISTER_FAIL,                                    //(PLCM_SAMPLE_ERRNO_START.getValue() + 5),
        PLCM_SAMPLE_SHARE_CONTENT_FAIL,                                    //(PLCM_SAMPLE_ERRNO_START.getValue() + 6),
        PLCM_SAMPLE_UPDATECONFIG_FAIL,                                    //(PLCM_SAMPLE_ERRNO_START.getValue() + 7),
        PLCM_SAMPLE_NULL_CALLHANDLE,                                    //(PLCM_SAMPLE_ERRNO_START.getValue() + 8),
        PLCM_SAMPLE_NULL_DEVICE,                                    //(PLCM_SAMPLE_ERRNO_START.getValue() + 9),
        PLCM_SAMPLE_NULL_CALLEE_ADDR,                                    //(PLCM_SAMPLE_ERRNO_START.getValue() + 10),
        PLCM_SAMPLE_NULL_DEVICE_NAME                                    //(PLCM_SAMPLE_ERRNO_START.getValue() + 11);
                                                                        //MORE...add sample error code
    }
}
