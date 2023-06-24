namespace Invoice_System.Services
{
    public class RandomNumberGenerater
    {
        public string Rand()
        {
            Random random = new Random();
            int randomNumber = random.Next(100,999);

            return randomNumber.ToString();
        }
    }
}
