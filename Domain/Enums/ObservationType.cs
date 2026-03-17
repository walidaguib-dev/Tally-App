using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    public enum ObservationType
    {
        // Merchandise condition
        Damaged,
        Wet,
        Contaminated,

        // Quantity issues
        Shortage,
        Excess,
        Mismatch,

        // Truck specific
        TruckDamaged, // truck arrived damaged
        TruckOverloaded, // exceeded capacity
        TruckAbandoned, // left without completing

        // Operational
        Weather,
        Congestion,
        EquipmentFault,

        // General
        Other,
    }
}
