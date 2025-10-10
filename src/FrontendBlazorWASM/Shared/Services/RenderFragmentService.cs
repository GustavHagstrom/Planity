using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Collections.Generic;

namespace Planity.FrontendBlazorWASM.Shared.Services
{
    public class RenderFragmentService
    {
        public RenderFragment Render<TComponent>(Dictionary<string, object>? parameters = null)
            where TComponent : ComponentBase
        {
            return Render(typeof(TComponent), parameters);
        }
        public RenderFragment Render(Type type, Dictionary<string, object>? parameters = null)
        {
            if (!typeof(ComponentBase).IsAssignableFrom(type))
                throw new ArgumentException($"Type '{type.FullName}' must inherit from ComponentBase.", nameof(type));

            return builder =>
            {
                var seq = 0;
                builder.OpenComponent(seq++, type);
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        builder.AddAttribute(seq++, param.Key, param.Value);
                    }
                }
                builder.CloseComponent();
            };
        }
    }
}
