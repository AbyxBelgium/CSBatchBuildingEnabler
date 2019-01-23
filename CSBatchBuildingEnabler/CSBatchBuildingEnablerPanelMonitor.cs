using ColossalFramework;
using ColossalFramework.UI;
using ICities;
using UnityEngine;

namespace CSBatchBuildingEnabler
{
    public class CSBatchBuildingEnablerPanelMonitor: ThreadingExtensionBase {
        private ShelterWorldInfoPanel panel;
        private UICheckBox onOffCheckBox;
       

        public override void OnUpdate(float realTimeDelta, float simulationTimeDelta) {
            Debug.Log("ON UPDATE!");
            if (!FindComponents()) {
                return;
            }

            if (panel.component.isVisible) {
                ushort buildingId = ShelterWorldInfoPanel.GetCurrentInstanceID().Building;
                
                // display the right checkbox state  
                onOffCheckBox.isChecked = Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingId].m_productionRate != 0; 
                Debug.Log("IsChecked: " + onOffCheckBox.isChecked);
            }
        }

        public bool FindComponents() {
            if(panel != null && onOffCheckBox != null) return true;

            panel = UIView.library.Get<ShelterWorldInfoPanel>(typeof(ShelterWorldInfoPanel).Name);
            if (panel == null) return false;
            
           
            onOffCheckBox = panel.component.Find<UICheckBox>("On/Off");
            return onOffCheckBox != null;
        }
    }
}
