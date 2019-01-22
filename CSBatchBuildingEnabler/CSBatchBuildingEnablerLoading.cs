using ICities;
using UnityEngine;
using ColossalFramework.UI;
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

            Debug.Log("BLUB");

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
            Debug.Log("Initialization has been completed!");
            Debug.Log(button);

            var buildingManager = Singleton<BuildingManager>.instance;
            var serviceBuildings = buildingManager.GetServiceBuildings(ItemClass.Service.Disaster);

            if (serviceBuildings.m_buffer == null || serviceBuildings.m_size > serviceBuildings.m_buffer.Length) {
                return;
            }

            for (var index = 0; index < serviceBuildings.m_size; ++index)
            {
                var id = serviceBuildings.m_buffer[index];
                var info = buildingManager.m_buildings.m_buffer[id].Info;
                var buildingName = info.m_buildingAI.name;
            }
        }
    }
}
