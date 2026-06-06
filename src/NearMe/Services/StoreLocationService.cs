#nullable disable
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using NearMe.Data;
using NearMe.Models;
using Syncfusion.Blazor.Maps;

namespace NearMe.Services;

public class StoreLocationService
{
    private readonly BlazorStoreFinderContext _context;

    public StoreLocationService(BlazorStoreFinderContext context)
    {
        _context = context;
    }

    public async Task<List<StoreLocations>> GetStoreLocations()
    {
        return await
            _context.StoreLocations.OrderBy(x => x.Id)
                .ToListAsync();
    }

    public async Task<StoreLocations> GetStoreLocation(int id)
    {
        return await
            _context.StoreLocations.FindAsync(id);
    }

    public async Task<StoreLocations> AddStoreLocation(StoreLocations storeLocation)
    {
        _context.StoreLocations.Add(storeLocation);
        await _context.SaveChangesAsync();
        return storeLocation;
    }

    public async Task DeleteStoreLocation(int id)
    {
        var storeLocation =
            await _context.StoreLocations.FindAsync(id);

        _context.StoreLocations.Remove(storeLocation);
        await _context.SaveChangesAsync();
    }

    public async Task<Coordinate> GeocodeAddress(string address)
    {
        Coordinate coordinate = new Coordinate();

        using var client = new HttpClient();

        var accessToken = await AuthService.GetAccessToken();

        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Add("x-ms-client-id", AuthService.ClientId);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        string url = $"https://atlas.microsoft.com/search/address/json?api-version=1.0&language=en-US&query={Uri.EscapeDataString(address)}";

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var addressResult = await client.GetFromJsonAsync<SearchAddressResult>(url, jsonOptions);

        var position = addressResult?.Results?.FirstOrDefault()?.Position;

        if (position != null)
        {
            coordinate = new Coordinate
            {
                Longitude = position.Lon,
                Latitude = position.Lat
            };
        }

        return coordinate;
    }
}