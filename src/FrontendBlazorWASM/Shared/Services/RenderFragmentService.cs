using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Collections.Generic;

namespace Planity.FrontendBlazorWASM.Shared.Services
{
    public class RenderFragmentService
    {
        public RenderFragment Create<TComponent>(Dictionary<string, object>? parameters = null)
            where TComponent : ComponentBase
        {
            return Create<TComponent>(typeof(TComponent), parameters);
        }
        public RenderFragment Create<TComponent>(Type type, Dictionary<string, object>? parameters = null)
            where TComponent : ComponentBase
        {
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
