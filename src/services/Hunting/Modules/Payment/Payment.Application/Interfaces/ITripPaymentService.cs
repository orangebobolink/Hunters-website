namespace Payment.Application.Interfaces
{
    public interface ITripPaymentService
    {
        Task<bool> TryToPayAsync(
           string tripNumber,
           CancellationToken cancellationToken);
    }
}
