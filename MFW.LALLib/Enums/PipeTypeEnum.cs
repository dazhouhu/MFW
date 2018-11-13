
namespace MFW.LALLib
{
    public enum PipeTypeEnum
    {

        PLCM_MFW_PIPE_UNKNOWN

        , PLCM_MFW_PIPE_ARX         /**< Incoming audio stream. */
        , PLCM_MFW_PIPE_ATX         /**< Outgoing audio stream. */
        , PLCM_MFW_PIPE_PVRX        /**< Incoming video stream. */
        , PLCM_MFW_PIPE_PVTX        /**< Outgoing video stream. */
        , PLCM_MFW_PIPE_CRX         /**< Incoming content stream. */
        , PLCM_MFW_PIPE_CTX         /**< Outgoing content stream. */
        , PLCM_MFW_PIPE_AVRX        /**< Mixed incoming audio and video stream. */
        , PLCM_MFW_PIPE_AVTX        /**< Mixed outgoing audio and video stream. */
        , PLCM_MFW_PIPE_ATRX        /**< Mixed outgoing and incoming audio streams. */
        , PLCM_MFW_PIPE_ATRX_PVRX    /**< ATRX and PVRX at the same time */

    }
}
