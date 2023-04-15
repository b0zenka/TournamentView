using System.Collections.Generic;
using System.Linq;

public abstract class Pool<T>
    where T : IPoolElement
{
    protected readonly List<T> poolElements = new List<T>();

    public IPoolElement Peek()
    {
        var freePlayerData = poolElements.FirstOrDefault(x => x.IsFree);

        if (freePlayerData == null)
        {
            freePlayerData = CreateNewPlayerData();
            poolElements.Add(freePlayerData);
        }

        return freePlayerData;
    }

    public void FreeAll()
    {
        poolElements.ForEach(x => x.Free());
    }

    protected abstract T CreateNewPlayerData();
}
