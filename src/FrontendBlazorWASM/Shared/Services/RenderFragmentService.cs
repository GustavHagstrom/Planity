using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Planity.FrontendBlazorWASM.Shared.Services
{
    public class RenderFragmentService
    {
        public RenderFragment Create<TComponent>(Dictionary<string, object>? parameters = null)
            where TComponent : IComponent
        {
            return builder =>
            {
                var seq = 0;
                builder.OpenComponent<TComponent>(seq++);
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
