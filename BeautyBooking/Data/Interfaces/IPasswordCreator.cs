namespace BeautyBooking.Data.Interfaces
{
    public interface IPasswordCreator
    {
        Task<string> CreatePassword();
    }
}
