namespace Business.Dtos.Categories;

public class UpdatedCategoryResponse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}