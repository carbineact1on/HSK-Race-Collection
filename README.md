# HSK-Race-Collection

HSK/CE-patched conversions of custom **race mods** for RimWorld, tuned for the [Hardcore SK](https://github.com/skyarkhangel/Hardcore-SK) modpack and bundled with the community Combat Extended compatibility patches.

Race mods are restructured to follow HSK's crafting conventions — class-component pipelines, refined-alloy economy, and HSK weapon/tailoring/smithing bench routing.

## Requirements

- **RimWorld 1.5**
- **Harmony**
- **Hardcore SK Modpack**
- **Combat Extended** (CE patches degrade gracefully if absent)
- **Humanoid Alien Races**
- **Biotech DLC**

Each subfolder lists its full dependencies in `About/About.xml`.

## What's Inside

### 🐺 HSK-Wolfein-Race
All-in-one fork of the **[Wolfein Race](https://steamcommunity.com/sharedfiles/filedetails/?id=3473140562)** mod (Race + Gene Patch + CE Patches bundled into one drop-in mod).

Fully native to HSK:

- **70+ weapons** re-recipe'd with HSK class-component pipeline (Mechanism, Electronics, Microchips, Wolfein racial alloy)
- **~75 clothing/apparel items** use `Wolfein_CompositeFabric` + Cloth instead of generic stuff categories
- **All structures** use Wolfein racial alloy + components, HSK power tuning
- **Mech recipes** use racial parts (civilian + military + drones + giant)
- **Auto-routes** to RN benches via HSKRNRouter for matching calibers

#### CE balance pass (mechs, weapons, armor)
- **All 12 mechs** with proper CE comp stack (`CompPawnGizmo`, `CompAmmoGiver`, `CompProperties_MechAmmo`) and HSK CE armor scale (Drone Sharp 4 → MilitaryC/E Sharp 18)
- **Mech melee tools** converted to `<li Class="CombatExtended.ToolCE">` with armor-penetration values scaled by mech power tier
- **Sensor weak-points** added via `PartialArmorExt` so head/sensor shots have reduced armor (CE community pattern)
- **17 apparel pieces** rebalanced for HSK CE armor scale (PowerArmor Sharp 12-34 → 40-50, GuardPowerArmor Mass 80 → 25)
- **12 ranged weapon outlier fixes** — HG_HomemadePistol range 12 → 18, ChargeMinigun burst 3 → 20, etc.
- **5 turrets** swapped to `Building_TurretGunCE` for proper CE ammo consumption — sentry rifle (7.62×51 NATO), rapid-fire MG (7.62×39 Soviet), laser, twin autocannon (.50 BMG), and rocket turret (130mm rocket missile, mortar-arc fire)

#### Faction tuning
- **MegaCorp raids gated** to mid-late game via `raidCommonalityFromPointsCurve` `(0,0) → (1500,1)` — no early-game megacorp drops
- **Multi-Legged Firepower Platform disabled** — removed from MegaCorp raid pool, manufacture, resurrect (was overpowered for HSK economy)

## Installation

1. Clone or download this repo
2. Place each subfolder in your RimWorld `Mods/` directory (or point your HSK launcher at this repo URL — it will scan the subfolders automatically)
3. Enable the individual mods in your modlist
4. Load **after** Hardcore SK, Combat Extended, Humanoid Alien Races, and the upstream race mods if you have them disabled

⚠ **Do not enable the upstream original versions alongside these.** Each HSK conversion is marked `incompatibleWith` its upstream `packageId`s in `About.xml` — the launcher will warn you if both are active.

## How It Works

Each conversion is a self-contained replacement of the upstream race mod:

- **Recipes** re-pointed at HSK benches with HSK research gates
- **Materials** mapped from vanilla generics to race-specific alloys + HSK component tiers
- **Combat Extended** patches add `Verb_ShootCE`, `CompProperties_AmmoUser`, `AmmoSet`, and proper projectile bindings to every weapon
- **Mech CE compat** adds the same comp stack HSK-CE adds to vanilla mechanoids (CompPawnGizmo, CompAmmoGiver, CompProperties_MechAmmo) so the race's mechs interact with the CE ammo system
- **HSKRNRouter integration** auto-routes weapon recipes to RN benches based on each weapon's `ammoSet`

## Reporting Issues

If you find a bug, please attach your `Player.log` and a description of which subfolder the issue is in. Issues that don't include logs may be closed.

## Authorship

- Original mods by their respective authors (credited in each subfolder's `About.xml`)
- HSK/CE conversion, recipe re-routing, integration patches, mech CE compat: **CarbineAction**
- CE patches by the **Combat Extended community**
- HSK material economy and bench conventions: **Hardcore SK Team**

## License

Each subfolder follows the original mod author's license where applicable. The HSK conversion / compatibility work (XML patches, ingredient swaps, bench routing) is released under the same terms as the upstream mods — free use, modification, and redistribution. Credit appreciated.

## Contact

Issues / suggestions / PRs → open an issue on this repo.
