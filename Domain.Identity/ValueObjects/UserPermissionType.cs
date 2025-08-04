namespace Domain.Identity.ValueObjects;

[Flags]
public enum UserPermissionType
{
    Read,
    Write,
    Execute,
    Delete,
    All = Read | Write | Execute | Delete
}