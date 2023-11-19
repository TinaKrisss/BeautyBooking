using BeautyBooking.Data.Interfaces;
using System;
using System.Text;

namespace BeautyBooking.Data.Services
{
    public class PasswordCreator : IPasswordCreator
    {
        private static string Chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static int Length = 8;
        public async Task<string> CreatePassword()
        {
            Random random = new Random();
            StringBuilder password = new StringBuilder(Length);

            for (int i = 0; i < Length; i++)
            {
                password.Append(Chars[random.Next(Chars.Length)]);
            }
            return password.ToString();
        }
    }
}
