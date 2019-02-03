using System.Collections;
using ICities;
using UnityEngine;
using ColossalFramework.UI;
using ColossalFramework.Globalization;
using ColossalFramework;

namespace CSBatchBuildingEnabler
{
    public class CSBatchBuildingEnablerLoading: LoadingExtensionBase {
        private UIButton batchToggleStateButton;

        public override void OnLevelLoaded(LoadMode mode) {
            Debug.Log("Modifying Shelter Panel!");

            if (batchToggleStateButton != null) {
                return;
            }


            ShelterWorldInfoPanel shelterInfoPanel = UIView.library.Get<ShelterWorldInfoPanel>(typeof(ShelterWorldInfoPanel).Name);
            UIButton button = shelterInfoPanel.component.AddUIComponent<UIButton>();

            button.width = 200f;
            button.height = 40f;

            button.text = "Batch enable/disable";

            // Style the button to look like a menu button.
            button.normalBgSprite = "ButtonMenu";
            button.disabledBgSprite = "ButtonMenuDisabled";
            button.hoveredBgSprite = "ButtonMenuHovered";
            button.focusedBgSprite = "ButtonMenuFocused";
            button.pressedBgSprite = "ButtonMenuPressed";
            button.textColor = new Color32(255, 255, 255, 255);
            button.disabledTextColor = new Color32(7, 7, 7, 255);
            button.hoveredTextColor = new Color32(7, 132, 255, 255);
            button.focusedTextColor = new Color32(255, 255, 255, 255);
            button.pressedTextColor = new Color32(30, 30, 44, 255);

            // Enable button sounds.
            button.playAudioEvents = true;

            // Place the button.
            button.relativePosition = new Vector3(230f, 260f);

            batchToggleStateButton = button;

            button.eventClicked += (component, state) => {
                var buildingManager = Singleton<BuildingManager>.instance;

                if (!Singleton<BuildingManager>.exists) {
                    return;
                }

                var serviceBuildings = buildingManager.GetServiceBuildings(ItemClass.Service.Disaster);

                if (serviceBuildings.m_buffer == null || serviceBuildings.m_size > serviceBuildings.m_buffer.Length) {
                    return;
                }

                for (var index = 0; index < serviceBuildings.m_size; ++index) {
                    var id = serviceBuildings.m_buffer[index];
                    var building = buildingManager.m_buildings.m_buffer[id];
                    var info = building.Info;

                    if (info.m_buildingAI.GetType() == typeof(ShelterAI)) {
                        Singleton<SimulationManager>.instance.AddAction(ToggleBuilding(id, !shelterInfoPanel.isCityServiceEnabled));
                    }
                }
            };
            
        }

        private IEnumerator ToggleBuilding(ushort id, bool value) {
            if (Singleton<BuildingManager>.exists) {
                BuildingInfo info = Singleton<BuildingManager>.instance.m_buildings.m_buffer[id].Info;
                info.m_buildingAI.SetProductionRate(id, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[id], (byte)(value ? 100 : 0));
            }
            yield return 0;
        }
    }
}
