using FC.Codeflix.Catalog.Domain;

namespace FC.Codeflix.Catalog.UnitTests;

public class CategoryTest
{
    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "Category - Aggregates")]
    public void Instantiate()
    {
        var validData = new 
        {
            Name = "category name",
            Description = "category description"
        };

        var dateTimeBefore = DateTime.Now;

        var category = new Category(validData.Name, validData.Description);

        var dateTimeAfter = DateTime.Now;

        Assert.NotNull(category);
        Assert.Equal(validData.Name, category.Name);
        Assert.Equal(validData.Description, category.Description);
        Assert.NotEqual(default(Guid), category.Id);
        Assert.NotEqual(default(DateTime), category.CreatedAt);
        Assert.True(category.CreatedAt > dateTimeBefore);
        Assert.True(category.CreatedAt < dateTimeAfter);
        Assert.True(category.IsActive);
    }

    [Theory(DisplayName = nameof(InstantiateWithIsActiveStatus))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData(true)]
    [InlineData(false)]
    public void InstantiateWithIsActiveStatus(bool IsActive)
    {
        var validData = new 
        {
            Name = "category name",
            Description = "category description"
        };

        var dateTimeBefore = DateTime.Now;

        var category = new Category(validData.Name, validData.Description, IsActive);

        var dateTimeAfter = DateTime.Now;

        Assert.NotNull(category);
        Assert.Equal(validData.Name, category.Name);
        Assert.Equal(validData.Description, category.Description);
        Assert.NotEqual(default(Guid), category.Id);
        Assert.NotEqual(default(DateTime), category.CreatedAt);
        Assert.True(category.CreatedAt > dateTimeBefore);
        Assert.True(category.CreatedAt < dateTimeAfter);
        Assert.Equal(category.IsActive, IsActive);
    }

    [Theory(DisplayName = nameof(InstantiateThrowsExceptionWhenNameIsEmpty))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("    ")]
    public void InstantiateThrowsExceptionWhenNameIsEmpty(string invalidName)
    {
        var validData = new {
            Description = "Category Description"
        };

        Action instanciateCategory = () => new Category(invalidName, validData.Description);

        var exception = Assert.Throws<EntityValidationException>(instanciateCategory);

        Assert.Equal("Name should not be empty nor null", exception.Message);
    }

    [Fact(DisplayName = nameof(InstantiateThrowsExceptionWhenDescriptionIsNull))]
    [Trait("Domain", "Category - Aggregates")]
    public void InstantiateThrowsExceptionWhenDescriptionIsNull()
    {
        var data = new {
            Name = "Category Name",
            InvalidDescription = (string?) null
        };

        Action instanciateCategory = () => new Category(data.Name, data.InvalidDescription!);

        var exception = Assert.Throws<EntityValidationException>(instanciateCategory);

        Assert.Equal("Description should not be null", exception.Message);
    }

    [Theory(DisplayName = nameof(InstantiateThrowsExceptionWhenNameIsLessThan3Characters))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("1")]
    [InlineData("12")]
    public void InstantiateThrowsExceptionWhenNameIsLessThan3Characters(string invalidName)
    {
        var validData = new
        {
            Description = "Category Description"
        };

        Action instanciateCategory = () => 
            new Category(invalidName, validData.Description);

        var exception = Assert.Throws<EntityValidationException>(instanciateCategory);

        Assert.Equal("Name should be at least 3 characters", exception.Message);
    }

    [Fact(DisplayName = nameof(InstantiateThrowsExceptionWhenNameIsGreaterThan255Characters))]
    public void InstantiateThrowsExceptionWhenNameIsGreaterThan255Characters()
    {
        var validData = new
        {
            Description = "Category Description"
        };

        var invalidName = String.Join(null, Enumerable.Range(0,256).Select(_ => "a").ToArray());

        Action instanciateCategory = () => 
            new Category(invalidName, validData.Description);

        var exception = Assert.Throws<EntityValidationException>(instanciateCategory);

        Assert.Equal("Name should be less or equal to 255 long", exception.Message);
    }

    [Fact(DisplayName = nameof(InstantiateThrowsExceptionWhenDescriptionIsGreaterThan10_000_Characters))]
    public void InstantiateThrowsExceptionWhenDescriptionIsGreaterThan10_000_Characters()
    {
        var validData = new
        {
            Name = "Some name"
        };

        var invalidDescription = String.Join(null, Enumerable.Range(0,10_001).Select(_ => "a").ToArray());

        Action instanciateCategory = () => 
            new Category(validData.Name, invalidDescription);

        var exception = Assert.Throws<EntityValidationException>(instanciateCategory);

        Assert.Equal("Description should be less or equal to 10.000 long", exception.Message);
    }

    [Fact(DisplayName = nameof(ActivateCategory))]
    public void ActivateCategory()
    {
        var validData = new 
        {
            Name = "category name",
            Description = "category description"
        };

        var category = new Category(validData.Name, validData.Description, false);

        category.Activate();

        Assert.True(category.IsActive);
    }

    [Fact(DisplayName = nameof(DeactivateCategory))]
    public void DeactivateCategory()
    {
        var validData = new 
        {
            Name = "category name",
            Description = "category description"
        };

        var category = new Category(validData.Name, validData.Description, true);

        category.Deactivate();

        Assert.False(category.IsActive);
    }
}
