using System.Collections.Generic;
using System.Threading.Tasks;

public interface IExportPlayerSumResult
{
    Task<bool> ExportAsync(string path, IEnumerable<IPlayerSumResult> playerSumResults);
}