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
- **91 apparel pieces** fully rescaled to HSK CE armor scale (vanilla CE 0.10-1.00 → HSK CE 1-13 Sharp / 2-33 Blunt) with proper ballistic ratio (Blunt > Sharp). Power armor matches HSK PowerArmor (10/28); top-tier AIPT matches HellPowerArmor (13/33). Includes underwear/civilian/light_uniform/light_shell/combat_shell/flak/power tiers + matching helmets.
- **12 ranged weapon outlier fixes** — HG_HomemadePistol range 12 → 18, ChargeMinigun burst 3 → 20, etc.
- **5 turrets** swapped to `Building_TurretGunCE` for proper CE ammo consumption — sentry rifle (7.62×51 NATO), rapid-fire MG (7.62×39 Soviet), laser, twin autocannon (.50 BMG), and rocket turret (130mm rocket missile, direct-fire `Verb_ShootCE` — HSK-turret-check compatible)

#### Faction tuning
- **MegaCorp raids gated** to mid-late game via `raidCommonalityFromPointsCurve` `(0,0) → (1500,1)` — no early-game megacorp drops
- **Multi-Legged Firepower Platform disabled** — removed from MegaCorp raid pool, manufacture, resurrect (was overpowered for HSK economy)

### 🦎 HSK-VRE-Saurid
Standalone bundle of **[Vanilla Races Expanded — Saurid](https://steamcommunity.com/sharedfiles/filedetails/?id=2880990495)** by Oskar Potocki and Neronix17. Lizard-like xenohuman race with scale skin, claws, and oviparous reproduction.

CE patches (adapted from Pacas Patches Compilation, re-tuned for HSK CE scale):
- `VRESaurids_ScaleSkin` gene — natural armor at HSK CE scale (Sharp 4 / Blunt 4, between bare skin and light flak; Pacas's vanilla CE 1.0/1.0 was functionally invisible under HSK CE math)
- `VRESaurids_SauridClaws` hediff — claws verb converted to `CombatExtended.ToolCE` with proper Sharp/Blunt armor penetration

Saurid is content-light (no recipes, no buildings, no costLists, no weapons, no apparel) — most HSK material conversion is moot since there's nothing to convert.

### 🌟 HSK-VRE-Archon
Standalone bundle of **[Vanilla Races Expanded — Archon](https://steamcommunity.com/sharedfiles/filedetails/?id=3067715093)** by Oskar Potocki, Sarg Bjornson, Taranchuk, Reann Shepard, xrushha. Archotech-tier psychic warrior xenohumans.

CE patches:
- `VREA_MeleeWeapon_ArchobladeBladelink` (psychic blade) — both tools converted to `CombatExtended.ToolCE` (handle Sharp 0.3/Blunt 3, blade Sharp 3/Blunt 5 archotech-tier AP). Custom `VREArchon.ArchoBlade` thingClass and `VREA_Sear` capacity preserved.
- `VREA_Apparel_Archoplate` (archotech psychic armor) — Sharp 1.15→30, Blunt 0.60→25, +Bulk 12 / WornBulk 4.5 (HSK CE archotech tier between marine 24 and power armor 40+). Heat 0.86 kept (already CE-compatible). PsychicSensitivity / EntropyMax / EntropyRecoveryRate equippedStatOffsets preserved.
- `VRE_Leatherskin` gene — Sharp 0.15/Blunt 0.1 → HSK CE 5/5 (mid-tier natural armor)
- Psychic projectiles (PsychicThrow/Warp/Lance) work under CE as-is — already have armorPenetrationBase + cast via Verb_CastAbility, not weapon-fired

Faction raid gating (VRE_Archons):
- Existing earliestRaidDays:45 kept
- Added `raidCommonalityFromPointsCurve` `(0, 0) → (2500, 0) → (5000, 1)` — Archotech-tier raids gated to colony threat 2500-5000+ instead of firing at full force after Day 45 regardless of wealth. ArchonWarrior combatPower 450 deserves a wealth gate (HSK MegaCorp pattern, scaled higher for archotech).

### ☢️ HSK-VRE-Waster
Standalone bundle of **[Vanilla Races Expanded — Waster](https://steamcommunity.com/sharedfiles/filedetails/?id=2983471725)** by Oskar Potocki, Sarg Bjornson. Toxic-wasteland xenohumans that thrive on pollution.

HSK building rewrite (1 building, full HSK pattern):
- `VRE_BurnBarrel`: switched from fixed Steel 45 → `stuffCategories RuggedMetallic` + costStuffCount 45 + `ComponentIndustrial` 1. Outdoor/weather-exposed metal fixture pattern (HSK convention) with a single fab component for the sealed combustion barrel. Bottom of the pollution-tier vs HSK's WastepackAtomizer high-tech atomizer.

No CE work needed — Waster's 17 pollution-themed genes (ToxAbsorption, PollutionRage, PollutionRegeneration, PollutionCognition, PollutionSustenance, PollutionAccuracy, etc.) and 11 pollution stimulus hediffs are pure stat-based mechanics with no tools, verbs, armor offsets, weapons, or apparel.

### 🤖 HSK-VRE-Android
Standalone bundle of **[Vanilla Races Expanded — Android](https://steamcommunity.com/sharedfiles/filedetails/?id=2975771801)** by Oskar Potocki, Taranchuk, ISOREX, Sarg Bjornson. Synthetic xenohumans assembled from artificial body parts.

HSK building rewrites (6 production/utility buildings, full HSK pattern):
- Switched from fixed Steel/Gold to `stuffCategories Metallic + costStuffCount`
- Added HSK class components (`ElectronicComponents`, `Mechanism`, `Electronics`, `Microchips`) per building tier
- AndroidStand (minimal), AndroidPartWorkbench (industrial), SubcorePolyanalyzer (ultratech analyzer), AndroidCreationStation (high-tech assembler), AndroidBehavioristStation (software bench, GoldBar 20), NeutroCasket (storage)

FULL HSK conversion on all 15 body parts — rewritten to match HSK Bionic / Cybernetic body part patterns from `Core_SK/Defs/BodyParts/`. Building a full android is now a premium endgame project comparable to outfitting an entire colony with HSK bionic upgrades:
- **Heart-tier organs** (Hyperweave + CarbonAlloy + SyntheticFibers + Microchips/Electronics): NeuroPump, AirFilter, Neutrofilter, MetabolismRegulator (+GoldBar)
- **Cybernetic top-tier** (ComponentUltra + Biomatter + BioMicrochips + Paraffins + ReinforcedGlass): Reactor (+DepletedUranium), ArtificialBrain
- **Sensory** (Biomatter + SyntheticFibers + Paraffins + Microchips): OpticalUnit (BionicEye), AudioProcessor (BionicEar), ArtificialNose
- **Skeletal/structural** (ComponentAdvanced + Biomatter + ArtificialBone + Microchips): ArtificialJaw, ArtificialRibcage, ArtificialSpine (+GoldBar)
- **Limbs** (ComponentAdvanced + Biomatter + SyntheticFibers + ArtificialBone + Microchips): AndroidLeg/Foot/Arm/Hand

No CE work — androids have no natural melee weapons (capability comes from stat-hediff bionic body parts, not Verb-givers).

### 🪲 HSK-VRE-Insector
Standalone bundle of **[Vanilla Races Expanded — Insector](https://steamcommunity.com/sharedfiles/filedetails/?id=3260509684)** by Oskar Potocki, Taranchuk, Sarg Bjornson. Insectoid xenohumans with mandibles, horns, ripper-blades, chitinous armor, and metamorphosis (metapod).

CE patches (4 insectoid melee hediffs → `CombatExtended.ToolCE`):
- `VRE_InsectMandibles` — Cut, Sharp 0.5 / Blunt 2.0
- `VRE_HornAttack` (megaspider horns) — Stab, Sharp 0.4 / Blunt 2.5
- `VRE_RipperBlades` (armor-rending) — Cut, Sharp 0.7 / Blunt 1.5
- `VRE_InsectRostrum` (cross-mod, VFE Insectoid 2 only) — Stab, Sharp 0.6 / Blunt 2.0

HSK CE armor rescale:
- `VRE_Insectskin` gene — Sharp 0.27/Blunt 0.18 → Sharp 4/Blunt 4 (HSK CE basic carapace, matches Saurid scaleskin)
- `VRE_MineralRichInsectskin` hediff (cross-mod, 2 health stages) — low-health 0.48/0.26 → 6/4 and full-health 0.9/0.52 → 12/8 (between flak and marine when intact)

HSK material rebalance:
- `VRE_InfestedShipPart_Spawned` — bumped to ~50% of HSK vanilla ShipChunk's deconstruct yield (was ~37%): SteelBar 30 + ComponentIndustrial 8 + Wire 8 + InsectJelly 5

### 🩸 HSK-VRE-Sanguophage
Standalone bundle of **[Vanilla Races Expanded — Sanguophage](https://steamcommunity.com/sharedfiles/filedetails/?id=2963116383)** by Oskar Potocki, Sarg Bjornson, Erin. Vampire xenohumans with deathrest, hemogen mechanics, and supernatural abilities.

HSK building rewrites (6 deathrest / hemogen buildings) — full HSK Biotech pattern matching the equivalent vanilla building's HSK costs (`Core_SK/Biotech/Patches/ThingDefs_Buildings/Buildings_Deathrest.xml`):
- Switched from fixed Steel to `stuffCategories Metallic + costStuffCount` (build from any metal)
- Added the missing HSK class component pipeline: `ElectronicComponents`, `Mechanism`, `Electronics`, `Microchips` per building tier
- Added `Rubber`, `Plastic`, `CarbonAlloy` minor materials as appropriate
- Kept `HemogenPack` as fuel cost (Biotech HSK convention)
- VRE_InvocationMatrix keeps `GoldBar` 40 for ritual flavor
- VRE_PsychofluidWell (Royalty DLC) gets the high-tier `PsychofluidPump` HSK pattern with CarbonAlloy + extra Rubber

CE patches:
- `VRE_SharpTalons` hediff — talons tool converted to `CombatExtended.ToolCE` with armor-penetration values (Sharp 0.65 / Blunt 2.0) calibrated against the rest of the collection's natural melee weapons

Sanguophage abilities (HeartCrush, Hemosmosis, ToxicCloud, ViscousGoo, AcidSpray, etc.) are psycaster-style and need no CE projectile work. The Goo projectile already has armorPenetrationBase set and works under CE.

### 🌿 HSK-VRE-Phytokin
Standalone bundle of **[Vanilla Races Expanded — Phytokin](https://steamcommunity.com/sharedfiles/filedetails/?id=2927323805)** by Oskar Potocki, Sarg Bjornson, Allie, Erin, Sir Van, Reann Shepard. Plant-based xenohumans with bark skin, photosynthesis, and dryad companions.

CE patches (adapted from Pacas Patches Compilation, re-tuned for HSK CE scale):
- `VRE_BarkSkin` gene — natural bark armor at HSK CE scale (Sharp 6 / Blunt 6, between bare skin and flak; Pacas's vanilla CE 3.5/3.5 was about a third of flak under HSK CE math)
- `VRE_CompanionDryad` (Ideology DLC only) — full CE conversion: 4 ToolCE entries (left claw, right claw, bite with stun surprise attack, head-blunt) + `CombatExtended.RacePropertiesExtensionCE` for proper Quadruped body shape + melee dodge/crit/parry stats

Phytokin has no recipes, no costLists with raw materials, no weapons, no apparel — just genes, hediffs, abilities, the Saplingchild tree, Polux bush, and SapBlood filth.

### 🧞 HSK-VRE-Genie
Standalone bundle of **[Vanilla Races Expanded — Genie](https://steamcommunity.com/sharedfiles/filedetails/?id=2901424072)** by Oskar Potocki and Sarg Bjornson. Genie xenohumans with hemophilia, weak immunity, blue blood, elongated heads, and a strong learning aptitude.

HSK integration (adapted from Pacas Patches Compilation):
- Removes the standalone `VRE_Antibiotics` drug item, ChemicalDef, NeedDef, Tolerance, and Addiction hediffs — duplicate of HSK's `MedicalDrink` role
- Removes `AddictionResistant_VRE_Antibiotics` gene from the Genie xenotype (drug no longer exists)
- Patches HSK's `MedicalDrink` to grant `VRE_AntibioticsHigh` via the `IngestionOutcomeDoer_GiveHediff_Discriminating` — Genies still get the immunity boost, just from drinking medicinal alcohol they'd consume anyway in HSK
- Keeps the four high-tier antibiotics hediffs (the discriminating doer needs them as targets)

Genie has no CE-relevant content (no recipes, buildings, weapons, apparel, tools, or armor offsets) — all 16+ Genie genes are pure stat factors.

### 🪖 HSK-VRE-Hussar
Standalone bundle of **[Vanilla Races Expanded — Hussar](https://steamcommunity.com/sharedfiles/filedetails/?id=2893586390)** by Oskar Potocki, xrushha, Taranchuk, Sarg Bjornson. Hussar super-soldier xenohumans with bulletproof skin, weapon-aptitude genes, and luciferium dependency.

CE patches (adapted from Pacas Patches Compilation, re-tuned for HSK CE scale):
- `VREH_BulletproofSkin` gene — kevlar-like natural armor at HSK CE scale (Sharp 8 / Blunt 8, between flak and marine armor; Pacas's vanilla CE 5.0/5.0 was about half-flak under HSK CE math)
- `VREH_BlackListedWeapons` — extended with 56 entries (Pacas's vanilla list + HSK modpack mech weapons) so the WeaponAptitude gene system doesn't roll silly aptitudes:
  - Vanilla mech weapons (9) + non-weapon items (6) — from Pacas
  - Wolfein racial mech weapons (4) — `MayRequire`-gated
  - VFE Pirates warcasket-only guns (15) — `MayRequire`-gated
  - VFE Mechanoids mech weapons (22) — `MayRequire`-gated

Hussar is content-light (no recipes, no buildings, no costLists, no apparel of its own — Hussars use vanilla weapons via the WeaponAptitude gene system).

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
