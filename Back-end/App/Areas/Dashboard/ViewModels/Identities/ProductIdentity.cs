using App.Areas.Common.ViewModels;

namespace App.Areas.Dashboard.ViewModels.Identities
{
    public record ProductIdentity : EntityIdentity
    {
        public ImageDto TitleImage { init; get; }
    }

}
