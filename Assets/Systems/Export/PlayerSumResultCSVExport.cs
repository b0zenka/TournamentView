using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class PlayerSumResultCSVExport : IExportPlayerSumResult
{
    public async Task<bool> ExportAsync(string path, IEnumerable<IPlayerSumResult> playerSumResults)
    {
        try
        {
            var directory = Path.GetDirectoryName(path);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var csvResult = string.Join(Environment.NewLine, playerSumResults
             .Select(r => $"{r.Player};{r.Points};{r.Wins};{r.Draws};{r.Defeats}"));

            await File.WriteAllTextAsync(path, $"Gracz;Punkty;Wygrane;Remisy;Przegrane{Environment.NewLine}{csvResult}");
            return true;
        }
        catch
        {
            return false;
        }
    }
}