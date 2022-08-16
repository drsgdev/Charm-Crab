# HK-Mage
 Charm Rework that Includes new Spells


# Installation
Install [Scarab](https://github.com/fifty-six/Scarab) to install mods for the game. Install Vasi and
SFCore through Scarab. (For now manual installation) In Scarab click on "Open Mods" and create Folder "CharmCrab" and drop the downloaded files into this folder. Open up Hollow Knight and play

# Interactivity with other Mods.
This mod doesn't attempt to play nicely with other overhaul mods, but can work with other mods, such as randomizer, which do not make complex changes to Player FSM and various variable manipulations.

# Overview of changes:

General changes are modified charm mechanics, rebalances to accomodate the heavy changes, and some new 
charms obtained through special ways in the game. This mod designed as a way to help promote playstyles 
that isn't simply just the caster playstyle that dominates the vanilla game. To do this, we promote higher 
damage scaling, and upgrading your nail also affects your spells. So old, somewhat unhelpful or hurtful, mechanics are made to be more valuable when making choices. This is heavily inspired by how RPG's handle orthogonality of playstyles and one should find themselves make a charm build to accomodate melee, spell, hybrid, or summoner styles. 

Some rebalances of note is enemy health, as damage is much higher than before, and thus is much higher. Base soul gain has also been nerfed, and this includes the base soul gain while wearing Soul Catcher/Eater so soul has more complex mechanics to be gathered quickly. The goal of this is accomodate builds that care about soul and builds that don't; Soul while useful for healing will be harder to gather by default but some changed charms accomodate healing so focusing isn't a primary healing methodology but is still useful in cases.

# Charm changes

- Wayward Compass: Now costs 0
- Gathering Swarm: Now costs 0
- Stalwart Shell:
- Soul Catcher: Applies the Soul Catcher Debuff
- Soul Eater: Applies the Soul Eater Debuff
- Shaman Stone: Quadruples spell damage, but requires a full soul orb (3x cost).
- Dash Master: Shadecloak also has no cooldown
- Sprintmaster: Now Costs 0
- Grubsong:                    
- Grubberfly's elegy:          
- Fragile Heart:               
- Fragile Strength:
- Fragile Greed: 
- Spell Twister: Spell costs no soul, but deal half damage (dmg ratio very likely to change).
- Steady Body: Reduce damage taken by 1, to a minimum of 1.
- Heavy Blow: Melee attacks inflict the Bleed debuff
- Quick Slash: 
- Long Nail: 
- Mark of Pride: Increase damage as you attck; Lose it when taking damage
- Fury of the Fallen: Works for all health values, but scales with relation to how much health is missing.
- Thorns of Agony: Not yet changed (have plans, but need thinking due to recent overhaul)
- Baldur Shell: No Longer breaks (Not yet Implemented)
- Fluke Nest: Now heals after enough strikes. (should hit hard, but not interact with soul eater/catcher)
- Defender's Crest: Unchanged (other effects modified by this charm are changed).
- Glowing Womb: Summon costs happens more frequently, costs more Soul, but can have up to 15 minions.
- Quick Focus: 
- Deep Focus: 
- Lifeblood Heart: Every (currently) 15 nailstrikes generates a lifeblood
- Lifeblood Core: 
- Joni's Blessing:
- Hiveblood: 
- Spore Shroom: Spawns on healing and any spell cast, inflicts the infested debuff             
- Sharp Shadow: Changed Damage (not necessarly proportionally better), and generates soul when hitting.
- Shape of Unn: Costs 0
- NMG: Increases Art damage also
- Weaversong: Increased damage and can proc some melee effects (like bleed).
- Dream Wielder:
- Dreamshield: Increase Shield count for nail upgrade lvl (Not yet implemented)
- Grimm Child: Increase Damage, and revamp scaling
- Carefree Melody: Convert to support minion (Not yet implemented)
- Kingsoul: Effect persists through voidheart and is stronger.


# Debuffs

- Bleed: Lasts 2 seconds, dealing damage every second, and can stack. Duration refreshed on hit.
- Soul Eater: Deals minor damage over time, and attacks and some spells will restore soul when the afflicted enemy is hit.
- Soul Catch: Spell Damage from most spells accumulates on the target, and melee damage will receive a boost proportional to the accumulated damage. This bonus decays quickly, but the debuff lasts indefinitely.
- Infested: Infested stacks can be struck by the nail to gain a large amount of soul. Soul can only be gained when a stack is available and a stack is applied every 10 ticks of infestation damage. These stacks decay over time, so one cannot save up a lot of stacks to gain soul as desired.

# New Charms
- Void Tendrils: Converts your shriek into a summon spell for Void Tendrils. Obtained by getting the journey entry for Void Tendrils.
- Aura of Purity: Turns your shriek into a spell that summons a persistent aura that will damage nearby enemies. This spell will consume all soul, regardless of charms, and damage and duration of this aura depends upon the soul consumed. This spell only interacts with soul catcher.
- Afflicted Devourer : Charm that causes debuffs to proc regardless of their stacks. So Bleed will cause damage, Soul Eater will deal damage/provide Soul. Does not interact with Soul Catcher or infested.
- Soul Infused Blade: Causes Nail arts to consume soul (up to a specific amount), and increase damage based off of the soul consumed.