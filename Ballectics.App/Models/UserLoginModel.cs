using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ballectics.App.Helper;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Ballectics.App.Models;

public class UserLoginModel
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public string Token { get; set; } = string.Empty;
}
