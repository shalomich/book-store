using App.Areas.Common.ViewModels;

namespace App.Areas.Dashboard.ViewModels.Identities
{
    public record ProductIdentity : FormEntityIdentity
    {
        public ImageDto TitleImage { init; get; }
    }

}
