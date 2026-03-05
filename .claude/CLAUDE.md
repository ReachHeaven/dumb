# Project Context

## Architecture
- Service-Oriented MonoBehaviour Architecture
- Foundation (reusable) / Project (game-specific) separation
- Assembly Definitions: Foundation.asmdef, Game.asmdef
- Game depends on Foundation, Foundation depends on nothing

## Key Patterns
- ServiceLocator for system access (no singletons)
- ScriptableObject Events for decoupling
- Command Pattern for player actions
- ObjectPool for frequent spawn/destroy
- StateMachine for game states

## Code Style
- Private fields: _camelCase with [SerializeField]
- No public fields, use properties
- Interfaces: IPascalCase
- Constants: PascalCase
- One class per file, filename = class name
- Braces on new line (Allman style)
- XML docs on public members

## Scene Philosophy
- Scenes are minimal: Bootstrap + Camera + Canvas
- Dynamic objects spawned from prefabs via code
- Inspector for data tuning, code for logic
- SO Events for cross-system communication

## Project Structure
- Assets/_Foundation/Runtime/ — reusable code
- Assets/_Project/Runtime/ — game-specific code
- Assets/_Project/Art/ — sprites, audio, fonts
- Assets/_Project/Prefabs/ — prefab assets
- Assets/_Project/ScriptableObjects/ — SO configs
- Assets/_Project/Scenes/ — game scenes
