# LootWizard
**LootWizard** is a tool designed to simplify the management of your Tarkov loot configuration files, used by various online tools for tracking your in-game metrics. This project is completely transparent, and for those interested, you can build it yourself.

### What is LootWizard?
LootWizard is a user-friendly tool for generating an ini file contianing loot ID's of items.
It allows you to favorite items, track items needed for quests, and manage your loot easily through the GUI. It supports searching, filtering by selected items and favorites. All your settings, including quest items tracking, favorites, and selected items, are saved persistently in plain text cache files in the executable's directory.

### Key Features
- **Generate JSON Files:** LootWizard exclusively generates `json` files without interacting with the game or windows internals.
- **Quest Tracking:** Simple up/down buttons for tracking quest items.
- **Automatic Updates:** The loot list updates automatically when you select an item.
- **Initial API Data Fetching:** First run fetches data from the API (a few seconds delay).
- **Settings Tab:** Currently lets you change the output path of the generated file, persistant.
- **Quest Display:** Only quests with turn-in item requirements are displayed.
- **Dark Mode:** Finally!

### Upcoming Features
- **Filter by Item Type:** Ammo, backpacks, meds, etc. (in progress).

## Technical Details
LootWizard uses the open-source Tarkov API (Tarkov.dev) for item and quest data, auto-updating daily. It caches this data for efficiency. The images for items are baked into the resources, so new game items will need an update to show images.

## Installation & Usage

```markdown
1. Run: Execute `LootWizard.exe`. The first start will populate data from the API.
2. Use: Select and favorite items, with easy access for future reference.
3. Integration: Use the generated `loot_generated.ini` file for your applications.
4. Profit!
```

### Important Notes
- LootWizard does **NOT** read/write game memory.
- The quest tracking feature is for convenience and does not impact the loot list (selecting items from here still does).
- The application might take a few seconds on first run to populate data from the API.
- This is an early version; expect some bugs and limitations.

---

Your feedback and contributions are welcome! Happy looting with LootWizard! 🧙‍♂️💼🔮


## Credits & Attributions

### Tarkov.Dev API
This project makes use of the **Tarkov API** developed and maintained by **The Hideout**. The Tarkov API offers comprehensive data about items and quests in Tarkov, contributing significantly to the functionality of LootWizard.
For more information about the Tarkov API or to contribute, visit their GitHub repository: [The Hideout - Tarkov API](https://github.com/the-hideout/tarkov-api).
