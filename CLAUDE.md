# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Fenrir 13 is an interactive fiction text adventure game written in C# (.NET 6.0). The story is set on the cargo freighter "Fenrir 13" on a mission to the moons of Proxima Centauri b. The game is text-based, runs as a console application, and is written in German (content/resources).

## Build & Run Commands

```bash
# Build
dotnet build Fenrir13.sln

# Run (interactive play)
dotnet run --project Fenrir13/Fenrir13.csproj

# Run with a saved game file
dotnet run --project Fenrir13/Fenrir13.csproj -- -F "SavedGames/heretic_savedgame.txt"

# Publish for a specific platform (example: Windows x64)
dotnet publish Fenrir13/Fenrir13.csproj -c Release -r win-x64 --self-contained true
```

There are no automated tests — the project is tested through manual play-testing.

## Architecture

The game is built on top of the **Heretic.InteractiveFiction** NuGet framework (v0.11.0), which provides the core game loop, text parser, verb handling, and printing abstraction. Fenrir 13 supplies the world definition, story content, and event logic on top of that framework.

### Key Layers

- **`Program.cs`** — Entry point; sets up DI container and starts the game host via `Microsoft.Extensions.Hosting`.
- **`GamePlay/GamePrerequisitesAssembler.cs`** — Wires up the entire game world: creates the `Universe`, registers all locations, items, characters, and initial events.
- **`GamePlay/Prerequisites/<LocationName>/`** — Each of the 17 ship locations is defined in its own directory. Each exposes a static `Get()` factory method that returns a configured `Location` with its items, exits (`DestinationNode`s), and event handler registrations.
- **`Events/EventProvider.cs`** — Central hub (~1000 LOC) for all game event handlers. Game logic (puzzle solutions, story triggers, state changes) lives here.
- **`Resources/*.resx`** — All displayed text (descriptions, item names, location names, metadata) is in `.resx` resource files in German, keeping content separate from code.
- **`Printing/ConsolePrintingSubsystem.cs`** — Implements `IPrintingSubsystem` from the framework; handles console output and word-wrapping.
- **`Help/HelpSubsystem.cs`** — In-game help text.
- **`Cli/CommandHandler.cs`** — Parses CLI arguments (file path, console width) using `PowerArgs`.

### World Model

- The `Universe` holds a `LocationMap` (graph) of `Location` objects.
- `Location`s contain `Item` collections and `DestinationNode` lists (directional exits).
- The player starts in `CryoChamber`.
- 17 interconnected locations form the interior of the spaceship.

### Event System

Game state and puzzle logic are entirely driven by events registered in `EventProvider`. When the player interacts with an object (e.g., uses an item, moves to a location), the framework fires the registered handler. All consequential story state is tracked via flags/state mutated inside these handlers.

### Scoring

`ScoreBoard` tracks 9 quests (`QUEST_I` through `QUEST_IX`). Scores are registered via `RegisterScore()` calls scattered across event handlers in `EventProvider`.

### German Grammar

The framework's `GermanGrammar` and `GermanVerbHandler` support natural-language German parsing. Items carry `IndividualObjectGrammar` (gender: Male/Female/Neutrum) to enable correct German adjective agreement in output.

## Key Dependencies

| Package | Version | Purpose |
|---|---|---|
| `Heretic.InteractiveFiction` | 0.11.0 | Core IF game engine |
| `Microsoft.Extensions.Hosting` | 8.0.1 | DI and app lifecycle |
| `PowerArgs` | 4.0.0 | CLI argument parsing |

## Adding a New Location

1. Create `GamePlay/Prerequisites/<LocationName>/<LocationName>Prerequisites.cs` with a static `Get()` method returning a configured `Location`.
2. Register exits by adding `DestinationNode` entries on neighbouring locations.
3. Register the location in `GamePrerequisitesAssembler`.
4. Add any event handlers in `EventProvider`.
5. Add all display strings to the appropriate `.resx` resource files.
