using ShareMarket.Core.Interfaces.Utility.Security;

namespace ShareMarket.WebApp.Components;

public class ComponentBaseClass : ComponentBase
{
    [Inject] internal IToastService         NotificationService { get; set; } = default!;
    [Inject] internal IMapper               Mapper              { get; set; } = default!;
    [Inject] private IUserIdentityProcessor UserIdentity        { get; set; } = default!;
    [Inject] internal IMessageService       MessageService      { get; set; } = default!;

    internal long   UserId      { get; set; }
    internal bool   IsLoading   { get; set; }

    protected override async Task OnInitializedAsync()
    {
        UserId = await UserIdentity.GetCurrentUserId();
        await base.OnInitializedAsync();
    }
}
