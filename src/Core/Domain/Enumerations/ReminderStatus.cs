using Ardalis.SmartEnum;

namespace Domain.Enumerations;

public class ReminderStatus(string name, int value) : SmartEnum<ReminderStatus>(name, value)
{
    public static readonly ReminderStatus Available = new(nameof(Available), 0);
    public static readonly ReminderStatus Active = new(nameof(Active), 1);
    public static readonly ReminderStatus Inactive = new(nameof(Inactive), 2);
    public static readonly ReminderStatus Completed = new(nameof(Completed), 3);
    public static readonly ReminderStatus Canceled = new(nameof(Canceled), 4);
    public static readonly ReminderStatus Failed = new(nameof(Failed), 5);

    public static explicit operator ReminderStatus(string value) => FromName(value);
    public static implicit operator string(ReminderStatus status) => status.Name;
}