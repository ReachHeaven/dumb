# Architecture Reference

## Principles
1. Foundation knows only interfaces. Game provides implementations.
2. Systems communicate via SO Events, never direct references.
3. Scenes are minimal. Dynamic objects spawned from prefabs.
4. Inspector for data, code for logic.
5. No singletons. ServiceLocator for global access.
6. Data and logic separated (inspired by ECS even in MonoBehaviour).

## Folder Structure
```
Assets/
  Foundation/          — reusable across all games
    Events/            — GameEvent, GameEventListener, generics
    Services/          — ServiceLocator
    Patterns/          — StateMachine, IState
    Utils/             — future helpers
  Game/                — game-specific code
    Core/              — Bootstrap, GameManager
    Data/              — shared ScriptableObject data containers (e.g. BoardData)
    Gameplay/          — player, enemies, mechanics
    UI/                — screens, HUD
  Art/
    Sprites/
    Audio/
    Fonts/
  Prefabs/
  ScriptableObjects/
  Scenes/
```

## Communication Flow
```
[System A] → Raise(GameEvent asset) → [GameEventListener on System B]
                                     → [GameEventListener on System C]
```
No system knows about the others. All know the event asset.

### Shared SO Data Bus
ScriptableObjects that hold runtime state (e.g. `BoardData`) act as a shared data bus:
```
[System A] → writes to SO → [SO asset] → read by [System B], [System C]
```
Any layer (Core, UI, Gameplay) may hold a `[SerializeField]` reference to a shared data SO.
This is NOT a coupling violation — the SO is the decoupling mechanism, not a direct system reference.

## Service Access Flow
```
[Bootstrap] → Register<IService>(implementation)
[Any code]  → ServiceLocator.Get<IService>().DoThing()
```

## State Flow
```
Bootstrap → SwitchTo<MenuState>()
Menu      → SwitchTo<PlayingState>()
Playing   → SwitchTo<GameOverState>()
GameOver  → SwitchTo<MenuState>()
```
Each state: Enter() → Update() every frame → Exit() on switch.

## Data-Logic Separation (ECS-inspired)
Even without DOTS, separate data from behavior:
- ScriptableObject = data definition (stats, configs, balance)
- MonoBehaviour = behavior (reads SO, executes logic)
- GameEvent = communication channel

Example:
```
EnemyConfig.asset (SO)     → HP: 100, Speed: 3, Damage: 10
EnemyBehaviour.cs (MB)     → reads config, executes AI
OnEnemyDied.asset (Event)  → notifies score, audio, VFX
```

This mirrors ECS thinking:
- Entity = GameObject
- Component = ScriptableObject (data) + [SerializeField] fields
- System = MonoBehaviour methods / Services

## When to consider DOTS/ECS
- Thousands of identical entities (bullets, particles, crowd)
- Performance-critical simulation (physics, pathfinding for many agents)
- NOT needed for: UI, game states, audio, saving, small-scale games

## Bootstrap Pattern
```csharp
public class Bootstrap : MonoBehaviour
{
    private StateMachine _stateMachine;

    private void Awake()
    {
        RegisterServices();
        SetupStateMachine();
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    private void RegisterServices()
    {
        ServiceLocator.Register<IAudioService>(GetComponent<AudioService>());
    }

    private void SetupStateMachine()
    {
        _stateMachine = new StateMachine();
        _stateMachine.AddState(new MenuState());
        _stateMachine.AddState(new PlayingState());
        _stateMachine.AddState(new GameOverState());
        _stateMachine.SwitchTo<MenuState>();
    }
}
```
