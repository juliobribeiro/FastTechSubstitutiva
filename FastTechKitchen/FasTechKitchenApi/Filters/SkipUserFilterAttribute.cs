using Microsoft.AspNetCore.Mvc.Filters;

namespace FasTechKitchen.Api.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class SkipUserFilterAttribute : Attribute, IFilterMetadata { }
