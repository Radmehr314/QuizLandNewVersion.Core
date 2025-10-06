using Microsoft.AspNetCore.Mvc.Filters;

namespace QuizLand.Framework.Minimal.Security;

/// <summary>مارکر: این اکشن/کنترلر باید امضای دستگاه بررسی شود.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public sealed class RequireDeviceSignatureAttribute : Attribute, IFilterMetadata { }