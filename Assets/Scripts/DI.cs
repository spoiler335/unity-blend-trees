

public class DI
{
    public static readonly DI di = new DI();

    public InputManager inputManager { get; private set; }

    public void SetInputManager(InputManager inputManager) => this.inputManager = inputManager;

    private DI() { }
}