using DigutusProject.Core.Common;

namespace DigutusProject.Core.Models;

public class Log : BaseEntity
{
    public string Email { get; set; }
    public bool IsLogin { get; set; }
}