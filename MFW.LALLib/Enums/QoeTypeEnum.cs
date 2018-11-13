
namespace MFW.LALLib
{
    public enum QoeTypeEnum
    {
        PLCM_MFW_QOE_VIDEO_CAPTURE_RAWONLY = 1 << 0,        /**< QoE type is video input raw data. */
        PLCM_MFW_QOE_VIDEO_CAPTURE_SCALED = 1 << 1,         /**< QoE type is video input scaled data. */
        PLCM_MFW_QOE_VIDEO_RENDER = 1 << 2,                 /**< QoE type is video input. */
        PLCM_MFW_QOE_AUDIO_RECORD = 1 << 3,                 /**< QoE type is audio output. */
        PLCM_MFW_QOE_AUDIO_PLAYING = 1 << 4,                /**< QoE type is audio input. */
        PLCM_MFW_QOE_ENCODED_VIDEO = 1 << 5,                /**< QoE type is H264 video stream data output. */
        PLCM_MFW_QOE_ENCODED_CONTENT = 1 << 6,              /**< QoE type is H264 content stream data output. */
        PLCM_MFW_QOE_DECODED_VIDEO = 1 << 7,                /**< QoE type is H264 video stream data input. */
        PLCM_MFW_QOE_DECODED_CONTENT = 1 << 8,              /**< QoE type is H264 content stream data input. */
        PLCM_MFW_QOE_RTP_RECEIVED_VIDEO = 1 << 9,           /**< QoE type is RTP video data output. */
        PLCM_MFW_QOE_RTP_RECEIVED_AUDIO = 1 << 10,          /**< QoE type is RTP audio data output. */
        PLCM_MFW_QOE_RTP_RECEIVED_CONTENT = 1 << 11,        /**< QoE type is RTP content data output. */
        PLCM_MFW_QOE_RTP_SENT_VIDEO = 1 << 12,              /**< QoE type is RTP video data input. */
        PLCM_MFW_QOE_RTP_SENT_AUDIO = 1 << 13,              /**< QoE type is RTP audio data input. */
        PLCM_MFW_QOE_RTP_SENT_CONTENT = 1 << 14             /**< QoE type is RTP content data input. */
    }
}
