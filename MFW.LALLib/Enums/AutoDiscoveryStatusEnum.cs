
namespace MFW.LALLib
{
    public enum AutoDiscoveryStatusEnum
    {
        PLCM_MFW_AUTODISCOVERY_UNKNOWN=0,
        PLCM_MFW_AUTODISCOVERY_SUCCESS,                     /**< Initiates auto discovery successful. */
        PLCM_MFW_AUTODISCOVERY_FAILURE,                     /**< Initiates auto discovery failed. */
        PLCM_MFW_AUTODISCOVERY_ERROR                       /**< Initiates auto discovery got an error. */
    }
}
