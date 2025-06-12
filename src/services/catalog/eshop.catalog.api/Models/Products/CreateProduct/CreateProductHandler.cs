using eshop.catalog.api.Models;
using eshop.catalog.api.Products.GetProducts;

namespace eshop.catalog.api.Products.CreateProduct;


public record   CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    string ImageFileName,
    List<string> Category
):ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);



public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(1000)
            .WithMessage("Product Name Should Not By Empty..");

        RuleFor(x => x.Description).MaximumLength(9000);
        RuleFor(x=>x.Price).NotNull().GreaterThan(1).LessThan(999999);
        RuleFor(x => x.Category).NotEmpty();
    }
}
internal class CreateProductCommandHandler(IDocumentSession session,
                                       ILogger<GetProductsQueryHandler> logger) :ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling CreateProductCommand");

        
        //var validateResult = await validator.ValidateAsync(command, cancellationToken);

        //var validateErrors = validateResult.Errors;//.Select(x=>x.ErrorMessage);
        //if (validateErrors.Any()) {
        //    var Errors = $"You Have Some Validation Errors Listed As : {Environment.NewLine}";
        //    foreach (var error in validateErrors)
        //    {
        //        //Errors += $"{error.PropertyName} : {error.ErrorMessage}{Environment.NewLine}";
        //        Errors += $"{error.ErrorMessage}{Environment.NewLine}";
        //    }
        //    throw new ValidationException(Errors);
        //}

        var product = new Product
        {
           // Id = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            ImageFileName = command.ImageFileName,
            Category = command.Category
        };


        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Product created with ID: {ProductId}", product.Id);
        // For demonstration, we are just returning a new Guid as the product ID.
        return  new   CreateProductResult(product.Id);
    }
}

