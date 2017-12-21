CREATE TABLE trigger_control (
  `name` text,
  `enabled` int
);

CREATE TABLE species (
  `id` int,
  `name` varchar(16),
  `ability1` smallint check (`ability1` between 0 AND 65535),
  `ability2` smallint check (`ability2` between 0 AND 65535),
  `gender_ratio` tinyint check (`gender_ratio` between 0 AND 255),
  `type1` tinyint check (`type1` between 0 AND 255),
  `type2` tinyint check (`type2` between 0 AND 255),
  `capture_rate` tinyint check (`capture_rate` between 0 AND 255),
  `max_exp` int,
  `ev_type` tinyint check (`ev_type` between 1 AND 6),
  `ev_val` tinyint check (`ev_val` between 0 AND 255),
  `base_atk` smallint check (`base_atk` between 0 AND 65535),
  `base_def` smallint check (`base_def` between 0 AND 65535),
  `base_spatk` smallint check (`base_spatk` between 0 AND 65535),
  `base_spdef` smallint check (`base_spdef` between 0 AND 65535),
  `base_hp` smallint check (`base_hp` between 0 AND 65535),
  `base_speed` smallint check (`base_speed` between 0 AND 65535),
  `max_atk` smallint check (`max_atk` between 0 AND 65535),
  `max_def` smallint check (`max_def` between 0 AND 65535),
  `max_spatk` smallint check (`max_spatk` between 0 AND 65535),
  `max_spdef` smallint check (`max_spdef` between 0 AND 65535),
  `max_hp` smallint check (`max_hp` between 0 AND 65535),
  `max_speed` smallint check (`max_speed` between 0 AND 65535),
  `tm_list` blob(16),
	`misc_info` blob(1),
	`egg_steps` smallint NULL check (`egg_steps` between 0 AND 65535),
	`egg_group1` tinyint NULL check (`egg_group1` between 0 AND 255),
	`egg_group2` tinyint NULL check (`egg_group2` between 0 AND 255),
	`wild_item` int,
	`wild_item_pct` int,
	`height` decimal(4,2),
	`weight` decimal(4,2),
	`classification` varchar(16),
	`dex_entry` text,
	`cry` int,
	`sprite_path` text,
  constraint `pk_species` primary key (`id`) ON CONFLICT ROLLBACK,
	constraint `fk_type1` foreign key (`type1`) references types(`id`) deferrable initially deferred,
	constraint `fk_type2` foreign key (`type2`) references types(`id`) deferrable initially deferred,
	constraint `fk_ability1` foreign key (`ability1`) references abilities(`id`) deferrable initially deferred,
	constraint `fk_ability2` foreign key (`ability2`) references abilities(`id`) deferrable initially deferred,
	constraint `fk_egg_group1` foreign key (`egg_group1`) references egg_groups(`id`) deferrable initially deferred,
	constraint `fk_egg_group2` foreign key (`egg_group2`) references egg_groups(`id`) deferrable initially deferred,
	constraint `fk_wild_item` foreign key (`wild_item`) references items(`id`) deferrable initially deferred,
	constraint `fk_cry` foreign key (`cry`) references sounds(`id`) deferrable initially deferred
);
CREATE TABLE evolution (
	`from_species` int,
	`to_species` int,
	`type` tinyint check (`type` between 0 AND 255),
	`value` int NULL,
	constraint `pk_evolution` primary key (`from_species`,`to_species`) ON CONFLICT ROLLBACK,
	constraint `fk_from_species` foreign key (`from_species`) references species(`id`) deferrable initially deferred,
	constraint `fk_to_species` foreign key (`to_species`) references species(`id`) deferrable initially deferred
);
CREATE TABLE egg_groups (
	`id` tinyint check (`id` between 0 AND 255),
	`name` varchar(16),
	constraint `pk_egg_groups` primary key (`id`) ON CONFLICT ROLLBACK
);
CREATE TABLE natures (
	`id` tinyint check (`id` between 0 AND 255),
	`name` varchar(16),
	`atk` signed tinyint check (`atk` between -127 AND 127),
	`def` signed tinyint check (`def` between -127 AND 127),
	`spatk` signed tinyint check (`spatk` between -127 AND 127),
	`spdef` signed tinyint check (`spdef` between -127 AND 127),
	`speed` signed tinyint check (`speed` between -127 AND 127),
	constraint `pk_natures` primary key (`id`) ON CONFLICT ROLLBACK
);
CREATE TABLE moves (
	`id` int,
	`name` varchar(16),
	`type` tinyint check (`type` between 0 AND 255),
	`pp` tinyint check (`pp` between 0 AND 255),
	`power` tinyint check (`power` between 0 AND 255),
	`accuracy` tinyint check (`accuracy` between 0 AND 255),
	`battle_effect_id` smallint NULL check (`battle_effect_id` between 0 AND 65535),
	`world_effect_id` smallint NULL check (`world_effect_id` between 0 AND 65535),
	`affinity` tinyint check (`affinity` between 0 AND 255),
	`misc_info` blob(2) NULL,
	`misc_val` tinyint NULL check (`misc_val` between 0 AND 255),
	`sfx` int NULL,
	`description` text,
	constraint `pk_moves` primary key (`id`) ON CONFLICT ROLLBACK,
	constraint `fk_type` foreign key (`type`) references types(`id`) deferrable initially deferred,
	constraint `fk_battle_effect_id` foreign key (`battle_effect_id`) references effects(`id`) deferrable initially deferred,
	constraint `fk_world_effect_id` foreign key (`world_effect_id`) references effects(`id`) deferrable initially deferred,
	constraint `fk_sfx` foreign key (`sfx`) references sounds(`id`) deferrable initially deferred
);
CREATE TABLE type_bonus (
	`atk_id` tinyint check (`atk_id` between 0 AND 255),
	`def_id` tinyint check (`def_id` between 0 AND 255),
	`bonus`  tinyint check (`bonus`  between 0 AND 200),
	constraint `pk_type_bonus` primary key (`atk_id`,`def_id`) ON CONFLICT ROLLBACK,
	constraint `fk_atk_id` foreign key (`atk_id`) references types(`id`) deferrable initially deferred,
	constraint `fk_def_id` foreign key (`def_id`) references types(`id`) deferrable initially deferred
);
CREATE TABLE types (
	`id` tinyint check (`id` between 0 AND 255),
	`name` varchar(16),
	constraint `pk_types` primary key (`id`) ON CONFLICT ROLLBACK
);
CREATE TABLE effects (
	`id` tinyint check (`id` between 0 AND 255),
	`name` varchar(16),
	`flags` blob(2) NULL,
	`val1` tinyint NULL check (`val1` between 0 AND 255),
	`val2` tinyint NULL check (`val2` between 0 AND 255),
	`length` tinyint NULL check (`length` between 0 AND 255),
	`sfx` int NULL,
	`text` text NULL,
	constraint `pk_effects` primary key (`id`) ON CONFLICT ROLLBACK,
	constraint `fk_sfx` foreign key (`sfx`) references sounds(`id`) deferrable initially deferred
);
CREATE TABLE abilities (
	`id` smallint check (`id` between 0 AND 65535),
	`name` varchar(16),
	`battle_effect_id` smallint check (`battle_effect_id` between 0 AND 65535),
	`world_effect_id` smallint check (`world_effect_id` between 0 AND 65535),
	`description` text,
	constraint `pk_abilities` primary key (`id`) ON CONFLICT ROLLBACK,
	constraint `fk_battle_effect_id` foreign key (`battle_effect_id`) references effects(`id`) deferrable initially deferred,
	constraint `fk_world_effect_id` foreign key (`world_effect_id`) references effects(`id`) deferrable initially deferred
);
CREATE TABLE unique_creature (
	`id` int,
	`species_id` int,
	`nickname` varchar(16),
	`ability` smallint check (`ability` between 0 AND 65535),
	`nature` tinyint check (`nature` between 0 AND 255),
	`level` tinyint check (`level` between 0 AND 255),
	`held_item` int,
	`move1_id` int,
	`move2_id` int,
	`move3_id` int,
	`move4_id` int,
	`hp_max` smallint check (`hp_max` between 0 AND 65535),
	`atk` smallint check (`atk` between 0 AND 65535),
	`def` smallint check (`def` between 0 AND 65535),
	`spatk` smallint check (`spatk` between 0 AND 65535),
	`spdef` smallint check (`spdef` between 0 AND 65535),
	`speed` smallint check (`speed` between 0 AND 65535),
	`misc_info` blob(2),
	constraint `pk_unique_creature` primary key (`id`) ON CONFLICT ROLLBACK,
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
	`type` tinyint check (`type` between 0 AND 255),
	`price` smallint check (`price` between -1 AND 65535),
	`battle_effect` smallint NULL check (`battle_effect` between 0 AND 65535),
	`world_effect` smallint NULL check (`world_effect` between 0 AND 65535),
	`held_effect` smallint NULL check (`held_effect` between 0 AND 65535),
	`misc_val1` tinyint NULL check (`misc_val1` between 0 AND 255),
	`misc_val2` tinyint NULL check (`misc_val2` between 0 AND 255),
	`misc_info` blob(1) NULL,
	`description` text,
	`sprite_path` text,
	constraint `pk_item` primary key (`id`) ON CONFLICT ROLLBACK,
	constraint `fk_type` foreign key (`type`) references item_type(`id`) deferrable initially deferred,
	constraint `fk_battle_effect` foreign key (`battle_effect`) references effects(`id`) deferrable initially deferred,
	constraint `fk_world_effect` foreign key (`world_effect`) references effects(`id`) deferrable initially deferred,
	constraint `fk_held_effect` foreign key (`held_effect`) references effects(`id`) deferrable initially deferred
);
CREATE TABLE item_type (
	`id` tinyint check (`id` between 0 AND 255),
	`name` varchar(16),
	`description` text,
	constraint `pk_item_type` primary key (`id`) ON CONFLICT ROLLBACK
);
CREATE TABLE trainer (
	`id` int,
	`name` varchar(10),
	`style` tinyint check (`style` between 0 AND 255),
	`reward` int,
	`quote` text,
	`rematch_id` int NULL,
	`misc_info` blob(1) NULL,
	constraint `pk_trainer` primary key (`id`) ON CONFLICT ROLLBACK,
	constraint `fk_style` foreign key (`style`) references trainer_style(`id`) deferrable initially deferred,
	constraint `rematch_id` foreign key (`rematch_id`) references trainer(`id`) deferrable initially deferred
);
CREATE TABLE trainer_creature (
  `trainer_id` int,
  `creature_id` int,
  `slot` tinyint CHECK (`slot` >= 0),
  CONSTRAINT `pk_trainer_creature` PRIMARY KEY (`trainer_id`,`slot`) ON CONFLICT ROLLBACK,
  CONSTRAINT `fk_creature_trainer` FOREIGN KEY (`trainer_id`) REFERENCES trainer(`id`) DEFERRABLE INITIALLY DEFERRED,
  CONSTRAINT `fk_trainer_creature` FOREIGN KEY (`creature_id`) REFERENCES unique_creature(`id`) DEFERRABLE INITIALLY DEFERRED
);
CREATE TABLE trainer_item (
  `trainer_id` int,
  `item_id` int,
  `count` tinyint CHECK (`count` > 0),
  CONSTRAINT `pk_trainer_creature` PRIMARY KEY (`trainer_id`,`item_id`) ON CONFLICT ROLLBACK,
  CONSTRAINT `fk_item_trainer` FOREIGN KEY (`trainer_id`) REFERENCES trainer(`id`) DEFERRABLE INITIALLY DEFERRED,
  CONSTRAINT `fk_trainer_item` FOREIGN KEY (`item_id`) REFERENCES items(`id`) DEFERRABLE INITIALLY DEFERRED
);
CREATE TABLE trainer_style (
	`id` tinyint check (`id` between 0 AND 255),
	`name` varchar(10),
	`bgm` smallint check (`bgm` between 0 AND 65535),
	`sprite_path` text,
	constraint `pk_trainer_style` primary key (`id`) ON CONFLICT ROLLBACK,
	constraint `fk_bgm` foreign key (`bgm`) references music(`id`) deferrable initially deferred
);	
CREATE TABLE npc (
	`id` int,
	`style` tinyint check (`style` between 0 AND 255),
	`map` smallint check (`map` between 0 AND 65535),
	`x` smallint check (`x` between 0 AND 65535),
	`y` smallint check (`y` between 0 AND 65535),
	`text` text NULL,
	`trainer` int NULL,
	`plot_flag` smallint NULL check (`plot_flag` between 0 AND 65535),
	`misc_info` blob NULL,
	constraint `pk_npc` primary key (`id`) ON CONFLICT ROLLBACK,
	constraint `fk_style` foreign key (`style`) references npc_style(`id`) deferrable initially deferred,
	constraint `fk_map` foreign key (`map`) references map(`id`) deferrable initially deferred,
	constraint `fk_plot_flag` foreign key (`plot_flag`) references plot_flag(`id`) deferrable initially deferred,
	constraint `fk_trainer` foreign key (`trainer`) references trainer(`id`) deferrable initially deferred
);
CREATE table npc_style (
	`id` tinyint check (`id` between 0 AND 255),
	`name` varchar(10),
	`sprite_path` text,
	constraint `pk_npc_style` primary key (`id`) ON CONFLICT ROLLBACK
);
CREATE TABLE training_machine (
  `item_id` int,
  `move_id` int,
  `uses` tinyint CHECK(`uses` BETWEEN 0 AND 255),
  CONSTRAINT `pk_training_machine` PRIMARY KEY (`item_id`) ON CONFLICT rollback,
  CONSTRAINT `fk_training_machine_item` FOREIGN KEY (`item_id`) REFERENCES items(`id`) DEFERRABLE INITIALLY DEFERRED,
  CONSTRAINT `fk_training_machine_move` FOREIGN KEY (`move_id`) REFERENCES moves(`id`) DEFERRABLE INITIALLY DEFERRED 
);

CREATE TABLE learnable_moves (
  `species_id` int,
  `move_id` int,
  `source_type` tinyint,
  `level` tinyint check (`level` between 0 AND 255),
  constraint `pk_level_moves` primary key (`species_id`,`move_id`,`source_type`) ON CONFLICT IGNORE,
  constraint `fk_species_id` foreign key (`species_id`) references species(`id`) deferrable initially deferred,
  constraint `fk_move_id` foreign key (`move_id`) references moves(`id`) deferrable initially deferred
);
CREATE TABLE plot_flag (
	`id` int,
	`name` varchar(10),
	`value` tinyint,
	constraint `pk_plot_flag` primary key (`id`) ON CONFLICT ROLLBACK
);
CREATE TABLE map (
	`id` smallint check (`id` between 0 AND 65535),
	`name` varchar(16) NULL,
	`width` smallint check (`width` between 0 AND 65535),
	`height` smallint check (`height` between 0 AND 65535),
	`bgm` smallint check (`bgm` between 0 AND 65535),
	`data_path` text,
	constraint `pk_map` primary key (`id`) ON CONFLICT ROLLBACK,
	constraint `fk_bgm` foreign key (`bgm`) references music(`id`) deferrable initially deferred
);
CREATE TABLE music (
	`id` smallint check (`id` between 0 AND 65535),
	`path` text,
	constraint `pk_music` primary key (`id`) ON CONFLICT ROLLBACK
);
CREATE TABLE sounds (
	`id` int,
	`path` text,
	constraint `pk_sounds` primary key (`id`) ON CONFLICT ROLLBACK
);
CREATE TABLE battle_type  ( 
  `battle_type_id` tinyint check (`battle_type_id` between 0 AND 127),
  `battle_team_id` tinyint check (`battle_team_id` between 0 AND 127),
  `team_creature_count` tinyint check(`team_creature_count` between 1 AND 127),
  constraint `pk_battle_type` primary key (`battle_type_id`,`battle_team_id`) ON CONFLICT ROLLBACK
);
CREATE TABLE battle_team_alliances (
  `battle_type_id` tinyint check (`battle_type_id` between 0 AND 127),
  `battle_team_id_A` tinyint check (`battle_team_id_A` between 0 AND 127),
  `battle_team_id_B` tinyint check (`battle_team_id_B` between 0 AND 127),
  constraint `pk_battle_team_type` primary key (`battle_type_id`,`battle_team_id_A`,`battle_team_id_B`) ON CONFLICT ROLLBACK,
  constraint `uq_team_id` check (`battle_team_id_A` < `battle_team_id_B`),
  constraint `fk_battle_team_id_A` foreign key (`battle_type_id`,`battle_team_id_A`) references battle_type(`battle_type_id`,`battle_team_id`) deferrable initially deferred,
  constraint `fk_battle_team_id_B` foreign key (`battle_type_id`,`battle_team_id_B`) references battle_type(`battle_type_id`,`battle_team_id`) deferrable initially deferred
);
CREATE TABLE translation_strings (
    `language_id` smallint check (`language_id` between 0 AND 65535),
    `table_id` smallint check (`table_id` between 0 AND 65535),
    `id` int,
    `text` text,
    constraint `pk_translation` primary key (`language_id`,`table_id`,`id`) ON CONFLICT ROLLBACK
);

CREATE TABLE lua_text (
	`id` int identity,
	`name` text,
  `type` int,
	`text` text,
	constraint `pk_translation` primary key (`name`) ON CONFLICT ROLLBACK
);

CREATE VIEW translation_strings_internal as
  select
    ts.table_id,
    ts_def.text as table_name,
    ts.id,
    ts.text
  from translation_strings ts join
    translation_strings ts_def on ts_def.id = ts.table_id
  where ts.language_id = 0
    and ts_def.language_id = 0
    and ts_def.table_id = 0;





CREATE TRIGGER tr_translation_strings_il after insert ON translation_strings 
WHEN new.table_id NOT IN (0,1) 
AND new.language_id NOT IN (SELECT id FROM translation_strings WHERE table_id = 1 AND language_id = 0)
AND (SELECT enabled FROM trigger_control WHERE name='tr_translation_strings_il') > 0
BEGIN SELECT raise(rollback,"Language not defined in languages subcategory"); END;

CREATE TRIGGER tr_translation_strings_it after insert ON translation_strings 
WHEN new.table_id NOT IN (0,1) 
AND new.table_id NOT IN (SELECT id FROM translation_strings WHERE table_id = 0 AND language_id = 0)
AND (SELECT enabled FROM trigger_control WHERE name='tr_translation_strings_it') > 0
BEGIN SELECT raise(rollback,"Table not defined in tables subcategory"); END;

CREATE TRIGGER tr_translation_strings_uil after UPDATE ON translation_strings 
WHEN new.table_id NOT IN (0,1) 
AND new.language_id NOT IN (SELECT id FROM translation_strings WHERE table_id = 1 AND language_id = 0)
AND (SELECT enabled FROM trigger_control WHERE name='tr_translation_strings_uil') > 0
BEGIN SELECT raise(rollback,"Language not defined in languages subcategory"); END;

CREATE TRIGGER tr_translation_strings_uit after UPDATE ON translation_strings 
WHEN new.table_id NOT IN (0,1) 
AND new.table_id NOT IN (SELECT id FROM translation_strings WHERE table_id = 0 AND language_id = 0)
AND (SELECT enabled FROM trigger_control WHERE name='tr_translation_strings_uit') > 0
BEGIN SELECT raise(rollback,"Table not defined in tables subcategory"); END;



CREATE TRIGGER tr_translation_strings_dl after DELETE ON translation_strings 
WHEN old.table_id = 1 
AND old.id IN (SELECT language_id FROM translation_strings)
AND (SELECT enabled FROM trigger_control WHERE name='tr_translation_strings_dl') > 0
BEGIN SELECT raise(rollback,"Cannot delete translation definition in use"); END;

CREATE TRIGGER tr_translation_strings_dt after DELETE ON translation_strings 
WHEN old.table_id = 0 
AND old.id IN (SELECT table_id FROM translation_strings)
AND (SELECT enabled FROM trigger_control WHERE name='tr_translation_strings_dt') > 0
BEGIN SELECT raise(rollback,"Cannot delete table definition in use"); END;

CREATE TRIGGER tr_translation_strings_udl after UPDATE ON translation_strings 
WHEN old.table_id = 1 
AND old.id IN (SELECT language_id FROM translation_strings)
AND (old.id != new.id OR old.table_id != new.table_id OR old.language_id != new.language_id)
AND (SELECT enabled FROM trigger_control WHERE name='tr_translation_strings_udl')
BEGIN SELECT raise(rollback,"Cannot delete translation definition in use"); END;

CREATE TRIGGER tr_translation_strings_udt after UPDATE ON translation_strings 
WHEN old.table_id = 0 
AND old.id IN (SELECT table_id FROM translation_strings)
AND (old.id != new.id OR old.table_id != new.table_id OR old.language_id != new.language_id)
AND (SELECT enabled FROM trigger_control WHERE name='tr_translation_strings_udt') > 0
BEGIN SELECT raise(rollback,"Cannot delete table definition in use"); END;



CREATE TRIGGER i_translation_strings_internal instead of INSERT on translation_strings_internal
WHEN (SELECT enabled FROM trigger_control WHERE name='i_translation_strings_internal') > 0
BEGIN insert into translation_strings (language_id,table_id,id,text) values (0,new.'table_id',new.'id',new.'text'); END;

CREATE TRIGGER u_translation_strings_internal instead of UPDATE on translation_strings_internal
  WHEN (SELECT enabled FROM trigger_control WHERE name='u_translation_strings_internal') > 0
BEGIN update translation_strings SET table_id = new.table_id, id = new.id, text = new.text WHERE language_id = 0 AND table_id = old.table_id AND id = old.id AND text = old.text; END;

CREATE TRIGGER d_translation_strings_internal instead of DELETE on translation_strings_internal
  WHEN (SELECT enabled FROM trigger_control WHERE name='d_translation_strings_internal') > 0
BEGIN DELETE from translation_strings WHERE language_id = 0 AND table_id = old.table_id AND id = old.id; END;

