
using ErrorOr;

public static class Errors
{
    public static class Product
    {
        public static Error NotFound(Guid id) =>
            Error.NotFound(code: "Product.NotFound",
                           description: $"No se encontró el producto con Id = {id}.");

        public static Error AlreadyDeactivated(Guid id) =>
            Error.Failure(code: "Product.AlreadyDeactivated",
                          description: $"El producto con Id = {id} ya está desactivado.");
    }
}