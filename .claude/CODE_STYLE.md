# Code Style

## Naming
| Element | Convention | Example |
|---------|-----------|---------|
| Private field | _camelCase | _moveSpeed |
| [SerializeField] private field | camelCase | moveSpeed |
| Public property | PascalCase | MoveSpeed |
| Constant | PascalCase | MaxHealth |
| Interface | IPascalCase | IAudioService |
| Method | PascalCase | TakeDamage() |
| Parameter | camelCase | damageAmount |
| Local variable | camelCase | currentHealth |

## Fields
- NEVER public fields
- [SerializeField] private for Inspector-exposed
- readonly when possible
- Order: [SerializeField] fields → private fields → properties → Unity methods → public methods → private methods

## Formatting
- Allman braces (new line)
- var only when type obvious from right side
- XML docs on all public members
- Usings sorted, System.* first
- One class per file, filename = class name

## Unity Rules
- No Find/FindObjectOfType in runtime
- No Camera.main in loops (cache it)
- No string tag comparisons (use constants or SO)
- Prefer TryGetComponent over GetComponent
- Null-check UnityEngine.Object with == null (not ?.)
- Unsubscribe events in OnDisable/OnDestroy
- Cache WaitForSeconds in coroutines

## Performance
- No allocations in Update (no new, no LINQ, no string concat)
- Initialize collections with capacity when size known
- Use object pooling for frequent spawn/destroy
- Profile on target device, not in Editor
- 20-30% dev budget for optimization from the start

## ECS-Inspired Practices (without DOTS)
- ScriptableObjects as pure data containers (configs, stats)
- MonoBehaviours as thin processors (read data, execute, raise events)
- Avoid god-objects: one responsibility per component
- Prefer composition over inheritance
- If a component has both data AND complex logic, split it
