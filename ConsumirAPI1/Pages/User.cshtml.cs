// En el archivo Users.cshtml.cs

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class UsersModel : PageModel
{
    public List<User> Users { get; private set; }

    public async Task OnGetAsync()
    {
        Users = await GetUsersFromApi();
    }

    private async Task<List<User>> GetUsersFromApi()
    {
        List<User> users = null;
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
            HttpResponseMessage response = await client.GetAsync("users");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                users = JsonSerializer.Deserialize<List<User>>(json);
            }
        }
        return users;
    }
}

public class User
{
    public int id { get; set; }
    public string? name { get; set; }
    public string? username { get; set; }
    public Address address { get; set; } // Propiedad para la dirección
    // Otros campos del usuario
}
public class Address
{
    public string street { get; set; }
    public string suite { get; set; }
    public string city { get; set; }
    public string zipcode { get; set; }
}