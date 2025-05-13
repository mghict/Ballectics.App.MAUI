using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Ballectics.App.Models;

namespace Ballectics.App.Services
{
    public class StorageService
    {
        public async Task<bool> SaveTokenAsync(string token)
        {
            try
            {
                await SecureStorage.SetAsync("auth_token", token);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving token: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SaveUserAsync(UserLoginModel model)
        {
            try
            {
                var modelJson = JsonSerializer.Serialize(model);
                await SecureStorage.SetAsync("auth_user", modelJson);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving user: {ex.Message}");
                return false;
            }
        }

        public async Task<string?> GetTokenAsync()
        {
            try
            {
                var token = await SecureStorage.GetAsync("auth_token");
                return token;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving token: {ex.Message}");
                return null;
            }
        }

        public async Task<UserLoginModel?> GetUserAsync()
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
                Console.WriteLine($"Error retrieving User: {ex.Message}");
                return null;
            }
        }

        public UserLoginModel? GetUser()
        {
            try
            {
                var userJson = SecureStorage.GetAsync("auth_user").Result;
                if (string.IsNullOrEmpty(userJson))
                {
                    return null;
                }

                var user = JsonSerializer.Deserialize<UserLoginModel>(userJson);
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving User: {ex.Message}");
                return null;
            }
        }

        public void DeleteToken()
        {
            try
            {
                SecureStorage.Remove("auth_token");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing token: {ex.Message}");
            }
        }

        public void DeleteUser()
        {
            try
            {
                SecureStorage.Remove("auth_user");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing User: {ex.Message}");
            }
        }
    }
}
