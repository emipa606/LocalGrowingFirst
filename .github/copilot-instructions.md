# GitHub Copilot Instructions for Local Growing First (Continued) Mod

## Mod Overview and Purpose

**Local Growing First (Continued)** is a mod for RimWorld, initially developed by FuriouslyEloquent and continued to ensure compatibility and improvements. The primary objective of this mod is to enhance the prioritization of pawns' activities within their respective growing zones. Specifically, it ensures that pawns will opt to harvest or sow crops within their current zone before proceeding to other zones, enhancing efficiency and localized task management.

## Key Features and Systems

- **Zone Prioritization**: Pawns prioritize growing tasks within their current growing zone, improving task efficiency and overall colony management.
- **Save Game Compatibility**: The mod is fully compatible with existing save games, where it can be seamlessly added or removed without issues.
- **Debugging Support**: Users are encouraged to troubleshoot by isolating the mod and checking for compatibility with other mods, utilizing tools like the Log Uploader for error reporting.

## Coding Patterns and Conventions

- **C# Naming Conventions**: Classes and methods follow the PascalCase convention. For instance, classes like `DefModExt_LocalZoneWrapping` and methods like `createLocalWorkGiverDef`.
- **Class Responsibilities**: Structuring of classes is maintained to encapsulate specific functionality, such as utility functions or the handling of work givers.
- **Mod Consistency**: Consistency is maintained across classes and methods to ensure ease of understanding and modification.
- **Commenting and Documentation**: Ensure that code is well-commented, explaining complex logic or decisions to facilitate further development or debugging efforts.

## XML Integration

While the current version of the mod does not include XML files, XML integration is crucial for modding RimWorld, especially for defining new game objects, items, or configurations. Developers are encouraged to follow these best practices:

- **Defining New Defs**: Utilize XML files to define new game defs when extending game content.
- **LoadOrder.xml**: Use a LoadOrder.xml file to manage the priority and loading order of different XML defs.
- **Integration with C#**: Ensure that XML defs are properly linked with C# logic, using unique def names and consistent namespaces.

## Harmony Patching

- **Purpose**: Harmony is used for modifying existing game behavior without altering base game files. It allows for injection of additional logic or alteration of existing methods.
- **Best Practices**:
  - Patch methods with efficiency in mind. Avoid unnecessary or overly complex patches that might degrade game performance.
  - Use descriptive naming for patches to easily identify their purpose and point of injection.
  - Test patches thoroughly across different game scenarios to ensure stability and conflict-free integration.
  
## Suggestions for Copilot

The following suggestions will help Copilot in understanding and expanding the codebase for this RimWorld mod:

1. **Suggestions for Task Prioritization Logic**:
   - Propose optimized algorithms for selecting the nearest or most immediate growing zones for pawns.
   - Suggest improvements or alternatives for `GetPriority` and task evaluation methods.
  
2. **Enhanced Debugging Helpers**:
   - Recommend additional utility methods that provide more granular logging and error checking.
   - Offer insights or traces into work giver efficacy and pawn behavior related to zone prioritization.

3. **Conditionals and Check Methods**:
   - Provide condition-based assists. For example, improve on the logic within `ShouldSkip` or `HasJobOnThing` to ensure they align comprehensively with game goals and conditions.

4. **Exploration of Unreachable Areas Logic**:
   - Propose enhancements to the `AllowUnreachable` method, aiming to better define pawns' movement logic in geography-complicated map areas.

By utilizing these guidelines and insights, developers can efficiently manage, extend, and maintain the Local Growing First (Continued) mod for RimWorld.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

