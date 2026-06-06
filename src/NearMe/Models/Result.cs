#nullable disable
using System.Text.Json.Serialization;

namespace NearMe.Models;

public class Result
{
    [JsonPropertyName("type")] public string Type { get; set; }
    [JsonPropertyName("id")] public string Id { get; set; }
    [JsonPropertyName("score")] public double Score { get; set; }
    [JsonPropertyName("address")] public Address Address { get; set; }
    [JsonPropertyName("position")] public Position Position { get; set; }
    [JsonPropertyName("viewport")] public Viewport Viewport { get; set; }
    [JsonPropertyName("entryPoints")] public EntryPoint[] EntryPoints { get; set; }
}
