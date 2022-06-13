using BookStore.Domain.Enums;

namespace BookStore.Domain.Entities.Products;
public class Image : IEntity
{
    public const int MinHeight = 1;
    public const int MinWidth = 1;

    public int Id { set; get; }
    public string Name { set; get; }

    public int Height { set; get; }
    public int Width { set; get; }
    public string Data { set; get; }        
    public ImageFormat Format { set; get; } 
    
    public Album Album { set; get; }
    public int AlbumId { set; get; }
}
