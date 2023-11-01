namespace MicrogrooveChallenge.BLL.Services
{
    public interface IAvatarService
    {
        Task<byte[]> CreateCustomerAvatarAsync(string customerName);
    }
}