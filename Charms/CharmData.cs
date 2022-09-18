using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace CharmCrab {
    class CharmData {
        /*
        public readonly static Dictionary<int, int> COSTS = new Dictionary<int, int>() {
            { 1, 0 }, // Gathering Swarm
            { 2, 0 }, // Wayward Compass
            { 3, 1 }, // Grubsong
            { 4, 1 }, // Stalwart Shell
            { 5, 1 }, // Baldur Shell
            { 6, 1 }, // Fury of the Fallen
            { 7, 1 }, // Quick Focus
            { 8, 1 }, // Lifeblood Heart
            { 9, 1 }, // Lifeblood Core
            { 10, 1 }, // Defender's Crest
             case Charm.DefenderCrest: return PlayerData.instance.GetBool("equippedCharm_10");
                case Charm.Flukenest: return PlayerData.instance.GetBool("equippedCharm_11");
                case Charm.Thorns: return PlayerData.instance.GetBool("equippedCharm_12");
                case Charm.MarkOfPride: return PlayerData.instance.GetBool("equippedCharm_13");
                case Charm.SteadyBody: return PlayerData.instance.GetBool("equippedCharm_14");
                case Charm.HeavyBlow: return PlayerData.instance.GetBool("equippedCharm_15");
                case Charm.SharpShadow: return PlayerData.instance.GetBool("equippedCharm_15");
            { 11, 1 }, // Flukenest
            { 12, 1 }, // Thorns of Agony
            { 13, 1 }, // Mark of Pride
            { 14, 1 }, // Steady Body
            { 15, 1 }, // Heavy Blow
            { 16, 1 }, // Sharp Shadow
            { 17, 1 }, // Spore Shroom
            { 18, 1 }, // Longnail
            { 19, 1 }, // Shaman Stone
            { 20, 1 }, // Soul Catcher
            { 21, 1 }, // Soul Eater
            { 22, 1 }, // Glowing Womb
            { 23, 1 }, // Fragile Heart
            { 24, 1 }, // Fragile Greed
            { 25, 1 }, // Fragile Strength
            { 26, 1 }, // Nailmaster's Glory
            { 27, 1 }, // Joni's Blessing
            { 28, 1 }, // Shape of Unn
            { 29, 1 }, // Hiveblood
            { 30, 1 }, // Dream Wielder
            { 31, 1 }, // Dashmaster
            { 32, 1 }, // Quick Slash
            { 33, 1 }, // Spell Twister
            { 34, 1 }, // Deep Focus
            { 35, 1 }, // Grubberfly's Elegy
            { 36, 1 }, // Kingsoul
            { 37, 1 }, // Sprintmaster
            { 38, 1 }, // Dreamshield
            { 39, 1 }, // Weaversong
            { 40, 1 }, // Grimmchild
        };
        */

        public static Charm Index(int i) {

            switch (i) {
                case 1: return Charm.GatheringSwarm;
                case 2: return Charm.WaywardCompass;
                case 3: return Charm.GrubSong;
                case 4: return Charm.StalwartShell;
                case 5: return Charm.BaldurShell;
                case 6: return Charm.Fury;
                case 7: return Charm.QuickFocus;
                case 8: return Charm.LifebloodHeart;
                case 9: return Charm.LifebloodCore;
                case 10: return Charm.DefenderCrest;
                case 11: return Charm.Flukenest;
                case 12: return Charm.Thorns;
                case 13: return Charm.MarkOfPride;
                case 14: return Charm.SteadyBody;
                case 15: return Charm.HeavyBlow;
                case 16: return Charm.SharpShadow;
                case 17: return Charm.SporeShroom;
                case 18: return Charm.LongNail;
                case 19: return Charm.ShamanStone;
                case 20: return Charm.SoulCatcher;
                case 21: return Charm.SoulEater;
                case 22: return Charm.GlowingWomb;
                case 23: return Charm.FragileHeart;
                case 24: return Charm.FragileGreed;
                case 25: return Charm.FragileStrength;
                case 26: return Charm.NailMaster;
                case 27: return Charm.JoniBlessing;
                case 28: return Charm.ShapeOfUnn;
                case 29: return Charm.HiveBlood;
                case 30: return Charm.DreamWielder;
                case 31: return Charm.DashMaster;
                case 32: return Charm.QuickSlash;
                case 33: return Charm.SpellTwister;
                case 34: return Charm.DeepFocus;
                case 35: return Charm.GrubElegy;
                case 36: return Charm.KingSoul;
                case 37: return Charm.Sprintmaster;
                case 38: return Charm.Dreamshield;
                case 39: return Charm.Weaversong;
                case 49: return Charm.Grimmchild;
                default: throw new ConstraintException("Charm value must be 1 <= x <= 40");
            }
        }

        public static string Description(Charm c, string orig) {
            switch (c) {
                case Charm.GatheringSwarm: return "Whispers its location to the bearer whenever a map is open, allowing wanderers to pinpoint their current location.";
                case Charm.WaywardCompass: return "A swarm will follow the bearer and gather up any loose Geo.\nUseful for those who can't bear to leave anything behind, no matter how insignificant.";
                case Charm.GrubSong: return "Contains the gratitude of freed grubs.\nGain SOUL when taking damage.";
                case Charm.StalwartShell: return "Builds resilience. When recovering from damage, the bearer will remain invulnerable for longer.\nReduces damage taken by one, but not to zero.";
                case Charm.BaldurShell: return "Protects its bearer with a hard shell while focusing SOUL or casting spells.\nThe shell is not indestructible and will shatter if it absorbs too much damage.";
                case Charm.Fury: return "Embodies the fury and heroism that comes upon those who are about to die.\nDamage increases as you lose health.";
                case Charm.LifebloodHeart: return "When resting, the bearer will gain a coating of lifeblood that protects from a modest amount of damage.\nAlso generates lifeblood after sufficient damage has been dealt.";
                case Charm.Flukenest: return "Living charm born in the gut of a Flukemarm.\nTransforms Vengeful Spirit into a horde of flukes that drain the life for their summoner.";
                case Charm.MarkOfPride: return "Freely given by the Mantis Tribe to those they respect.\nGreatly increases the range of the bearer's nail, and increases damage as one continues to strike.\nDamage bonus lost on taking damage.";
                case Charm.SteadyBody: return "Keeps its bearer from recoiling backwards when they strike an enemy with a nail.\nAlso has a chance to reduce damage taken.";
                case Charm.HeavyBlow: return "Increases the force of the bearer's nail, causing enemies to recoil further when hit.\nAlso inflicts a stacking bleed affliction.";
                case Charm.SporeShroom: return "When focusing SOUL, emit a spore cloud that slowly damages enemies.\nAlso infests enemies, so they explode with soul when struck by your nail.";
                case Charm.ShamanStone: return "Massively increases spell damage, at increased cost of SOUL.";
                case Charm.SoulCatcher: return "Causes your spell damage to linger on enemies, and erupt when struck by your nail.";
                case Charm.SoulEater: return "Afflicts enemies struck by your spells; This affliction slowly drains their vitality and causes you to gain more soul when attacking.";
                case Charm.FragileGreed: return "Increases the Geo received, but your nail strikes cost Geo while being more powerful should you be able to pay the cost.";
                case Charm.UnbreakableGreed: return "Increases the Geo received, but your nail strikes cost Geo while being more powerful should you be able to pay the cost.";
                case Charm.FragileStrength: return "Increase the strength of your nail to a keen edge." + (!PlayerData.instance.GetBool("fragileStrength_unbreakable") ? "However, but you become fragile and take more damage." : "");
                case Charm.UnbreakableStrength: return "Increase the strength of your nail to a keen edge.";
                case Charm.NailMaster: return "Increase the reach of your nail arts, while also increasing their potency.";
                case Charm.SpellTwister: return "Your spells now cost nothing, but their effectiveness is reduced.";
                default: return orig;
            }
        }

        public static int Cost(Charm c) {
            switch (c) {
                case Charm.GatheringSwarm: return 0;
                case Charm.WaywardCompass: return 0;
                case Charm.GrubSong: return 1;
                case Charm.StalwartShell: return 3;
                case Charm.BaldurShell: return 1;
                case Charm.Fury: return 3;
                case Charm.QuickFocus: return 1;
                case Charm.LifebloodHeart: return 3;
                case Charm.LifebloodCore: return 1;
                case Charm.DefenderCrest: return 1;
                case Charm.Flukenest: return 1;
                case Charm.Thorns: return 1;
                case Charm.MarkOfPride: return 2;
                case Charm.SteadyBody: return 2;
                case Charm.HeavyBlow: return 1;
                case Charm.SharpShadow: return 1;
                case Charm.SporeShroom: return 1;
                case Charm.LongNail: return 1;
                case Charm.ShamanStone: return 2;
                case Charm.SoulCatcher: return 1;
                case Charm.SoulEater: return 1;
                case Charm.GlowingWomb: return 1;
                case Charm.FragileHeart: return 1;
                case Charm.BrokenHeart: return 1;
                case Charm.UnbreakableHeart: return 1;
                case Charm.FragileGreed: return 1;
                case Charm.BrokenGreed: return 1;
                case Charm.UnbreakableGreed: return 1;
                case Charm.FragileStrength: return 2;
                case Charm.BrokenStrength: return 1;
                case Charm.UnbreakableStrength: return 1;
                case Charm.NailMaster: return 2;
                case Charm.JoniBlessing: return 1;
                case Charm.ShapeOfUnn: return 0;
                case Charm.HiveBlood: return 1;
                case Charm.DreamWielder: return 1;
                case Charm.DashMaster: return 1;
                case Charm.QuickSlash: return 1;
                case Charm.SpellTwister: return 3;
                case Charm.DeepFocus: return 1;
                case Charm.GrubElegy: return 1;
                case Charm.WhiteFragment: return 1;
                case Charm.KingSoul: return 1;
                case Charm.VoidHeart: return 1;
                case Charm.Sprintmaster: return 0;
                case Charm.Dreamshield: return 1;
                case Charm.Weaversong: return 1;
                case Charm.Grimmchild: return 1;
                case Charm.CarefreeMelody: return 1;
                default: return 5;
            }
        }

        public static bool Equipped(Charm c) {
            switch (c) {
                case Charm.GatheringSwarm: return PlayerData.instance.GetBool("equippedCharm_1");
                case Charm.WaywardCompass: return PlayerData.instance.GetBool("equippedCharm_2");
                case Charm.GrubSong: return PlayerData.instance.GetBool("equippedCharm_3");
                case Charm.StalwartShell: return PlayerData.instance.GetBool("equippedCharm_4");
                case Charm.BaldurShell: return PlayerData.instance.GetBool("equippedCharm_5");
                case Charm.Fury: return PlayerData.instance.GetBool("equippedCharm_6");
                case Charm.QuickFocus: return PlayerData.instance.GetBool("equippedCharm_7");
                case Charm.LifebloodHeart: return PlayerData.instance.GetBool("equippedCharm_8");
                case Charm.LifebloodCore: return PlayerData.instance.GetBool("equippedCharm_9");
                case Charm.DefenderCrest: return PlayerData.instance.GetBool("equippedCharm_10");
                case Charm.Flukenest: return PlayerData.instance.GetBool("equippedCharm_11");
                case Charm.Thorns: return PlayerData.instance.GetBool("equippedCharm_12");
                case Charm.MarkOfPride: return PlayerData.instance.GetBool("equippedCharm_13");
                case Charm.SteadyBody: return PlayerData.instance.GetBool("equippedCharm_14");
                case Charm.HeavyBlow: return PlayerData.instance.GetBool("equippedCharm_15");
                case Charm.SharpShadow: return PlayerData.instance.GetBool("equippedCharm_16");
                case Charm.SporeShroom: return PlayerData.instance.GetBool("equippedCharm_17");
                case Charm.LongNail: return PlayerData.instance.GetBool("equippedCharm_18");
                case Charm.ShamanStone: return PlayerData.instance.GetBool("equippedCharm_19");
                case Charm.SoulCatcher: return PlayerData.instance.GetBool("equippedCharm_20");
                case Charm.SoulEater: return PlayerData.instance.GetBool("equippedCharm_21");
                case Charm.GlowingWomb: return PlayerData.instance.GetBool("equippedCharm_22");
                case Charm.FragileHeart: return PlayerData.instance.GetBool("equippedCharm_23") && !GameManager.instance.playerData.GetBool("brokenCharm_23");
                case Charm.BrokenHeart: return false;
                case Charm.UnbreakableHeart: return PlayerData.instance.GetBool("equippedCharm_23") && PlayerData.instance.GetBool("fragileHealth_unbreakable");
                case Charm.FragileGreed: return PlayerData.instance.GetBool("equippedCharm_24") && !GameManager.instance.playerData.GetBool("brokenCharm_24");
                case Charm.BrokenGreed: return false;
                case Charm.UnbreakableGreed: return PlayerData.instance.GetBool("equippedCharm_24") && PlayerData.instance.GetBool("fragileGreed_unbreakable");
                case Charm.FragileStrength: return PlayerData.instance.GetBool("equippedCharm_25") && !GameManager.instance.playerData.GetBool("brokenCharm_25");
                case Charm.BrokenStrength: return false;
                case Charm.UnbreakableStrength: return PlayerData.instance.GetBool("equippedCharm_25") && PlayerData.instance.GetBool("fragileStrength_unbreakable");
                case Charm.NailMaster: return PlayerData.instance.GetBool("equippedCharm_26");
                case Charm.JoniBlessing: return PlayerData.instance.GetBool("equippedCharm_27");
                case Charm.ShapeOfUnn: return PlayerData.instance.GetBool("equippedCharm_28");
                case Charm.HiveBlood: return PlayerData.instance.GetBool("equippedCharm_29");
                case Charm.DreamWielder: return PlayerData.instance.GetBool("equippedCharm_30");
                case Charm.DashMaster: return PlayerData.instance.GetBool("equippedCharm_31");
                case Charm.QuickSlash: return PlayerData.instance.GetBool("equippedCharm_32");
                case Charm.SpellTwister: return PlayerData.instance.GetBool("equippedCharm_33");
                case Charm.DeepFocus: return PlayerData.instance.GetBool("equippedCharm_034");
                case Charm.GrubElegy: return PlayerData.instance.GetBool("equippedCharm_35");
                case Charm.WhiteFragment: return PlayerData.instance.GetBool("equippedCharm_36_A");
                case Charm.KingSoul: return PlayerData.instance.GetBool("equippedCharm_36_B");
                case Charm.VoidHeart: return PlayerData.instance.GetBool("equippedCharm_36_C");
                case Charm.Sprintmaster: return PlayerData.instance.GetBool("equippedCharm_37");
                case Charm.Dreamshield: return PlayerData.instance.GetBool("equippedCharm_38");
                case Charm.Weaversong: return PlayerData.instance.GetBool("equippedCharm_39");
                case Charm.Grimmchild: return PlayerData.instance.GetBool("equippedCharm_40");
                case Charm.CarefreeMelody: return PlayerData.instance.GetBool("equippedCharm_40_N");
                default: return false;
            }
        }

        public static bool Obtained(Charm c) {
            switch (c) {
                case Charm.GatheringSwarm: return PlayerData.instance.GetBool("gotCharm_1");
                case Charm.WaywardCompass: return PlayerData.instance.GetBool("gotCharm_2");
                case Charm.GrubSong: return PlayerData.instance.GetBool("gotCharm_3");
                case Charm.StalwartShell: return PlayerData.instance.GetBool("gotCharm_4");
                case Charm.BaldurShell: return PlayerData.instance.GetBool("gotCharm_5");
                case Charm.Fury: return PlayerData.instance.GetBool("gotCharm_6");
                case Charm.QuickFocus: return PlayerData.instance.GetBool("gotCharm_7");
                case Charm.LifebloodHeart: return PlayerData.instance.GetBool("gotCharm_8");
                case Charm.LifebloodCore: return PlayerData.instance.GetBool("gotCharm_9");
                case Charm.DefenderCrest: return PlayerData.instance.GetBool("gotCharm_10");
                case Charm.Flukenest: return PlayerData.instance.GetBool("gotCharm_11");
                case Charm.Thorns: return PlayerData.instance.GetBool("gotCharm_12");
                case Charm.MarkOfPride: return PlayerData.instance.GetBool("gotCharm_13");
                case Charm.SteadyBody: return PlayerData.instance.GetBool("gotCharm_14");
                case Charm.HeavyBlow: return PlayerData.instance.GetBool("gotCharm_15");
                case Charm.SharpShadow: return PlayerData.instance.GetBool("gotCharm_16");
                case Charm.SporeShroom: return PlayerData.instance.GetBool("gotCharm_17");
                case Charm.LongNail: return PlayerData.instance.GetBool("gotCharm_18");
                case Charm.ShamanStone: return PlayerData.instance.GetBool("gotCharm_19");
                case Charm.SoulCatcher: return PlayerData.instance.GetBool("gotCharm_20");
                case Charm.SoulEater: return PlayerData.instance.GetBool("gotCharm_21");
                case Charm.GlowingWomb: return PlayerData.instance.GetBool("gotCharm_22");
                case Charm.FragileHeart: return PlayerData.instance.GetBool("gotCharm_23");
                case Charm.BrokenHeart: return PlayerData.instance.GetBool("gotCharm_23_BRK");
                case Charm.UnbreakableHeart: return PlayerData.instance.GetBool("gotCharm_23_G");
                case Charm.FragileGreed: return PlayerData.instance.GetBool("gotCharm_24");
                case Charm.BrokenGreed: return PlayerData.instance.GetBool("gotCharm_24_BRK");
                case Charm.UnbreakableGreed: return PlayerData.instance.GetBool("gotCharm_24_G");
                case Charm.FragileStrength: return PlayerData.instance.GetBool("gotCharm_25");
                case Charm.BrokenStrength: return PlayerData.instance.GetBool("gotCharm_25_BRK");
                case Charm.UnbreakableStrength: return PlayerData.instance.GetBool("gotCharm_25_G");
                case Charm.NailMaster: return PlayerData.instance.GetBool("gotCharm_26");
                case Charm.JoniBlessing: return PlayerData.instance.GetBool("gotCharm_27");
                case Charm.ShapeOfUnn: return PlayerData.instance.GetBool("gotCharm_28");
                case Charm.HiveBlood: return PlayerData.instance.GetBool("gotCharm_29");
                case Charm.DreamWielder: return PlayerData.instance.GetBool("gotCharm_30");
                case Charm.DashMaster: return PlayerData.instance.GetBool("gotCharm_31");
                case Charm.QuickSlash: return PlayerData.instance.GetBool("gotCharm_32");
                case Charm.SpellTwister: return PlayerData.instance.GetBool("gotCharm_33");
                case Charm.DeepFocus: return PlayerData.instance.GetBool("gotCharm_034");
                case Charm.GrubElegy: return PlayerData.instance.GetBool("gotCharm_35");
                case Charm.WhiteFragment: return PlayerData.instance.GetBool("gotCharm_36_A");
                case Charm.KingSoul: return PlayerData.instance.GetBool("gotCharm_36_B");
                case Charm.VoidHeart: return PlayerData.instance.GetBool("gotCharm_36_C");
                case Charm.Sprintmaster: return PlayerData.instance.GetBool("gotCharm_37");
                case Charm.Dreamshield: return PlayerData.instance.GetBool("gotCharm_38");
                case Charm.Weaversong: return PlayerData.instance.GetBool("gotCharm_39");
                case Charm.Grimmchild: return PlayerData.instance.GetBool("gotCharm_40");
                case Charm.CarefreeMelody: return PlayerData.instance.GetBool("gotCharm_40_N");
                default: return false;
            }
        }
    }
}
