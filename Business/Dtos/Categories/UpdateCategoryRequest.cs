namespace Business.Dtos.Categories;

public class UpdateCategoryRequest
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

}
