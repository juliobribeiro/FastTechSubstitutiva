using Microsoft.AspNetCore.Mvc.Filters;

namespace FastTech.Api.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class SkipUserFilterAttribute : Attribute, IFilterMetadata { }
