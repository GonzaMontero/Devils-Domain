/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID ATTACK = 180661997U;
        static const AkUniqueID AUTOBATTLEMUSIC = 641649455U;
        static const AkUniqueID CONFIRMATIONBUTTON = 2834637694U;
        static const AkUniqueID DIE = 445985469U;
        static const AkUniqueID EXITBACKBUTTON = 579888164U;
        static const AkUniqueID GACHAMUSIC = 4027500490U;
        static const AkUniqueID GOTOAUTOBATTLEBUTTON = 674526087U;
        static const AkUniqueID GOTOGACHABUTTON = 4120200410U;
        static const AkUniqueID GOTOIDLEBUTTON = 1567002330U;
        static const AkUniqueID GOTOLINEUPBUTTON = 1177290145U;
        static const AkUniqueID GOTOMENUBUTTON = 1464228145U;
        static const AkUniqueID LINEUPMUSIC = 3502195145U;
        static const AkUniqueID MENUMUSIC = 679636833U;
        static const AkUniqueID WIN = 979765101U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace AUTOBATTLE
        {
            static const AkUniqueID GROUP = 2432184674U;

            namespace STATE
            {
                static const AkUniqueID COMBAT = 2764240573U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID WIN = 979765101U;
            } // namespace STATE
        } // namespace AUTOBATTLE

        namespace MUSIC
        {
            static const AkUniqueID GROUP = 3991942870U;

            namespace STATE
            {
                static const AkUniqueID IDLE = 1874288895U;
                static const AkUniqueID MENU = 2607556080U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace MUSIC

    } // namespace STATES

    namespace SWITCHES
    {
        namespace ATTACK
        {
            static const AkUniqueID GROUP = 180661997U;

            namespace SWITCH
            {
                static const AkUniqueID BLUNT = 3376675122U;
                static const AkUniqueID SPELL = 523506649U;
                static const AkUniqueID SWORD = 2454616260U;
            } // namespace SWITCH
        } // namespace ATTACK

        namespace DIE
        {
            static const AkUniqueID GROUP = 445985469U;

            namespace SWITCH
            {
                static const AkUniqueID F = 84696441U;
                static const AkUniqueID M = 84696434U;
            } // namespace SWITCH
        } // namespace DIE

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID WEAPONTYPE = 767731869U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID TEST = 3157003241U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
