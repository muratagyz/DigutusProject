using DigutusProject.Core.Common;

namespace DigutusProject.Core.Models;

public class Time : BaseEntity
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}