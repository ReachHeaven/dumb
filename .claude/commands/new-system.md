Create a new game system following project architecture.

Ask for:
1. System name (e.g., Audio, Score, Save)
2. What it does (one sentence)

Then generate:
1. Interface in Foundation/Services/ (IXxxService.cs)
2. Implementation stub in Game/Core/ (XxxService.cs : MonoBehaviour, IXxxService)
3. Registration line for Bootstrap
4. ScriptableObject config if system needs tunable data

Follow all rules from .claude/CODE_STYLE.md.
Include XML docs on all public members.
Separate data (SO) from logic (MB).
