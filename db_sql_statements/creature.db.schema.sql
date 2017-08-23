CREATE TABLE species (
    `id` int,
    `name` varchar(16),
    `ability1` smallint check (`ability1` between 0 and 65535),
    `ability2` smallint NULL check (`ability2` between 0 and 65535),
    `gender_ratio` tinyint check (`gender_ratio` between 0 and 255),
    `type1` tinyint check (`type1` between 0 and 255),
    `type2` tinyint NULL check (`type2` between 0 and 255),
    `capture_rate` tinyint check (`capture_rate` between 0 and 255),
    `max_exp` int,
    `ev_type` tinyint check (`ev_type` between 1 and 6),
    `ev_val` tinyint check (`ev_val` between 0 and 255),
    `base_atk` smallint check (`base_atk` between 0 and 65535),
    `base_def` smallint check (`base_def` between 0 and 65535),
    `base_spatk` smallint check (`base_spatk` between 0 and 65535),
    `base_spdef` smallint check (`base_spdef` between 0 and 65535),
    `base_hp` smallint check (`base_hp` between 0 and 65535),
    `base_speed` smallint check (`base_speed` between 0 and 65535),
    `max_atk` smallint check (`max_atk` between 0 and 65535),
    `max_def` smallint check (`max_def` between 0 and 65535),
    `max_spatk` smallint check (`max_spatk` between 0 and 65535),
    `max_spdef` smallint check (`max_spdef` between 0 and 65535),
    `max_hp` smallint check (`max_hp` between 0 and 65535),
    `max_speed` smallint check (`max_speed` between 0 and 65535),
    `tm_list` blob(16),
	`misc_info` blob(1) NULL,
	`egg_steps` smallint NULL check (`egg_steps` between 0 and 65535),
	`egg_group1` tinyint NULL check (`egg_group1` between 0 and 255),
	`egg_group2` tinyint NULL check (`egg_group2` between 0 and 255),
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
	`type` tinyint check (`type` between 0 and 255),
	`value` int NULL,
	constraint `pk_evolution` primary key (`from_species`,`to_species`) on conflict rollback,
	constraint `fk_from_species` foreign key (`from_species`) references species(`id`) deferrable initially deferred,
	constraint `fk_to_species` foreign key (`to_species`) references species(`id`) deferrable initially deferred
);
CREATE TABLE egg_groups (
	`id` tinyint check (`id` between 0 and 255),
	`name` varchar(16),
	constraint `pk_egg_groups` primary key (`id`) on conflict rollback
);
CREATE TABLE natures (
	`id` tinyint check (`id` between 0 and 255),
	`name` varchar(16),
	`atk` tinyint check (`atk` between -127 and 127),
	`def` tinyint check (`def` between -127 and 127),
	`spatk` tinyint check (`spatk` between -127 and 127),
	`spdef` tinyint check (`spdef` between -127 and 127),
	`speed` tinyint check (`speed` between -127 and 127),
	constraint `pk_natures` primary key (`id`) on conflict rollback
);
CREATE TABLE moves (
	`id` int,
	`name` varchar(16),
	`type` tinyint check (`type` between 0 and 255),
	`power` tinyint check (`power` between 0 and 255),
	`accuracy` tinyint check (`accuracy` between 0 and 255),
	`pp` tinyint check (`pp` between 0 and 255),
	`effect_id` smallint NULL check (`effect_id` between 0 and 65535),
	`world_effect_id` smallint NULL check (`world_effect_id` between 0 and 65535),
	`affinity` tinyint check (`affinity` between 0 and 255),
	`misc_info` blob(2) NULL,
	`misc_val` tinyint NULL check (`misc_val` between 0 and 255),
	`sfx` int NULL,
	`description` text,
	constraint `pk_moves` primary key (`id`) on conflict rollback,
	constraint `fk_type` foreign key (`type`) references types(`id`) deferrable initially deferred,
	constraint `fk_effect_id` foreign key (`effect_id`) references effects(`id`) deferrable initially deferred,
	constraint `fk_world_effect_id` foreign key (`world_effect_id`) references effects(`id`) deferrable initially deferred,
	constraint `fk_sfx` foreign key (`sfx`) references sounds(`id`) deferrable initially deferred
);
CREATE TABLE type_bonus (
	`atk_id` tinyint check (`atk_id` between 0 and 255),
	`def_id` tinyint check (`def_id` between 0 and 255),
	`bonus` decimal(2,1),
	constraint `pk_type_bonus` primary key (`atk_id`,`def_id`) on conflict rollback,
	constraint `fk_atk_id` foreign key (`atk_id`) references types(`id`) deferrable initially deferred,
	constraint `fk_def_id` foreign key (`def_id`) references types(`id`) deferrable initially deferred
);
CREATE TABLE level_moves (
	`poke_id` int,
	`move_id` int,
	`level` tinyint check (`level` between 0 and 255),
	constraint `pk_level_moves` primary key (`poke_id`,`move_id`,`level`) on conflict ignore,
	constraint `fk_poke_id` foreign key (`poke_id`) references species(`id`) deferrable initially deferred,
	constraint `fk_move_id` foreign key (`move_id`) references moves(`id`) deferrable initially deferred
);
CREATE TABLE types (
	`id` tinyint check (`id` between 0 and 255),
	`name` varchar(16),
	constraint `pk_types` primary key (`id`) on conflict rollback
);
CREATE TABLE effects (
	`id` tinyint check (`id` between 0 and 255),
	`name` varchar(16),
	`flags` blob(2) NULL,
	`val1` tinyint NULL check (`val1` between 0 and 255),
	`val2` tinyint NULL check (`val2` between 0 and 255),
	`length` tinyint NULL check (`length` between 0 and 255),
	`sfx` int NULL,
	`text` text NULL,
	constraint `pk_effects` primary key (`id`) on conflict rollback,
	constraint `fk_sfx` foreign key (`sfx`) references sounds(`id`) deferrable initially deferred
);
CREATE TABLE abilities (
	`id` smallint check (`id` between 0 and 65535),
	`name` varchar(16),
	`battle_effect_id` smallint check (`battle_effect_id` between 0 and 65535),
	`world_effect_id` smallint check (`world_effect_id` between 0 and 65535),
	`description` text,
	constraint `pk_abilities` primary key (`id`) on conflict rollback,
	constraint `fk_battle_effect_id` foreign key (`battle_effect_id`) references effects(`id`) deferrable initially deferred,
	constraint `fk_world_effect_id` foreign key (`world_effect_id`) references effects(`id`) deferrable initially deferred
);
CREATE TABLE unique_creature (
	`id` int,
	`species_id` int,
	`nickname` varchar(16),
	`ability` smallint check (`ability` between 0 and 65535),
	`nature` tinyint check (`nature` between 0 and 255),
	`level` tinyint check (`level` between 0 and 255),
	`held_item` int,
	`move1_id` int,
	`move2_id` int,
	`move3_id` int,
	`move4_id` int,
	`hp_max` smallint check (`hp_max` between 0 and 65535),
	`atk` smallint check (`atk` between 0 and 65535),
	`def` smallint check (`def` between 0 and 65535),
	`spatk` smallint check (`spatk` between 0 and 65535),
	`spdef` smallint check (`spdef` between 0 and 65535),
	`speed` smallint check (`speed` between 0 and 65535),
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
	`type` tinyint check (`type` between 0 and 255),
	`price` smallint check (`price` between 0 and 65535),
	`battle_effect` smallint NULL check (`battle_effect` between 0 and 65535),
	`world_effect` smallint NULL check (`world_effect` between 0 and 65535),
	`held_effect` smallint NULL check (`held_effect` between 0 and 65535),
	`misc_val1` tinyint NULL check (`misc_val1` between 0 and 255),
	`misc_val2` tinyint NULL check (`misc_val2` between 0 and 255),
	`misc_info` blob(1) NULL,
	`sprite_path` text,
	constraint `pk_item` primary key (`id`) on conflict rollback,
	constraint `fk_type` foreign key (`type`) references item_type(`id`) deferrable initially deferred,
	constraint `fk_battle_effect` foreign key (`battle_effect`) references effects(`id`) deferrable initially deferred,
	constraint `fk_world_effect` foreign key (`world_effect`) references effects(`id`) deferrable initially deferred,
	constraint `fk_held_effect` foreign key (`held_effect`) references effects(`id`) deferrable initially deferred
);
CREATE TABLE item_type (
	`id` tinyint check (`id` between 0 and 255),
	`name` varchar(16),
	`description` text,
	constraint `pk_item_type` primary key (`id`) on conflict rollback
);
CREATE TABLE trainer (
	`id` int,
	`name` varchar(10),
	`style` tinyint check (`style` between 0 and 255),
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
	`id` tinyint check (`id` between 0 and 255),
	`name` varchar(10),
	`bgm` smallint check (`bgm` between 0 and 65535),
	`sprite_path` text,
	constraint `pk_trainer_style` primary key (`id`) on conflict rollback,
	constraint `fk_bgm` foreign key (`bgm`) references music(`id`) deferrable initially deferred
);	
CREATE TABLE npc (
	`id` int,
	`style` tinyint check (`style` between 0 and 255),
	`map` smallint check (`map` between 0 and 65535),
	`x` smallint check (`x` between 0 and 65535),
	`y` smallint check (`y` between 0 and 65535),
	`text` text NULL,
	`trainer` int NULL,
	`plot_flag` smallint NULL check (`plot_flag` between 0 and 65535),
	`misc_info` blob NULL,
	constraint `pk_npc` primary key (`id`) on conflict rollback,
	constraint `fk_style` foreign key (`style`) references npc_style(`id`) deferrable initially deferred,
	constraint `fk_map` foreign key (`map`) references map(`id`) deferrable initially deferred,
	constraint `fk_plot_flag` foreign key (`plot_flag`) references plot_flag(`id`) deferrable initially deferred,
	constraint `fk_trainer` foreign key (`trainer`) references trainer(`id`) deferrable initially deferred
);
create table npc_style (
	`id` tinyint check (`id` between 0 and 255),
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
	`id` smallint check (`id` between 0 and 65535),
	`name` varchar(16) NULL,
	`width` smallint check (`width` between 0 and 65535),
	`height` smallint check (`height` between 0 and 65535),
	`bgm` smallint check (`bgm` between 0 and 65535),
	`data_path` text,
	constraint `pk_map` primary key (`id`) on conflict rollback,
	constraint `fk_bgm` foreign key (`bgm`) references music(`id`) deferrable initially deferred
);
CREATE TABLE music (
	`id` smallint check (`id` between 0 and 65535),
	`path` text,
	constraint `pk_music` primary key (`id`) on conflict rollback
);
CREATE TABLE sounds (
	`id` int,
	`path` text,
	constraint `pk_sounds` primary key (`id`) on conflict rollback
);
	
	