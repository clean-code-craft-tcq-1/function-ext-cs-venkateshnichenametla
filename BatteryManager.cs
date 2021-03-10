namespace BatteryManagementSystem {
    public class BatteryManager {
        public float Temperature { get; set; }
        public float StateOfCharge { get; set; }
        public float ChargeRate { get; set; }
        public BatteryManager(float temperature, float stateOfCharge, float chargeRate) {
            Temperature = temperature;
            StateOfCharge = stateOfCharge;
            ChargeRate = chargeRate;
        }
    }
}
