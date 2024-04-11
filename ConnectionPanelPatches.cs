using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace HideMe;

public class ConnectionPanelPatches
{
    [HarmonyPatch(typeof(ConnectPanel), nameof(ConnectPanel.Update))]
    static class ConnectionPanelPatch
    {
        static void Postfix(ConnectPanel __instance)
        {
            if (Player.m_localPlayer != null && ZNet.instance && __instance.m_root.gameObject.activeSelf)
            {
                List<ZNet.PlayerInfo> playerList = ZNet.instance.GetPlayerList();
                for (int index = 0; index < playerList.Count; ++index)
                {
                    // Just leaving some extra code here in case people want to disable/enable other things later.
                    ZNet.PlayerInfo playerInfo = playerList[index];
                    Text component1 = __instance.m_playerListElements[index].transform.Find("name")
                        .GetComponent<Text>();
                    Text component2 = __instance.m_playerListElements[index].transform.Find("hostname")
                        .GetComponent<Text>();
                    Button component3 = __instance.m_playerListElements[index].transform.Find("KickButton")
                        .GetComponent<Button>();
                    component1.text = playerInfo.m_name;
                    if (HideMePlugin.isAdmin && !HideMePlugin.HideMeCompletely.Value)
                    {
                        if (HideMePlugin.isAdmin && !HideMePlugin.HideForMeToo.Value)
                        {
                            component2.text = playerInfo.m_host;
                        }
                        else
                        {
                            component2.text = string.Empty;
                        }
                    }
                    else
                    {
                        component1.text = string.Empty;
                        component2.text = string.Empty;
                    }
                    
                    
                    

                    component3.gameObject.SetActive(false);
                }
            }
        }
    }
}