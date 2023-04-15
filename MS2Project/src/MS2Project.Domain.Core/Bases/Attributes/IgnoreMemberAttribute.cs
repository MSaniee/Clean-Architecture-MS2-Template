namespace $ext_safeprojectname$.Domain.Core.Bases.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class IgnoreMemberAttribute : Attribute
{
}