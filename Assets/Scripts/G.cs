public class G : Singleton<G> {
    // Prevent non-singleton constructor use.
    protected G() { }

    public int MAX_PLAYERS = 4;
}
