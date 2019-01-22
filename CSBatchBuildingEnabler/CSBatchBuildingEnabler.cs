using ICities;
using UnityEngine;

namespace CSBatchBuildingEnabler {
    public class CSBatchBuildingEnabler: IUserMod {
        public string Name {
            get {
                return "Batch Building Enabler";
            }
        }

        public string Description
        {
            get {
                return "Batch enable/disable all buildings of a specific type.";
            }
        }
    }
}
