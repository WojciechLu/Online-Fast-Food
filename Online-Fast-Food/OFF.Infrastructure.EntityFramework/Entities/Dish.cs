namespace OFF.Infrastructure.EntityFramework.Entities;

public class Dish
{
    //public string Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public byte[] ProductImage { get; set; }
    public float Price { get; set; }
    public bool Avaible { get; set; } = true;
    public ICollection<Category> Categories { get; set; } 
    public ICollection<DishOrder> Ordered { get; set; }
}
