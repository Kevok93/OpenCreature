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
    `tm_list` blob(16),
	`misc_info` blob(1) NULL,
	`egg_steps` smallint NULL,
	`egg_group1` tinyint NULL,
	`egg_group2` tinyint NULL,
	`wild_item` int NULL,
	`wild_item_pct` int NULL,
	`height` decimal(4,2) NULL,
	`weight` decimal(4,2) NULL,
	`classification` varchar(16) NULL,
	`dex_entry` text NULL,
	`cry` int NULL,
	`sprite_path` text,
    constraint `pk_species` primary key (`id`) on conflict rollback,
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
	`type` tinyint,
	`value` int NULL,
	constraint `pk_evolution` primary key (`from_species`,`to_species`) on conflict rollback,
	constraint `fk_from_species` foreign key (`from_species`) references species(`id`) deferrable initially deferred,
	constraint `fk_to_species` foreign key (`to_species`) references species(`id`) deferrable initially deferred
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
	`sfx` int NULL,
	`description` text,
	constraint `pk_moves` primary key (`id`) on conflict rollback,
	constraint `fk_type` foreign key (`type`) references types(`id`) deferrable initially deferred,
	constraint `fk_effect_id` foreign key (`effect_id`) references effects(`id`) deferrable initially deferred,
	constraint `fk_world_effect_id` foreign key (`world_effect_id`) references effects(`id`) deferrable initially deferred,
	constraint `fk_sfx` foreign key (`sfx`) references sounds(`id`) deferrable initially deferred
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
	`flags` blob(2) NULL,
	`val1` tinyint NULL,
	`val2` tinyint NULL,
	`length` tinyint NULL,
	`sfx` int NULL,
	`text` text NULL,
	constraint `pk_effects` primary key (`id`) on conflict rollback,
	constraint `fk_sfx` foreign key (`sfx`) references sounds(`id`) deferrable initially deferred
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
	`sprite_path` text,
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
	`bgm` smallint,
	`sprite_path` text,
	constraint `pk_trainer_style` primary key (`id`) on conflict rollback,
	constraint `fk_bgm` foreign key (`bgm`) references music(`id`) deferrable initially deferred
);	
CREATE TABLE npc (
	`id` int,
	`style` tinyint,
	`map` smallint,
	`x` smallint,
	`y` smallint,
	`text` text NULL,
	`trainer` int NULL,
	`plot_flag` smallint NULL,
	`misc_info` blob NULL,
	constraint `pk_npc` primary key (`id`) on conflict rollback,
	constraint `fk_style` foreign key (`style`) references npc_style(`id`) deferrable initially deferred,
	constraint `fk_map` foreign key (`map`) references map(`id`) deferrable initially deferred,
	constraint `fk_plot_flag` foreign key (`plot_flag`) references plot_flag(`id`) deferrable initially deferred,
	constraint `fk_trainer` foreign key (`trainer`) references trainer(`id`) deferrable initially deferred
);
create table npc_style (
	`id` tinyint,
	`name` varchar(10),
	`sprite_path` text,
	constraint `pk_npc_style` primary key (`id`) on conflict rollback
);
CREATE TABLE plot_flag (
	`id` int,
	`name` varchar(10),
	constraint `pk_plot_flag` primary key (`id`) on conflict rollback
);
CREATE TABLE map (
	`id` smallint,
	`name` varchar(16) NULL,
	`width` smallint,
	`height` smallint,
	`bgm` smallint,
	`data_path` text,
	constraint `pk_map` primary key (`id`) on conflict rollback,
	constraint `fk_bgm` foreign key (`bgm`) references music(`id`) deferrable initially deferred
);
CREATE TABLE music (
	`id` smallint,
	`path` text,
	constraint `pk_music` primary key (`id`) on conflict rollback
);
CREATE TABLE sounds (
	`id` int,
	`path` text,
	constraint `pk_sounds` primary key (`id`) on conflict rollback
);
	
	