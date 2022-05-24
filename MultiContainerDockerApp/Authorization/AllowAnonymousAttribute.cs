namespace MultiContainerDockerApp.Authorization;

[AttributeUsage(AttributeTargets.Method)]
public class AllowAnonymousAttribute : Attribute
{ }