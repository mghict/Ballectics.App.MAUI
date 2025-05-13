using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Ballectics.App.Models;
using QRCoder;

namespace Ballectics.App.Helper;

public static class Storage
{
    public static async Task<UserLoginModel?> GetUserAsync()
    {
        try
        {
            var userJson = await SecureStorage.GetAsync("auth_user");
            if (string.IsNullOrEmpty(userJson))
            {
                return null;
            }

            var user = JsonSerializer.Deserialize<UserLoginModel>(userJson);
            return user;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
