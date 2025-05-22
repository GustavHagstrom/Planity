namespace Planity.FrontendBlazorWASM.Features.Resources;

public class Resource
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string? RoleId { get; set; } 
    public string? Role { get; set; } // t.ex. Snickare, Projektledare

}
