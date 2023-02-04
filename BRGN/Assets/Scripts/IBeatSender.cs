public interface IBeatSender
{
    public void SendBeat();
    public void RegisterListener(IBeatFollower follower);
    public void UnRegisterListener(IBeatFollower follower);
}