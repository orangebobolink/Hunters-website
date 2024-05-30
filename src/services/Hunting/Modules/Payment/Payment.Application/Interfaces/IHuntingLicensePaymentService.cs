namespace Payment.Application.Interfaces
{
    public interface IHuntingLicensePaymentService
    {
        Task<bool> TryToPayAsync(
            string huntingLicense,
            CancellationToken cancellationToken);
    }
}
