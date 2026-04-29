# HSK-Race-Collection

HSK/CE-patched conversions of custom **race mods** for RimWorld, tuned for the [Hardcore SK modpack](https://github.com/skyarkhangel/Hardcore-SK) and bundled with the community CE compatibility patches.

This repository contains race mods restructured to follow HSK's crafting conventions — class-component pipelines, refined-alloy economy, and HSK weapon/tailoring/smithing bench routing.

## Included mods

| Folder | Upstream | Status | Notes |
|---|---|---|---|
| [`HSK-Wolfein-Race/`](./HSK-Wolfein-Race) | [Wolfein Race](https://steamcommunity.com/sharedfiles/filedetails/?id=3473140562) | Stable | All-in-one fork: Race + Gene Patch + CE Patches bundled. 70 weapons + ~75 apparel rerouted to HSK benches with HSK class-component additions |

## Requirements

- **Hardcore SK** (Core_SK and its ecosystem)
- **Combat Extended** (for CE patches; gracefully degraded if absent)
- **Harmony**
- **Humanoid Alien Races**
- **Biotech DLC**

Each subfolder lists its full dependencies in `About/About.xml`.

## Install

Clone or download this repo. Place each subfolder in your RimWorld `Mods/` directory (or point your HSK launcher at this repo URL — it will scan the subfolders automatically). Enable the individual mods in your modlist.

Do **not** enable the upstream original versions alongside these — each HSK conversion is marked `incompatibleWith` its upstream package IDs in its `About.xml`.

## Authorship

- Original mods by their respective authors (credited in each subfolder's `About.xml`)
- HSK/CE conversion, recipe re-routing, integration patches: **CarbineAction**
- CE patches by the **Combat Extended community**

## License

Each subfolder follows the original mod author's license where applicable. The HSK conversion / compatibility work (XML patches, ingredient swaps, bench routing) is released under the same terms as the upstream mods — free use, modification, and redistribution, credit appreciated.

## Contact

Issues / suggestions / PRs → open an issue on this repo.
