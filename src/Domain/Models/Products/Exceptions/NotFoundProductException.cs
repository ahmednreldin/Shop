namespace Domain.Models.Products.Exceptions
{
    public class NotFoundProductException : Exception
    {
        public NotFoundProductException(Guid ProductId) :
            base($"Couldn't find product with Id :{ProductId}.")
        { }
    }
}
