namespace GameOfLife.Core;

public class Game
{
    public async ValueTask Run(CancellationToken cancellationToken)
    {
        await Initialize();
        while (!cancellationToken.IsCancellationRequested)
        {
            await Input();
            await Update();
            await Render();
        }
    }

    private ValueTask Input()
    {
        return ValueTask.CompletedTask;
    }
    
    private ValueTask Update()
    {
        return ValueTask.CompletedTask;
    }
    
    private ValueTask Render()
    {
        return ValueTask.CompletedTask;
    }
    
    private ValueTask Initialize()
    {
        return ValueTask.CompletedTask;
    }
}