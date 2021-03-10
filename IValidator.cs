namespace BatteryManagementSystem {
    public interface IValidator<T> {
        bool IsValid(T t);
        BreachLevel GetBreachLevel(T t);
        ToleranceLevel GetToleranceLevel(T t);
    }
    public enum BreachLevel {
        Low,
        Normal,
        High
    }
    public enum ToleranceLevel {
        ApproachingDischarnge,
        Normal,
        ApproachingPeak
    }
}
