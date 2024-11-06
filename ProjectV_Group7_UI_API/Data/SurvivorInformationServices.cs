using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class SurvivorInformationService
{
    private List<Survivor_Information> survivors;

    public SurvivorInformationService()
    {
        survivors = LoadSurvivors();
    }

    private List<Survivor_Information> LoadSurvivors()
    {
        if (!File.Exists("survivor-information.json"))
            return new List<Survivor_Information>();

        var json = File.ReadAllText("survivor-information.json");
        return JsonSerializer.Deserialize<List<Survivor_Information>>(json) ?? new List<Survivor_Information>();
    }

    public List<Survivor_Information> GetSurvivors()
    {
        return survivors;
    }

    public void AddSurvivor(Survivor_Information survivor)
    {
        survivors.Add(survivor);
        SaveSurvivors();
    }

    private void SaveSurvivors()
    {
        var json = JsonSerializer.Serialize(survivors, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("survivor-information.json", json);
    }
}
