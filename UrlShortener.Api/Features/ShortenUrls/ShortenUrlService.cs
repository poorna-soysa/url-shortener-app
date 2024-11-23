﻿namespace UrlShortener.Api.Features.ShortenUrls;

public class ShortenUrlService : IShortenUrlService
{
    public async Task<string> GenerateUniqueCode()
    {
        const int MAX_LENGTH = 7;
        const string Alphabet =
       "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        Random random = new();

        var chars = new char[MAX_LENGTH];

        while (true)
        {
            for (int i = 0; i < MAX_LENGTH; i++)
            {
                var randomIndex = random.Next(Alphabet.Length);

                chars[i] = Alphabet[randomIndex];
            }

            string uniqueCode = new string(chars);

            //if (true)
            //{
            //    return uniqueCode;
            //}

            return uniqueCode;
        }
    }
}

