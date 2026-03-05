Review all changed C# files in this project.

For each file:
1. Read the file completely
2. Check against every rule in .claude/CODE_STYLE.md
3. Check against architecture rules in .claude/ARCHITECTURE.md
4. Report issues grouped by severity:
   - ERROR: breaks architecture or will cause bugs
   - WARNING: violates code style or performance risk
   - SUGGESTION: could be improved but works fine

Format:
## filename.cs
- [ERROR] line X: description
- [WARNING] line X: description
- [SUGGESTION] line X: description

If file passes all checks: ✅ filename.cs — clean

End with summary: total errors, warnings, suggestions.
