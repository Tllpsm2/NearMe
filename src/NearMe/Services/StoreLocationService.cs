#nullable disable
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Data.SqlClient;
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

        string url =
            $"https://atlas.microsoft.com/search/address/json?api-version=1.0&language=en-US&query={Uri.EscapeDataString(address)}";

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

    public async Task<SearchAddressResultReverse> GeocodeReverse(Coordinate coordinate)
    {
        using var client = new HttpClient();

        var accessToken = await AuthService.GetAccessToken();

        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Add("x-ms-client-id", AuthService.ClientId);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        string url = string.Format(System.Globalization.CultureInfo.InvariantCulture,
            "https://atlas.microsoft.com/search/address/reverse/json?api-version=1.0&language=en-US&query={0},{1}",
            coordinate.Latitude, coordinate.Longitude);

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var result = await client.GetFromJsonAsync<SearchAddressResultReverse>(url, jsonOptions);

        return result;
    }

    public async Task<List<StoreSearchResult>> GetNearbyStoreLocationsAsync(Coordinate coordinate)
    {
        var colStoreLocations = new List<StoreSearchResult>();

        string wktPoint = string.Format(System.Globalization.CultureInfo.InvariantCulture, "POINT({0} {1})", coordinate.Longitude, coordinate.Latitude);

        string sql = """
                     DECLARE @Distance AS INT = 25;
                     DECLARE @location sys.geography = geography::STPointFromText(@WktPoint, 4326);

                     SELECT
                         [LocationName],
                         [LocationAddress],
                         [LocationLatitude],
                         [LocationLongitude],
                         [LocationData].STDistance(@location) / 1000.0 AS [DistanceInKm]
                     FROM [StoreLocations]
                     WHERE [LocationData].STDistance(@location) / 1000.0 < @Distance
                     ORDER BY [LocationData].STDistance(@location) / 1000.0;
                     """;

        await using var connection = new SqlConnection(_context.Database.GetConnectionString());
        await using var command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@WktPoint", wktPoint);

        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            colStoreLocations.Add(new StoreSearchResult
            {
                LocationName = reader["LocationName"].ToString(),
                LocationAddress = reader["LocationAddress"].ToString(),
                LocationLatitude = Convert.ToDouble(reader["LocationLatitude"], System.Globalization.CultureInfo.InvariantCulture),
                LocationLongitude = Convert.ToDouble(reader["LocationLongitude"], System.Globalization.CultureInfo.InvariantCulture),
                Distance = Convert.ToDouble(reader["DistanceInKm"])
            });
        }

        return colStoreLocations;
    }
}
