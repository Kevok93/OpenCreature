CREATE TABLE species (
    `id` int,
    `name` varchar(16),
    `ability1` smallint,
    `ability2` smallint NULL,
    `gender_ratio` tinyint,
    `type1` tinyint,
    `type2` tinyint NULL,
    `capture_rate` tinyint,
    `max_exp` int,
    `ev_type` tinyint,
    `ev_val` tinyint,
    `base_atk` smallint,
    `base_def` smallint,
    `base_spatk` smallint,
    `base_spdef` smallint,
    `base_hp` smallint,
    `base_speed` smallint,
    `max_atk` smallint,
    `max_def` smallint,
    `max_spatk` smallint,
    `max_spdef` smallint,
    `max_hp` smallint,
    `max_speed` smallint,
	`alt_form` int NULL,
    `tm_list` blob(16),
	`misc_info` blob(1) NULL,
	`height` decimal(4,2) NULL,
	`weight` decimal(4,2) NULL,
	`egg_steps` smallint NULL,
	`classification` varchar(16) NULL,
	`egg_group1` tinyint NULL,
	`egg_group2` tinyint NULL,
	`dex_entry` text NULL,
	`wild_item` int NULL,
	`wild_item_pct` int NULL,
    constraint `pk_species` primary key (`id`) on conflict rollback,
	constraint `fk_type1` foreign key (`type1`) references types(`id`) deferrable initially deferred,
	constraint `fk_type2` foreign key (`type2`) references types(`id`) deferrable initially deferred,
	constraint `fk_ability1` foreign key (`ability1`) references abilities(`id`) deferrable initially deferred,
	constraint `fk_ability2` foreign key (`ability2`) references abilities(`id`) deferrable initially deferred,
	constraint `fk_alt_form` foreign key (`alt_form`) references species(`id`) deferrable initially deferred,
	constraint `fk_egg_group1` foreign key (`egg_group1`) references egg_groups(`id`) deferrable initially deferred,
	constraint `fk_egg_group2` foreign key (`egg_group2`) references egg_groups(`id`) deferrable initially deferred,
	constraint `fk_wild_item` foreign key (`wild_item`) references items(`id`) deferrable initially deferred
);
CREATE TABLE egg_groups (
	`id` tinyint,
	`name` varchar(16),
	constraint `pk_egg_groups` primary key (`id`) on conflict rollback
);
CREATE TABLE natures (
	`id` tinyint,
	`name` varchar(16),
	`atk` tinyint,
	`def` tinyint,
	`spatk` tinyint,
	`spdef` tinyint,
	`speed` tinyint,
	constraint `pk_natures` primary key (`id`) on conflict rollback
);
CREATE TABLE moves (
	`id` int,
	`name` varchar(16),
	`type` tinyint,
	`power` tinyint,
	`accuracy` tinyint,
	`pp` tinyint,
	`effect_id` smallint NULL,
	`world_effect_id` smallint NULL,
	`affinity` tinyint,
	`misc_info` blob(2) NULL,
	`misc_val` tinyint NULL,
	`description` text,
	constraint `pk_moves` primary key (`id`) on conflict rollback,
	constraint `fk_type` foreign key (`type`) references types(`id`) deferrable initially deferred,
	constraint `fk_effect_id` foreign key (`effect_id`) references effects(`id`) deferrable initially deferred,
	constraint `fk_world_effect_id` foreign key (`world_effect_id`) references effects(`id`) deferrable initially deferred
);
CREATE TABLE type_bonus (
	`atk_id` tinyint,
	`def_id` tinyint,
	`bonus` decimal(2,1),
	constraint `pk_type_bonus` primary key (`atk_id`,`def_id`) on conflict rollback,
	constraint `fk_atk_id` foreign key (`atk_id`) references types(`id`) deferrable initially deferred,
	constraint `fk_def_id` foreign key (`def_id`) references types(`id`) deferrable initially deferred
);
CREATE TABLE level_moves (
	`poke_id` int,
	`move_id` int,
	`level` tinyint,
	constraint `pk_level_moves` primary key (`poke_id`,`move_id`,`level`) on conflict ignore,
	constraint `fk_poke_id` foreign key (`poke_id`) references species(`id`) deferrable initially deferred,
	constraint `fk_move_id` foreign key (`move_id`) references moves(`id`) deferrable initially deferred
);
CREATE TABLE types (
	`id` tinyint,
	`name` varchar(16),
	constraint `pk_types` primary key (`id`) on conflict rollback
);
CREATE TABLE effects (
	`id` tinyint,
	`name` varchar(16),
	constraint `pk_effects` primary key (`id`) on conflict rollback
);
CREATE TABLE abilities (
	`id` smallint,
	`name` varchar(16),
	`effect_id` smallint,
	`world_effect_id` smallint,
	`description` text,
	constraint `pk_abilities` primary key (`id`) on conflict rollback,
	constraint `fk_effect_id` foreign key (`effect_id`) references effects(`id`) deferrable initially deferred,
	constraint `fk_world_effect_id` foreign key (`world_effect_id`) references effects(`id`) deferrable initially deferred
);
CREATE TABLE unique_creature (
	`id` int,
	`species_id` int,
	`nickname` varchar(16),
	`ability` smallint,
	`nature` tinyint,
	`level` tinyint,
	`held_item` int,
	`move1_id` int,
	`move2_id` int,
	`move3_id` int,
	`move4_id` int,
	`hp_max` smallint,
	`atk` smallint,
	`def` smallint,
	`spatk` smallint,
	`spdef` smallint,
	`speed` smallint,
	`misc_info` blob(2),
	constraint `pk_unique_creature` primary key (`id`) on conflict rollback,
	constraint `fk_species_id` foreign key (`species_id`) references species(`id`) deferrable initially deferred,
	constraint `fk_nature` foreign key (`nature`) references natures(`id`) deferrable initially deferred,
	constraint `fk_ability` foreign key (`ability`) references abilities(`id`) deferrable initially deferred,
	constraint `fk_move1_id` foreign key (`move1_id`) references moves(`id`) deferrable initially deferred,
	constraint `fk_move2_id` foreign key (`move2_id`) references moves(`id`) deferrable initially deferred,
	constraint `fk_move3_id` foreign key (`move3_id`) references moves(`id`) deferrable initially deferred,
	constraint `fk_move4_id` foreign key (`move4_id`) references moves(`id`) deferrable initially deferred,
	constraint `fk_held_item` foreign key (`held_item`) references items(`id`) deferrable initially deferred
);
CREATE TABLE items (
	`id` int,
	`name` varchar(16),
	`type` tinyint,
	`price` smallint,
	`battle_effect` smallint NULL,
	`world_effect` smallint NULL,
	`held_effect` smallint NULL,
	`misc_val1` tinyint NULL,
	`misc_val2` tinyint NULL,
	`misc_info` blob(1) NULL,
	constraint `pk_item` primary key (`id`) on conflict rollback,
	constraint `fk_type` foreign key (`type`) references item_type(`id`) deferrable initially deferred,
	constraint `fk_battle_effect` foreign key (`battle_effect`) references effects(`id`) deferrable initially deferred,
	constraint `fk_world_effect` foreign key (`world_effect`) references effects(`id`) deferrable initially deferred,
	constraint `fk_held_effect` foreign key (`held_effect`) references effects(`id`) deferrable initially deferred
);
CREATE TABLE item_type (
	`id` tinyint,
	`name` varchar(16),
	`description` text,
	constraint `pk_item_type` primary key (`id`) on conflict rollback
);
CREATE TABLE trainer (
	`id` int,
	`name` varchar(10),
	`style` tinyint,
	`poke1` int,
	`poke2` int NULL,
	`poke3` int NULL,
	`poke4` int NULL,
	`poke5` int NULL,
	`poke6` int NULL,
	`item1` int NULL,
	`item2` int NULL,
	`item3` int NULL,
	`item4` int NULL,
	`item5` int NULL,
	`item6` int NULL,
	`reward` int,
	`quote` text,
	`rematch_id` int NULL,
	`misc_info` blob(1) NULL,
	constraint `pk_trainer` primary key (`id`) on conflict rollback,
	constraint `fk_style` foreign key (`style`) references trainer_style(`id`) deferrable initially deferred,
	constraint `fk_poke1` foreign key (`poke1`) references unique_creature(`id`) deferrable initially deferred,
	constraint `fk_poke2` foreign key (`poke2`) references unique_creature(`id`) deferrable initially deferred,
	constraint `fk_poke3` foreign key (`poke3`) references unique_creature(`id`) deferrable initially deferred,
	constraint `fk_poke4` foreign key (`poke4`) references unique_creature(`id`) deferrable initially deferred,
	constraint `fk_poke5` foreign key (`poke5`) references unique_creature(`id`) deferrable initially deferred,
	constraint `fk_poke6` foreign key (`poke6`) references unique_creature(`id`) deferrable initially deferred,
	constraint `fk_item1` foreign key (`item1`) references items(`id`) deferrable initially deferred,
	constraint `fk_item2` foreign key (`item2`) references items(`id`) deferrable initially deferred,
	constraint `fk_item3` foreign key (`item3`) references items(`id`) deferrable initially deferred,
	constraint `fk_item4` foreign key (`item4`) references items(`id`) deferrable initially deferred,
	constraint `fk_item5` foreign key (`item5`) references items(`id`) deferrable initially deferred,
	constraint `fk_item6` foreign key (`item6`) references items(`id`) deferrable initially deferred,
	constraint `rematch_id` foreign key (`rematch_id`) references trainer(`id`) deferrable initially deferred
);
CREATE TABLE trainer_style (
	`id` tinyint,
	`name` varchar(10),
	constraint `pk_trainer_style` primary key (`id`) on conflict rollback
);	
INSERT INTO "natures" VALUES(0,'Adamant',10,0,-10,0,0);
INSERT INTO "natures" VALUES(1,'Bashful',0,0,0,0,0);
INSERT INTO "natures" VALUES(2,'Bold',-10,10,0,0,0);
INSERT INTO "natures" VALUES(3,'Brave',10,0,0,0,-10);
INSERT INTO "natures" VALUES(4,'Calm',-10,0,0,10,0);
INSERT INTO "natures" VALUES(5,'Careful',0,0,-10,10,0);
INSERT INTO "natures" VALUES(6,'Docile',0,0,0,0,0);
INSERT INTO "natures" VALUES(7,'Gentle',0,-10,0,10,0);
INSERT INTO "natures" VALUES(8,'Hardy',0,0,0,0,0);
INSERT INTO "natures" VALUES(9,'Hasty',0,-10,0,0,10);
INSERT INTO "natures" VALUES(10,'Impish',0,10,-10,0,0);
INSERT INTO "natures" VALUES(11,'Jolly',0,0,-10,0,10);
INSERT INTO "natures" VALUES(12,'Lax',0,10,0,-10,0);
INSERT INTO "natures" VALUES(13,'Lonely',10,-10,0,0,0);
INSERT INTO "natures" VALUES(14,'Mild',0,-10,10,0,0);
INSERT INTO "natures" VALUES(15,'Modest',-10,0,10,0,0);
INSERT INTO "natures" VALUES(16,'Naive',0,0,0,-10,10);
INSERT INTO "natures" VALUES(17,'Naughty',10,0,0,-10,0);
INSERT INTO "natures" VALUES(18,'Quiet',0,0,10,0,-10);
INSERT INTO "natures" VALUES(19,'Quirky',0,0,0,0,0);
INSERT INTO "natures" VALUES(20,'Rash',0,0,10,-10,0);
INSERT INTO "natures" VALUES(21,'Relaxed',0,10,0,0,-10);
INSERT INTO "natures" VALUES(22,'Sassy',0,0,0,10,-10);
INSERT INTO "natures" VALUES(23,'Serious',0,0,0,0,0);
INSERT INTO "natures" VALUES(24,'Timid',-10,0,0,0,10);
INSERT INTO "moves" VALUES(1,'Absorb','Grass',25,20,100,0,0,2,X'0000',0,'Restores the user''s HP by 1/2 of the damage inflicted on the target.');
INSERT INTO "moves" VALUES(2,'Acid','Poison',30,40,100,0,0,2,X'0000',0,'Has a 10% chance to lower the target''s Special Defense by 1 stage.');
INSERT INTO "moves" VALUES(3,'Acid Armor','Poison',20,255,255,0,0,3,X'0000',0,'Raises the user''s Defense by 2 stages.');
INSERT INTO "moves" VALUES(4,'Acid Spray','Poison',20,40,100,0,0,2,X'0000',0,'Lowers the target''s Special Defense by 2 stages.');
INSERT INTO "moves" VALUES(5,'Acrobatics','Flying',15,55,100,0,0,1,X'0000',0,'Doubles in Power if the user is not holding an item. The user can hit any opponent with this move in a Triple Battle.');
INSERT INTO "moves" VALUES(6,'Acupressure','Normal',30,255,255,0,0,3,X'0000',0,'One of the user''s stats, including Accuracy or Evasion, is randomly selected and boosted by 2 stages. A stat which is already at max will not be selected. User can also select its partner in Double or Triple Battles to randomly boost one of its stats.');
INSERT INTO "moves" VALUES(7,'Aerial Ace','Flying',20,60,255,0,0,1,X'0000',0,'Ignores Evasion and Accuracy modifiers and never misses except against Protect, Detect or a target in the middle of Dig, Fly, Dive or Bounce.');
INSERT INTO "moves" VALUES(8,'Aeroblast','Flying',5,100,95,0,0,2,X'0000',0,'Has a high critical hit ratio.');
INSERT INTO "moves" VALUES(9,'After You','Normal',15,255,255,0,0,3,X'0000',0,'The target''s turn comes immediately after the user''s. This move fails if the target has already moved for this turn.');
INSERT INTO "moves" VALUES(10,'Agility','Psychic',30,255,255,0,0,3,X'0000',0,'Raises the user''s Speed by 2 stages.');
INSERT INTO "moves" VALUES(11,'Air Cutter','Flying',25,60,95,0,0,2,X'0000',0,'Has a high critical hit ratio.');
INSERT INTO "moves" VALUES(12,'Air Slash','Flying',20,75,95,0,0,2,X'0000',0,'Has a 30% chance to make the target flinch.');
INSERT INTO "moves" VALUES(13,'Ally Switch','Psychic',15,255,255,0,0,3,X'0000',0,'When used by a Pokemon who isn''t in the center position during a Triple Battle, this move switches the user with the ally on the other side of the center Pokemon.');
INSERT INTO "moves" VALUES(14,'Amnesia','Psychic',20,255,255,0,0,3,X'0000',0,'Raises the user''s Special Defense by 2 stages.');
INSERT INTO "moves" VALUES(15,'Ancient Power','Rock',5,60,100,0,0,2,X'0000',0,'Has a 10% chance to raise all of the user''s stats by 1 stage.');
INSERT INTO "moves" VALUES(16,'Aqua Jet','Water',20,40,100,0,0,1,X'0000',0,'Usually goes first.');
INSERT INTO "moves" VALUES(17,'Aqua Ring','Water',20,255,255,0,0,3,X'0000',0,'The user recovers 1/16 of its max HP per turn until it faints or switches out; this can be Baton Passed to another Pokemon.');
INSERT INTO "moves" VALUES(18,'Aqua Tail','Water',10,90,90,0,0,1,X'0000',0,'Damages the target.');
INSERT INTO "moves" VALUES(19,'Arm Thrust','Fighting',20,15,100,0,0,1,X'0000',0,'Attacks 2-5 times in one turn; if one of these attacks breaks a target''s Substitute, the target will take damage for the rest of the hits. This move has a 3/8 chance to hit twice, a 3/8 chance to hit three times, a 1/8 chance to hit four times and a 1/8 chance to hit five times. If the user of this move has Skill Link, this move will always strike five times.');
INSERT INTO "moves" VALUES(20,'Aromatherapy','Grass',5,255,255,0,0,3,X'0000',0,'Every Pokemon in the user''s party is cured of status conditions.');
INSERT INTO "moves" VALUES(21,'Aromatic Mist','Fairy',20,255,100,0,0,3,X'0000',0,'The user raises the Sp. Def stat of ally Pokémon with a mysterious aroma.');
INSERT INTO "moves" VALUES(22,'Assist','Normal',20,255,255,0,0,3,X'0000',0,'The user performs a random move from any of the Pokemon on its team. Assist cannot generate itself, Chatter, Copycat, Counter, Covet, Destiny Bond, Detect, Endure, Feint, Focus Punch, Follow Me, Helping Hand, Me First, Metronome, Mimic, Mirror Coat, Mirror Move, Protect, Sketch, Sleep Talk, Snatch, Struggle, Switcheroo, Thief or Trick.');
INSERT INTO "moves" VALUES(23,'Assurance','Dark',10,60,100,0,0,1,X'0000',0,'Base power doubles if, since the beginning of the current turn, the target has taken residual damage from any factors such as recoil, Spikes, Stealth Rock or the effects of holding Life Orb.');
INSERT INTO "moves" VALUES(24,'Astonish','Ghost',15,30,100,0,0,1,X'0000',0,'Has a 30% chance to make the target flinch.');
INSERT INTO "moves" VALUES(25,'Attack Order','Bug',15,90,100,0,0,1,X'0000',0,'Has a high critical hit ratio.');
INSERT INTO "moves" VALUES(26,'Attract','Normal',15,255,100,0,0,3,X'0000',0,'Infatuates Pokemon of the opposite gender, even if they have a Substitute, causing a 50% chance for the target''s attack to fail.');
INSERT INTO "moves" VALUES(27,'Aura Sphere','Fighting',20,80,255,0,0,2,X'0000',0,'Ignores Evasion and Accuracy modifiers and never misses except against Protect, Detect or a target in the middle of Dig, Fly, Dive or Bounce.');
INSERT INTO "moves" VALUES(28,'Aurora Beam','Ice',20,65,100,0,0,2,X'0000',0,'Has a 10% chance to lower the target''s Attack by 1 stage.');
INSERT INTO "moves" VALUES(29,'Autotomize','Steel',15,255,255,0,0,3,X'0000',0,'Raises the user''s speed by 2 stages and halves the user''s weight.');
INSERT INTO "moves" VALUES(30,'Avalanche','Ice',10,60,100,0,0,1,X'0000',0,'Almost always goes last, even after another Pokemon''s Focus Punch; this move''s base power doubles if the user is damaged before its turn.');
INSERT INTO "moves" VALUES(31,'Baby-Doll Eyes','Fairy',30,255,100,0,0,3,X'0000',0,'The user stares at the target with its baby-doll eyes, which lowers its Attack stat. This move always goes first.');
INSERT INTO "moves" VALUES(32,'Barrage','Normal',20,15,85,0,0,1,X'0000',0,'Attacks 2-5 times in one turn; if one of these attacks breaks a target''s Substitute, the target will take damage for the rest of the hits. This move has a 3/8 chance to hit twice, a 3/8 chance to hit three times, a 1/8 chance to hit four times and a 1/8 chance to hit five times. If the user of this move has Skill Link, this move will always strike five times.');
INSERT INTO "moves" VALUES(33,'Barrier','Psychic',20,255,255,0,0,3,X'0000',0,'Raises the user''s Defense by 2 stages.');
INSERT INTO "moves" VALUES(34,'Baton Pass','Normal',40,255,255,0,0,3,X'0000',0,'The user returns to its party, bypassing any trapping moves and Pursuit; it passes all stat modifiers (positive or negative, including from Charge and Stockpile), as well as confusion, Focus Energy/Lansat Berry boosts, Ingrain, Aqua Ring, Embargo, Gastro Acid, Power Trick, Magnet Rise, Stockpiles, Perish Song count, Mist, Leech Seed, Ghost Curses, Mind Reader, Lock-On, Block, Mean Look, Spider Web and Substitute to the replacement Pokemon.');
INSERT INTO "moves" VALUES(35,'Beat Up','Dark',10,1,100,0,0,1,X'0000',0,'Each healthy (i.e. not fainted nor inflicted with a status condition) Pokemon in your party contributes a typeless 10 base power hit determined solely by their base Attack and Defense stats.');
INSERT INTO "moves" VALUES(36,'Belch','Poison',10,120,90,0,0,2,X'0000',0,'The user lets out a damaging belch on the target. The user must eat a Berry to use this move.');
INSERT INTO "moves" VALUES(37,'Belly Drum','Normal',10,255,255,0,0,3,X'0000',0,'The user maximizes its Attack but sacrifices 50% of its max HP.');
INSERT INTO "moves" VALUES(38,'Bestow','Normal',15,255,255,0,0,3,X'0000',0,'If target is not holding an item, the user passes its hold item to the target.');
INSERT INTO "moves" VALUES(39,'Bide','Normal',10,255,255,0,0,1,X'0000',0,'Usually goes first for the duration of the move. The user absorbs all damage for two turns and then, during the third turn, the user inflicts twice the absorbed damage on its target. This move ignores the target''s type and even hits Ghost-type Pokemon.');
INSERT INTO "moves" VALUES(40,'Bind','Normal',20,15,85,0,0,1,X'0000',0,'Traps the target for 4-5 turns, causing damage equal to 1/16 of its max HP each turn; this trapped effect can be broken by Rapid Spin. The target can still switch out if it is holding Shed Shell or uses Baton Pass, U-Turn or Volt Switch.');
INSERT INTO "moves" VALUES(41,'Bite','Dark',25,60,100,0,0,1,X'0000',0,'Has a 30% chance to make the target flinch.');
INSERT INTO "moves" VALUES(42,'Blast Burn','Fire',5,150,90,0,0,2,X'0000',0,'The user recharges during its next turn; as a result, until the end of the next turn, the user becomes uncontrollable.');
INSERT INTO "moves" VALUES(43,'Blaze Kick','Fire',10,85,90,0,0,1,X'0000',0,'Has a high critical hit ratio and a 10% chance to burn the target.');
INSERT INTO "moves" VALUES(44,'Blizzard','Ice',5,110,70,0,0,2,X'0000',0,'Has a 10% chance to freeze the target. During Hail, this move will never miss under normal circumstances.');
INSERT INTO "moves" VALUES(45,'Block','Normal',5,255,255,0,0,3,X'0000',0,'As long as the user remains in battle, the target cannot switch out unless it is holding Shed Shell or uses Baton Pass, U-Turn or Volt Switch. The target will still be trapped if the user switches out by using Baton Pass.');
INSERT INTO "moves" VALUES(46,'Blue Flare','Fire',5,130,85,0,0,2,X'0000',0,'Has a 20% chance to burn the target.');
INSERT INTO "moves" VALUES(47,'Body Slam','Normal',15,85,100,0,0,1,X'0000',0,'Has a 30% chance to paralyze the target.');
INSERT INTO "moves" VALUES(48,'Bolt Strike','Electric',5,130,85,0,0,1,X'0000',0,'Has a 20% chance to paralyze the target.');
INSERT INTO "moves" VALUES(49,'Bone Club','Ground',20,65,85,0,0,1,X'0000',0,'Has a 10% chance to make the target flinch.');
INSERT INTO "moves" VALUES(50,'Bone Rush','Ground',10,25,90,0,0,1,X'0000',0,'Attacks 2-5 times in one turn; if one of these attacks breaks a target''s Substitute, the target will take damage for the rest of the hits. This move has a 3/8 chance to hit twice, a 3/8 chance to hit three times, a 1/8 chance to hit four times and a 1/8 chance to hit five times. If the user of this move has Skill Link, this move will always strike five times.');
INSERT INTO "moves" VALUES(51,'Bonemerang','Ground',10,50,90,0,0,1,X'0000',0,'Strikes twice; if the first hit breaks the target''s Substitute, the real Pokemon will take damage from the second hit.');
INSERT INTO "moves" VALUES(52,'Boomburst','Normal',10,140,100,0,0,2,X'0000',0,'The user attacks everything around it with the destructive power of a terrible, explosive sound.');
INSERT INTO "moves" VALUES(53,'Bounce','Flying',5,85,85,0,0,1,X'0000',0,'On the first turn, the user bounces into the air, becoming uncontrollable, and evades most attacks. Gust, Twister, Thunder and Sky Uppercut have normal accuracy against a mid-air Pokemon, with Gust and Twister also gaining doubled power. The user may also be hit in mid-air if it was previously targeted by Lock-On or Mind Reader or if it is attacked by a Pokemon with No Guard. On the second turn, the user descends and has a 30% chance to paralyze the target.');
INSERT INTO "moves" VALUES(54,'Brave Bird','Flying',15,120,100,0,0,1,X'0000',0,'The user receives 1/3 recoil damage.');
INSERT INTO "moves" VALUES(55,'Brick Break','Fighting',15,75,100,0,0,1,X'0000',0,'Reflect and Light Screen are removed from the target''s field even if the attack misses. However, if the target is a Ghost-type, this will not occur.');
INSERT INTO "moves" VALUES(56,'Brine','Water',10,65,100,0,0,2,X'0000',0,'Base power doubles if the target is at least 50% below full health.');
INSERT INTO "moves" VALUES(57,'Bubble','Water',30,40,100,0,0,2,X'0000',0,'Has a 10% chance to lower the target''s Speed by 1 stage.');
INSERT INTO "moves" VALUES(58,'Bubble Beam','Water',20,65,100,0,0,2,X'0000',0,'Has a 10% chance to lower the target''s Speed by 1 stage.');
INSERT INTO "moves" VALUES(59,'Bug Bite','Bug',20,60,100,0,0,1,X'0000',0,'The user eats the target''s held berry and, if applicable, receives its benefits. Jaboca Berry will be removed without damaging the user, but Tanga Berry will still activate and reduce this move''s power. The target can still recover its held berry by using Recycle.');
INSERT INTO "moves" VALUES(60,'Bug Buzz','Bug',10,90,100,0,0,2,X'0000',0,'Has a 10% chance to lower the target''s Special Defense by 1 stage.');
INSERT INTO "moves" VALUES(61,'Bulk Up','Fighting',20,255,255,0,0,3,X'0000',0,'Raises the user''s Attack and Defense by 1 stage each.');
INSERT INTO "moves" VALUES(62,'Bulldoze','Ground',20,60,100,0,0,1,X'0000',0,'Lowers the target''s Speed by 1 stage.');
INSERT INTO "moves" VALUES(63,'Bullet Punch','Steel',30,40,100,0,0,1,X'0000',0,'Usually goes first.');
INSERT INTO "moves" VALUES(64,'Bullet Seed','Grass',30,25,100,0,0,1,X'0000',0,'Attacks 2-5 times in one turn; if one of these attacks breaks a target''s Substitute, the target will take damage for the rest of the hits. This move has a 3/8 chance to hit twice, a 3/8 chance to hit three times, a 1/8 chance to hit four times and a 1/8 chance to hit five times. If the user of this move has Skill Link, this move will always strike five times.');
INSERT INTO "moves" VALUES(65,'Calm Mind','Psychic',20,255,255,0,0,3,X'0000',0,'Raises the user''s Special Attack and Special Defense by 1 stage each.');
INSERT INTO "moves" VALUES(66,'Camouflage','Normal',20,255,255,0,0,3,X'0000',0,'The user''s type changes according to the current terrain. It becomes Grass-type in tall grass and very tall grass (as well as in puddles), Water-type while surfing on any body of water, Rock-type while inside any caves or on any rocky terrain, Ground-type on beach sand, desert sand and dirt roads, Ice-type in snow, and Normal-type everywhere else. The user will always become Normal-type during Wi-Fi battles.');
INSERT INTO "moves" VALUES(67,'Captivate','Normal',20,255,100,0,0,3,X'0000',0,'Lowers the target''s Special Attack by 2 stages if the target is the opposite gender of the user. Fails if either Pokemon is genderless.');
INSERT INTO "moves" VALUES(68,'Celebrate','Normal',40,255,255,0,0,3,X'0000',0,'The Pokémon congratulates you on your special day!');
INSERT INTO "moves" VALUES(69,'Charge','Electric',20,255,255,0,0,3,X'0000',0,'Doubles the power of the user''s Electric attacks on the next turn and increases the user''s Special Defense by 1 stage.');
INSERT INTO "moves" VALUES(70,'Charge Beam','Electric',10,50,90,0,0,2,X'0000',0,'Has a 70% chance to raise the user''s Special Attack by 1 stage.');
INSERT INTO "moves" VALUES(71,'Charm','Normal',20,255,100,0,0,3,X'0000',0,'Lowers the target''s Attack by 2 stages.');
INSERT INTO "moves" VALUES(72,'Chatter','Flying',20,65,100,0,0,2,X'0000',0,'Has a 1%, 11% or 31% chance to confuse the target if its user is Chatot. The confusion rate increases directly with the volume of a sound recorded using the DS''s microphone; Chatot''s default cry has a 1% chance to confuse its target and is replaced by the player''s recordings. This move cannot be randomly generated by moves such as Metronome and it cannot be copied with Sketch. If another Pokemon uses Transform to turn into Chatot, its Chatter cannot cause confusion.');
INSERT INTO "moves" VALUES(73,'Chip Away','Normal',20,70,100,0,0,1,X'0000',0,'This move ignores the target''s positive Defense and Evasion stat modifiers, but does not ignore Reflect.');
INSERT INTO "moves" VALUES(74,'Circle Throw','Fighting',10,60,90,0,0,1,X'0000',0,'This move usually goes last. When it hits, the target is forced to switch out.');
INSERT INTO "moves" VALUES(75,'Clamp','Water',15,35,85,0,0,1,X'0000',0,'Traps the target for 4-5 turns, causing damage equal to 1/16 of its max HP each turn; this trapped effect can be broken by Rapid Spin. The target can still switch out if it is holding Shed Shell or uses Baton Pass, U-Turn or Volt Switch.');
INSERT INTO "moves" VALUES(76,'Clear Smog','Poison',15,50,255,0,0,2,X'0000',0,'Removes the target''s stat modifiers.');
INSERT INTO "moves" VALUES(77,'Close Combat','Fighting',5,120,100,0,0,1,X'0000',0,'Lowers the user''s Defense and Special Defense by 1 stage.');
INSERT INTO "moves" VALUES(78,'Coil','Poison',20,255,255,0,0,3,X'0000',0,'Raises the user''s Attack, Defense and Accuracy by 1 stage each.');
INSERT INTO "moves" VALUES(79,'Comet Punch','Normal',15,18,85,0,0,1,X'0000',0,'Attacks 2-5 times in one turn; if one of these attacks breaks a target''s Substitute, the target will take damage for the rest of the hits. This move has a 3/8 chance to hit twice, a 3/8 chance to hit three times, a 1/8 chance to hit four times and a 1/8 chance to hit five times. If the user of this move has Skill Link, this move will always strike five times.');
INSERT INTO "moves" VALUES(80,'Confide','Normal',20,255,255,0,0,3,X'0000',0,'The user tells the target a secret, and the target loses its ability to concentrate. This lowers the target''s Sp. Atk. stat.');
INSERT INTO "moves" VALUES(81,'Confuse Ray','Ghost',10,255,100,0,0,3,X'0000',0,'Confuses the target.');
INSERT INTO "moves" VALUES(82,'Confusion','Psychic',25,50,100,0,0,2,X'0000',0,'Has a 10% chance to confuse the target.');
INSERT INTO "moves" VALUES(83,'Constrict','Normal',35,10,100,0,0,1,X'0000',0,'Has a 10% chance to lower the target''s Speed by 1 stage.');
INSERT INTO "moves" VALUES(84,'Conversion','Normal',30,255,255,0,0,3,X'0000',0,'The user''s type changes to match the type of one of its four attacks. This move fails if the user cannot change its type under this condition.');
INSERT INTO "moves" VALUES(85,'Conversion 2','Normal',30,255,255,0,0,3,X'0000',0,'The user''s type changes to one that resists the type of the last attack that hit the user. In double battles, this situation holds even when the user is last hit by an attack from its partner.');
INSERT INTO "moves" VALUES(86,'Copycat','Normal',20,255,255,0,0,3,X'0000',0,'The user performs the battle''s last successful move. This move could be from the current opponent, a previous opponent, a teammate or even another move from the user''s own moveset. This move fails if no Pokemon has used a move yet. Copycat cannot copy itself.');
INSERT INTO "moves" VALUES(87,'Cosmic Power','Psychic',20,255,255,0,0,3,X'0000',0,'Raises the user''s Defense and Special Defense by 1 stage each.');
INSERT INTO "moves" VALUES(88,'Cotton Guard','Grass',10,255,255,0,0,3,X'0000',0,'Raises the user''s Defense by 3 stages.');
INSERT INTO "moves" VALUES(89,'Cotton Spore','Grass',40,255,100,0,0,3,X'0000',0,'Lowers the target''s Speed by 2 stages.');
INSERT INTO "moves" VALUES(90,'Counter','Fighting',20,255,100,0,0,1,X'0000',0,'Almost always goes last; if an opponent strikes with a Physical attack before the user''s turn, the user retaliates for twice the damage it had endured. In double battles, this attack targets the last opponent to hit the user with a Physical attack and cannot hit the user''s teammate.');
INSERT INTO "moves" VALUES(91,'Covet','Normal',40,60,100,0,0,1,X'0000',0,'Steals the target''s held item unless the user is already holding an item or the target has Sticky Hold or Multitype. Recycle cannot recover the stolen item.');
INSERT INTO "moves" VALUES(92,'Crabhammer','Water',10,100,90,0,0,1,X'0000',0,'Has a high critical hit ratio.');
INSERT INTO "moves" VALUES(93,'Crafty Shield','Fairy',10,255,255,0,0,3,X'0000',0,'The user protects itself and its allies from status moves with a mysterious power. This does not stop moves that do damage.');
INSERT INTO "moves" VALUES(94,'Cross Chop','Fighting',5,100,80,0,0,1,X'0000',0,'Has a high critical hit ratio.');
INSERT INTO "moves" VALUES(95,'Cross Poison','Poison',20,70,100,0,0,1,X'0000',0,'Has a high critical hit ratio and a 10% chance to poison the target.');
INSERT INTO "moves" VALUES(96,'Crunch','Dark',15,80,100,0,0,1,X'0000',0,'Has a 20% chance to lower the target''s Defense by 1 stage.');
INSERT INTO "moves" VALUES(97,'Crush Claw','Normal',10,75,95,0,0,1,X'0000',0,'Has a 50% chance to lower the target''s Defense by 1 stage.');
INSERT INTO "moves" VALUES(98,'Crush Grip','Normal',5,'Var BP',100,0,0,1,X'0000',0,'Base power decreases as the target''s HP decreases.');
INSERT INTO "moves" VALUES(99,'Curse','Ghost',10,255,255,0,0,3,X'0000',0,'When used by a Ghost-type, the user sacrifices half of its max HP to sap the target by 1/4 of its max HP per turn. When used by anything else, the user''s Speed is decreased by 1 stage and its Attack and Defense are increased by 1 stage.');
INSERT INTO "moves" VALUES(100,'Cut','Normal',30,50,95,0,0,1,X'0000',0,'Damages the target.');
INSERT INTO "moves" VALUES(101,'Dark Pulse','Dark',15,80,100,0,0,2,X'0000',0,'Has a 20% chance to make the target flinch.');
INSERT INTO "moves" VALUES(102,'Dark Void','Dark',10,255,80,0,0,3,X'0000',0,'Puts the target to sleep. In double battles, this move will put both opponents to sleep.');
INSERT INTO "moves" VALUES(103,'Dazzling Gleam','Fairy',10,80,100,0,0,2,X'0000',0,'The user damages opposing Pokémon by emitting a powerful flash.');
INSERT INTO "moves" VALUES(104,'Defend Order','Bug',10,255,255,0,0,3,X'0000',0,'Raises the user''s Defense and Special Defense by 1 stage each.');
INSERT INTO "moves" VALUES(105,'Defense Curl','Normal',40,255,255,0,0,3,X'0000',0,'Raises the user''s Defense by 1 stage; after one use of this move, the user''s starting base power is doubled for every use of Rollout or Ice Ball.');
INSERT INTO "moves" VALUES(106,'Defog','Flying',15,255,255,0,0,3,X'0000',0,'Lowers the target''s Evasion by 1 stage and removes Reflect, Light Screen, Safeguard, Mist, Spikes, Stealth Rock and Toxic Spikes from the target''s side of the field.');
INSERT INTO "moves" VALUES(107,'Destiny Bond','Ghost',5,255,255,0,0,3,X'0000',0,'Causes an opponent to faint if its next attack KOs the user.');
INSERT INTO "moves" VALUES(108,'Detect','Fighting',5,255,255,0,0,3,X'0000',0,'Almost always goes first. The user is protected from all attacks for one turn, but the move''s success rate halves with each consecutive use of Protect, Detect or Endure. If a Pokemon has No Guard, or used Lock-On or Mind Reader against the user during the previous turn, its attack has a [100 - move''s normal accuracy]% chance to hit through Detect; OHKO moves do not benefit from this effect. Blizzard has a 30% to hit through this move during Hail, as does Thunder during Rain Dance.');
INSERT INTO "moves" VALUES(109,'Diamond Storm','Rock',5,100,95,0,0,1,X'0000',0,'The user whips up a storm of diamonds to damage opposing Pokémon. This may also raise the user''s Defense stat.');
INSERT INTO "moves" VALUES(110,'Dig','Ground',10,80,100,0,0,1,X'0000',0,'On the first turn, the user digs underground, becoming uncontrollable, and evades all attacks. Earthquake and Magnitude can hit underground and gain doubled power. The user may also be hit underground if it was previously targeted by Lock-On or Mind Reader or if it is attacked by a Pokemon with No Guard. On the second turn, the user attacks.');
INSERT INTO "moves" VALUES(111,'Disable','Normal',20,255,100,0,0,3,X'0000',0,'The target cannot choose its last move for 4-7 turns. Disable only works on one move at a time and fails if the target has not yet used a move or if its move has run out of PP. The target does nothing if it is about to use a move that becomes disabled.');
INSERT INTO "moves" VALUES(112,'Disarming Voice','Fairy',15,40,255,0,0,2,X'0000',0,'Letting out a charming cry, the user does emotional damage to opposing Pokémon. This attack never misses.');
INSERT INTO "moves" VALUES(113,'Discharge','Electric',15,80,100,0,0,2,X'0000',0,'Has a 30% chance to paralyze the target.');
INSERT INTO "moves" VALUES(114,'Dive','Water',10,80,100,0,0,1,X'0000',0,'On the first turn, the user dives underwater, becoming uncontrollable, and evades all attacks except for Surf and Whirlpool, which have doubled power; the user may also be hit underwater if it was previously targeted by Lock-On or Mind Reader or if it is attacked by a Pokemon with No Guard. On the second turn, the user attacks.');
INSERT INTO "moves" VALUES(115,'Dizzy Punch','Normal',10,70,100,0,0,1,X'0000',0,'Has a 20% chance to confuse the target.');
INSERT INTO "moves" VALUES(116,'Doom Desire','Steel',5,140,100,0,0,2,X'0000',0,'This move, even if the user and/or the target switch out, will strike the active target at the end of the second turn after its use. This move cannot cause a critical hit and its damage is calculated by using the original user''s Special Attack and the original target''s Special Defense. Only one instance of Doom Desire or Future Sight may be active at a time; Doom Desire is now a Steel-type move that gets STAB. Since it occurs at the end of the round, it also hits through Protect, Detect anf Endure.');
INSERT INTO "moves" VALUES(117,'Double Hit','Normal',10,35,90,0,0,1,X'0000',0,'Strikes twice; if the first hit breaks the target''s Substitute, the real Pokemon will take damage from the second hit.');
INSERT INTO "moves" VALUES(118,'Double Kick','Fighting',30,30,100,0,0,1,X'0000',0,'Strikes twice; if the first hit breaks the target''s Substitute, the real Pokemon will take damage from the second hit.');
INSERT INTO "moves" VALUES(119,'Double Slap','Normal',10,15,85,0,0,1,X'0000',0,'Attacks 2-5 times in one turn; if one of these attacks breaks a target''s Substitute, the target will take damage for the rest of the hits. This move has a 3/8 chance to hit twice, a 3/8 chance to hit three times, a 1/8 chance to hit four times and a 1/8 chance to hit five times. If the user of this move has Skill Link, this move will always strike five times.');
INSERT INTO "moves" VALUES(120,'Double Team','Normal',15,255,255,0,0,3,X'0000',0,'Raises the user''s Evasion by 1 stage.');
INSERT INTO "moves" VALUES(121,'Double-Edge','Normal',15,120,100,0,0,1,X'0000',0,'The user receives 1/3 recoil damage.');
INSERT INTO "moves" VALUES(122,'Draco Meteor','Dragon',5,130,90,0,0,2,X'0000',0,'Lowers the user''s Special Attack by 2 stages after use.');
INSERT INTO "moves" VALUES(123,'Dragon Ascent','Flying',5,120,100,0,0,1,X'0000',0,'Unknown effect');
INSERT INTO "moves" VALUES(124,'Dragon Breath','Dragon',20,60,100,0,0,2,X'0000',0,'Has a 30% chance to paralyze the target.');
INSERT INTO "moves" VALUES(125,'Dragon Claw','Dragon',15,80,100,0,0,1,X'0000',0,'Damages the target.');
INSERT INTO "moves" VALUES(126,'Dragon Dance','Dragon',20,255,255,0,0,3,X'0000',0,'Raises the user''s Attack and Speed by 1 stage each.');
INSERT INTO "moves" VALUES(127,'Dragon Pulse','Dragon',10,85,100,0,0,2,X'0000',0,'Damages the target.');
INSERT INTO "moves" VALUES(128,'Dragon Rage','Dragon',10,40,100,0,0,2,X'0000',0,'Always deals 40 points of damage.');
INSERT INTO "moves" VALUES(129,'Dragon Rush','Dragon',10,100,75,0,0,1,X'0000',0,'Has a 20% chance to make the target flinch.');
INSERT INTO "moves" VALUES(130,'Dragon Tail','Dragon',10,60,90,0,0,1,X'0000',0,'This move usually goes last. When it hits, the target is forced to switch out.');
INSERT INTO "moves" VALUES(131,'Drain Punch','Fighting',10,75,100,0,0,1,X'0000',0,'Restores the user''s HP by 1/2 of the damage inflicted on the target.');
INSERT INTO "moves" VALUES(132,'Draining Kiss','Fairy',20,50,100,0,0,2,X'0000',0,'The user steals the target''s energy with a kiss. The user''s HP is restored by over half of the damage taken by the target.');
INSERT INTO "moves" VALUES(133,'Dream Eater','Psychic',15,100,100,0,0,2,X'0000',0,'Restores the user''s HP by 1/2 of the damage inflicted on the target but only works on a sleeping target.');
INSERT INTO "moves" VALUES(134,'Drill Peck','Flying',20,80,100,0,0,1,X'0000',0,'Damages the target.');
INSERT INTO "moves" VALUES(135,'Drill Run','Ground',10,80,95,0,0,1,X'0000',0,'Has a high critical hit ratio.');
INSERT INTO "moves" VALUES(136,'Dual Chop','Dragon',15,40,90,0,0,1,X'0000',0,'Strikes twice; if the first hit breaks the target''s Substitute, the real Pokemon will take damage from the second hit.');
INSERT INTO "moves" VALUES(137,'Dynamic Punch','Fighting',5,100,50,0,0,1,X'0000',0,'Confuses the target.');
INSERT INTO "moves" VALUES(138,'Earth Power','Ground',10,90,100,0,0,2,X'0000',0,'Has a 10% chance to lower the target''s Special Defense by 1 stage.');
INSERT INTO "moves" VALUES(139,'Earthquake','Ground',10,100,100,0,0,1,X'0000',0,'Power doubles when performed against Pokemon using Dig.');
INSERT INTO "moves" VALUES(140,'Echoed Voice','Normal',15,40,100,0,0,2,X'0000',0,'Gains 40 base power each time this move is used on consecutive turns.');
INSERT INTO "moves" VALUES(141,'Eerie Impulse','Electric',15,255,100,0,0,3,X'0000',0,'The user''s body generates an eerie impulse. Exposing the target to it harshly lowers the target''s Sp. Atk stat.');
INSERT INTO "moves" VALUES(142,'Egg Bomb','Normal',10,100,75,0,0,1,X'0000',0,'Damages the target.');
INSERT INTO "moves" VALUES(143,'Electric Terrain','Electric',10,255,255,0,0,3,X'0000',0,'The user electrifies the ground under everyone''s feet for five turns. Pokémon on the ground no longer fall asleep.');
INSERT INTO "moves" VALUES(144,'Electrify','Electric',20,255,255,0,0,3,X'0000',0,'If the target is electrified before it uses a move during that turn, the target''s move becomes Electric type.');
INSERT INTO "moves" VALUES(145,'Electro Ball','Electric',10,1,100,0,0,2,X'0000',0,'Power is determined from the speeds of the user and the target; stat modifiers are taken into account. Max power is achieved when the user is much faster than the target.');
INSERT INTO "moves" VALUES(146,'Electroweb','Electric',15,55,95,0,0,2,X'0000',0,'Lowers the target''s Speed by 1 stage.');
INSERT INTO "moves" VALUES(147,'Embargo','Dark',15,255,100,0,0,3,X'0000',0,'Prevents the target from using its held item for five turns. During in-game battles, trainers also cannot use any items from their bag on a Pokemon under the effects of Embargo.');
INSERT INTO "moves" VALUES(148,'Ember','Fire',25,40,100,0,0,2,X'0000',0,'Has a 10% chance to burn the target.');
INSERT INTO "moves" VALUES(149,'Encore','Normal',5,255,100,0,0,3,X'0000',0,'The target is forced to use its last attack for the next three turns. The effects of this move will end immediately if the target runs out of PP for the repeated attack. In double battles, a Pokemon who has received an Encore will target a random opponent with single-target attacks.');
INSERT INTO "moves" VALUES(150,'Endeavor','Normal',5,255,100,0,0,1,X'0000',0,'Inflicts damage equal to the target''s current HP - the user''s current HP.');
INSERT INTO "moves" VALUES(151,'Endure','Normal',10,255,255,0,0,3,X'0000',0,'Almost always goes first. The user is left with at least 1 HP following any attacks for one turn, but the move''s success rate halves with each consecutive use of Protect, Detect or Endure.');
INSERT INTO "moves" VALUES(152,'Energy Ball','Grass',10,90,100,0,0,2,X'0000',0,'Has a 10% chance to lower the target''s Special Defense by 1 stage.');
INSERT INTO "moves" VALUES(153,'Entrainment','Normal',15,255,100,0,0,3,X'0000',0,'The target''s ability is replaced with the user''s ability, unless the target has Truant, Multitype or Zen Mode.');
INSERT INTO "moves" VALUES(154,'Eruption','Fire',5,255,100,0,0,2,X'0000',0,'Base power decreases as the user''s HP decreases.');
INSERT INTO "moves" VALUES(155,'Explosion','Normal',5,250,100,0,0,1,X'0000',0,'The user faints after performing this move. Unlike previous games, the target''s Defense stat is NOT halved.');
INSERT INTO "moves" VALUES(156,'Extrasensory','Psychic',20,80,100,0,0,2,X'0000',0,'Has a 10% chance to make the target flinch.');
INSERT INTO "moves" VALUES(157,'Extreme Speed','Normal',5,80,100,0,0,1,X'0000',0,'Usually goes first.');
INSERT INTO "moves" VALUES(158,'Facade','Normal',20,70,100,0,0,1,X'0000',0,'Power doubles if the user is inflicted with burn, paralysis or poison.');
INSERT INTO "moves" VALUES(159,'Fairy Lock','Fairy',10,255,255,0,0,3,X'0000',0,'By locking down the battlefield, the user keeps all Pokémon from fleeing during the next turn.');
INSERT INTO "moves" VALUES(160,'Fairy Wind','Fairy',30,40,100,0,0,2,X'0000',0,'The user stirs up a fairy wind and strikes the target with it.');
INSERT INTO "moves" VALUES(161,'Fake Out','Normal',10,40,100,0,0,1,X'0000',0,'If this is the user''s first move after being sent or switched into battle, the target flinches; this move fails otherwise and against targets with Inner Focus.');
INSERT INTO "moves" VALUES(162,'Fake Tears','Dark',20,255,100,0,0,3,X'0000',0,'Lowers the target''s Special Defense by 2 stages.');
INSERT INTO "moves" VALUES(163,'False Swipe','Normal',40,40,100,0,0,1,X'0000',0,'Leaves the target with at least 1 HP.');
INSERT INTO "moves" VALUES(164,'Feather Dance','Flying',15,255,100,0,0,3,X'0000',0,'Lowers the target''s Attack by 2 stages.');
INSERT INTO "moves" VALUES(165,'Feint','Normal',10,30,100,0,0,1,X'0000',0,'Almost always goes first. This move no longer fails if the target has not used Protect or Detect.');
INSERT INTO "moves" VALUES(166,'Feint Attack','Dark',20,60,255,0,0,1,X'0000',0,'Ignores Evasion and Accuracy modifiers and never misses except against Protect, Detect or a target in the middle of Dig, Fly, Dive or Bounce.');
INSERT INTO "moves" VALUES(167,'Fell Stinger','Bug',25,30,100,0,0,1,X'0000',0,'When the user knocks out a target with this move, the user''s Attack stat rises sharply.');
INSERT INTO "moves" VALUES(168,'Fiery Dance','Fire',10,80,100,0,0,2,X'0000',0,'Has a 50% chance to raise the user''s Special Attack by 1 stage.');
INSERT INTO "moves" VALUES(169,'Final Gambit','Fighting',5,255,100,0,0,2,X'0000',0,'The user faints, inflicting damage equal to the amount of lost HP onto the target.');
INSERT INTO "moves" VALUES(170,'Fire Blast','Fire',5,110,85,0,0,2,X'0000',0,'Has a 10% chance to burn the target.');
INSERT INTO "moves" VALUES(171,'Fire Fang','Fire',15,65,95,0,0,1,X'0000',0,'Has a 10% chance to burn the target. Has a 10% chance to make the target flinch. Both effects can occur from a single use.');
INSERT INTO "moves" VALUES(172,'Fire Pledge','Fire',10,80,100,0,0,2,X'0000',0,'When used with Grass Pledge, this move creates a burning field that causes damage after each turn. When used with Water Pledge, this move creates a rainbow that confuses all targets.');
INSERT INTO "moves" VALUES(173,'Fire Punch','Fire',15,75,100,0,0,1,X'0000',0,'Has a 10% chance to burn the target.');
INSERT INTO "moves" VALUES(174,'Fire Spin','Fire',15,35,85,0,0,2,X'0000',0,'Traps the target for 4-5 turns, causing damage equal to 1/16 of its max HP each turn; this trapped effect can be broken by Rapid Spin. The target can still switch out if it is holding Shed Shell or uses Baton Pass, U-Turn or Volt Switch.');
INSERT INTO "moves" VALUES(175,'Fissure','Ground',5,255,30,0,0,1,X'0000',0,'The target faints; doesn''t work on higher-leveled Pokemon.');
INSERT INTO "moves" VALUES(176,'Flail','Normal',15,255,100,0,0,1,X'0000',0,'Base power increases as the user''s HP decreases.');
INSERT INTO "moves" VALUES(177,'Flame Burst','Fire',15,70,100,0,0,2,X'0000',0,'The user''s teammates in Double or Triple Battles are hit with typeless damage equal to 1/16 of their maximum HP.');
INSERT INTO "moves" VALUES(178,'Flame Charge','Fire',20,50,100,0,0,1,X'0000',0,'Raises the user''s Speed by 1 stage.');
INSERT INTO "moves" VALUES(179,'Flame Wheel','Fire',25,60,100,0,0,1,X'0000',0,'Has a 10% chance to burn the target; can be used while frozen, which both attacks the target normally and thaws the user.');
INSERT INTO "moves" VALUES(180,'Flamethrower','Fire',15,90,100,0,0,2,X'0000',0,'Has a 10% chance to burn the target.');
INSERT INTO "moves" VALUES(181,'Flare Blitz','Fire',15,120,100,0,0,1,X'0000',0,'The user receives 1/3 recoil damage; has a 10% chance to burn the target.');
INSERT INTO "moves" VALUES(182,'Flash','Normal',20,255,100,0,0,3,X'0000',0,'Lowers the target''s Accuracy by 1 stage.');
INSERT INTO "moves" VALUES(183,'Flash Cannon','Steel',10,80,100,0,0,2,X'0000',0,'Has a 10% chance to lower the target''s Special Defense by 1 stage.');
INSERT INTO "moves" VALUES(184,'Flatter','Dark',15,255,100,0,0,3,X'0000',0,'Confuses the target and raises its Special Attack by 1 stage.');
INSERT INTO "moves" VALUES(185,'Fling','Dark',10,255,100,0,0,1,X'0000',0,'The user''s held item is thrown at the target. Base power and additional effects vary depending on the thrown item. Note that the target will instantly benefit from the effects of thrown berries. The held item is gone for the rest of the battle unless Recycle is used; the item will return to the original holder after wireless battles but will be permanently lost if it is thrown during in-game battles.');
INSERT INTO "moves" VALUES(186,'Flower Shield','Fairy',25,255,255,0,0,3,X'0000',0,'The user raises the Defense stat of all Grass-type Pokémon in battle with a mysterious power.');
INSERT INTO "moves" VALUES(187,'Fly','Flying',15,90,95,0,0,1,X'0000',0,'On the first turn, the user flies into the air, becoming uncontrollable, and evades most attacks. Gust, Twister, Thunder and Sky Uppercut have normal accuracy against a mid-air Pokemon, with Gust and Twister also gaining doubled power. The user may also be hit in mid-air if it was previously targeted by Lock-On or Mind Reader or if it is attacked by a Pokemon with No Guard. On the second turn, the user attacks.');
INSERT INTO "moves" VALUES(188,'Flying Press','Fighting',10,80,95,0,0,1,X'0000',0,'The user dives down onto the target from the sky. This move is Fighting and Flying type simultaneously.');
INSERT INTO "moves" VALUES(189,'Focus Blast','Fighting',5,120,70,0,0,2,X'0000',0,'Has a 10% chance to lower the target''s Special Defense by 1 stage.');
INSERT INTO "moves" VALUES(190,'Focus Energy','Normal',30,255,255,0,0,3,X'0000',0,'Raises the user''s chance for a critical hit by two domains.');
INSERT INTO "moves" VALUES(191,'Focus Punch','Fighting',20,150,100,0,0,1,X'0000',0,'At the beginning of the round, the user tightens its focus; the attack itself usually goes last and will fail if the user is attacked by any other Pokemon during the turn.');
INSERT INTO "moves" VALUES(192,'Follow Me','Normal',20,255,255,0,0,3,X'0000',0,'Almost always goes first. For the rest of the turn, all attacks from the opponent''s team that target the user''s team are redirected toward the user. In double battles, the user''s teammate will not be protected from attacks that have more than one intended target.');
INSERT INTO "moves" VALUES(193,'Force Palm','Fighting',10,60,100,0,0,1,X'0000',0,'Has a 30% chance to paralyze the target.');
INSERT INTO "moves" VALUES(194,'Foresight','Normal',40,255,255,0,0,3,X'0000',0,'Until the target faints or switches, the user''s Accuracy modifiers and the target''s Evasion modifiers are ignored. Ghost-type targets also lose their immunities against Normal-type and Fighting-type moves.');
INSERT INTO "moves" VALUES(195,'Forest''s Curse','Grass',20,255,100,0,0,3,X'0000',0,'The user puts a forest curse on the target. Afflicted targets are now Grass type as well.');
INSERT INTO "moves" VALUES(196,'Foul Play','Dark',15,95,100,0,0,1,X'0000',0,'This move uses the target''s Attack stat, including stat modifiers, in the damage calculation, rather than the user''s.');
INSERT INTO "moves" VALUES(197,'Freeze Shock','Ice',5,140,90,0,0,1,X'0000',0,'The user prepares on turn one, becoming uncontrollable, and then attacks on turn two. This move has a 30% chance to paralyze the target.');
INSERT INTO "moves" VALUES(198,'Freeze-Dry','Ice',15,70,100,0,0,2,X'0000',0,'The user rapidly cools the target. This may also leave the target frozen. This move is super effective on Water types.');
INSERT INTO "moves" VALUES(199,'Frenzy Plant','Grass',5,150,90,0,0,2,X'0000',0,'The user recharges during its next turn; as a result, until the end of the next turn, the user becomes uncontrollable.');
INSERT INTO "moves" VALUES(200,'Frost Breath','Ice',10,60,90,0,0,2,X'0000',0,'This move always lands as a critical hit, unless the target is under the effect of Lucky Chant or has an ability that prevents Critical Hits.');
INSERT INTO "moves" VALUES(201,'Frustration','Normal',20,255,100,0,0,1,X'0000',0,'Power increases as user''s happiness decreases; maximum 102 BP.');
INSERT INTO "moves" VALUES(202,'Fury Attack','Normal',20,15,85,0,0,1,X'0000',0,'Attacks 2-5 times in one turn; if one of these attacks breaks a target''s Substitute, the target will take damage for the rest of the hits. This move has a 3/8 chance to hit twice, a 3/8 chance to hit three times, a 1/8 chance to hit four times and a 1/8 chance to hit five times. If the user of this move has Skill Link, this move will always strike five times.');
INSERT INTO "moves" VALUES(203,'Fury Cutter','Bug',20,40,95,0,0,1,X'0000',0,'The base power of this move doubles with each consecutive hit; however, power is capped at a maximum 160 BP and remains there for any subsequent uses. If this move misses, base power will be reset to 10 BP on the next turn. The user can also select other attacks without resetting this move''s power; it will continue to double after each use until it either misses or reaches the 160 BP cap.');
INSERT INTO "moves" VALUES(204,'Fury Swipes','Normal',15,18,80,0,0,1,X'0000',0,'Attacks 2-5 times in one turn; if one of these attacks breaks a target''s Substitute, the target will take damage for the rest of the hits. This move has a 3/8 chance to hit twice, a 3/8 chance to hit three times, a 1/8 chance to hit four times and a 1/8 chance to hit five times. If the user of this move has Skill Link, this move will always strike five times.');
INSERT INTO "moves" VALUES(205,'Fusion Bolt','Electric',5,100,100,0,0,1,X'0000',0,'Base Power increases if this move is used after Fusion Flare.');
INSERT INTO "moves" VALUES(206,'Fusion Flare','Fire',5,100,100,0,0,2,X'0000',0,'Base Power increases if this move is used after Fusion Bolt.');
INSERT INTO "moves" VALUES(207,'Future Sight','Psychic',10,120,100,0,0,2,X'0000',0,'This move, even if the user and/or the target switch out, will strike the active target at the end of the second turn after its use. This move cannot cause a critical hit and its damage is calculated by using the original user''s Special Attack and the original target''s Special Defense. Only one instance of Future Sight or Doom Desire may be active at a time; Future Sight is now a Psychic-type move that gets STAB. Since it occurs at the end of the round, it also hits through Protect, Detect and Endure.');
INSERT INTO "moves" VALUES(208,'Gastro Acid','Poison',10,255,100,0,0,3,X'0000',0,'Negates the target''s ability as long as it remains in battle.');
INSERT INTO "moves" VALUES(209,'Gear Grind','Steel',15,50,85,0,0,1,X'0000',0,'Strikes twice; if the first hit breaks the target''s Substitute, the real Pokemon will take damage from the second hit.');
INSERT INTO "moves" VALUES(210,'Geomancy','Fairy',10,255,255,0,0,3,X'0000',0,'The user absorbs energy and sharply raises its Sp. Atk, Sp. Def, and Speed stats on the next turn');
INSERT INTO "moves" VALUES(211,'Giga Drain','Grass',10,75,100,0,0,2,X'0000',0,'Restores the user''s HP by 1/2 of the damage inflicted on the target.');
INSERT INTO "moves" VALUES(212,'Giga Impact','Normal',5,150,90,0,0,1,X'0000',0,'The user recharges during its next turn; as a result, until the end of the next turn, the user becomes uncontrollable.');
INSERT INTO "moves" VALUES(213,'Glaciate','Ice',10,65,95,0,0,2,X'0000',0,'Lowers the target''s Speed by 1 stage.');
INSERT INTO "moves" VALUES(214,'Glare','Normal',30,255,100,0,0,3,X'0000',0,'Paralyzes the target.');
INSERT INTO "moves" VALUES(215,'Grass Knot','Grass',20,255,100,0,0,2,X'0000',0,'Base power increases as the target''s weight increases.');
INSERT INTO "moves" VALUES(216,'Grass Pledge','Grass',10,80,100,0,0,2,X'0000',0,'When used with Fire Pledge, this move creates a burning field that causes damage after each turn. When used with Water Pledge, this move creates a swamp that lowers the Speed of all targets.');
INSERT INTO "moves" VALUES(217,'Grass Whistle','Grass',15,255,55,0,0,3,X'0000',0,'Puts the target to sleep.');
INSERT INTO "moves" VALUES(218,'Grassy Terrain','Grass',10,255,255,0,0,3,X'0000',0,'The user turns the ground under everyone''s feet to grass for five turns. This restores the HP of Pokémon on the ground a little every turn.');
INSERT INTO "moves" VALUES(219,'Gravity','Psychic',5,255,255,0,0,3,X'0000',0,'The immunities provided by Magnet Rise, Levitate and the Flying-type are negated for all active Pokemon for five turns; these Pokemon will be affected by Ground-type moves, Arena Trap, Spikes and Toxic Spikes. Pokemon in the middle of using Bounce or Fly when Gravity is activated will immediately return to the ground, and Bounce, Fly, Hi Jump Kick, Jump Kick and Splash cannot be used until Gravity wears off.');
INSERT INTO "moves" VALUES(220,'Growl','Normal',40,255,100,0,0,3,X'0000',0,'Lowers the target''s Attack by 1 stage.');
INSERT INTO "moves" VALUES(221,'Growth','Normal',40,255,255,0,0,3,X'0000',0,'Raises the user''s Attack and Special Attack by 1 stage each. During the effects of Sunny Day, raises the user''s Attack and Special Attack by 2 stages each.');
INSERT INTO "moves" VALUES(222,'Grudge','Ghost',5,255,255,0,0,3,X'0000',0,'The target''s next move is set to 0 PP if it directly KOs the user.');
INSERT INTO "moves" VALUES(223,'Guard Split','Psychic',10,255,255,0,0,3,X'0000',0,'The user''s Defense and Special Defense as well as the target''s Defense and Special Defense are averaged, with the result becoming the new value for all four stats. Stat modifiers are ignored in this calculation.');
INSERT INTO "moves" VALUES(224,'Guard Swap','Psychic',10,255,255,0,0,3,X'0000',0,'The user swaps Defense and Special Defense modifiers with its target.');
INSERT INTO "moves" VALUES(225,'Guillotine','Normal',5,255,30,0,0,1,X'0000',0,'The target faints; doesn''t work on higher-leveled Pokemon.');
INSERT INTO "moves" VALUES(226,'Gunk Shot','Poison',5,120,80,0,0,1,X'0000',0,'Has a 30% chance to poison the target.');
INSERT INTO "moves" VALUES(227,'Gust','Flying',35,40,100,0,0,2,X'0000',0,'Power doubles if the target is in mid-air via Fly or Bounce.');
INSERT INTO "moves" VALUES(228,'Gyro Ball','Steel',5,255,100,0,0,1,X'0000',0,'Power is determined from the speeds of the user and the target; stat modifiers are taken into account. Max power, 150 BP, is achieved when the user is much slower than the target.');
INSERT INTO "moves" VALUES(229,'Hail','Ice',10,255,255,0,0,3,X'0000',0,'Cancels all other weather moves. For 5 turns: Blizzard never misses and has a 30% chance to hit through Protect and Detect, each active Pokemon, even when protected by a Substitute, loses 1/16 of its max HP unless it is an Ice-type, the power of Solarbeam is halved, and the healing power of Morning Sun, Synthesis and Moonlight is halved. The effects of Hail will last for eight turns if its user is holding Icy Rock.');
INSERT INTO "moves" VALUES(230,'Hammer Arm','Fighting',10,100,90,0,0,1,X'0000',0,'Lowers the user''s Speed by 1 stage.');
INSERT INTO "moves" VALUES(231,'Happy Hour','Normal',30,255,255,0,0,3,X'0000',0,'Using Happy Hour doubles the amount of prize money received after battle.');
INSERT INTO "moves" VALUES(232,'Harden','Normal',30,255,255,0,0,3,X'0000',0,'Raises the user''s Defense by 1 stage.');
INSERT INTO "moves" VALUES(233,'Haze','Ice',30,255,255,0,0,3,X'0000',0,'Eliminates any stat modifiers from all active Pokemon. The stat boosts from Choice Band, Choice Specs and Choice Scarf are not affected.');
INSERT INTO "moves" VALUES(234,'Head Charge','Normal',15,120,100,0,0,1,X'0000',0,'The user receives 1/4 recoil damage.');
INSERT INTO "moves" VALUES(235,'Head Smash','Rock',5,150,80,0,0,1,X'0000',0,'The user receives 1/2 recoil damage.');
INSERT INTO "moves" VALUES(236,'Headbutt','Normal',15,70,100,0,0,1,X'0000',0,'Has a 30% chance to make the target flinch.');
INSERT INTO "moves" VALUES(237,'Heal Bell','Normal',5,255,255,0,0,3,X'0000',0,'Every Pokemon in the user''s party is cured of status conditions. Allied Pokemon who have Soundproof are not affected.');
INSERT INTO "moves" VALUES(238,'Heal Block','Psychic',15,255,100,0,0,3,X'0000',0,'For five turns, or until switching out, the target(s) will not be healed by Absorb, Aqua Ring, Drain Punch, Dream Eater, Giga Drain, Heal Order, Ingrain, Leech Life, Leech Seed, Mega Drain, Milk Drink, Moonlight, Morning Sun, Recover, Rest, Roost, Slack Off, Softboiled, Swallow, Synthesis or Wish, but any additional effects from these moves, such as damaging another target, will still occur. Healing caused from held items or Pain Split will not be prevented.');
INSERT INTO "moves" VALUES(239,'Heal Order','Bug',10,255,255,0,0,3,X'0000',0,'Restores 1/2 of the user''s max HP.');
INSERT INTO "moves" VALUES(240,'Heal Pulse','Psychic',10,255,255,0,0,3,X'0000',0,'Restores 1/2 of the target''s max HP to the target; also works on the target''s allies in Triple Battles.');
INSERT INTO "moves" VALUES(241,'Healing Wish','Psychic',10,255,255,0,0,3,X'0000',0,'The user sacrifices itself so that its replacement will be cured of status conditions and have its HP fully restored upon entering the field. This move fails if the user is the only non-fainted Pokemon on its team.');
INSERT INTO "moves" VALUES(242,'Heart Stamp','Psychic',25,60,100,0,0,1,X'0000',0,'Has a 30% chance to make the target flinch.');
INSERT INTO "moves" VALUES(243,'Heart Swap','Psychic',10,255,255,0,0,3,X'0000',0,'The user swaps stat modifiers with the target.');
INSERT INTO "moves" VALUES(244,'Heat Crash','Fire',10,1,100,0,0,1,X'0000',0,'Base Power increases as the user''s weight increases in comparison to the target''s weight.');
INSERT INTO "moves" VALUES(245,'Heat Wave','Fire',10,95,90,0,0,2,X'0000',0,'Has a 10% chance to burn the target.');
INSERT INTO "moves" VALUES(246,'Heavy Slam','Steel',10,1,100,0,0,1,X'0000',0,'Base Power increases as the user''s weight increases in comparison to the target''s weight.');
INSERT INTO "moves" VALUES(247,'Helping Hand','Normal',20,255,255,0,0,3,X'0000',0,'Always goes first. In double battles, the power of the user''s partner''s attacks is increased by 1.5x for that turn; does nothing in single battles.');
INSERT INTO "moves" VALUES(248,'Hex','Ghost',10,65,100,0,0,2,X'0000',0,'Base power doubles if the target has a status condition.');
INSERT INTO "moves" VALUES(249,'Hidden Power','Normal',15,60,100,0,0,2,X'0000',0,'Varies in power and type depending on the user''s IVs; maximum 70 BP. Will always run off the user''s Special Attack regardless of type.');
INSERT INTO "moves" VALUES(250,'High Jump Kick','Fighting',10,130,90,0,0,1,X'0000',0,'If this attack misses the target, the user receives 1/8 recoil damage of what it would have inflicted.');
INSERT INTO "moves" VALUES(251,'Hold Back','Normal',40,40,100,0,0,1,X'0000',0,'The user holds back when it attacks and the target is left with at least 1 HP.');
INSERT INTO "moves" VALUES(252,'Hold Hands','Normal',40,255,255,0,0,3,X'0000',0,'The user and an ally hold hands. This makes them very happy.');
INSERT INTO "moves" VALUES(253,'Hone Claws','Dark',15,255,255,0,0,3,X'0000',0,'Raises the user''s Attack and Accuracy by 1 stage each.');
INSERT INTO "moves" VALUES(254,'Horn Attack','Normal',25,65,100,0,0,1,X'0000',0,'Damages the target.');
INSERT INTO "moves" VALUES(255,'Horn Drill','Normal',5,255,30,0,0,1,X'0000',0,'The target faints; doesn''t work on higher-leveled Pokemon.');
INSERT INTO "moves" VALUES(256,'Horn Leech','Grass',10,75,100,0,0,1,X'0000',0,'Restores the user''s HP by 1/2 of the damage inflicted on the target.');
INSERT INTO "moves" VALUES(257,'Howl','Normal',40,255,255,0,0,3,X'0000',0,'Raises the user''s Attack by 1 stage.');
INSERT INTO "moves" VALUES(258,'Hurricane','Flying',10,110,70,0,0,2,X'0000',0,'This move has a 30% chance to confuse the target. During Rain Dance, it becomes 100% accurate; but during Sunny Day, it becomes 50% accurate. This move can hit targets in the middle of using Fly, Bounce or Sky Drop.');
INSERT INTO "moves" VALUES(259,'Hydro Cannon','Water',5,150,90,0,0,2,X'0000',0,'The user recharges during its next turn; as a result, until the end of the next turn, the user becomes uncontrollable.');
INSERT INTO "moves" VALUES(260,'Hydro Pump','Water',5,110,80,0,0,2,X'0000',0,'Damages the target.');
INSERT INTO "moves" VALUES(261,'Hyper Beam','Normal',5,150,90,0,0,2,X'0000',0,'The user recharges during its next turn; as a result, until the end of the next turn, the user becomes uncontrollable.');
INSERT INTO "moves" VALUES(262,'Hyper Fang','Normal',15,80,90,0,0,1,X'0000',0,'Has a 10% chance to make the target flinch.');
INSERT INTO "moves" VALUES(263,'Hyper Voice','Normal',10,90,100,0,0,2,X'0000',0,'Damages the target.');
INSERT INTO "moves" VALUES(264,'Hyperspace Fury','Dark',5,100,100,0,0,2,X'0000',0,'Unknown effect');
INSERT INTO "moves" VALUES(265,'Hyperspace Hole','Psychic',5,80,255,0,0,2,X'0000',0,'Using a hyperspace hole, the user appears right next to the target and strikes. This also hits a target using Protect or Detect.');
INSERT INTO "moves" VALUES(266,'Hypnosis','Psychic',20,255,60,0,0,3,X'0000',0,'Puts the target to sleep.');
INSERT INTO "moves" VALUES(267,'Ice Ball','Ice',20,30,90,0,0,1,X'0000',0,'The user attacks uncontrollably for five turns; this move''s power doubles after each turn and also if Defense Curl was used beforehand. Its power resets after five turns have ended or if the attack misses.');
INSERT INTO "moves" VALUES(268,'Ice Beam','Ice',10,90,100,0,0,2,X'0000',0,'Has a 10% chance to freeze the target.');
INSERT INTO "moves" VALUES(269,'Ice Burn','Ice',5,140,90,0,0,2,X'0000',0,'The user prepares on turn one, becoming uncontrollable, and then attacks on turn two. This move has a 30% chance to burn the target.');
INSERT INTO "moves" VALUES(270,'Ice Fang','Ice',15,65,95,0,0,1,X'0000',0,'Has a 10% chance to freeze the target. Has 10% chance to make the target flinch. Both effects can occur from a single use.');
INSERT INTO "moves" VALUES(271,'Ice Punch','Ice',15,75,100,0,0,1,X'0000',0,'Has a 10% chance to freeze the target.');
INSERT INTO "moves" VALUES(272,'Ice Shard','Ice',30,40,100,0,0,1,X'0000',0,'Usually goes first.');
INSERT INTO "moves" VALUES(273,'Icicle Crash','Ice',10,85,90,0,0,1,X'0000',0,'Has a 30% chance to make the target flinch.');
INSERT INTO "moves" VALUES(274,'Icicle Spear','Ice',30,25,100,0,0,1,X'0000',0,'Attacks 2-5 times in one turn; if one of these attacks breaks a target''s Substitute, the target will take damage for the rest of the hits. This move has a 3/8 chance to hit twice, a 3/8 chance to hit three times, a 1/8 chance to hit four times and a 1/8 chance to hit five times. If the user of this move has Skill Link, this move will always strike five times.');
INSERT INTO "moves" VALUES(275,'Icy Wind','Ice',15,55,95,0,0,2,X'0000',0,'Lowers the target''s Speed by 1 stage.');
INSERT INTO "moves" VALUES(276,'Imprison','Psychic',10,255,255,0,0,3,X'0000',0,'Until the user faints or switches out, the opponent cannot select any moves that it has in common with the user. In double battles, this move affects both opponents.');
INSERT INTO "moves" VALUES(277,'Incinerate','Fire',15,60,100,0,0,2,X'0000',0,'Destroys the target''s held Berry. Occa Berry is consumed before Incinerate causes damage or destroys it.');
INSERT INTO "moves" VALUES(278,'Inferno','Fire',5,100,50,0,0,2,X'0000',0,'Burns the target.');
INSERT INTO "moves" VALUES(279,'Infestation','Bug',35,20,100,0,0,2,X'0000',0,'The target is infested and attacked for four to five turns. The target can''t flee during this time.');
INSERT INTO "moves" VALUES(280,'Ingrain','Grass',20,255,255,0,0,3,X'0000',0,'The user recovers 1/16 of its max HP after each turn, but it cannot be switched out or forced to switch out. If a Flying-type Pokemon or a Pokemon with Levitate comes under the effect of Ingrain, it will no longer have immunity from Ground-type attacks.');
INSERT INTO "moves" VALUES(281,'Ion Deluge','Electric',25,255,255,0,0,3,X'0000',0,'The user disperses electrically charged particles, which changes Normal-type moves to Electric-type moves.');
INSERT INTO "moves" VALUES(282,'Iron Defense','Steel',15,255,255,0,0,3,X'0000',0,'Raises the user''s Defense by 2 stages.');
INSERT INTO "moves" VALUES(283,'Iron Head','Steel',15,80,100,0,0,1,X'0000',0,'Has a 30% chance to make the target flinch.');
INSERT INTO "moves" VALUES(284,'Iron Tail','Steel',15,100,75,0,0,1,X'0000',0,'Has a 30% chance to lower the target''s Defense by 1 stage.');
INSERT INTO "moves" VALUES(285,'Judgment','Normal',10,100,100,0,0,2,X'0000',0,'This move''s type changes according to the user''s held plate.');
INSERT INTO "moves" VALUES(286,'Jump Kick','Fighting',10,100,95,0,0,1,X'0000',0,'If this attack misses the target, the user receives 1/8 recoil damage of what it would have inflicted.');
INSERT INTO "moves" VALUES(287,'Karate Chop','Fighting',25,50,100,0,0,1,X'0000',0,'Has a high critical hit ratio.');
INSERT INTO "moves" VALUES(288,'Kinesis','Psychic',15,255,80,0,0,3,X'0000',0,'Lowers the target''s Accuracy by 1 stage.');
INSERT INTO "moves" VALUES(289,'King''s Shield','Steel',10,255,255,0,0,3,X'0000',0,'The user takes a defensive stance while it protects itself from damage. It also harshly lowers the Attack stat of any attacker who makes direct contact.');
INSERT INTO "moves" VALUES(290,'Knock Off','Dark',20,65,100,0,0,1,X'0000',0,'Disables the target''s held item unless it has Sticky Hold or Multitype. Items lost to this move cannot be recovered by using Recycle.');
INSERT INTO "moves" VALUES(291,'Land''s Wrath','Ground',10,90,100,0,0,1,X'0000',0,'The user gathers the energy of the land and focuses that power on opposing Pokémon to damage them.');
INSERT INTO "moves" VALUES(292,'Last Resort','Normal',5,140,100,0,0,1,X'0000',0,'Fails until each other move in the user''s moveset has been performed at least once; the user must also know at least one other move.');
INSERT INTO "moves" VALUES(293,'Lava Plume','Fire',15,80,100,0,0,2,X'0000',0,'Has a 30% chance to burn the target.');
INSERT INTO "moves" VALUES(294,'Leaf Blade','Grass',15,90,100,0,0,1,X'0000',0,'Has a high critical hit ratio.');
INSERT INTO "moves" VALUES(295,'Leaf Storm','Grass',5,130,90,0,0,2,X'0000',0,'Lowers the user''s Special Attack by 2 stages after use.');
INSERT INTO "moves" VALUES(296,'Leaf Tornado','Grass',10,65,90,0,0,2,X'0000',0,'Has a 50% chance to lower the target''s Accuracy by 1 stage.');
INSERT INTO "moves" VALUES(297,'Leech Life','Bug',15,20,100,0,0,1,X'0000',0,'Restores the user''s HP by 1/2 of the damage inflicted on the target.');
INSERT INTO "moves" VALUES(298,'Leech Seed','Grass',10,255,90,0,0,3,X'0000',0,'The user steals 1/8 of the target''s max HP until the target is switched out, is KO''ed, or uses Rapid Spin; does not work against Grass-type Pokemon or Pokemon behind Substitutes.');
INSERT INTO "moves" VALUES(299,'Leer','Normal',30,255,100,0,0,3,X'0000',0,'Lowers the target''s Defense by 1 stage.');
INSERT INTO "moves" VALUES(300,'Lick','Ghost',30,30,100,0,0,1,X'0000',0,'Has a 30% chance to paralyze the target.');
INSERT INTO "moves" VALUES(301,'Light of Ruin','Fairy',5,140,90,0,0,2,X'0000',0,'Drawing power from the Eternal Flower, the user fires a powerful beam of light. This also damages the user quite a lot.');
INSERT INTO "moves" VALUES(302,'Light Screen','Psychic',30,255,255,0,0,3,X'0000',0,'All Pokemon in the user''s party receive 1/2 damage from Special attacks for 5 turns. Light Screen will be removed from the user''s field if an opponent''s Pokemon uses Brick Break. It will also last for eight turns if its user is holding Light Clay. In double battles, both Pokemon are shielded, but damage protection is reduced from 1/2 to 1/3.');
INSERT INTO "moves" VALUES(303,'Lock On','Normal',5,255,255,0,0,3,X'0000',0,'This move ensures that the user''s next attack will hit against its current target. This effect can be Baton Passed to another Pokemon. Lock-On fails against Pokemon in the middle of using Protect, Detect, Dig Fly, Bounce or Dive, as well as Pokemon behind a Substitute. If the target uses Protect or Detect during its next turn, the user''s next move has a [100 - move''s normal accuracy]% chance to hit through Protect or Detect. OHKO moves do not benefit from this trait.');
INSERT INTO "moves" VALUES(304,'Lovely Kiss','Normal',10,255,75,0,0,3,X'0000',0,'Puts the target to sleep.');
INSERT INTO "moves" VALUES(305,'Low Kick','Fighting',20,255,100,0,0,1,X'0000',0,'Base power increases as the target''s weight increases.');
INSERT INTO "moves" VALUES(306,'Low Sweep','Fighting',20,65,100,0,0,1,X'0000',0,'Lowers the target''s Speed by 1 stage.');
INSERT INTO "moves" VALUES(307,'Lucky Chant','Normal',30,255,255,0,0,3,X'0000',0,'Critical hits are prevented against every Pokemon on the user''s team, even if the user is switched out, for five turns.');
INSERT INTO "moves" VALUES(308,'Lunar Dance','Psychic',10,255,255,0,0,3,X'0000',0,'The user sacrifices itself so that its replacement will be cured of status conditions and have its HP and PP fully restored upon entering the field. This move fails if the user is the only non-fainted Pokemon on its team.');
INSERT INTO "moves" VALUES(309,'Luster Purge','Psychic',5,70,100,0,0,2,X'0000',0,'Has a 50% chance to lower the target''s Special Defense by 1 stage.');
INSERT INTO "moves" VALUES(310,'Mach Punch','Fighting',30,40,100,0,0,1,X'0000',0,'Usually goes first.');
INSERT INTO "moves" VALUES(311,'Magic Coat','Psychic',15,255,255,0,0,3,X'0000',0,'Almost always goes first. Until the end of the turn, the user will reflect one non-damaging move back at its user (including allies). In Double or Triple Battles, this move will only reflect the first applicable move before wearing off.');
INSERT INTO "moves" VALUES(312,'Magic Room','Psychic',10,255,255,0,0,3,X'0000',0,'Always goes last. Negates all held items on the field for five turns, including through the moves Fling and Natural Gift, but not through Trick, Bug Bite or Pluck; using this move again ends this effect.');
INSERT INTO "moves" VALUES(313,'Magical Leaf','Grass',20,60,255,0,0,2,X'0000',0,'Ignores Evasion and Accuracy modifiers and never misses except against Protect, Detect or a target in the middle of Dig, Fly, Dive or Bounce.');
INSERT INTO "moves" VALUES(314,'Magma Storm','Fire',5,120,75,0,0,2,X'0000',0,'Traps the target for 4-5 turns, causing damage equal to 1/16 of its max HP each turn; this trapped effect can be broken by Rapid Spin. The target can still switch out if it is holding Shed Shell or uses Baton Pass, U-Turn or Volt Switch.');
INSERT INTO "moves" VALUES(315,'Magnet Bomb','Steel',20,60,255,0,0,1,X'0000',0,'Ignores Evasion and Accuracy modifiers and never misses except against Protect, Detect or a target in the middle of Dig, Fly, Dive or Bounce.');
INSERT INTO "moves" VALUES(316,'Magnet Rise','Electric',10,255,255,0,0,3,X'0000',0,'The user receives immunity against Ground-type attacks for five turns.');
INSERT INTO "moves" VALUES(317,'Magnetic Flux','Electric',20,255,255,0,0,3,X'0000',0,'The user manipulates magnetic fields which raises the Defense and Sp. Def stats of ally Pokémon with the Plus or Minus Ability.');
INSERT INTO "moves" VALUES(318,'Magnitude','Ground',30,255,100,0,0,1,X'0000',0,'Deals variable damage, between 10 base power and 130 base power, as well as double damage against Digging Pokemon.');
INSERT INTO "moves" VALUES(319,'Mat Block','Fighting',15,255,255,0,0,3,X'0000',0,'Using a pulled-up mat as a shield, the user protects itself and its allies from damaging moves. This does not stop status moves.');
INSERT INTO "moves" VALUES(320,'Me First','Normal',20,255,255,0,0,3,X'0000',0,'This move fails if it goes last; if the target selects a damaging move for its turn, the user copies the move and performs it with 1.5x power. In a double battle, a move copied by Me First that targets a single Pokemon will hit a random opponent; Me First cannot target the user''s teammate.');
INSERT INTO "moves" VALUES(321,'Mean Look','Normal',5,255,255,0,0,3,X'0000',0,'As long as the user remains in battle, the target cannot switch out unless it is holding Shed Shell or uses Baton Pass, U-Turn or Volt Switch. The target will still be trapped if the user switches out by using Baton Pass.');
INSERT INTO "moves" VALUES(322,'Meditate','Psychic',40,255,255,0,0,3,X'0000',0,'Raises the user''s Attack by 1 stage.');
INSERT INTO "moves" VALUES(323,'Mega Drain','Grass',15,40,100,0,0,2,X'0000',0,'Restores the user''s HP by 1/2 of the damage inflicted on the target.');
INSERT INTO "moves" VALUES(324,'Mega Kick','Normal',5,120,75,0,0,1,X'0000',0,'Damages the target.');
INSERT INTO "moves" VALUES(325,'Mega Punch','Normal',20,80,85,0,0,1,X'0000',0,'Damages the target.');
INSERT INTO "moves" VALUES(326,'Megahorn','Bug',10,120,85,0,0,1,X'0000',0,'Damages the target.');
INSERT INTO "moves" VALUES(327,'Memento','Dark',10,255,100,0,0,3,X'0000',0,'The user sacrifices itself to lower the target''s Attack and Special Attack by 2 stages each.');
INSERT INTO "moves" VALUES(328,'Metal Burst','Steel',10,255,100,0,0,1,X'0000',0,'Fails unless the user goes last; if an opponent strikes with a Physical or a Special attack before the user''s turn, the user retaliates for 1.5x the damage it had endured.');
INSERT INTO "moves" VALUES(329,'Metal Claw','Steel',35,50,95,0,0,1,X'0000',0,'Has a 10% chance to raise the user''s Attack by 1 stage.');
INSERT INTO "moves" VALUES(330,'Metal Sound','Steel',40,255,85,0,0,3,X'0000',0,'Lowers the target''s Special Defense by 2 stages.');
INSERT INTO "moves" VALUES(331,'Meteor Mash','Steel',10,90,90,0,0,1,X'0000',0,'Has a 20% chance to raise the user''s Attack by 1 stage.');
INSERT INTO "moves" VALUES(332,'Metronome','Normal',10,255,255,0,0,3,X'0000',0,'The user performs a randomly selected move; almost any move in the game could be picked. Metronome cannot generate itself, Assist, Chatter, Copycat, Counter, Covet, Destiny Bond, Detect, Endure, Feint, Focus Punch, Follow Me, Helping Hand, Me First, Mimic, Mirror Coat, Mirror Move, Protect, Sketch, Sleep Talk, Snatch, Struggle, Switcheroo, Thief, Trick or any move that the user already knows.');
INSERT INTO "moves" VALUES(333,'Milk Drink','Normal',10,255,255,0,0,3,X'0000',0,'Restores 1/2 of the user''s max HP.');
INSERT INTO "moves" VALUES(334,'Mimic','Normal',10,255,255,0,0,3,X'0000',0,'This move is temporarily replaced by the target''s last move; the replacement move will have 5 PP and become part of the user''s moveset until the user switches out or the battle ends. Mimic copies attacks even if they miss or the user has immunity toward their type; it cannot copy itself, Struggle, Transform, Sketch, Metronome or moves that the user already knows, and it will fail if the target has yet to use a move.');
INSERT INTO "moves" VALUES(335,'Mind Reader','Normal',5,255,255,0,0,3,X'0000',0,'This move ensures that the user''s next attack will hit against its current target. This effect can be Baton Passed to another Pokemon. Mind Reader fails against Pokemon in the middle of using Protect, Detect, Dig Fly, Bounce or Dive, as well as Pokemon behind a Substitute. If the target uses Protect or Detect during its next turn, the user''s next move has a [100 - move''s normal accuracy]% chance to hit through Protect or Detect. OHKO moves do not benefit from this trait.');
INSERT INTO "moves" VALUES(336,'Minimize','Normal',10,255,255,0,0,3,X'0000',0,'Raises the user''s Evasion by 2 stages; however, Stomp and Steamroller retain their normal accuracy and double in power against Minimized opponents.');
INSERT INTO "moves" VALUES(337,'Miracle Eye','Psychic',40,255,255,0,0,3,X'0000',0,'Until the target faints or switches, the user''s Accuracy modifiers and the target''s Evasion modifiers are ignored. Dark-type targets also lose their immunity against Psychic-type moves.');
INSERT INTO "moves" VALUES(338,'Mirror Coat','Psychic',20,255,100,0,0,2,X'0000',0,'Almost always goes last; if an opponent strikes with a Special attack before the user''s turn, the user retaliates for twice the damage it had endured. In double battles, this attack targets the last opponent to hit the user with a Special attack and cannot hit the user''s teammate.');
INSERT INTO "moves" VALUES(339,'Mirror Move','Flying',20,255,255,0,0,3,X'0000',0,'The user performs the last move executed by its target; if applicable, an attack''s damage is calculated with the user''s stats, level and type(s). This moves fails if the target has not yet used a move. Mirror Move cannot copy Encore, Struggle, global moves affecting all Pokemon on the field (such as Gravity, Hail, Rain Dance, Sandstorm and Sunny Day) moves that can bypass Protect (Acupressure, Doom Desire, Future Sight, Imprison, Perish Song, Psych Up, Role Play and Transform) and moves that do not have a specific target (such as Light Screen, Reflect, Safeguard, Spikes, Stealth Rock and Toxic Spikes).');
INSERT INTO "moves" VALUES(340,'Mirror Shot','Steel',10,65,85,0,0,2,X'0000',0,'Has a 30% chance to lower the target''s Accuracy by 1 stage.');
INSERT INTO "moves" VALUES(341,'Mist','Ice',30,255,255,0,0,3,X'0000',0,'Protects every Pokemon on the user''s team from negative stat modifiers caused by other Pokemon (including teammates), but not by itself, for five turns. The team''s Accuracy and Evasion stats are also protected. Moves that cause negative stat modifiers as a secondary effect, such as Psychic, still deal their regular damage.');
INSERT INTO "moves" VALUES(342,'Mist Ball','Psychic',5,70,100,0,0,2,X'0000',0,'Has a 50% chance to lower the target''s Special Attack by 1 stage.');
INSERT INTO "moves" VALUES(343,'Misty Terrain','Fairy',10,255,255,0,0,3,X'0000',0,'The user covers the ground under everyone''s feet with mist for five turns. This protects Pokémon on the ground from status conditions.');
INSERT INTO "moves" VALUES(344,'Moonblast','Fairy',15,95,100,0,0,2,X'0000',0,'Borrowing the power of the moon, the user attacks the target. This may also lower the target''s Sp. Atk stat.');
INSERT INTO "moves" VALUES(345,'Moonlight','Normal',5,255,255,0,0,3,X'0000',0,'Restores a fraction of the user''s max HP depending on the weather: 2/3 during Sunny Day, 1/2 during regular weather and 1/4 during Rain Dance, Hail and Sandstorm.');
INSERT INTO "moves" VALUES(346,'Morning Sun','Normal',5,255,255,0,0,3,X'0000',0,'Restores a fraction of the user''s max HP depending on the weather: 2/3 during Sunny Day, 1/2 during regular weather and 1/4 during Rain Dance, Hail and Sandstorm.');
INSERT INTO "moves" VALUES(347,'Mud Bomb','Ground',10,65,85,0,0,2,X'0000',0,'Has a 30% chance to lower the target''s Accuracy by 1 stage.');
INSERT INTO "moves" VALUES(348,'Mud Shot','Ground',15,55,95,0,0,2,X'0000',0,'Lowers the target''s Speed by 1 stage.');
INSERT INTO "moves" VALUES(349,'Mud Sport','Ground',15,255,255,0,0,3,X'0000',0,'All Electric-type moves are 50% weaker until the user switches out.');
INSERT INTO "moves" VALUES(350,'Mud-Slap','Ground',10,20,100,0,0,2,X'0000',0,'Lowers the target''s Accuracy by 1 stage.');
INSERT INTO "moves" VALUES(351,'Muddy Water','Water',10,90,85,0,0,2,X'0000',0,'Has a 30% chance to lower the target''s Accuracy by 1 stage.');
INSERT INTO "moves" VALUES(352,'Mystical Fire','Fire',10,65,100,0,0,2,X'0000',0,'The user attacks by breathing a special, hot fire. This also lowers the target''s Sp. Atk stat.');
INSERT INTO "moves" VALUES(353,'Nasty Plot','Dark',20,255,255,0,0,3,X'0000',0,'Raises the user''s Special Attack by 2 stages.');
INSERT INTO "moves" VALUES(354,'Natural Gift','Normal',15,255,100,0,0,1,X'0000',0,'The user''s berry is thrown at the target. This attack''s base power and type vary depending on the thrown berry. The berry is gone for the rest of the battle unless Recycle is used; it will return to the original holder after wireless battles but will be permanently lost if it is thrown during in-game battles.');
INSERT INTO "moves" VALUES(355,'Nature Power','Normal',20,255,255,0,0,3,X'0000',0,'The user generates another move depending on the battle''s current terrain. It generates Seed Bomb in any type of grass (as well as in puddles), Hydro Pump while surfing on top of water, Rock Slide on any rocky outdoor terrain and inside of caves, Earthquake on beach sand, desert sand and dirt paths, Blizzard in snow, and Tri Attack everywhere else (including Wi-Fi battles). In Battle Revolution, the move generates Tri Attack at Courtyard, Main Street and Neon, Seed Bomb at Sunny Park and Waterfall, Hydro Pump at Gateway, Rock Slide at Crystal, Magma and Stargazer and Earthquake at Sunset.');
INSERT INTO "moves" VALUES(356,'Needle Arm','Grass',15,60,100,0,0,1,X'0000',0,'Has a 30% chance to make the target flinch.');
INSERT INTO "moves" VALUES(357,'Night Daze','Dark',10,85,95,0,0,2,X'0000',0,'Has a 40% chance to lower the target''s Accuracy by 1 stage.');
INSERT INTO "moves" VALUES(358,'Night Shade','Ghost',15,255,100,0,0,2,X'0000',0,'Does damage equal to user''s level.');
INSERT INTO "moves" VALUES(359,'Night Slash','Dark',15,70,100,0,0,1,X'0000',0,'Has a high critical hit ratio.');
INSERT INTO "moves" VALUES(360,'Nightmare','Ghost',15,255,100,0,0,3,X'0000',0,'This move only works on a sleeping target; as long as the target remains asleep and in battle, 1/4 of its max HP is sapped after each turn.');
INSERT INTO "moves" VALUES(361,'Noble Roar','Normal',30,255,100,0,0,3,X'0000',0,'Letting out a noble roar, the user intimidates the target and lowers its Attack and Sp. Atk stats.');
INSERT INTO "moves" VALUES(362,'Nuzzle','Electric',20,20,100,0,0,1,X'0000',0,'The user attacks by nuzzling its electrified cheeks against the target. This also leaves the target with paralysis.');
INSERT INTO "moves" VALUES(363,'Oblivion Wing','Flying',10,80,100,0,0,2,X'0000',0,'The user absorbs its target''s HP. The user''s HP is restored by over half of the damage taken by the target.');
INSERT INTO "moves" VALUES(364,'Octazooka','Water',10,65,85,0,0,2,X'0000',0,'Has a 50% chance to lower the target''s Accuracy by 1 stage.');
INSERT INTO "moves" VALUES(365,'Odor Sleuth','Normal',40,255,255,0,0,3,X'0000',0,'Until the target faints or switches, the user''s Accuracy modifiers and the target''s Evasion modifiers are ignored. Ghost-type targets also lose their immunities against Normal-type and Fighting-type moves.');
INSERT INTO "moves" VALUES(366,'Ominous Wind','Ghost',5,60,100,0,0,2,X'0000',0,'Has a 10% chance to raise all of the user''s stats by 1 stage.');
INSERT INTO "moves" VALUES(367,'Origin Pulse','Water',10,110,85,0,0,2,X'0000',0,'Unknown effect');
INSERT INTO "moves" VALUES(368,'Outrage','Dragon',10,120,100,0,0,1,X'0000',0,'The user attacks uncontrollably for 2-3 turns and then gets confused.');
INSERT INTO "moves" VALUES(369,'Overheat','Fire',5,130,90,0,0,2,X'0000',0,'Lowers the user''s Special Attack by 2 stages after use.');
INSERT INTO "moves" VALUES(370,'Pain Split','Normal',20,255,255,0,0,3,X'0000',0,'Calculates the average of the user''s current HP and the target''s HP; the HP of both Pokemon is set to this average. Pokemon can be healed by Pain Split even under the effects of Heal Block.');
INSERT INTO "moves" VALUES(371,'Parabolic Charge','Electric',20,50,100,0,0,2,X'0000',0,'The user attacks everything around it. The user''s HP is restored by half the damage taken by those hit.');
INSERT INTO "moves" VALUES(372,'Parting Shot','Dark',20,255,100,0,0,3,X'0000',0,'With a parting threat, the user lowers the target''s Attack and Sp. Atk stats. Then it switches with a party Pokémon.');
INSERT INTO "moves" VALUES(373,'Pay Day','Normal',20,40,100,0,0,1,X'0000',0,'The player picks up extra money after in-game battles; the money received is equal to: [user''s level * 5 * number of times Pay Day is used]. The player does not lose money if the opponent uses Pay Day but the player wins the battle.');
INSERT INTO "moves" VALUES(374,'Payback','Dark',10,50,100,0,0,1,X'0000',0,'Power doubles if the target switches out or goes before the user.');
INSERT INTO "moves" VALUES(375,'Peck','Flying',35,35,100,0,0,1,X'0000',0,'Damages the target.');
INSERT INTO "moves" VALUES(376,'Perish Song','Normal',5,255,255,0,0,3,X'0000',0,'All active Pokemon will faint in 3 turns unless they are switched out.');
INSERT INTO "moves" VALUES(377,'Petal Blizzard','Grass',15,90,100,0,0,1,X'0000',0,'The user stirs up a violent petal blizzard and attacks everything around it.');
INSERT INTO "moves" VALUES(378,'Petal Dance','Grass',10,120,100,0,0,2,X'0000',0,'The user attacks uncontrollably for 2-3 turns and then gets confused.');
INSERT INTO "moves" VALUES(379,'Phantom Force','Ghost',10,90,100,0,0,1,X'0000',0,'The user vanishes somewhere, then strikes the target on the next turn. This move hits even if the target protects itself.');
INSERT INTO "moves" VALUES(380,'Pin Missile','Bug',20,25,95,0,0,1,X'0000',0,'Attacks 2-5 times in one turn; if one of these attacks breaks a target''s Substitute, the target will take damage for the rest of the hits. This move has a 3/8 chance to hit twice, a 3/8 chance to hit three times, a 1/8 chance to hit four times and a 1/8 chance to hit five times. If the user of this move has Skill Link, this move will always strike five times.');
INSERT INTO "moves" VALUES(381,'Play Nice','Normal',20,255,255,0,0,3,X'0000',0,'The user and the target become friends, and the target loses its will to fight. This lowers the target''s Attack stat.');
INSERT INTO "moves" VALUES(382,'Play Rough','Fairy',10,90,90,0,0,1,X'0000',0,'The user plays rough with the target and attacks it. This may also lower the target''s Attack stat.');
INSERT INTO "moves" VALUES(383,'Pluck','Flying',20,60,100,0,0,1,X'0000',0,'The user eats the target''s held berry and, if applicable, receives its benefits. Jaboca Berry will be removed without damaging the user, but Coba Berry will still activate and reduce this move''s power. The target can still recover its held berry by using Recycle.');
INSERT INTO "moves" VALUES(384,'Poison Fang','Poison',15,50,100,0,0,1,X'0000',0,'Has a 30% chance to inflict Toxic poison on the target.');
INSERT INTO "moves" VALUES(385,'Poison Gas','Poison',40,255,90,0,0,3,X'0000',0,'Poisons the target.');
INSERT INTO "moves" VALUES(386,'Poison Jab','Poison',20,80,100,0,0,1,X'0000',0,'Has a 30% chance to poison the target.');
INSERT INTO "moves" VALUES(387,'Poison Powder','Poison',35,255,75,0,0,3,X'0000',0,'Poisons the target.');
INSERT INTO "moves" VALUES(388,'Poison Sting','Poison',35,15,100,0,0,1,X'0000',0,'Has a 30% chance to poison the target.');
INSERT INTO "moves" VALUES(389,'Poison Tail','Poison',25,50,100,0,0,1,X'0000',0,'Has a high critical hit ratio and a 10% chance to poison the target.');
INSERT INTO "moves" VALUES(390,'Pound','Normal',35,40,100,0,0,1,X'0000',0,'Damages the target.');
INSERT INTO "moves" VALUES(391,'Powder','Bug',20,255,100,0,0,3,X'0000',0,'The user covers the target in a powder that explodes and damages the target if it uses a Fire-type move.');
INSERT INTO "moves" VALUES(392,'Powder Snow','Ice',25,40,100,0,0,2,X'0000',0,'Has a 10% chance to freeze the target.');
INSERT INTO "moves" VALUES(393,'Power Gem','Rock',20,80,100,0,0,2,X'0000',0,'Damages the target.');
INSERT INTO "moves" VALUES(394,'Power Split','Psychic',10,255,255,0,0,3,X'0000',0,'The user''s Attack and Special Attack as well as the target''s Attack and Special Attack are averaged, with the result becoming the new value for all four stats. Stat modifiers are ignored in this calculation.');
INSERT INTO "moves" VALUES(395,'Power Swap','Psychic',10,255,255,0,0,3,X'0000',0,'The user swaps Attack and Special Attack modifiers with its target.');
INSERT INTO "moves" VALUES(396,'Power Trick','Psychic',10,255,255,0,0,3,X'0000',0,'The user switches its Defense and Attack stats. Attack and Defense modifiers continue to affect their original stats only.');
INSERT INTO "moves" VALUES(397,'Power Whip','Grass',10,120,85,0,0,1,X'0000',0,'Damages the target.');
INSERT INTO "moves" VALUES(398,'Power-Up Punch','Fighting',30,40,100,0,0,1,X'0000',0,'Striking opponents over and over makes the user''s fists harder. Hitting a target raises the Attack stat.');
INSERT INTO "moves" VALUES(399,'Precipice Blades','Ground',10,120,85,0,0,1,X'0000',0,'Unknown effect');
INSERT INTO "moves" VALUES(400,'Present','Normal',15,255,90,0,0,1,X'0000',0,'Randomly either attacks with a variable power, between 40 base power and 120 base power, or heals the target by 80 HP.');
INSERT INTO "moves" VALUES(401,'Protect','Normal',10,255,255,0,0,3,X'0000',0,'Almost always goes first. The user is protected from all attacks for one turn, but the move''s success rate halves with each consecutive use of Protect, Detect or Endure. If a Pokemon has No Guard, or used Lock-On or Mind Reader against the user during the previous turn, its attack has a [100 - move''s normal accuracy]% chance to hit through Protect; OHKO moves do not benefit from this effect. Blizzard has a 30% to hit through this move during Hail, as does Thunder during Rain Dance.');
INSERT INTO "moves" VALUES(402,'Psybeam','Psychic',20,65,100,0,0,2,X'0000',0,'Has a 10% chance to confuse the target.');
INSERT INTO "moves" VALUES(403,'Psych Up','Normal',10,255,255,0,0,3,X'0000',0,'The user copies all seven of the target''s current stat modifiers.');
INSERT INTO "moves" VALUES(404,'Psychic','Psychic',10,90,100,0,0,2,X'0000',0,'Has a 10% chance to lower the target''s Special Defense by 1 stage.');
INSERT INTO "moves" VALUES(405,'Psycho Boost','Psychic',5,140,90,0,0,2,X'0000',0,'Lowers the user''s Special Attack by 2 stages after use.');
INSERT INTO "moves" VALUES(406,'Psycho Cut','Psychic',20,70,100,0,0,1,X'0000',0,'Has a high critical hit ratio.');
INSERT INTO "moves" VALUES(407,'Psycho Shift','Psychic',10,255,90,0,0,3,X'0000',0,'The user is cured of status effects by passing them to a healthy target.');
INSERT INTO "moves" VALUES(408,'Psyshock','Psychic',10,80,100,0,0,2,X'0000',0,'This attack deals Physical damage, meaning that the user''s Special Attack and the target''s Defense are used in the damage calculation.');
INSERT INTO "moves" VALUES(409,'Psystrike','Psychic',10,100,100,0,0,2,X'0000',0,'This attack deals Physical damage, meaning that the user''s Special Attack and the target''s Defense are used in the damage calculation.');
INSERT INTO "moves" VALUES(410,'Psywave','Psychic',15,255,80,0,0,2,X'0000',0,'Randomly inflicts set damage equal to .5x, .6x, .7x, .8x, .9x, 1.0x, 1.1x, 1.2x, 1.3x, 1.4x or 1.5x the user''s level.');
INSERT INTO "moves" VALUES(411,'Punishment','Dark',5,255,100,0,0,1,X'0000',0,'Power increases for every positive stat modifier of the target''s.');
INSERT INTO "moves" VALUES(412,'Pursuit','Dark',20,40,100,0,0,1,X'0000',0,'If the target switches out on the current turn, this move strikes with doubled power before the switch. Baton Passers still escape safely. When a faster Pokemon uses Pursuit against a U-Turner or Volt Switch, these Pokemon are hit for normal damage; when a slower Pokemon uses Pursuit against Pokemon who uses these moves, the latter make their attack, then are hit by Pursuit for double power, and switches out.');
INSERT INTO "moves" VALUES(413,'Quash','Dark',15,255,100,0,0,3,X'0000',0,'Makes the target perform its move last.');
INSERT INTO "moves" VALUES(414,'Quick Attack','Normal',30,40,100,0,0,1,X'0000',0,'Usually goes first.');
INSERT INTO "moves" VALUES(415,'Quick Guard','Fighting',15,255,255,0,0,3,X'0000',0,'Protects the user''s team from moves with high priority, except for Feint. Success rate diminishes with consecutive usage.');
INSERT INTO "moves" VALUES(416,'Quiver Dance','Bug',20,255,255,0,0,3,X'0000',0,'Raises the user''s Special Attack, Special Defense and Speed by 1 stage each.');
INSERT INTO "moves" VALUES(417,'Rage','Normal',20,20,100,0,0,1,X'0000',0,'The user''s Attack rises by 1 stage if attacked before its next move.');
INSERT INTO "moves" VALUES(418,'Rage Powder','Bug',20,255,255,0,0,3,X'0000',0,'Almost always goes first. For the rest of the turn, all attacks from the opponent''s team that target the user''s team are redirected toward the user. In double battles, the user''s teammate will not be protected from attacks that have more than one intended target.');
INSERT INTO "moves" VALUES(419,'Rain Dance','Water',5,255,255,0,0,3,X'0000',0,'Cancels all other weather moves. For 5 turns: the power of Water attacks is increased by 50%, the power of Fire attacks is decreased by 50%, Thunder never misses and has a 30% chance to hit through Protect and Detect, the power of Solarbeam is halved, and the healing power of Morning Sun, Synthesis and Moonlight is decreased from 1/2 to 1/4 of the user''s max HP. The effects of Rain Dance will last for eight turns if its user is holding Damp Rock.');
INSERT INTO "moves" VALUES(420,'Rapid Spin','Normal',40,20,100,0,0,1,X'0000',0,'Removes Spikes, Stealth Rock, Toxic Spikes and Leech Seed from the user''s side of the field; also frees the user from Bind, Clamp, Fire Spin, Magma Storm, Sand Tomb, Whirlpool and Wrap. These effects do not occur if the move misses or is used against Ghost-type Pokemon.');
INSERT INTO "moves" VALUES(421,'Razor Leaf','Grass',25,55,95,0,0,1,X'0000',0,'Has a high critical hit ratio.');
INSERT INTO "moves" VALUES(422,'Razor Shell','Water',10,75,95,0,0,1,X'0000',0,'Has a 50% chance to lower the target''s Defense by 1 stage.');
INSERT INTO "moves" VALUES(423,'Razor Wind','Normal',10,80,100,0,0,2,X'0000',0,'The user prepares on turn one, becoming uncontrollable, and then attacks on turn two. Has a high critical hit ratio.');
INSERT INTO "moves" VALUES(424,'Recover','Normal',10,255,255,0,0,3,X'0000',0,'Restores 1/2 of the user''s max HP.');
INSERT INTO "moves" VALUES(425,'Recycle','Normal',10,255,255,0,0,3,X'0000',0,'The user''s lost item is recovered. Items lost to Bug Bite, Fling, Natural Gift and Pluck will be recovered if the user of Recycle was the item''s original holder; items lost to Trick, Switcheroo, Thief, Covet or Knock Off cannot be recovered.');
INSERT INTO "moves" VALUES(426,'Reflect','Psychic',20,255,255,0,0,3,X'0000',0,'All Pokemon in the user''s party receive 1/2 damage from Physical attacks for 5 turns. Reflect will be removed from the user''s field if an opponent''s Pokemon uses Brick Break. It will also last for eight turns if its user is holding Light Clay. In double battles, both Pokemon are shielded, but damage protection is reduced from 1/2 to 1/3.');
INSERT INTO "moves" VALUES(427,'Reflect Type','Normal',15,255,255,0,0,3,X'0000',0,'The user becomes the same type as the target.');
INSERT INTO "moves" VALUES(428,'Refresh','Normal',20,255,255,0,0,3,X'0000',0,'The user recovers from burn, poison and paralysis.');
INSERT INTO "moves" VALUES(429,'Relic Song','Normal',10,75,100,0,0,2,X'0000',0,'Has a 10% chance to put any targets to sleep. Meloetta changes between its Aria and Pirouette forms after it uses this move.');
INSERT INTO "moves" VALUES(430,'Rest','Psychic',10,255,255,0,0,3,X'0000',0,'The user is cured of status effects (and confusion), and recovers full HP, but falls asleep for 2 turns. Pokemon who have Early Bird will wake up one turn early.');
INSERT INTO "moves" VALUES(431,'Retaliate','Normal',5,70,100,0,0,1,X'0000',0,'Base Power increases if the user''s teammate was knocked out on the previous turn.');
INSERT INTO "moves" VALUES(432,'Return','Normal',20,255,100,0,0,1,X'0000',0,'Power increases as user''s happiness increases; maximum 102 BP.');
INSERT INTO "moves" VALUES(433,'Revenge','Fighting',10,60,100,0,0,1,X'0000',0,'Almost always goes last, even after another Pokemon''s Focus Punch; this move''s base power doubles if the user is damaged before its turn.');
INSERT INTO "moves" VALUES(434,'Reversal','Fighting',15,255,100,0,0,1,X'0000',0,'Base power increases as the user''s HP decreases.');
INSERT INTO "moves" VALUES(435,'Roar','Normal',20,255,255,0,0,3,X'0000',0,'Almost always goes last; in trainer battles, the target is switched out for a random member of its team. Escapes from wild battles. Has no effect if the target has Suction Cups, Soundproof or used Ingrain.');
INSERT INTO "moves" VALUES(436,'Roar of Time','Dragon',5,150,90,0,0,2,X'0000',0,'The user recharges during its next turn; as a result, until the end of the next turn, the user becomes uncontrollable.');
INSERT INTO "moves" VALUES(437,'Rock Blast','Rock',10,25,90,0,0,1,X'0000',0,'Attacks 2-5 times in one turn; if one of these attacks breaks a target''s Substitute, the target will take damage for the rest of the hits. This move has a 3/8 chance to hit twice, a 3/8 chance to hit three times, a 1/8 chance to hit four times and a 1/8 chance to hit five times. If the user of this move has Skill Link, this move will always strike five times.');
INSERT INTO "moves" VALUES(438,'Rock Climb','Normal',20,90,85,0,0,1,X'0000',0,'Has a 20% chance to confuse the target.');
INSERT INTO "moves" VALUES(439,'Rock Polish','Rock',20,255,255,0,0,3,X'0000',0,'Raises the user''s Speed by 2 stages.');
INSERT INTO "moves" VALUES(440,'Rock Slide','Rock',10,75,90,0,0,1,X'0000',0,'Has a 30% chance to make the target flinch.');
INSERT INTO "moves" VALUES(441,'Rock Smash','Fighting',15,40,100,0,0,1,X'0000',0,'Has a 50% chance to lower the target''s Defense by 1 stage.');
INSERT INTO "moves" VALUES(442,'Rock Throw','Rock',15,50,90,0,0,1,X'0000',0,'Damages the target.');
INSERT INTO "moves" VALUES(443,'Rock Tomb','Rock',15,60,95,0,0,1,X'0000',0,'Lowers the target''s Speed by 1 stage.');
INSERT INTO "moves" VALUES(444,'Rock Wrecker','Rock',5,150,90,0,0,1,X'0000',0,'The user recharges during its next turn; as a result, until the end of the next turn, the user becomes uncontrollable.');
INSERT INTO "moves" VALUES(445,'Role Play','Psychic',10,255,255,0,0,3,X'0000',0,'The user''s own ability is overwritten with the target''s ability; does nothing if the target''s ability is Wonder Guard.');
INSERT INTO "moves" VALUES(446,'Rolling Kick','Fighting',15,60,85,0,0,1,X'0000',0,'Has a 30% chance to make the target flinch.');
INSERT INTO "moves" VALUES(447,'Rollout','Rock',20,30,90,0,0,1,X'0000',0,'The user attacks uncontrollably for five turns; this move''s power doubles after each turn and also if Defense Curl was used beforehand. Its power resets after five turns have ended or if the attack misses.');
INSERT INTO "moves" VALUES(448,'Roost','Flying',10,255,255,0,0,3,X'0000',0,'The user recovers 1/2 of its max HP; if it is a Flying-type Pokemon, it also loses its Flying-type classification until the start of the next turn.');
INSERT INTO "moves" VALUES(449,'Rototiller','Ground',10,255,255,0,0,3,X'0000',0,'Tilling the soil, the user makes it easier for plants to grow. This raises the Attack and Sp. Atk stats of Grass-type Pokémon.');
INSERT INTO "moves" VALUES(450,'Round','Normal',15,60,100,0,0,2,X'0000',0,'Base power doubles after an ally Pokemon on the field uses this move on the same turn. If they have this move, allies who move after the initial Round are forced to use it on that turn.');
INSERT INTO "moves" VALUES(451,'Sacred Fire','Fire',5,100,95,0,0,1,X'0000',0,'Has a 50% chance to burn the target; can be used while frozen, which both attacks the target normally and thaws the user.');
INSERT INTO "moves" VALUES(452,'Sacred Sword','Fighting',20,90,100,0,0,1,X'0000',0,'This move ignores the target''s positive Defense and Evasion stat modifiers, but does not ignore Reflect.');
INSERT INTO "moves" VALUES(453,'Safeguard','Normal',25,255,255,0,0,3,X'0000',0,'Protects the user''s entire team from status conditions for five turns.');
INSERT INTO "moves" VALUES(454,'Sand Attack','Ground',15,255,100,0,0,3,X'0000',0,'Lowers the target''s Accuracy by 1 stage.');
INSERT INTO "moves" VALUES(455,'Sand Tomb','Ground',15,35,85,0,0,1,X'0000',0,'Traps the target for 4-5 turns, causing damage equal to 1/16 of its max HP each turn; this trapped effect can be broken by Rapid Spin. The target can still switch out if it is holding Shed Shell or uses Baton Pass, U-Turn or Volt Switch.');
INSERT INTO "moves" VALUES(456,'Sandstorm','Rock',10,255,255,0,0,3,X'0000',0,'Cancels all other weather moves. For 5 turns: the Special Defense of Rock-type Pokemon is boosted by 50%; each active Pokemon, even when protected by a Substitute, loses 1/16 of its max HP unless it has Sand Force, Sand Rush or Sand Veil, or is a Ground-, Rock-, or Steel-type; the power of Solarbeam is halved, and the healing power of Morning Sun, Synthesis and Moonlight is halved. The effects of Sandstorm will last for eight turns if its user is holding Smooth Rock.');
INSERT INTO "moves" VALUES(457,'Scald','Water',15,80,100,0,0,2,X'0000',0,'Has a 30% chance to burn the target.');
INSERT INTO "moves" VALUES(458,'Scary Face','Normal',10,255,100,0,0,3,X'0000',0,'Lowers the target''s Speed by 2 stages.');
INSERT INTO "moves" VALUES(459,'Scratch','Normal',35,40,100,0,0,1,X'0000',0,'Damages the target.');
INSERT INTO "moves" VALUES(460,'Screech','Normal',40,255,85,0,0,3,X'0000',0,'Lowers the target''s Defense by 2 stages.');
INSERT INTO "moves" VALUES(461,'Searing Shot','Fire',5,100,100,0,0,2,X'0000',0,'Has a 30% chance to burn the target.');
INSERT INTO "moves" VALUES(462,'Secret Power','Normal',20,70,100,0,0,1,X'0000',0,'This move has a 30% chance to inflict a side effect depending on the battle''s current terrain. The target may be put to sleep in any type of grass (or in puddles), its Attack may be lowered by 1 stage while surfing on any body of water, its Speed may be lowered by 1 stage while on marshy terrain, its Accuracy may be lowered by 1 stage on beach sand, desert sand and dirt paths, it may flinch in caves or on rocky outdoor terrain, it may become frozen on snowy terrain and it may become paralyzed everywhere else (including Wi-Fi battles).');
INSERT INTO "moves" VALUES(463,'Secret Sword','Fighting',10,85,100,0,0,2,X'0000',0,'This attack deals Physical damage, meaning that the user''s Special Attack and the target''s Defense are used in the damage calculation.');
INSERT INTO "moves" VALUES(464,'Seed Bomb','Grass',15,80,100,0,0,1,X'0000',0,'Damages the target.');
INSERT INTO "moves" VALUES(465,'Seed Flare','Grass',5,120,85,0,0,2,X'0000',0,'Has a 40% chance to lower the target''s Special Defense by 2 stages.');
INSERT INTO "moves" VALUES(466,'Seismic Toss','Fighting',20,255,100,0,0,1,X'0000',0,'Does damage equal to user''s level.');
INSERT INTO "moves" VALUES(467,'Self-Destruct','Normal',5,200,100,0,0,1,X'0000',0,'The user faints after performing this move. Unlike previous games, the target''s Defense stat is NOT halved.');
INSERT INTO "moves" VALUES(468,'Shadow Ball','Ghost',15,80,100,0,0,2,X'0000',0,'Has a 20% chance to lower the target''s Special Defense by 1 stage.');
INSERT INTO "moves" VALUES(469,'Shadow Claw','Ghost',15,70,100,0,0,1,X'0000',0,'Has a high critical hit ratio.');
INSERT INTO "moves" VALUES(470,'Shadow Force','Ghost',5,120,100,0,0,1,X'0000',0,'The user disappears on the first turn, becoming uncontrollable and evading all attacks, and strikes on the second turn. This move is not stopped by Protect or Detect.');
INSERT INTO "moves" VALUES(471,'Shadow Punch','Ghost',20,60,255,0,0,1,X'0000',0,'Ignores Evasion and Accuracy modifiers and never misses except against Protect, Detect or a target in the middle of Dig, Fly, Dive or Bounce.');
INSERT INTO "moves" VALUES(472,'Shadow Sneak','Ghost',30,40,100,0,0,1,X'0000',0,'Usually goes first.');
INSERT INTO "moves" VALUES(473,'Sharpen','Normal',30,255,255,0,0,3,X'0000',0,'Raises the user''s Attack by 1 stage.');
INSERT INTO "moves" VALUES(474,'Sheer Cold','Ice',5,255,30,0,0,2,X'0000',0,'The target faints; doesn''t work on higher-leveled Pokemon.');
INSERT INTO "moves" VALUES(475,'Shell Smash','Normal',15,255,255,0,0,3,X'0000',0,'Lower the user''s Defense and Special Defense by 1 stage, but raises its Attack, Special Attack and Speed by 2 stages.');
INSERT INTO "moves" VALUES(476,'Shift Gear','Steel',10,255,255,0,0,3,X'0000',0,'Raises the user''s Attack by 1 stage and Speed by 2 stages.');
INSERT INTO "moves" VALUES(477,'Shock Wave','Electric',20,60,255,0,0,2,X'0000',0,'Ignores Evasion and Accuracy modifiers and never misses except against Protect, Detect or a target in the middle of Dig, Fly, Dive or Bounce.');
INSERT INTO "moves" VALUES(478,'Signal Beam','Bug',15,75,100,0,0,2,X'0000',0,'Has a 10% chance to confuse the target.');
INSERT INTO "moves" VALUES(479,'Silver Wind','Bug',5,60,100,0,0,2,X'0000',0,'Has a 10% chance to raise all of the user''s stats by 1 stage.');
INSERT INTO "moves" VALUES(480,'Simple Beam','Normal',15,255,100,0,0,3,X'0000',0,'The target''s ability becomes Simple until it switches out. This move fails against Pokemon who have the Truant or Multitype abilities.');
INSERT INTO "moves" VALUES(481,'Sing','Normal',15,255,55,0,0,3,X'0000',0,'Puts the target to sleep.');
INSERT INTO "moves" VALUES(482,'Sketch','Normal',1,255,255,0,0,3,X'0000',0,'The user permanently replaces Sketch with the last move used by the target. Sketch cannot copy itself, Chatter, Memento or Struggle. Transform and moves that generate other moves can be Sketched successfully, as can Explosion and Selfdestruct if a Pokemon with Damp is present. Healing Wish and Lunar Dance can be Sketched because they automatically fail when the user is the last Pokemon of its team. This move fails automatically when selected in wireless or Wi-Fi battles.');
INSERT INTO "moves" VALUES(483,'Skill Swap','Psychic',10,255,255,0,0,3,X'0000',0,'The user exchanges abilities with the target; does not work if Wonder Guard is the ability of either the user or the target.');
INSERT INTO "moves" VALUES(484,'Skull Bash','Normal',10,130,100,0,0,1,X'0000',0,'The user prepares on turn one, raising its Defense by 1 stage and becoming uncontrollable, and then attacks on turn two.');
INSERT INTO "moves" VALUES(485,'Sky Attack','Flying',5,140,90,0,0,1,X'0000',0,'The user prepares on turn one, becoming uncontrollable, and then attacks on turn two. Also has a 30% chance to make the target flinch.');
INSERT INTO "moves" VALUES(486,'Sky Drop','Flying',10,60,100,0,0,1,X'0000',0,'On the first turn, the user flies into the air with the target, preventing the target''s next move. On the second turn, the user inflicts damage. When used by teammates on the ground, the moves Gust, Thunder, Twister, Hurricane, Sky Uppercut and Smack Down can hit the user of Sky Drop during the interim.');
INSERT INTO "moves" VALUES(487,'Sky Uppercut','Fighting',15,85,90,0,0,1,X'0000',0,'Also hits targets in mid-air via Fly, Bounce or Sky Drop.');
INSERT INTO "moves" VALUES(488,'Slack Off','Normal',10,255,255,0,0,3,X'0000',0,'Restores 1/2 of the user''s max HP.');
INSERT INTO "moves" VALUES(489,'Slam','Normal',20,80,75,0,0,1,X'0000',0,'Damages the target.');
INSERT INTO "moves" VALUES(490,'Slash','Normal',20,70,100,0,0,1,X'0000',0,'Has a high critical hit ratio.');
INSERT INTO "moves" VALUES(491,'Sleep Powder','Grass',15,255,75,0,0,3,X'0000',0,'Puts the target to sleep.');
INSERT INTO "moves" VALUES(492,'Sleep Talk','Normal',10,255,255,0,0,3,X'0000',0,'Does nothing if the user is awake. If the user asleep, it randomly performs one of its attacks. Rest will fail if it is selected. Sleep Talk''s generated attacks do not cost PP, but it cannot generate moves with 0 PP, itself, Assist, Bide, Chatter, Copycat, Disabled attacks, Focus Punch, Me First, Metronome, Mirror Move, Uproar, or attacks with a charge-up turn like Fly or Skull Bash. (Moves with a recharge turn like Hyper Beam can be generated.)');
INSERT INTO "moves" VALUES(493,'Sludge','Poison',20,65,100,0,0,2,X'0000',0,'Has a 30% chance to poison the target.');
INSERT INTO "moves" VALUES(494,'Sludge Bomb','Poison',10,90,100,0,0,2,X'0000',0,'Has a 30% chance to poison the target.');
INSERT INTO "moves" VALUES(495,'Sludge Wave','Poison',10,95,100,0,0,2,X'0000',0,'Has a 10% chance to poison the target.');
INSERT INTO "moves" VALUES(496,'Smack Down','Rock',15,50,100,0,0,1,X'0000',0,'Until the target switches out, it loses its immunity to Ground-type moves if it is a Flying-type or has Levitate, unless it has used Roost on the same turn. The effects of Magnet Rise and Telekinesis are also removed, and this move can hit opponents in the middle of using Sky Drop, Fly or Bounce, immediately canceling the latter two.');
INSERT INTO "moves" VALUES(497,'Smelling Salts','Normal',10,70,100,0,0,1,X'0000',0,'If the target is paralyzed, power is doubled but the target will be cured.');
INSERT INTO "moves" VALUES(498,'Smog','Poison',20,30,70,0,0,2,X'0000',0,'Has a 40% chance to poison the target.');
INSERT INTO "moves" VALUES(499,'Smokescreen','Normal',20,255,100,0,0,3,X'0000',0,'Lowers the target''s Accuracy by 1 stage.');
INSERT INTO "moves" VALUES(500,'Snarl','Dark',15,55,95,0,0,2,X'0000',0,'Lowers the target''s Special Attack by 1 stage.');
INSERT INTO "moves" VALUES(501,'Snatch','Dark',10,255,255,0,0,3,X'0000',0,'Almost always goes first. Until the end of the turn, the user will steal a supporting move from another Pokemon (including teammates). In Double or Triple Battles, Snatch only steals the first applicable move performed by another Pokemon before wearing off.');
INSERT INTO "moves" VALUES(502,'Snore','Normal',15,50,100,0,0,2,X'0000',0,'Has a 30% chance to make the target flinch; fails if user is awake.');
INSERT INTO "moves" VALUES(503,'Soak','Water',20,255,100,0,0,3,X'0000',0,'The target becomes a Water-type until it switches out or faints. Pokemon that have the Multitype ability are not affected.');
INSERT INTO "moves" VALUES(504,'Soft-Boiled','Normal',10,255,255,0,0,3,X'0000',0,'Restores 1/2 of the user''s max HP.');
INSERT INTO "moves" VALUES(505,'Solar Beam','Grass',10,120,100,0,0,2,X'0000',0,'The user prepares on turn one, becoming uncontrollable, and then attacks on turn two. During Sunny Day, this move fires immediately; during Rain Dance, Sandstorm and Hail, this move has half power.');
INSERT INTO "moves" VALUES(506,'Sonic Boom','Normal',20,20,90,0,0,2,X'0000',0,'Always deals 20 points of damage.');
INSERT INTO "moves" VALUES(507,'Spacial Rend','Dragon',5,100,95,0,0,2,X'0000',0,'Has a high critical hit ratio.');
INSERT INTO "moves" VALUES(508,'Spark','Electric',20,65,100,0,0,1,X'0000',0,'Has a 30% chance to paralyze the target.');
INSERT INTO "moves" VALUES(509,'Spider Web','Bug',10,255,255,0,0,3,X'0000',0,'As long as the user remains in battle, the target cannot switch out unless it is holding Shed Shell or uses Baton Pass, U-Turn or Volt Switch. The target will still be trapped if the user switches out by using Baton Pass.');
INSERT INTO "moves" VALUES(510,'Spike Cannon','Normal',15,20,100,0,0,1,X'0000',0,'Attacks 2-5 times in one turn; if one of these attacks breaks a target''s Substitute, the target will take damage for the rest of the hits. This move has a 3/8 chance to hit twice, a 3/8 chance to hit three times, a 1/8 chance to hit four times and a 1/8 chance to hit five times. If the user of this move has Skill Link, this move will always strike five times.');
INSERT INTO "moves" VALUES(511,'Spikes','Ground',20,255,255,0,0,3,X'0000',0,'Damages opponents, unless they are Flying-type or have Levitate, every time they are switched in; hits through Wonder Guard. Can be used up to three times: saps 1/8 of max HP with one layer, 3/16 of max HP with two layers and 1/4 of max HP for three layers.');
INSERT INTO "moves" VALUES(512,'Spiky Shield','Grass',20,255,255,0,0,3,X'0000',0,'In addition to protecting the user from attacks, this move also damages any attacker who makes direct contact.');
INSERT INTO "moves" VALUES(513,'Spit Up','Normal',10,255,100,0,0,2,X'0000',0,'Power increases with user''s Stockpile count; fails with zero Stockpiles.');
INSERT INTO "moves" VALUES(514,'Spite','Ghost',10,255,100,0,0,3,X'0000',0,'The target''s most recent move is reduced by 4 PP; this fails if the target has not yet performed a move, if the target''s last move has run out of PP, or if the target can only use Struggle.');
INSERT INTO "moves" VALUES(515,'Splash','Normal',40,255,255,0,0,3,X'0000',0,'Doesn''t do anything (but we still love it). Unfortunately, it also cannot be used during the effects of Gravity! :(');
INSERT INTO "moves" VALUES(516,'Spore','Grass',15,255,100,0,0,3,X'0000',0,'Puts the target to sleep.');
INSERT INTO "moves" VALUES(517,'Stealth Rock','Rock',20,255,255,0,0,3,X'0000',0,'Damages opponents every time they switch in until they use Rapid Spin. Saps a fraction of max HP determined by the effectiveness of Rock-type attacks against the opponent''s type: 1/32 for 1/4x, 1/16 for 1/2x, 1/8 for 1x, 1/4 for 2x and 1/2 for 4x. For example, Stealth Rock saps 50% of an Ice/Flying Pokemon''s max HP when it switches in.');
INSERT INTO "moves" VALUES(518,'Steam Eruption','Water',5,110,95,0,0,2,X'0000',0,'The user immerses the target in superheated steam. This may also leave the target with a burn.');
INSERT INTO "moves" VALUES(519,'Steamroller','Bug',20,65,100,0,0,1,X'0000',0,'Has a 30% chance to make the target flinch; also retains its normal accuracy and gains doubled power against Minimized Pokemon.');
INSERT INTO "moves" VALUES(520,'Steel Wing','Steel',25,70,90,0,0,1,X'0000',0,'Has a 10% chance to raise the user''s Defense by 1 stage.');
INSERT INTO "moves" VALUES(521,'Sticky Web','Bug',20,255,255,0,0,3,X'0000',0,'The user weaves a sticky net around the opposing team, which lowers their Speed stat upon switching into battle.');
INSERT INTO "moves" VALUES(522,'Stockpile','Normal',20,255,255,0,0,3,X'0000',0,'Can be used up to three times to power Spit Up or Swallow. Also raises the user''s Defense and Special Defense by 1 stage each. Stockpiles and the defensive boosts are lost when these Swallow or Spit Up is used or the user switches out. However, the boosts can be Baton Passed.');
INSERT INTO "moves" VALUES(523,'Stomp','Normal',20,65,100,0,0,1,X'0000',0,'Has a 30% chance to make the target flinch; also retains its normal accuracy and gains doubled power against Minimized Pokemon.');
INSERT INTO "moves" VALUES(524,'Stone Edge','Rock',5,100,80,0,0,1,X'0000',0,'Has a high critical hit ratio.');
INSERT INTO "moves" VALUES(525,'Stored Power','Psychic',10,20,100,0,0,2,X'0000',0,'Base power increases by 20 for each of the target''s positive stat modifiers.');
INSERT INTO "moves" VALUES(526,'Storm Throw','Fighting',10,60,100,0,0,1,X'0000',0,'This move always lands as a critical hit, unless the target is under the effect of Lucky Chant or has an ability that prevents Critical Hits.');
INSERT INTO "moves" VALUES(527,'Strength','Normal',15,80,100,0,0,1,X'0000',0,'Damages the target.');
INSERT INTO "moves" VALUES(528,'String Shot','Bug',40,255,95,0,0,3,X'0000',0,'Lowers the target''s Speed by 1 stage.');
INSERT INTO "moves" VALUES(529,'Struggle','Normal',1,50,255,0,0,1,X'0000',0,'Used automatically when all of the user''s other moves have run out of PP or are otherwise inaccessible. The user receives recoil damage equal to 1/4 of its max HP. Struggle is classified as a typeless move and will hit any Pokemon, including Rock-, Steel- and Ghost-type Pokemon, for normal damage.');
INSERT INTO "moves" VALUES(530,'Struggle Bug','Bug',20,50,100,0,0,2,X'0000',0,'Lowers the target''s Special Attack by 1 stage.');
INSERT INTO "moves" VALUES(531,'Stun Spore','Grass',30,255,75,0,0,3,X'0000',0,'Paralyzes the target.');
INSERT INTO "moves" VALUES(532,'Submission','Fighting',25,80,80,0,0,1,X'0000',0,'The user receives 1/4 recoil damage.');
INSERT INTO "moves" VALUES(533,'Substitute','Normal',10,255,255,0,0,3,X'0000',0,'The user takes one-fourth of its maximum HP to create a substitute; this move fails if the user does not have enough HP for this. Until the substitute is broken, it receives damage from all attacks made by other Pokemon and shields the user from status effects and stat modifiers caused by other Pokemon. The user is still affected by Tickle, Hail, Sandstorm and Attract from behind its Substitute. If a Substitute breaks from a hit during a multistrike move such as Fury Attack, the user takes damage from the remaining strikes.');
INSERT INTO "moves" VALUES(534,'Sucker Punch','Dark',5,80,100,0,0,1,X'0000',0,'Almost always goes first, but fails if the target doesn''t select a move that will damage the user or its allies. The move also fails if the target uses an attack with higher priority or if the target is faster and uses an attack with the same priority.');
INSERT INTO "moves" VALUES(535,'Sunny Day','Fire',5,255,255,0,0,3,X'0000',0,'Cancels all other weather moves. For 5 turns: freezing is prevented, the power of Fire attacks is increased by 50%, the power of Water attacks is decreased by 50%, Solarbeam fires immediately, Thunder becomes 50% accurate, and the healing power of Morning Sun, Synthesis and Moonlight is increased from 1/2 to 2/3 of the user''s max HP. The effects of Sunny Day will last for eight turns if its user is holding Heat Rock.');
INSERT INTO "moves" VALUES(536,'Super Fang','Normal',10,255,90,0,0,1,X'0000',0,'This move halves the target''s current HP.');
INSERT INTO "moves" VALUES(537,'Superpower','Fighting',5,120,100,0,0,1,X'0000',0,'Lowers the user''s Attack and Defense by 1 stage each after use.');
INSERT INTO "moves" VALUES(538,'Supersonic','Normal',20,255,55,0,0,3,X'0000',0,'Confuses the target.');
INSERT INTO "moves" VALUES(539,'Surf','Water',15,90,100,0,0,2,X'0000',0,'Power doubles against a target who is in the middle of using Dive.');
INSERT INTO "moves" VALUES(540,'Swagger','Normal',15,255,90,0,0,3,X'0000',0,'Confuses the target and raises its Attack by 2 stages.');
INSERT INTO "moves" VALUES(541,'Swallow','Normal',10,255,255,0,0,3,X'0000',0,'This move only works if the user has at least one Stockpile. Restores 1/4 of user''s max HP with one Stockpile, 1/2 of user''s max HP with two Stockpiles and fully restores the user''s HP with three Stockpiles.');
INSERT INTO "moves" VALUES(542,'Sweet Kiss','Normal',10,255,75,0,0,3,X'0000',0,'Confuses the target.');
INSERT INTO "moves" VALUES(543,'Sweet Scent','Normal',20,255,100,0,0,3,X'0000',0,'Lowers the target''s Evasion by 1 stage.');
INSERT INTO "moves" VALUES(544,'Swift','Normal',20,60,255,0,0,2,X'0000',0,'Ignores Evasion and Accuracy modifiers and never misses except against Protect, Detect or a target in the middle of Dig, Fly, Dive or Bounce.');
INSERT INTO "moves" VALUES(545,'Switcheroo','Dark',10,255,100,0,0,3,X'0000',0,'Exchanges items with the target unless it has Sticky Hold or Multitype. Items lost to this move cannot be recovered by using Recycle.');
INSERT INTO "moves" VALUES(546,'Swords Dance','Normal',20,255,255,0,0,3,X'0000',0,'Raises the user''s Attack by 2 stages.');
INSERT INTO "moves" VALUES(547,'Synchronoise','Psychic',15,120,100,0,0,2,X'0000',0,'This move only affects targets that are the same type as the user. For dual-type users, it hits any Pokemon that have either of the user''s types.');
INSERT INTO "moves" VALUES(548,'Synthesis','Grass',5,255,255,0,0,3,X'0000',0,'Restores a fraction of the user''s max HP depending on the weather: 2/3 during Sunny Day, 1/2 during regular weather and 1/4 during Rain Dance, Hail and Sandstorm.');
INSERT INTO "moves" VALUES(549,'Tackle','Normal',35,50,100,0,0,1,X'0000',0,'Damages the target.');
INSERT INTO "moves" VALUES(550,'Tail Glow','Bug',20,255,255,0,0,3,X'0000',0,'Raises the user''s Special Attack by 3 stages.');
INSERT INTO "moves" VALUES(551,'Tail Slap','Normal',10,25,85,0,0,1,X'0000',0,'Attacks 2-5 times in one turn; if one of these attacks breaks a target''s Substitute, the target will take damage for the rest of the hits. This move has a 3/8 chance to hit twice, a 3/8 chance to hit three times, a 1/8 chance to hit four times and a 1/8 chance to hit five times. If the user of this move has Skill Link, this move will always strike five times.');
INSERT INTO "moves" VALUES(552,'Tail Whip','Normal',30,255,100,0,0,3,X'0000',0,'Lowers the target''s Defense by 1 stage.');
INSERT INTO "moves" VALUES(553,'Tailwind','Flying',30,255,255,0,0,3,X'0000',0,'Speed is doubled for every Pokemon on the user''s team for four turns; the turn used to perform Tailwind is included in this total, as are any turns used to switch around Pokemon.');
INSERT INTO "moves" VALUES(554,'Take Down','Normal',20,90,85,0,0,1,X'0000',0,'The user receives 1/4 recoil damage.');
INSERT INTO "moves" VALUES(555,'Taunt','Dark',20,255,100,0,0,3,X'0000',0,'Prevents the target from using non-damaging moves for three turns. Assist, Copycat, Me First, Metronome, Mirror Move and Sleep Talk are prevented during this time, but Bide, Counter, Endeavor, Metal Burst and Mirror Coat are not prevented.');
INSERT INTO "moves" VALUES(556,'Techno Blast','Normal',5,85,100,0,0,2,X'0000',0,'This move''s type changes according to the user''s held Drives. Note that there are only Drives corresponding to four types: Electric (Shock), Fire (Burn), Ice (Chill) and Water (Douse).');
INSERT INTO "moves" VALUES(557,'Teeter Dance','Normal',20,255,100,0,0,3,X'0000',0,'Each active Pokemon, except the user, becomes confused.');
INSERT INTO "moves" VALUES(558,'Telekinesis','Psychic',15,255,255,0,0,3,X'0000',0,'For three turns, the target becomes immune to Ground-type moves, entry hazards Spikes and Toxic Spikes, and the Arena Trap ability, but all other moves except for one-hit KO moves have perfect accuracy against the target. The moves Gravity or Ingrain and the hold item Iron Ball negate this effect and prevent this move from succeeding.');
INSERT INTO "moves" VALUES(559,'Teleport','Psychic',20,255,255,0,0,3,X'0000',0,'Escapes from wild battles; fails automatically in trainer and link battles.');
INSERT INTO "moves" VALUES(560,'Thief','Dark',10,60,100,0,0,1,X'0000',0,'Steals the target''s held item unless the user is already holding an item or the target has Sticky Hold or Multitype. A stolen item cannot be recovered by using Recycle.');
INSERT INTO "moves" VALUES(561,'Thousand Arrows','Ground',10,90,100,0,0,1,X'0000',0,'This move also hits opposing Pokémon that are in the air. Those Pokémon are knocked down to the ground.');
INSERT INTO "moves" VALUES(562,'Thousand Waves','Ground',10,90,100,0,0,1,X'0000',0,'The user attacks with a wave that crawls along the ground. Those hit can''t flee from battle.');
INSERT INTO "moves" VALUES(563,'Thrash','Normal',10,120,100,0,0,1,X'0000',0,'The user attacks uncontrollably for 2-3 turns and then gets confused.');
INSERT INTO "moves" VALUES(564,'Thunder','Electric',10,110,70,0,0,2,X'0000',0,'Has a 30% chance to paralyze the target. It also has normal accuracy against mid-air Pokemon who have used Fly, Bounce or Sky Drop. During Sunny Day, this move has 50% accuracy. During Rain Dance, this move will never miss under normal circumstances.');
INSERT INTO "moves" VALUES(565,'Thunder Fang','Electric',15,65,95,0,0,1,X'0000',0,'Has a 10% chance to paralyze the target. Has 10% chance to make the target flinch. Both effects can occur from a single use.');
INSERT INTO "moves" VALUES(566,'Thunder Punch','Electric',15,75,100,0,0,1,X'0000',0,'Has a 10% chance to paralyze the target.');
INSERT INTO "moves" VALUES(567,'Thunder Shock','Electric',30,40,100,0,0,2,X'0000',0,'Has a 10% chance to paralyze the target.');
INSERT INTO "moves" VALUES(568,'Thunder Wave','Electric',20,255,100,0,0,3,X'0000',0,'Paralyzes the target. This move activates Volt Absorb and Motor Drive.');
INSERT INTO "moves" VALUES(569,'Thunderbolt','Electric',15,90,100,0,0,2,X'0000',0,'Has a 10% chance to paralyze the target.');
INSERT INTO "moves" VALUES(570,'Tickle','Normal',20,255,100,0,0,3,X'0000',0,'Lowers the target''s Attack and Defense by 1 stage each.');
INSERT INTO "moves" VALUES(571,'Topsy-Turvy','Dark',10,255,255,0,0,3,X'0000',0,'All stat changes affecting the target turn topsy-turvy and become the opposite of what they were.');
INSERT INTO "moves" VALUES(572,'Torment','Dark',15,255,100,0,0,3,X'0000',0,'Prevents the target from using the same move for two turns in a row until the target is switched out. The target will use Struggle every other turn if it cannot attack without using the same move.');
INSERT INTO "moves" VALUES(573,'Toxic','Poison',10,255,90,0,0,3,X'0000',0,'The target is badly poisoned, with the damage caused by poison doubling after each turn. Toxic poisoning will remain with the Pokemon during the battle even after switching out.');
INSERT INTO "moves" VALUES(574,'Toxic Spikes','Poison',20,255,255,0,0,3,X'0000',0,'Opponents will be poisoned as they enter the field until they use Rapid Spin. One layer inflicts regular poison while two layers inflict Toxic; switching in a non-Flying-type Poison Pokemon without Levitate will remove any layers. Flying-type and Levitate Pokemon are only affected if they switch in while Gravity is in effect, Iron Ball is their held item or they are receiving a Baton Passed Ingrain; Steel-type Pokemon and Pokemon who enter with Baton Passed Substitutes are not affected.');
INSERT INTO "moves" VALUES(575,'Transform','Normal',10,255,255,0,0,3,X'0000',0,'The user morphs into a near-exact copy of the target. Stats, stat modifiers, type, moves, Hidden Power data and appearance are changed; the user''s level and HP remain the same and each copied move receives only 5 PP. (If Transform is used by Ditto, the effects of Metal Powder and Quick Powder stop working after transformation.)');
INSERT INTO "moves" VALUES(576,'Tri Attack','Normal',10,80,100,0,0,2,X'0000',0,'Has a 20% chance to burn, paralyze or freeze the target.');
INSERT INTO "moves" VALUES(577,'Trick','Psychic',10,255,100,0,0,3,X'0000',0,'Exchanges items with the target unless it has Sticky Hold or Multitype. Items lost to this move cannot be recovered by using Recycle.');
INSERT INTO "moves" VALUES(578,'Trick Room','Psychic',5,255,255,0,0,3,X'0000',0,'Always goes last. Attacking order is reversed for all active Pokemon for five turns; the slowest Pokemon moves first and vice versa. Note that move order is still determined by the regular priority categories and the effects of Trick Room apply only when Pokemon have chosen moves with the same priority. Using Trick Room a second time reverses this effect. This effect is also ignored by Stall and held items that may affect the turn order: Full Incense, Lagging Tail, Quick Claw and Custap Berry.');
INSERT INTO "moves" VALUES(579,'Trick-or-Treat','Ghost',20,255,100,0,0,3,X'0000',0,'The user takes the target trick-or-treating. This adds Ghost type to the target''s type.');
INSERT INTO "moves" VALUES(580,'Triple Kick','Fighting',10,10,90,0,0,1,X'0000',0,'Attacks three times in one turn, adding 10 BP for each kick. If a kick misses, the move ends instantly; if one of the kicks breaks a target''s Substitute, the real Pokemon will take damage for the remaining kicks.');
INSERT INTO "moves" VALUES(581,'Trump Card','Normal',5,255,255,0,0,2,X'0000',0,'This move''s base power increases as its remaining PP decreases.');
INSERT INTO "moves" VALUES(582,'Twineedle','Bug',20,25,100,0,0,1,X'0000',0,'Strikes twice; if the first hit breaks the target''s Substitute, the real Pokemon will take damage from the second hit. Has a 20% chance to poison the target .');
INSERT INTO "moves" VALUES(583,'Twister','Dragon',20,40,100,0,0,2,X'0000',0,'Has a 20% chance to make the target flinch; power doubles while the target is in mid-air via Fly or Bounce.');
INSERT INTO "moves" VALUES(584,'U-turn','Bug',20,70,100,0,0,1,X'0000',0,'The user switches out after use, even if it is currently trapped by another Pokemon. If the replacement Pokemon knows this move and is holding Choice Band, Choice Specs or Choice Scarf, it will be locked into the move, though Pokemon holding those items who don''t know the move can select their attack as normal. When a faster Pokemon uses Pursuit against a Pokemon who uses this move, the latter is hit for normal damage; when a slower Pokemon uses Pursuit against a move who uses this move, the latter makes its attack, then is hit by Pursuit for double power, and switches out.');
INSERT INTO "moves" VALUES(585,'Uproar','Normal',10,90,100,0,0,2,X'0000',0,'The user causes an Uproar and attacks uncontrollably for three turns. During the effects of the Uproar, active Pokemon cannot fall asleep and sleeping Pokemon sent into battle will wake up.');
INSERT INTO "moves" VALUES(586,'V-create','Fire',5,180,95,0,0,1,X'0000',0,'Lowers the user''s Defense, Special Defense and Speed by 1 stage each. Also, in Double or Triple Battles, the user''s allies take damage equal to 1/16 of their maximum HP.');
INSERT INTO "moves" VALUES(587,'Vacuum Wave','Fighting',30,40,100,0,0,2,X'0000',0,'Usually goes first.');
INSERT INTO "moves" VALUES(588,'Venom Drench','Poison',20,255,100,0,0,3,X'0000',0,'Opposing Pokémon are drenched in an odd poisonous liquid. This lowers the Attack, Sp. Atk, and Speed stats of a poisoned target.');
INSERT INTO "moves" VALUES(589,'Venoshock','Poison',10,65,100,0,0,2,X'0000',0,'Base Power doubles if the target is poisoned.');
INSERT INTO "moves" VALUES(590,'Vice Grip','Normal',30,55,100,0,0,1,X'0000',0,'Damages the target.');
INSERT INTO "moves" VALUES(591,'Vine Whip','Grass',25,45,100,0,0,1,X'0000',0,'Damages the target.');
INSERT INTO "moves" VALUES(592,'Vital Throw','Fighting',10,70,255,0,0,1,X'0000',0,'This move usually goes last. It ignores Evasion and Accuracy modifiers and never misses except against Protect, Detect or a target in the middle of Dig, Fly, Dive or Bounce.');
INSERT INTO "moves" VALUES(593,'Volt Switch','Electric',20,70,100,0,0,2,X'0000',0,'The user switches out after use, even if it is currently trapped by another Pokemon. If the replacement Pokemon knows this move and is holding Choice Band, Choice Specs or Choice Scarf, it will be locked into the move, though Pokemon holding those items who don''t know the move can select their attack as normal. When a faster Pokemon uses Pursuit against a Pokemon who uses this move, the latter is hit for normal damage; when a slower Pokemon uses Pursuit against a move who uses this move, the latter makes its attack, then is hit by Pursuit for double power, and switches out. If this move is used against a Ground-type Pokemon, the switching effect does not occur.');
INSERT INTO "moves" VALUES(594,'Volt Tackle','Electric',15,120,100,0,0,1,X'0000',0,'The user receives 1/3 recoil damage; has a 10% chance to paralyze the target.');
INSERT INTO "moves" VALUES(595,'Wake-Up Slap','Fighting',10,70,100,0,0,1,X'0000',0,'If the target is asleep, power is doubled but the target will awaken.');
INSERT INTO "moves" VALUES(596,'Water Gun','Water',25,40,100,0,0,2,X'0000',0,'Damages the target.');
INSERT INTO "moves" VALUES(597,'Water Pledge','Water',10,80,100,0,0,2,X'0000',0,'When used with Fire Pledge, this move creates a rainbow that confuses all targets. When used with Grass Pledge, this move creates a swamp that lowers the Speed of all targets.');
INSERT INTO "moves" VALUES(598,'Water Pulse','Water',20,60,100,0,0,2,X'0000',0,'Has a 20% chance to confuse the target.');
INSERT INTO "moves" VALUES(599,'Water Shuriken','Water',20,15,100,0,0,1,X'0000',0,'The user hits the target with throwing stars two to five times in a row. This move always goes first.');
INSERT INTO "moves" VALUES(600,'Water Sport','Water',15,255,255,0,0,3,X'0000',0,'All Fire-type moves are 50% weaker until the user switches out.');
INSERT INTO "moves" VALUES(601,'Water Spout','Water',5,255,100,0,0,2,X'0000',0,'Base power decreases as the user''s HP decreases.');
INSERT INTO "moves" VALUES(602,'Waterfall','Water',15,80,100,0,0,1,X'0000',0,'Has a 20% chance to make the target flinch.');
INSERT INTO "moves" VALUES(603,'Weather Ball','Normal',10,50,100,0,0,2,X'0000',0,'Base power doubles and move''s type changes during weather effects: becomes Fire-type during Sunny Day, Water-type during Rain Dance, Ice-type during Hail and Rock-type during Sandstorm.');
INSERT INTO "moves" VALUES(604,'Whirlpool','Water',15,35,85,0,0,2,X'0000',0,'Traps the target for 4-5 turns, causing damage equal to 1/16 of its max HP each turn; this trapped effect can be broken by Rapid Spin. The target can still switch out if it is holding Shed Shell or uses Baton Pass, U-Turn or Volt Switch. Base power also doubles when performed against a Pokemon in the middle of using Dive.');
INSERT INTO "moves" VALUES(605,'Whirlwind','Normal',20,255,255,0,0,3,X'0000',0,'Almost always goes last; in trainer battles, the target is switched out for a random member of its team. Escapes from wild battles. Has no effect if the target has Suction Cups or used Ingrain.');
INSERT INTO "moves" VALUES(606,'Wide Guard','Rock',10,255,255,0,0,3,X'0000',0,'Protects the user''s team from moves that hit all team members, including those from the user''s own team. Success rate diminishes with consecutive usage.');
INSERT INTO "moves" VALUES(607,'Wild Charge','Electric',15,90,100,0,0,1,X'0000',0,'The user receives 1/4 recoil damage.');
INSERT INTO "moves" VALUES(608,'Will-O-Wisp','Fire',15,255,85,0,0,3,X'0000',0,'Burns the target. This move activates Flash Fire.');
INSERT INTO "moves" VALUES(609,'Wing Attack','Flying',35,60,100,0,0,1,X'0000',0,'Damages the target.');
INSERT INTO "moves" VALUES(610,'Wish','Normal',10,255,255,0,0,3,X'0000',0,'At the end of the next turn, the active Pokemon from the user''s team is healed by 1/2 of the Wish user''s max HP; this can be any member of the user''s team.');
INSERT INTO "moves" VALUES(611,'Withdraw','Water',40,255,255,0,0,3,X'0000',0,'Raises the user''s Defense by 1 stage.');
INSERT INTO "moves" VALUES(612,'Wonder Room','Psychic',10,255,255,0,0,3,X'0000',0,'Always goes last. Each Pokemon''s Defense and Special Defense stats are swapped for five turns; using this move again ends this effect. Existing stat modifiers stay in effect for their original stat.');
INSERT INTO "moves" VALUES(613,'Wood Hammer','Grass',15,120,100,0,0,1,X'0000',0,'The user receives 1/3 recoil damage.');
INSERT INTO "moves" VALUES(614,'Work Up','Normal',30,255,255,0,0,3,X'0000',0,'Raises the user''s Attack and Special Attack by 1 stage each.');
INSERT INTO "moves" VALUES(615,'Worry Seed','Grass',10,255,100,0,0,3,X'0000',0,'The target''s ability, unless it has Multitype or Truant, is changed to Insomnia until it switches out.');
INSERT INTO "moves" VALUES(616,'Wrap','Normal',20,15,90,0,0,1,X'0000',0,'Traps the target for 4-5 turns, causing damage equal to 1/16 of its max HP each turn; this trapped effect can be broken by Rapid Spin. The target can still switch out if it is holding Shed Shell or uses Baton Pass, U-Turn or Volt Switch.');
INSERT INTO "moves" VALUES(617,'Wring Out','Normal',5,255,100,0,0,2,X'0000',0,'Base power decreases as the target''s HP decreases.');
INSERT INTO "moves" VALUES(618,'X-Scissor','Bug',15,80,100,0,0,1,X'0000',0,'Damages the target.');
INSERT INTO "moves" VALUES(619,'Yawn','Normal',10,255,255,0,0,3,X'0000',0,'If the target stays in battle, it falls asleep at the end of the next turn.');
INSERT INTO "moves" VALUES(620,'Zap Cannon','Electric',5,120,50,0,0,2,X'0000',0,'Paralyzes the target.');
INSERT INTO "moves" VALUES(621,'Zen Headbutt','Psychic',15,80,90,0,0,1,X'0000',0,'Has a 20% chance to make the target flinch.');
INSERT INTO "abilities" VALUES(0,'Adaptability',0,0,'This Pokemon''s attacks that receive STAB (Same Type Attack Bonus) are increased from 50% to 100%.');
INSERT INTO "abilities" VALUES(1,'Aerilate',0,0,'All of this Pokemon''s Normal-typed moves are now Flying-type.');
INSERT INTO "abilities" VALUES(2,'Aftermath',0,0,'If a direct attack knocks out this Pokemon, the opponent receives damage equal to one-fourth of its max HP.');
INSERT INTO "abilities" VALUES(3,'Air Lock',0,0,'While this Pokemon is active, all weather conditions and their effects are disabled.');
INSERT INTO "abilities" VALUES(4,'Analytic',0,0,'If the user goes after the opponent, the power of the user''s attack receives a 30% boost.');
INSERT INTO "abilities" VALUES(5,'Anger Point',0,0,'If this Pokemon, or its Substitute, is struck by a Critical Hit, its Attack is boosted by six stages.');
INSERT INTO "abilities" VALUES(6,'Anticipation',0,0,'A warning is displayed if an opposing Pokemon has the moves Selfdestruct, Explosion, Fissure, Guillotine, Horn Drill, Sheer Cold or any move from a type that is considered Super Effective against this Pokemon. Hidden Power, Judgment, Natural Gift and Weather Ball are viewed as Normal-type moves; Counter, Mirror Coat and Metal Burst do not receive warnings.');
INSERT INTO "abilities" VALUES(7,'Arena Trap',0,0,'When this Pokemon enters the field, its opponents cannot switch or flee the battle unless they are part Flying-type, have the Levitate ability, are holding Shed Shell, or they use the moves Baton Pass or U-Turn. Flying-type and Levitate Pokemon cannot escape if they are holding Iron Ball or Gravity is in effect. Levitate Pokemon also cannot escape if their ability is disabled through other means, such as Skill Swap or Gastro Acid. [Field Effect] If this Pokemon is in the lead spot, the rate of wild Pokemon battles is doubled.');
INSERT INTO "abilities" VALUES(8,'Aroma Veil',0,0,'Protects allied Pokemon from the effects of Taunt and Torment.');
INSERT INTO "abilities" VALUES(9,'Aura Break',0,0,'Reverses the effect of Fairy Aura and Dark Aura.');
INSERT INTO "abilities" VALUES(10,'Bad Dreams',0,0,'If asleep, each of this Pokemon''s opponents receives damage equal to one-eighth of its max HP.');
INSERT INTO "abilities" VALUES(11,'Battle Armor',0,0,'Critical Hits cannot strike this Pokemon.');
INSERT INTO "abilities" VALUES(12,'Big Pecks',0,0,'This Pokemon''s Defense cannot be lowered.');
INSERT INTO "abilities" VALUES(13,'Blaze',0,0,'When its health reaches one-third or less of its max HP, this Pokemon''s Fire-type attacks receive a 50% boost in power.');
INSERT INTO "abilities" VALUES(14,'Bulletproof',0,0,'This Pokemon is immune to the effects of the bomb and ball-related moves Acid Spray, Aura Sphere, Barrage, Bullet Seed, Egg Bomb, Electro Ball, Energy Ball, Explosion, Focus Blast, Gyro Ball, Ice Ball, Magnet Bomb, Mist Ball, Mud Bomb, Octazooka, Rollout, Seed Bomb, Selfdestruct, Shadow Ball, Sludge Bomb and Weather Ball.');
INSERT INTO "abilities" VALUES(15,'Cheek Pouch',0,0,'When this Pokemon''s berry activates, it also restores HP.');
INSERT INTO "abilities" VALUES(16,'Chlorophyll',0,0,'If this Pokemon is active while Sunny Day is in effect, its speed is temporarily doubled.');
INSERT INTO "abilities" VALUES(17,'Clear Body',0,0,'Opponents cannot reduce this Pokemon''s stats; they can, however, modify stat changes with Power Swap, Guard Swap and Heart Swap and inflict stat boosts with Swagger and Flatter. This ability does not prevent self-inflicted stat reductions.');
INSERT INTO "abilities" VALUES(18,'Cloud Nine',0,0,'While this Pokemon is active, all weather conditions and their effects are disabled.');
INSERT INTO "abilities" VALUES(19,'Color Change',0,0,'This Pokemon''s type changes according to the type of the last move that hit this Pokemon.');
INSERT INTO "abilities" VALUES(20,'Competitive',0,0,'When this Pokemon is hit with a stat-lowering move, its Special Attack increases by one level.');
INSERT INTO "abilities" VALUES(21,'Compound Eyes',0,0,'The accuracy of this Pokemon''s moves receives a 30% increase; for example, a 75% accurate move becomes 97.5% accurate. [Field Effect] If this Pokemon is in the lead spot, the rate of wild Pokemon holding an item increases.');
INSERT INTO "abilities" VALUES(22,'Contrary',0,0,'This Pokemon''s stat modifiers, including self-inflicted ones, are reversed; i.e., negative modifiers become positive while positive ones become negative.');
INSERT INTO "abilities" VALUES(23,'Cursed Body',0,0,'When this Pokemon is attacked, there is a 30% chance that the opponent''s attack will be disabled.');
INSERT INTO "abilities" VALUES(24,'Cute Charm',0,0,'If an opponent of the opposite gender directly attacks this Pokemon, there is a 30% chance that the opponent will become Attracted to this Pokemon. [Field Effect] If this Pokemon is in the lead spot, the rate of wild Pokemon of the opposite gender appearing increases to 66.7%.');
INSERT INTO "abilities" VALUES(25,'Damp',0,0,'While this Pokemon is active, no Pokemon on the field can use Selfdestruct or Explosion.');
INSERT INTO "abilities" VALUES(26,'Dark Aura',0,0,'This Pokemon receives a power boost for Dark-type moves.');
INSERT INTO "abilities" VALUES(27,'Defeatist',0,0,'When this Pokemon''s HP falls below 50%, its Attack and Special Attack stats are halved.');
INSERT INTO "abilities" VALUES(28,'Defiant',0,0,'This Pokemon''s Attack rises by 2 stages when an opponent lowers any of this Pokemon''s stats.');
INSERT INTO "abilities" VALUES(29,'Delta Stream',0,0,'When this Pokemon enters the battle field, it causes strong winds which cancels all effects of the moves Sunny Day, Rain Dance, Sandstorm, and Hail, and the abilities Drought, Drizzle, Sand Stream and Snow Warning. Additionally, these moves will fail when used and these abilities will fail to activate. Delta Stream causes attacks that deal super-effective damage to Flying-type Pokemon to deal only normal damage to any Flying-type Pokemon in the battle.');
INSERT INTO "abilities" VALUES(30,'Desolate Land',0,0,'When this Pokemon enters the battle field, it causes harsh sunlight which cancels all effects of the moves Sunny Day, Rain Dance, Sandstorm, and Hail, and the abilities Drought, Drizzle, Sand Stream and Snow Warning. Additionally, these moves will fail when used and these abilities will fail to activate. Desolate Land provides all the regular effects that using Sunny Day/Drought provides, with the extra effect that all Water-type moves will now fail. Harsh sunlight will remain active as long as this Pokemon is active.');
INSERT INTO "abilities" VALUES(31,'Download',0,0,'If this Pokemon switches into an opponent with equal Defenses or higher Defense than Special Defense, this Pokemon''s Special Attack receives a 50% boost. If this Pokemon switches into an opponent with higher Special Defense than Defense, this Pokemon''s Attack receive a 50% boost.');
INSERT INTO "abilities" VALUES(32,'Drizzle',0,0,'When this Pokemon enters the battlefield, it causes an automatic Rain Dance that lasts for five turns, or eight turns if the Pokemon is holding a Damp Rock.');
INSERT INTO "abilities" VALUES(33,'Drought',0,0,'When this Pokemon enters the battlefield, it causes an automatic Sunny Day that lasts for five turns, or eight turns if the Pokemon is holding a Heat Rock.');
INSERT INTO "abilities" VALUES(34,'Dry Skin',0,0,'This Pokemon absorbs Water attacks up to 25% of its maximum HP but receives an extra 25% damage from Fire attacks. If Sunny Day is in effect, this Pokemon takes damage equal to 12.5% of its maximum HP. If Rain Dance is in effect, this Pokemon recovers health equal to 12.5% of its maximum HP.');
INSERT INTO "abilities" VALUES(35,'Early Bird',0,0,'This Pokemon will remain asleep for half as long as it normally would; this includes both opponent-induced sleep and user-induced sleep via Rest.');
INSERT INTO "abilities" VALUES(36,'Effect Spore',0,0,'If an opponent directly attacks this Pokemon, there is a 30% chance that the opponent will become either poisoned, paralyzed or put to sleep. There is an equal chance to inflict each status.');
INSERT INTO "abilities" VALUES(37,'Fairy Aura',0,0,'This Pokemon receives a power boost for Fairy-type moves.');
INSERT INTO "abilities" VALUES(38,'Filter',0,0,'This Pokemon receives one-fourth reduced damage from Super Effective attacks.');
INSERT INTO "abilities" VALUES(39,'Flame Body',0,0,'If an opponent directly attacks this Pokemon, there is a 30% chance that the opponent will become burned. [Field Effect] Pokemon Eggs hatch in half the time.');
INSERT INTO "abilities" VALUES(40,'Flare Boost',0,0,'This Pokemon''s Special Attack increases by 50% when it is burned.');
INSERT INTO "abilities" VALUES(41,'Flash Fire',0,0,'This Pokemon is immune to all Fire-type attacks; additionally, its own Fire-type attacks receive a 50% boost if a Fire-type move hits this Pokemon. Multiple boosts do not occur if this Pokemon is hit with multiple Fire-type attacks.');
INSERT INTO "abilities" VALUES(42,'Flower Gift',0,0,'If this Pokemon is active while Sunny Day is in effect, its Attack and Special Defense stats (as well as its partner''s stats in double battles) receive a 50% boost.');
INSERT INTO "abilities" VALUES(43,'Flower Veil',0,0,'Prevents allied Grass Pokemon from having their stats lowered.');
INSERT INTO "abilities" VALUES(44,'Forecast',0,0,'This Pokemon''s type changes according to the current weather conditions: it becomes Fire-type during Sunny Day, Water-type during Rain Dance, Ice-type during Hail and remains its regular type otherwise.');
INSERT INTO "abilities" VALUES(45,'Forewarn',0,0,'The move with the highest Base Power in the opponent''s moveset is revealed.');
INSERT INTO "abilities" VALUES(46,'Friend Guard',0,0,'This Pokemon receives 25% reduced damage from allies in Double or Triple Battles.');
INSERT INTO "abilities" VALUES(47,'Frisk',0,0,'When this Pokemon enters the field, it identifies the opponent''s held item; in double battles, the held item of an unrevealed, randomly selected opponent is identified.');
INSERT INTO "abilities" VALUES(48,'Fur Coat',0,0,'This Pokemon receives half damage from physical attacks.');
INSERT INTO "abilities" VALUES(49,'Gale Wings',0,0,'This Pokemon''s Flying-type moves receive +1 priority.');
INSERT INTO "abilities" VALUES(50,'Gluttony',0,0,'This Pokemon consumes its held berry when its health reaches 50% max HP or lower.');
INSERT INTO "abilities" VALUES(51,'Gooey',0,0,'If an opponent directly attacks this Pokemon, the opponent''s speed is lowered.');
INSERT INTO "abilities" VALUES(52,'Grass Pelt',0,0,'This Pokemon receives a defence boost when the terrain is grassy.');
INSERT INTO "abilities" VALUES(53,'Guts',0,0,'When this Pokemon is poisoned (including Toxic), burned,0,0, paralyzed or asleep (including self-induced Rest), its Attack stat receives a 50% boost; the burn status'' Attack drop is also ignored.');
INSERT INTO "abilities" VALUES(54,0,0,'Harvest','If this Pokemon consumes its held Berry, there is a chance that it will be restored at the end of the turn. During Sunny Day, the Berry always will be restored.');
INSERT INTO "abilities" VALUES(55,'Healer',0,0,'The user has a 30% chance to heal its allies of status conditions in Double or Triple Battles.');
INSERT INTO "abilities" VALUES(56,'Heatproof',0,0,'This Pokemon receives half damage from both Fire-type attacks and residual burn damage.');
INSERT INTO "abilities" VALUES(57,'Heavy Metal',0,0,'This Pokemon''s weight is doubled.');
INSERT INTO "abilities" VALUES(58,'Honey Gather',0,0,'If it is not already holding an item, this Pokemon may find and be holding Honey after a battle.');
INSERT INTO "abilities" VALUES(59,'Huge Power',0,0,'This Pokemon''s Attack stat is doubled. Therefore, if this Pokemon''s Attack stat on the status screen is 200, it effectively has an Attack stat of 400; which is then subject to the full range of stat boosts and reductions.');
INSERT INTO "abilities" VALUES(60,'Hustle',0,0,'This Pokemon''s Attack receives a 50% boost but its Physical attacks receive a 20% drop in Accuracy. For example, a 100% accurate move would become an 80% accurate move. The accuracy of moves that never miss, such as Aerial Ace, remains unaffected. [Field Effect] If this Pokemon is in the lead spot, the rate of wild Pokemon battles decreases.');
INSERT INTO "abilities" VALUES(61,'Hydration',0,0,'If this Pokemon is active while Rain Dance is in effect, it recovers from poison, paralysis, burn, sleep and freeze at the end of the turn.');
INSERT INTO "abilities" VALUES(62,'Hyper Cutter',0,0,'Opponents cannot reduce this Pokemon''s Attack stat; they can, however, modify stat changes with Power Swap or Heart Swap and inflict a stat boost with Swagger. This ability does not prevent self-inflicted stat reductions.');
INSERT INTO "abilities" VALUES(63,'Ice Body',0,0,'If active while Hail is in effect, this Pokemon recovers one-sixteenth of its max HP after each turn. If a non-Ice-type Pokemon receives this ability through Skill Swap, Role Play or the Trace ability, it will not take damage from Hail.');
INSERT INTO "abilities" VALUES(64,'Illuminate',0,0,'When this Pokemon is in the first slot of the player''s party, it doubles the rate of wild encounters.');
INSERT INTO "abilities" VALUES(65,'Illusion',0,0,'This Pokemon takes on the appearance, name, gender and Pokeball of the last Pokemon in the player''s party.');
INSERT INTO "abilities" VALUES(66,'Immunity',0,0,'This Pokemon cannot become poisoned nor Toxic poisoned.');
INSERT INTO "abilities" VALUES(67,'Imposter',0,0,'This Pokemon Transforms into the opponent upon entering the field.');
INSERT INTO "abilities" VALUES(68,'Infiltrator',0,0,'This Pokemon ignore certain effects of the opponent''s, such as Safeguard, Reflect and Light Screen.');
INSERT INTO "abilities" VALUES(69,'Inner Focus',0,0,'This Pokemon cannot be made to flinch.');
INSERT INTO "abilities" VALUES(70,'Insomnia',0,0,'This Pokemon cannot be put to sleep; this includes both opponent-induced sleep as well as user-induced sleep via Rest.');
INSERT INTO "abilities" VALUES(71,'Intimidate',0,0,'When this Pokemon enters the field, the Attack stat of each of its opponents lowers by one stage. [Field Effect] If this Pokemon is in the lead spot, the rate of wild Pokemon battles, whose level is at least 5 levels less than that of this Pokemon, halves.');
INSERT INTO "abilities" VALUES(72,'Iron Barbs',0,0,'When this Pokemon is directly attacked, the opponent receives damage equal to 1/8 of its max HP.');
INSERT INTO "abilities" VALUES(73,'Iron Fist',0,0,'This Pokemon receives a 20% power boost for the following attacks: Bullet Punch, Comet Punch, Dizzy Punch, Drain Punch, Dynamicpunch, Fire Punch, Focus Punch, Hammer Arm, Ice Punch, Mach Punch, Mega Punch, Meteor Mash, Shadow Punch, Sky Uppercut, and Thunderpunch. Sucker Punch, which is known Ambush in Japan, is not boosted.');
INSERT INTO "abilities" VALUES(74,'Justified',0,0,'When Dark-type moves hit this Pokemon, its Attack increases by one level.');
INSERT INTO "abilities" VALUES(75,'Keen Eye',0,0,'This Pokemon''s Accuracy cannot be lowered. [Field Effect] If this Pokemon is in the lead spot, the rate of low-leveled wild Pokemon battles decreases.');
INSERT INTO "abilities" VALUES(76,'Klutz',0,0,'This Pokemon ignores both the positive and negative effects of its held item, other than the speed-halving effects of Iron Ball and the seven EV training items.');
INSERT INTO "abilities" VALUES(77,'Leaf Guard',0,0,'If this Pokemon is active while Sunny Day is in effect, it cannot become poisoned, burned, paralyzed or put to sleep (other than user-induced Rest). Leaf Guard does not heal status effects that existed before Sunny Day came into effect.');
INSERT INTO "abilities" VALUES(78,'Levitate',0,0,'This Pokemon is immune to Ground-type attacks, Spikes, Toxic Spikes and the Arena Trap ability; it loses these immunities while holding Iron Ball, after using Ingrain or if Gravity is in effect.');
INSERT INTO "abilities" VALUES(79,'Light Metal',0,0,'This Pokemon''s weight is halved.');
INSERT INTO "abilities" VALUES(80,'Lightning Rod',0,0,'During double and triple battles, this Pokemon draws any single-target Electric-type attack to itself, including Electric-type Hidden Power, Judgment and Natural Gift; when hit by a Electric-type move, this Pokemon does not take damage and its Special Attack increases by one level. If an opponent uses an Electric-type attack that affects multiple Pokemon, those targets will be hit.');
INSERT INTO "abilities" VALUES(81,'Limber',0,0,'This Pokemon cannot become paralyzed.');
INSERT INTO "abilities" VALUES(82,'Liquid Ooze',0,0,'When another Pokemon uses Absorb, Drain Punch, Dream Eater, Giga Drain, Leech Life, Leech Seed or Mega Drain against this Pokemon, the attacking Pokemon loses the amount of health that it would have gained.');
INSERT INTO "abilities" VALUES(83,'Magic Bounce',0,0,'Reflects non-damaging moves back at the originator. The affected moves are the same ones susceptible to Magic Coat.');
INSERT INTO "abilities" VALUES(84,'Magic Guard',0,0,'Prevents all damage except from direct attacks.');
INSERT INTO "abilities" VALUES(85,'Magician',0,0,'This Pokemon steals the held item of a Pokemon it hits with a move, as long as it is not currently holding an item.');
INSERT INTO "abilities" VALUES(86,'Magma Armor',0,0,'This Pokemon cannot become frozen. [Field Effect] Pokemon Eggs hatch in half the time');
INSERT INTO "abilities" VALUES(87,'Magnet Pull',0,0,'When this Pokemon enters the field, Steel-type opponents cannot switch out nor flee the battle unless they are holding Shed Shell or use the attacks U-Turn or Baton Pass. [Field Effect] If this Pokemon is in the lead spot, the rate of encountering a Steel-type Pokemon increases by 50%.');
INSERT INTO "abilities" VALUES(88,'Marvel Scale',0,0,'When this Pokemon becomes burned, poisoned (including Toxic), paralyzed,0,0, frozen or put to sleep (including self-induced sleep via Rest), its Defense receives a 50% boost.');
INSERT INTO "abilities" VALUES(89,0,0,'Mega Launcher','This Pokemon receives a power boost for the following aura and pulse-related moves Aura Sphere, Dark Pulse, Dragon Pulse, Heal Pulse and Water Pulse.');
INSERT INTO "abilities" VALUES(90,'Minus',0,0,'This Pokemon''s Special Attack receives a 50% boost in double battles if its partner has the Plus ability.');
INSERT INTO "abilities" VALUES(91,'Mold Breaker',0,0,'When this Pokemon becomes active, it nullifies the abilities of opposing active Pokemon that hinder this Pokemon''s attacks. These abilities include Battle Armor, Clear Body, Damp, Dry Skin, Filter, Flash Fire, Flower Gift, Heatproof, Hyper Cutter, Immunity, Inner Focus, Insomnia, Keen Eye, Leaf Guard, Levitate, Lightningrod, Limber, Magma Armor, Marvel Scale, Motor Drive, Oblivious, Own Tempo, Sand Veil, Shell Armor, Shield Dust, Simple, Snow Cloak, Solid Rock, Soundproof, Sticky Hold, Storm Drain, Sturdy, Suction Cups, Tangled Feet, Thick Fat, Unaware, Vital Spirit, Volt Absorb, Water Absorb, Water Veil, White Smoke and Wonder Guard.');
INSERT INTO "abilities" VALUES(92,'Moody',0,0,'After every turn, one of this Pokemon''s stats is increased by two stages while another stat is decreased by one stage. These stats include Accuracy and Evasion.');
INSERT INTO "abilities" VALUES(93,'Motor Drive',0,0,'This Pokemon is immune to all Electric-type attacks, including Thunder Wave, and if an Electric-type attack hits this Pokemon, it receives a one-level Speed boost.');
INSERT INTO "abilities" VALUES(94,'Moxie',0,0,'When this Pokemon knocks out another Pokemon, its Attack is raised by one level.');
INSERT INTO "abilities" VALUES(95,'Multiscale',0,0,'When this Pokemon is at full health, it takes halved damage from attacks.');
INSERT INTO "abilities" VALUES(96,'Multitype',0,0,'This Pokemon changes its type to match its corresponding held Plate; this ability only works for Arceus, prevents the removal of Arceus'' held item and cannot be Skill Swapped, Role Played or Traced.');
INSERT INTO "abilities" VALUES(97,'Mummy',0,0,'When this Pokemon is hit with a contact move, or if this Pokemon hits another Pokemon with a contact move, the other Pokemon''s ability becomes Mummy, unless it has Multitype or Wonder Guard.');
INSERT INTO "abilities" VALUES(98,'Natural Cure',0,0,'When this Pokemon switches out of battle, it is cured of poison (including Toxic), paralysis,0,0, burn, freeze and sleep (including self-induced Rest).');
INSERT INTO "abilities" VALUES(99,'No Guard',0,0,'Both this Pokemon and the opponent will have 100% accuracy for any attack. [Field Effect] If Pokemon is in the lead slot, wild encounters will increase.');
INSERT INTO "abilities" VALUES(100,'Normalize',0,0,'Makes all of this Pokemon''s attacks Normal-typed.');
INSERT INTO "abilities" VALUES(101,'Oblivious',0,0,'This Pokemon cannot become attracted to another Pokemon.');
INSERT INTO "abilities" VALUES(102,'Overcoat',0,0,'This Pokemon is immune to the residual damage of Hail and Sandstorm.');
INSERT INTO "abilities" VALUES(103,'Overgrow',0,0,'When its health reaches one-third or less of its max HP, this Pokemon''s Grass-type attacks receive a 50% boost in power.');
INSERT INTO "abilities" VALUES(104,'Own Tempo',0,0,'This Pokemon cannot become confused.');
INSERT INTO "abilities" VALUES(105,'Parental Bond',0,0,'This Pokemon attacks twice in one turn.');
INSERT INTO "abilities" VALUES(106,'Pickpocket',0,0,'When another Pokemon makes contact with this Pokemon, its hold item is stolen unless this Pokemon is already holding an item.');
INSERT INTO "abilities" VALUES(107,'Pickup',0,0,'If it is not already holding an item, this Pokemon may find and be holding a variety of items after a battle: see the full Pickup List for more information.');
INSERT INTO "abilities" VALUES(108,'Pixilate',0,0,'All of this Pokemon''s Normal-typed moves are now Fairy-type.');
INSERT INTO "abilities" VALUES(109,'Plus',0,0,'This Pokemon''s Special Attack receives a 50% boost in double battles if its partner has the Minus ability.');
INSERT INTO "abilities" VALUES(110,'Poison Heal',0,0,'If this Pokemon become poisoned or Toxic Poisoned, it will recover one-eighth of its max HP after each turn. However, this Pokemon will continue to lose health as the player walks on the overworld screen.');
INSERT INTO "abilities" VALUES(111,'Poison Point',0,0,'If an opponent directly attacks this Pokemon, there is a 30% chance that the opponent will become poisoned.');
INSERT INTO "abilities" VALUES(112,'Poison Touch',0,0,'This Pokemon has a 20% chance to inflict Poison if it makes contact with its target.');
INSERT INTO "abilities" VALUES(113,'Prankster',0,0,'The non-damaging moves of this Pokemon have one priority stage higher than usual.');
INSERT INTO "abilities" VALUES(114,'Pressure',0,0,'When an opponent uses a move that affects this Pokemon, an additional PP is required for the opponent to use that move. [Field Effect] If this Pokemon is in the lead spot, the rate of wild Pokemon battles is halved.');
INSERT INTO "abilities" VALUES(115,'Primordial Sea',0,0,'When this Pokemon enters the battle field, it causes heavy rain which cancels all effects of the moves Sunny Day, Rain Dance, Sandstorm, and Hail, and the abilities Drought, Drizzle, Sand Stream and Snow Warning. Additionally, these moves will fail when used and these abilities will fail to activate. Primordial Sea provides all the regular effects that using Rain Dance/Drizzle provides, with the extra effect that all Fire-type moves will now fail. Heavy rain will remain active as long as this Pokemon is active.');
INSERT INTO "abilities" VALUES(116,'Protean',0,0,'This Pokemon changes type to the type of whatever move it just used.');
INSERT INTO "abilities" VALUES(117,'Pure Power',0,0,'This Pokemon''s Attack stat is doubled. Therefore, if this Pokemon''s Attack stat on the status screen is 200, it effectively has an Attack stat of 400; which is then subject to the full range of stat boosts and reductions.');
INSERT INTO "abilities" VALUES(118,'Quick Feet',0,0,'When this Pokemon is poisoned (including Toxic), burned,0,0, paralyzed, asleep (including self-induced Rest) or frozen, its Speed stat receives a 50% boost; the paralysis status'' Speed drop is also ignored. [Field Effect] If Pokemon is in lead slot,0,0, wild encounters decrease.');
INSERT INTO "abilities" VALUES(119,'Rain Dish',0,0,'If active while Rain Dance is in effect, this Pokemon recovers one-sixteenth of its max HP after each turn.');
INSERT INTO "abilities" VALUES(120,'Rattled',0,0,'When Bug-, Dark- or Ghost-type attacks hit this Pokemon, its Speed increases by one level.');
INSERT INTO "abilities" VALUES(121,'Reckless',0,0,'When this Pokemon uses an attack that causes recoil damage, or an attack that has a chance to cause recoil damage such as Jump Kick and Hi Jump Kick, the attacks''s power receives a 20% boost.');
INSERT INTO "abilities" VALUES(122,'Refrigerate',0,0,'All of this Pokemon''s Normal-typed moves are now Ice-type.');
INSERT INTO "abilities" VALUES(123,'Regenerator',0,0,'When this Pokemon switches out, it recovers 1/3 of its max HP.');
INSERT INTO "abilities" VALUES(124,'Rivalry',0,0,'Increases base power of Physical and Special attacks by 25% if the opponent is the same gender, but decreases base power by 25% if opponent is the opposite gender.');
INSERT INTO "abilities" VALUES(125,'Rock Head',0,0,'This Pokemon does not receive recoil damage unless it uses Struggle, it misses with Jump Kick or Hi Jump Kick or it is holding Life Orb, Jaboca Berry or Rowap Berry.');
INSERT INTO "abilities" VALUES(126,'Rough Skin',0,0,'When this Pokemon is directly attacked, the opponent receives damage equal to 1/8 of its max HP.');
INSERT INTO "abilities" VALUES(127,'Run Away',0,0,'Unless this Pokemon is under the effects of a trapping move or ability, such as Mean Look or Shadow Tag, it will escape from wild Pokemon battles without fail.');
INSERT INTO "abilities" VALUES(128,'Sand Force',0,0,'The power of this Pokemon''s Rock-, Ground- and Steel-type moves increases by 30% during Sandstorm. This Pokemon is also immune to the residual damage of Sandstorm.');
INSERT INTO "abilities" VALUES(129,'Sand Rush',0,0,'This Pokemon''s Speed doubles during Sandstorm. This Pokemon is also immune to the residual damage of Sandstorm.');
INSERT INTO "abilities" VALUES(130,'Sand Stream',0,0,'When this Pokemon enters the battlefield, it causes an automatic Sandstorm that lasts for five turns, or eight turns if the Pokemon is holding a Smooth Rock.');
INSERT INTO "abilities" VALUES(131,'Sand Veil',0,0,'If active while Sandstorm is in effect, this Pokemon''s Evasion receives a 20% boost; if this Pokemon has a typing that would normally take damage from Sandstorm, this Pokemon is also immune to Sandstorm''s damage. [Field Effect] If this Pokemon is in the lead spot, the rate of wild Pokemon battles during a Sandstorm is halved.');
INSERT INTO "abilities" VALUES(132,'Sap Sipper',0,0,'This Pokemon takes no damage from Grass-type moves, and, if hit by one, its Attack is raised by one level.');
INSERT INTO "abilities" VALUES(133,'Scrappy',0,0,'This Pokemon has the ability to hit Ghost-type Pokemon with Normal-type and Fighting-type moves. Effectiveness of these moves takes into account the Ghost-type Pokemon''s other weaknesses and resistances.');
INSERT INTO "abilities" VALUES(134,'Serene Grace',0,0,'The side effects of this Pokemon''s attack occur twice as often. For example, if this Pokemon uses Ice Beam, it will have a 20% chance to freeze its target.');
INSERT INTO "abilities" VALUES(135,'Shadow Tag',0,0,'When this Pokemon enters the field, its opponents cannot switch or flee the battle unless they have the same ability, are holding Shed Shell, or they use the moves Baton Pass or U-Turn.');
INSERT INTO "abilities" VALUES(136,'Shed Skin',0,0,'After each turn, this Pokemon has a 33% chance to heal itself from poison (including Toxic), paralysis,0,0, burn, freeze or sleep (including self-induced Rest).');
INSERT INTO "abilities" VALUES(137,'Sheer Force',0,0,'When this Pokemon uses an attack that has secondary effects, the move receives a 30% Power boost but it cannot trigger the secondary effect.');
INSERT INTO "abilities" VALUES(138,'Shell Armor',0,0,'Critical Hits cannot strike this Pokemon.');
INSERT INTO "abilities" VALUES(139,'Shield Dust',0,0,'If the opponent uses a move that has secondary effects that affect this Pokemon in addition to damage, the move''s secondary effects will not trigger. (For example, an Ice Beam will lose its 10% chance to freeze this Pokemon.)');
INSERT INTO "abilities" VALUES(140,0,0,'Simple','This Pokemon doubles all of its positive and negative stat modifiers. For example, if this Pokemon uses Curse, its Attack and Defense stats each receive a two-level increase while its Speed stat receives a two-level decrease.');
INSERT INTO "abilities" VALUES(141,'Skill Link',0,0,'When this Pokemon uses an attack that strikes multiple times in one turn, such as Fury Attack or Spike Cannon, such attacks will always strike for the maximum number of hits.');
INSERT INTO "abilities" VALUES(142,'Slow Start',0,0,'After this Pokemon switches into the battle, its Attack and Speed stats are halved for five turns; for example, if this Pokemon has an Attack stat of 400, it will effectively have an Attack stat of 200 until the effects of Slow Start wear off.');
INSERT INTO "abilities" VALUES(143,'Sniper',0,0,'When this Pokemon lands a Critical Hit, the base power of its attack tripled rather than doubled.');
INSERT INTO "abilities" VALUES(144,'Snow Cloak',0,0,'If active while Hail is in effect, this Pokemon''s Evasion receives a 20% boost; if this Pokemon has a typing that would normally take damage from Hail, this Pokemon is also immune to Hail''s damage.');
INSERT INTO "abilities" VALUES(145,'Snow Warning',0,0,'When this Pokemon enters the battlefield, it causes an automatic Hail that lasts for five turns, or eight turns if the Pokemon is holding an Icy Rock.');
INSERT INTO "abilities" VALUES(146,'Solar Power',0,0,'If this Pokemon is active while Sunny Day is in effect, its Special Attack temporarily receives a 50% boost but this Pokemon also receives damage equal to one-eighth of its max HP after each turn.');
INSERT INTO "abilities" VALUES(147,'Solid Rock',0,0,'This Pokemon receives one-fourth reduced damage from Super Effective attacks.');
INSERT INTO "abilities" VALUES(148,'Soundproof',0,0,'This Pokemon is immune to the effects of the sound-related moves Boomburst, Bug Buzz, Chatter, Disarming Voice, Grasswhistle, Growl, Heal Bell, Hyper Voice, Metal Sound, Noble Roar, Perish Song, Roar, Roar of Time, Sing, Sonicboom, Supersonic, Screech, Snore and Uproar.');
INSERT INTO "abilities" VALUES(149,'Speed Boost',0,0,'While this Pokemon is active, its Speed increased by one stage at the end of every turn; the six stage maximum for stat boosts is still in effect.');
INSERT INTO "abilities" VALUES(150,'Stall',0,0,'When all active Pokemon use moves that have the same priority value, this Pokemon always attacks last. See the priority page for more information.');
INSERT INTO "abilities" VALUES(151,'Stance Change',0,0,'This Pokemon changes forme based on whether it uses offensive or defensive moves.');
INSERT INTO "abilities" VALUES(152,'Static',0,0,'If an opponent directly attacks this Pokemon, there is a 30% chance that the opponent will become paralyzed. [Field Effect] If this Pokemon is in the lead spot, the rate of encountering an Electric-type Pokemon increases by 50%.');
INSERT INTO "abilities" VALUES(153,'Steadfast',0,0,'If this Pokemon is made to flinch, its Speed receives a one-level boost.');
INSERT INTO "abilities" VALUES(154,'Stench',0,0,'When this Pokemon is in the first slot of the player''s party, it halves the rate of wild encounters. Additionally, this Pokemon''s damaging moves have a 10% chance to make the target flinch.');
INSERT INTO "abilities" VALUES(155,'Sticky Hold',0,0,'Opponents cannot remove items from this Pokemon. [Field Effect] Pokemon hooked by a fishing rod are easier to catch.');
INSERT INTO "abilities" VALUES(156,'Storm Drain',0,0,'During double and triple battles, this Pokemon draws any single-target Water-type attack to itself, including Water-type Hidden Power, Judgment and Natural Gift; when hit by a Water-type move, this Pokemon does not take damage and its Special Attack increases by one level. If an opponent uses an Water-type attack that affects multiple Pokemon, those targets will be hit.');
INSERT INTO "abilities" VALUES(157,'Strong Jaw',0,0,'This Pokemon receives a power boost for the following bite-related moves Bite, Bug Bite, Crunch, Fire Fang, Hyper Fang, Ice Fang, Poison Fang and Thunder Fang.');
INSERT INTO "abilities" VALUES(158,'Sturdy',0,0,'The one-hit KO moves Fissure, Guillotine, Horn Drill and Sheer Cold do not affect this Pokemon. Also, if the Pokemon is at full health and an attack normally would knock that Pokemon out, it retains 1 HP and does not faint.');
INSERT INTO "abilities" VALUES(159,'Suction Cups',0,0,'Roar and Whirlwind do not affect this Pokemon. [Field Effect] Pokemon hooked by a fishing rod are easier to catch.');
INSERT INTO "abilities" VALUES(160,'Super Luck',0,0,'Raises the chance of this Pokemon scoring a Critical Hit.');
INSERT INTO "abilities" VALUES(161,'Swarm',0,0,'When its health reaches one-third or less of its max HP, this Pokemon''s Bug-type attacks receive a 50% boost in power. [Field Effect] Pokemon cries are heard more often.');
INSERT INTO "abilities" VALUES(162,'Sweet Veil',0,0,'Prevents allied Pokemon from being put to sleep.');
INSERT INTO "abilities" VALUES(163,'Swift Swim',0,0,'If this Pokemon is active while Rain Dance is in effect, its speed is temporarily doubled.');
INSERT INTO "abilities" VALUES(164,'Symbiosis',0,0,'This Pokemon can pass an item to an ally.');
INSERT INTO "abilities" VALUES(165,'Synchronize',0,0,'If an opponent burns, poisons or paralyzes this Pokemon, the same condition inflicts the opponent. [Field Effect] If this Pokemon is in the lead spot, the rate of encountering a Pokemon with the same nature increases by 50%');
INSERT INTO "abilities" VALUES(166,'Tangled Feet',0,0,'When this Pokemon is confused, its opponent''s attacks have a 50% chance of missing.');
INSERT INTO "abilities" VALUES(167,'Technician',0,0,'When this Pokemon uses an attack that has 60 Base Power or less, the move''s Base Power receives a 50% boost. For example, a move with 60 Base Power effectively becomes a move with 90 Base Power.');
INSERT INTO "abilities" VALUES(168,'Telepathy',0,0,'This Pokemon avoids damage from its teammates in Double or Triple Battles.');
INSERT INTO "abilities" VALUES(169,'Teravolt',0,0,'When this Pokemon becomes active, it nullifies the abilities of opposing active Pokemon that hinder this Pokemon''s attacks. These abilities include Battle Armor, Clear Body, Damp, Dry Skin, Filter, Flash Fire, Flower Gift, Heatproof, Hyper Cutter, Immunity, Inner Focus, Insomnia, Keen Eye, Leaf Guard, Levitate, Lightningrod, Limber, Magma Armor, Marvel Scale, Motor Drive, Oblivious, Own Tempo, Sand Veil, Shell Armor, Shield Dust, Simple, Snow Cloak, Solid Rock, Soundproof, Sticky Hold, Storm Drain, Sturdy, Suction Cups, Tangled Feet, Thick Fat, Unaware, Vital Spirit, Volt Absorb, Water Absorb, Water Veil, White Smoke and Wonder Guard.');
INSERT INTO "abilities" VALUES(170,'Thick Fat',0,0,'This Pokemon receives halved damage from Ice-type and Fire-type attacks.');
INSERT INTO "abilities" VALUES(171,'Tinted Lens',0,0,'Doubles the power of moves that are Not Very Effective against opponents.');
INSERT INTO "abilities" VALUES(172,'Torrent',0,0,'When its health reaches one-third or less of its max HP, this Pokemon''s Water-type attacks receive a 50% boost in power.');
INSERT INTO "abilities" VALUES(173,'Tough Claws',0,0,'This Pokemon receives a power boost for direct contact moves.');
INSERT INTO "abilities" VALUES(174,'Toxic Boost',0,0,'This Pokemon''s Attack increases by 50% when it is poisoned.');
INSERT INTO "abilities" VALUES(175,'Trace',0,0,'When this Pokemon enters the field, it temporarily copies an opponent''s ability (except Multitype). This ability remains with this Pokemon until it leaves the field.');
INSERT INTO "abilities" VALUES(176,'Truant',0,0,'After this Pokemon is switched into battle, it skips every other turn.');
INSERT INTO "abilities" VALUES(177,'Turboblaze',0,0,'When this Pokemon becomes active, it nullifies the abilities of opposing active Pokemon that hinder this Pokemon''s attacks. These abilities include Battle Armor, Clear Body, Damp, Dry Skin, Filter, Flash Fire, Flower Gift, Heatproof, Hyper Cutter, Immunity, Inner Focus, Insomnia, Keen Eye, Leaf Guard, Levitate, Lightningrod, Limber, Magma Armor, Marvel Scale, Motor Drive, Oblivious, Own Tempo, Sand Veil, Shell Armor, Shield Dust, Simple, Snow Cloak, Solid Rock, Soundproof, Sticky Hold, Storm Drain, Sturdy, Suction Cups, Tangled Feet, Thick Fat, Unaware, Vital Spirit, Volt Absorb, Water Absorb, Water Veil, White Smoke and Wonder Guard.');
INSERT INTO "abilities" VALUES(178,'Unaware',0,0,'This Pokemon ignores an opponent''s stat boosts for Attack, Defense, Special Attack and Special Defense. These boosts will still be calculated if this Pokemon uses Punishment.');
INSERT INTO "abilities" VALUES(179,'Unburden',0,0,'Increases Speed by one level if this Pokemon loses its held item through usage (i.e. Berries) or via Thief, Knock Off,0,0, etc.');
INSERT INTO "abilities" VALUES(180,'Unnerve',0,0,'The opponent cannot consume its Berry.');
INSERT INTO "abilities" VALUES(181,'Victory Star',0,0,'The Accuracy of this Pokemon and its partners in Double and Triple Battles receives a 10% boost.');
INSERT INTO "abilities" VALUES(182,'Vital Spirit',0,0,'This Pokemon cannot be put to sleep; this includes both opponent-induced sleep as well as user-induced sleep via Rest. [Field Effect] If this Pokemon is in the lead spot, the rate of high-levelled wild Pokemon battles decreases by 50%.');
INSERT INTO "abilities" VALUES(183,'Volt Absorb',0,0,'When an Electric-type attack hits this Pokemon, it recovers health equal to the damage that it would have taken; this Pokemon can recover up to 25% of its max HP in this way.');
INSERT INTO "abilities" VALUES(184,'Water Absorb',0,0,'When a Water-type attack hits this Pokemon, it recovers health equal to the damage that it would have taken; this Pokemon can recover up to 25% of its max HP in this way.');
INSERT INTO "abilities" VALUES(185,'Water Veil',0,0,'This Pokemon cannot become burned.');
INSERT INTO "abilities" VALUES(186,'Weak Armor',0,0,'When Physical attacks hit this Pokemon, its Defense decreases by one stage while its Speed increases by one stage.');
INSERT INTO "abilities" VALUES(187,'White Smoke',0,0,'Opponents cannot reduce this Pokemon''s stats; they can, however, modify stat changes with Power Swap, Guard Swap and Heart Swap and inflict stat boosts with Swagger and Flatter. This ability does not prevent self-inflicted stat reductions. [Field Effect] If this Pokemon is in the lead spot, the rate of wild Pokemon battles decreases by 50%.');
INSERT INTO "abilities" VALUES(188,'Wonder Guard',0,0,'This Pokemon only receives damage from attacks belonging to types that cause Super Effective to this Pokemon. Wonder Guard does not protect a Pokemon from status ailments (burn, freeze,0,0, paralysis, poison, sleep, Toxic or any of their side effects or damage), recoil damage nor the moves Beat Up, Bide, Doom Desire, Fire Fang, Future Sight, Hail, Leech Seed, Sandstorm, Spikes, Stealth Rock and Struggle. Wonder Guard cannot be Skill Swapped nor Role Played. Trace, however, does copy Wonder Guard.');
INSERT INTO "abilities" VALUES(189,'Wonder Skin',0,0,'Halves the chance that a non-damaging move will inflict its effect on this Pokemon.');
INSERT INTO "abilities" VALUES(190,'Zen Mode',0,0,'When this Pokemon falls below 50% of its maximum HP, it changes to its Zen Form.');
INSERT INTO "types" VALUES(0,'Normal');
INSERT INTO "types" VALUES(1,'Fighting');
INSERT INTO "types" VALUES(2,'Flying');
INSERT INTO "types" VALUES(3,'Poison');
INSERT INTO "types" VALUES(4,'Ground');
INSERT INTO "types" VALUES(5,'Rock');
INSERT INTO "types" VALUES(6,'Bug');
INSERT INTO "types" VALUES(7,'Ghost');
INSERT INTO "types" VALUES(8,'Steel');
INSERT INTO "types" VALUES(9,'Fire');
INSERT INTO "types" VALUES(10,'Water');
INSERT INTO "types" VALUES(11,'Grass');
INSERT INTO "types" VALUES(12,'Electric');
INSERT INTO "types" VALUES(13,'Psychic');
INSERT INTO "types" VALUES(14,'Ice');
INSERT INTO "types" VALUES(15,'Dragon');
INSERT INTO "types" VALUES(16,'Dark');
INSERT INTO "types" VALUES(17,'Fairy');
INSERT INTO "level_moves" VALUES(1,549,0);
INSERT INTO "level_moves" VALUES(1,220,3);
INSERT INTO "level_moves" VALUES(1,591,7);
INSERT INTO "type_bonus" VALUES(0,5,0.5);
INSERT INTO "type_bonus" VALUES(0,7,0);
INSERT INTO "type_bonus" VALUES(0,8,0.5);
INSERT INTO "type_bonus" VALUES(1,0,2);
INSERT INTO "type_bonus" VALUES(1,2,0.5);
INSERT INTO "type_bonus" VALUES(1,3,0.5);
INSERT INTO "type_bonus" VALUES(1,5,2);
INSERT INTO "type_bonus" VALUES(1,6,0.5);
INSERT INTO "type_bonus" VALUES(1,7,0);
INSERT INTO "type_bonus" VALUES(1,8,2);
INSERT INTO "type_bonus" VALUES(1,13,0.5);
INSERT INTO "type_bonus" VALUES(1,14,2);
INSERT INTO "type_bonus" VALUES(1,16,2);
INSERT INTO "type_bonus" VALUES(1,17,0.5);
INSERT INTO "type_bonus" VALUES(2,1,2);
INSERT INTO "type_bonus" VALUES(2,5,0.5);
INSERT INTO "type_bonus" VALUES(2,6,2);
INSERT INTO "type_bonus" VALUES(2,8,0.5);
INSERT INTO "type_bonus" VALUES(2,11,2);
INSERT INTO "type_bonus" VALUES(2,12,0.5);
INSERT INTO "type_bonus" VALUES(3,3,0.5);
INSERT INTO "type_bonus" VALUES(3,4,0.5);
INSERT INTO "type_bonus" VALUES(3,5,0.5);
INSERT INTO "type_bonus" VALUES(3,7,0.5);
INSERT INTO "type_bonus" VALUES(3,8,0);
INSERT INTO "type_bonus" VALUES(3,11,2);
INSERT INTO "type_bonus" VALUES(3,17,2);
INSERT INTO "type_bonus" VALUES(4,2,0);
INSERT INTO "type_bonus" VALUES(4,3,2);
INSERT INTO "type_bonus" VALUES(4,5,2);
INSERT INTO "type_bonus" VALUES(4,6,0.5);
INSERT INTO "type_bonus" VALUES(4,8,2);
INSERT INTO "type_bonus" VALUES(4,9,2);
INSERT INTO "type_bonus" VALUES(4,11,0.5);
INSERT INTO "type_bonus" VALUES(4,12,2);
INSERT INTO "type_bonus" VALUES(5,1,0.5);
INSERT INTO "type_bonus" VALUES(5,2,2);
INSERT INTO "type_bonus" VALUES(5,4,0.5);
INSERT INTO "type_bonus" VALUES(5,6,2);
INSERT INTO "type_bonus" VALUES(5,8,0.5);
INSERT INTO "type_bonus" VALUES(5,9,2);
INSERT INTO "type_bonus" VALUES(5,14,2);
INSERT INTO "type_bonus" VALUES(6,1,0.5);
INSERT INTO "type_bonus" VALUES(6,2,0.5);
INSERT INTO "type_bonus" VALUES(6,3,0.5);
INSERT INTO "type_bonus" VALUES(6,7,0.5);
INSERT INTO "type_bonus" VALUES(6,8,0.5);
INSERT INTO "type_bonus" VALUES(6,9,0.5);
INSERT INTO "type_bonus" VALUES(6,11,2);
INSERT INTO "type_bonus" VALUES(6,13,2);
INSERT INTO "type_bonus" VALUES(6,16,2);
INSERT INTO "type_bonus" VALUES(6,17,0.5);
INSERT INTO "type_bonus" VALUES(7,0,0);
INSERT INTO "type_bonus" VALUES(7,7,2);
INSERT INTO "type_bonus" VALUES(7,13,2);
INSERT INTO "type_bonus" VALUES(7,16,0.5);
INSERT INTO "type_bonus" VALUES(8,5,2);
INSERT INTO "type_bonus" VALUES(8,8,0.5);
INSERT INTO "type_bonus" VALUES(8,9,0.5);
INSERT INTO "type_bonus" VALUES(8,10,0.5);
INSERT INTO "type_bonus" VALUES(8,12,0.5);
INSERT INTO "type_bonus" VALUES(8,14,2);
INSERT INTO "type_bonus" VALUES(8,17,2);
INSERT INTO "type_bonus" VALUES(9,5,0.5);
INSERT INTO "type_bonus" VALUES(9,6,2);
INSERT INTO "type_bonus" VALUES(9,8,2);
INSERT INTO "type_bonus" VALUES(9,9,0.5);
INSERT INTO "type_bonus" VALUES(9,10,0.5);
INSERT INTO "type_bonus" VALUES(9,11,2);
INSERT INTO "type_bonus" VALUES(9,14,2);
INSERT INTO "type_bonus" VALUES(9,15,0.5);
INSERT INTO "type_bonus" VALUES(10,4,2);
INSERT INTO "type_bonus" VALUES(10,5,2);
INSERT INTO "type_bonus" VALUES(10,9,2);
INSERT INTO "type_bonus" VALUES(10,10,0.5);
INSERT INTO "type_bonus" VALUES(10,11,0.5);
INSERT INTO "type_bonus" VALUES(10,15,0.5);
INSERT INTO "type_bonus" VALUES(11,2,0.5);
INSERT INTO "type_bonus" VALUES(11,3,0.5);
INSERT INTO "type_bonus" VALUES(11,4,2);
INSERT INTO "type_bonus" VALUES(11,5,2);
INSERT INTO "type_bonus" VALUES(11,6,0.5);
INSERT INTO "type_bonus" VALUES(11,8,0.5);
INSERT INTO "type_bonus" VALUES(11,9,0.5);
INSERT INTO "type_bonus" VALUES(11,10,2);
INSERT INTO "type_bonus" VALUES(11,11,0.5);
INSERT INTO "type_bonus" VALUES(11,15,0.5);
INSERT INTO "type_bonus" VALUES(12,2,2);
INSERT INTO "type_bonus" VALUES(12,4,0);
INSERT INTO "type_bonus" VALUES(12,10,2);
INSERT INTO "type_bonus" VALUES(12,11,0.5);
INSERT INTO "type_bonus" VALUES(12,12,0.5);
INSERT INTO "type_bonus" VALUES(12,15,0.5);
INSERT INTO "type_bonus" VALUES(13,1,2);
INSERT INTO "type_bonus" VALUES(13,3,2);
INSERT INTO "type_bonus" VALUES(13,8,0.5);
INSERT INTO "type_bonus" VALUES(13,13,0.5);
INSERT INTO "type_bonus" VALUES(13,16,0);
INSERT INTO "type_bonus" VALUES(14,2,2);
INSERT INTO "type_bonus" VALUES(14,4,2);
INSERT INTO "type_bonus" VALUES(14,8,0.5);
INSERT INTO "type_bonus" VALUES(14,9,0.5);
INSERT INTO "type_bonus" VALUES(14,10,0.5);
INSERT INTO "type_bonus" VALUES(14,11,2);
INSERT INTO "type_bonus" VALUES(14,14,0.5);
INSERT INTO "type_bonus" VALUES(14,15,2);
INSERT INTO "type_bonus" VALUES(15,8,0.5);
INSERT INTO "type_bonus" VALUES(15,15,2);
INSERT INTO "type_bonus" VALUES(15,17,0);
INSERT INTO "type_bonus" VALUES(16,1,0.5);
INSERT INTO "type_bonus" VALUES(16,7,2);
INSERT INTO "type_bonus" VALUES(16,13,2);
INSERT INTO "type_bonus" VALUES(16,16,0.5);
INSERT INTO "type_bonus" VALUES(16,17,0.5);
INSERT INTO "type_bonus" VALUES(17,1,2);
INSERT INTO "type_bonus" VALUES(17,3,0.5);
INSERT INTO "type_bonus" VALUES(17,8,0.5);
INSERT INTO "type_bonus" VALUES(17,9,0.5);
INSERT INTO "type_bonus" VALUES(17,15,2);
INSERT INTO "type_bonus" VALUES(17,16,2);
--INSERT INTO "species" VALUES(1,'Bulbasaur',104,17,224,12,13,45,1059860,3,1,49,49,65,65,45,45,103,103,135,135,200,'95',0,X'800014C48B8010808023341084398720',X'00');


