using System.Collections;

public interface IGameMode
{
    IEnumerator OnStart();
    IEnumerator OnEnd();
}
