namespace UrlShortener.Api.Features.ShortenUrls;

public sealed class ShortenUrlService(ApplicationDbContext dbContext) : IShortenUrlService
{
    private const int MAX_LENGTH = 7;
    private const string AllLettersAndNumbers = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    public async Task<string> GenerateUniqueCode()
    {
        Random random = new();

        var chars = new char[MAX_LENGTH];

        while (true)
        {
            for (int i = 0; i < MAX_LENGTH; i++)
            {
                var randomIndex = random.Next(AllLettersAndNumbers.Length);

                chars[i] = AllLettersAndNumbers[randomIndex];
            }

            string uniqueCode = new string(chars);

            if (!await IsUniqueCodeExist(uniqueCode))
            {
                return uniqueCode;
            }
        }
    }

    private async Task<bool> IsUniqueCodeExist(string uniqueCode) =>
        await dbContext
       .ShortenUrls
       .AnyAsync(d => string.Equals(d.UniqueCode, uniqueCode));
}

