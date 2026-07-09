namespace DayFlow.Web.Common.Mvc
{
    using Microsoft.AspNetCore.Mvc.Controllers;
    using System.Reflection;

    public class NamespaceControllerFeatureProvider
        : ControllerFeatureProvider
    {
        private readonly string _namespace;

        public NamespaceControllerFeatureProvider(string @namespace)
        {
            _namespace = @namespace;
        }

        protected override bool IsController(TypeInfo typeInfo)
        {
            if (!base.IsController(typeInfo))
                return false;

            return typeInfo.Namespace?
                .StartsWith(_namespace) == true;
        }
    }
}
